import {ChangeDetectorRef, Component, OnInit, TemplateRef} from '@angular/core';
import {AbstractControl, FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {JsonPipe, NgClass} from '@angular/common';
import {ActivatedRoute, Router} from '@angular/router';
import {EventoService} from '@app/_services/evento-service';
import {Evento} from '@app/_models/evento';
import {ToastrService} from 'ngx-toastr';
import {DateTimeFormatPipe} from '@app/helpers/DateTimeFormat/date-time-format-pipe';
import {NgxSpinnerService} from 'ngx-spinner';
import {TooltipDirective} from 'ngx-bootstrap/tooltip';
import {FaIconComponent, IconDefinition} from '@fortawesome/angular-fontawesome';
import {faPlusCircle, faWindowClose} from '@fortawesome/free-solid-svg-icons';
import {Lote} from '@app/_models/lote';
import {LoteService} from '@app/_services/lote-service';
import {BsModalService} from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-evento-detalhe',
	imports: [
		ReactiveFormsModule,
		NgClass,
		TooltipDirective,
		FaIconComponent,
		JsonPipe
	],
  templateUrl: './evento-detalhe.html',
  styleUrl: './evento-detalhe.scss',
})
export class EventoDetalhe implements OnInit {
	public eventoId: number | null = null;
	public loteId: number | null = null;

	public form: FormGroup = new FormGroup({
		lotes: new FormArray([])
	});

	public readonly faPlusCircle = faPlusCircle;
	public readonly faWindowClose = faWindowClose;

	get f(): any {
		return this.form.controls;
	}

	get lotes(): FormArray {
		return this.form.get("lotes") as FormArray;
	}

	get modoEditar(): boolean {
		return !!this.eventoId;
	}

	constructor(
		private formBuilder: FormBuilder,
		private changeDetectorRef: ChangeDetectorRef,
		private activeRouter: ActivatedRoute,
		private router: Router,
		private eventosService: EventoService,
		private lotesService: LoteService,
		private toastr: ToastrService,
		private spinner: NgxSpinnerService,
		private modal: BsModalService
	) {}

	ngOnInit() {
		this.buscarInformacoesDoEvento();
		this.criarCamposForm();
		this.changeDetectorRef.detectChanges();
	}


	private lotesControls(index: number, control: string): AbstractControl<any, any, any> | null {
		return this.lotes.at(index).get(control);
	}

	protected validarCampo(control: AbstractControl | null) {
		const invalido: boolean = (control?.errors && control?.touched) || false;
		return {'is-invalid': invalido};
	}

	public validarCampoEventos(control: string) {
		return this.validarCampo(this.form.get(control));
	}

	public validarCampoLotes(index: number, control: string) {
		return this.validarCampo(this.lotesControls(index, control));
	}


	buscarInformacoesDoEvento(): void {
		const paramId = this.activeRouter.snapshot.params['id'];

		if (!!paramId) {
			this.spinner.show();
			this.eventoId = +paramId;

			this.eventosService.getEventoById(this.eventoId).subscribe({
				next: (evento: Evento) => {

					if (!evento) {
						this.router.navigate(["/eventos/lista"]);
						return;
					}

					this.form.patchValue(evento);
					this.definirValoresLotes(evento?.lotes);
					this.spinner.hide();
				},
				error: (evento: Evento) => {
					this.spinner.hide();
					this.toastr.error('Erro ao caregar informações do evento.', 'Erro!')
				}
			})
		}
	}

	criarCamposForm(): void {
		this.form = this.formBuilder.group(
		{
			local: ['', Validators.required],
			dataEvento: ['', [
				Validators.required
			]],
			tema: ['', [
				Validators.required,
				Validators.minLength(4),
				Validators.maxLength(50)
			]],
			qtdPessoas: ['', [
				Validators.required,
				Validators.max(120000)
			]],
			telefone: ['', Validators.required],
			email: ['', [
				Validators.required,
				Validators.email
			]],
			imagemURL: ['', Validators.required],
			lotes: this.formBuilder.array([])
		});
	}

