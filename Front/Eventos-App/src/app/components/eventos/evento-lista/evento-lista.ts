import {Component, OnInit, TemplateRef} from '@angular/core';
import { ChangeDetectorRef } from '@angular/core';
import {CollapseDirective} from 'ngx-bootstrap/collapse';
import {FormsModule} from '@angular/forms';
import {EventoService} from '@app/_services/evento-service';
import {Evento} from '@app/_models/evento';
import {FontAwesomeModule, IconDefinition} from '@fortawesome/angular-fontawesome';
import {
	faCalendarAlt,
	faEye,
	faEyeSlash,
	faPenToSquare,
	faPlusCircle,
	faTrash
} from '@fortawesome/free-solid-svg-icons';
import {DateTimeFormatPipe} from '@app/helpers/DateTimeFormat/date-time-format-pipe';
import {BsModalRef, BsModalService} from 'ngx-bootstrap/modal';
import {ToastrService} from 'ngx-toastr';
import {NgxSpinnerService} from 'ngx-spinner';
import {Router, RouterLink} from '@angular/router';

@Component({
  selector: 'app-evento-lista',
	imports: [
		CollapseDirective,
		FormsModule,
		FontAwesomeModule,
		DateTimeFormatPipe,
		RouterLink
	],
  templateUrl: './evento-lista.html',
  styleUrl: './evento-lista.scss',
})
export class EventoLista implements OnInit {
// -- PROPS --
	// EVENTOS + FILTROS
	public eventos: Evento[] = [];
	public eventosFiltrados: Evento[] = [];
	private _filtro: string = '';

	// IMAGENS
	public largImagem: number = 100;
	public margImagem: number = 2;
	public mostrarImagens: boolean = false;

	// JANELA CONFIRMAÇÃO
	modalRef?: BsModalRef;

	// ÍCONES
	public faEye: IconDefinition = faEye;
	public faEyeSlash: IconDefinition = faEyeSlash;
	public faPenToSquare: IconDefinition = faPenToSquare;
	public faTrash: IconDefinition = faTrash;
	public faCalendar: IconDefinition = faCalendarAlt;
	public faPlusCircle: IconDefinition = faPlusCircle;

	// -- MÉTODOS --
	// CONSTRUTORES
	constructor(
		private eventoService: EventoService,
		private cd: ChangeDetectorRef,
		private modalService: BsModalService,
		private toastr: ToastrService,
		private spinner: NgxSpinnerService,
		private router: Router,
	) { }

	// DEFAULT
	public ngOnInit(): void {
		this.getEventos();
	}

	// EVENTOS
	public getEventos(): void {
		this.eventoService.getEventos().subscribe({
			next: (res: Evento[]) => {
				this.eventos = res;
				this.eventosFiltrados = this.eventos;
				this.cd.detectChanges();
			},
			error: (err: any) => {
				this.spinner.hide().then(() =>
					this.toastr.error('Erro ao caregar eventos.', 'Erro!')
				);
			},
			complete: () => this.spinner.hide()
		});
	}

	// FILTROS
	private normalizarString(str: string): string {
		return str.toLowerCase().trim();
	}

	public get filtro(): string {
		return this._filtro;
	}

	public set filtro(value: string) {
		this._filtro = value;
		let novoFiltro: string = this.normalizarString(this._filtro );

		this.eventosFiltrados = novoFiltro !== '' ? this.filtrarEventos(novoFiltro) : this.eventos;
	}

	public filtrarEventos(filtro: string): Evento[] {
		return this.eventos.filter((evento: any) =>
			this.normalizarString(evento.tema).indexOf(filtro) !== -1
			|| this.normalizarString(evento.local).indexOf(filtro) !== -1);
	}

	// IMAGENS
	public alterarImagens(): void {
		this.mostrarImagens = !this.mostrarImagens;
	}

	// JANELA CONFIRMAÇÃO
	openModal(template: TemplateRef<void>) {
		this.modalRef = this.modalService.show(template, { class: 'modal-sm' });
	}

	public confirm(): void {
		this.modalRef?.hide();
		this.toastr.success('O evento foi deletado com sucesso.',  'Deletado!');
	}

	public decline(): void {
		this.modalRef?.hide();
	}

	// DETALHES EVENTO
	public detalheEvento(id: number): void {
		this.router.navigate([`/eventos/detalhe/${id}`])
	}
}
