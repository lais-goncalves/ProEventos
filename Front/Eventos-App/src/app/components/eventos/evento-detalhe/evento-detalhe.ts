import {ChangeDetectorRef, Component, OnInit} from '@angular/core';
import {FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {NgClass} from '@angular/common';
import {ActivatedRoute, Router} from '@angular/router';
import {EventoService} from '@app/_services/evento-service';
import {Evento} from '@app/_models/evento';
import {ToastrService} from 'ngx-toastr';
import {DateTimeFormatPipe} from '@app/helpers/DateTimeFormat/date-time-format-pipe';
import {NgxSpinnerService} from 'ngx-spinner';

@Component({
  selector: 'app-evento-detalhe',
	imports: [
		ReactiveFormsModule,
		NgClass,
		DateTimeFormatPipe
	],
  templateUrl: './evento-detalhe.html',
  styleUrl: './evento-detalhe.scss',
})
export class EventoDetalhe implements OnInit {
	public eventoId: number | null = null;
	public form: FormGroup = new FormGroup({});

	get f(): any {
		return this.form.controls;
	}

	constructor(
		private formBuilder: FormBuilder,
		private changeDetectorRef: ChangeDetectorRef,
		private activeRouter: ActivatedRoute,
		private router: Router,
		private eventosService: EventoService,
		private toastr: ToastrService,
		private spinner: NgxSpinnerService
	) { }

	ngOnInit() {
		this.buscarInformacoesDoEvento();
		this.validarCampos();
		this.changeDetectorRef.detectChanges();
	}

	buscarInformacoesDoEvento(): void {
		const paramId = this.activeRouter.snapshot.params['id'];

		if (!!paramId) {
			this.spinner.show();
			this.eventoId = +paramId;

			this.eventosService.getEventoById(this.eventoId).subscribe({
				next: (evento: Evento) => {
					if (!evento) {
						this.spinner.hide();
						this.router.navigate(["/eventos/lista"]);
						return;
					}

					this.form.patchValue(evento);
					this.spinner.hide();
				},
				error: (evento: Evento) => {
					this.spinner.hide();
					this.toastr.error('Erro ao caregar informações do evento.', 'Erro!')
				}
			})
		}
	}

	validarCampos(): void {
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
		});
	}

	resetarCampos(): void {
		this.form.reset();
		console.log("hahahaha")
	}

	private _cadastrarNovoEvento(evento: Evento): void {
		this.spinner.show();

		this.eventosService.postEvento(evento).subscribe({
			next: () => {
				this.spinner.hide();
				this.toastr.success('Evento cadastrado com sucesso!', 'Successo!');
			},
			error: () => {
				this.spinner.hide();
				this.toastr.error('Ocorreu um erro ao tentar cadastrar evento. Tente novamente.', 'Erro!');
			}
		});
	}

	private _salvarAlteracoesEvento(evento: Evento): void {
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

	salvarAlteracoes(): void {
		if (this.form.invalid) {
			this.toastr.error('Verifique se todos os campos estão preenchidos corretamente.', 'Aviso');
			return;
		}

		const novoEvento = {... this.form.value};
		if (!this.eventoId) {
			this._cadastrarNovoEvento(novoEvento)
		} else {
			this._salvarAlteracoesEvento(novoEvento);
		}
	}
}