	resetarCamposEvento(): void {
		this.form.reset();
	}

	private _redirecionarParaPaginaDoEventoCadastrado(eventoId: number): void {
		this.router.navigate([`/eventos/detalhe/${eventoId}`]);
	}

	private _cadastrarNovoEvento(evento: Evento): void {
		this.spinner.show();

		this.eventosService.postEvento(evento).subscribe({
			next: (evento: any) => {
				this.spinner.hide();
				this.toastr.success('Evento cadastrado com sucesso!', 'Successo!');

				this._redirecionarParaPaginaDoEventoCadastrado(evento.Id);
			},
			error: () => {
				this.spinner.hide();
				this.toastr.error('Ocorreu um erro ao tentar cadastrar evento. Tente novamente.', 'Erro!');
			}
		});
	}

	private _atualizarEvento(evento: Evento): void {
		if (!this.eventoId) {
			this.toastr.error('Ocorreu um erro ao tentar cadastrar evento. Tente novamente.', 'Erro!');
			return;
		}

		this.spinner.show();

		this.eventosService.putEvento(this.eventoId, evento).subscribe({
			next: () => {
				this.spinner.hide();
				this.toastr.success('Evento modificado com sucesso!', 'Successo!');
			},
			error: () => {
				this.spinner.hide();
				this.toastr.error('Ocorreu um erro ao tentar modificar evento. Tente novamente.', 'Erro!');
			}
		});
	}

	salvarAlteracoesEvento(): void {
		if (this.form.invalid) {
			this.toastr.error('Verifique se todos os campos estão preenchidos corretamente.', 'Aviso');
			return;
		}

		const novoEvento = {... this.form.value};
		if (this.modoEditar) {
			this._atualizarEvento(novoEvento);
		} else {
			this._cadastrarNovoEvento(novoEvento)
		}
	}

	definirValoresLotes(lotes: Array<any>): void {
		lotes.forEach(lote => {
			this.adicionarLote(lote);
		});

		this.changeDetectorRef.detectChanges();
	}

	adicionarLote(lote: any = {int: 0}): void {
		this.lotes.push(this.criarCamposLote(lote));
	}

	criarCamposLote(lote: any): FormGroup {
		return this.formBuilder.group({
			id: lote.id || 0,
			nome: [lote?.nome, [
				Validators.required,
				Validators.minLength(4)
			]],
			quantidade: [lote?.quantidade, Validators.required],
			preco: [lote?.preco ?? '', Validators.required],
			dataInicio: [lote.dataInicio ?? ''],
			dataFim: [lote.dataFim ?? '']
		})
	}

	public excluirLote(template: TemplateRef<any>, index: number): void {
		this.loteId = index;
		this.modal.show(template);
	}

	public confirmarDeletarLote(): void {
		if (this.loteId == null) {
			return;
		}

		this.modal.hide();
		this.spinner.show();

		this.lotes.removeAt(this.loteId);

		this.spinner.hide();
		this.toastr.success(`Lote #${this.loteId} deletado com sucesso.`, 'Successo!');
	}

	public cancelarDeletarLote(): void {
		this.modal.hide();
	}

	public salvarAlteracoesLotes(): void {
		this.spinner.show();

		if (this.lotes.valid && !!this.eventoId) {
			this.lotesService.saveLotes(this.eventoId, this.form.value.lotes).subscribe({
				next: (retorno: any) => {
					this.spinner.hide();
					this.toastr.success("Lotes salvos com sucesso.", "Sucesso!");
				},
				error: () => {
					this.toastr.error("Não foi possível salvar lotes.", "Erro!");
				}
			}).add(() => this.spinner.hide())
		}
	}

	public resetarCamposLotes() {
		this.lotes.reset();
	}
}
