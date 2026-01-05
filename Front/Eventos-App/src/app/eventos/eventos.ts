import {Component, OnInit} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef } from '@angular/core';
import {CollapseDirective} from 'ngx-bootstrap/collapse';
import {FormsModule} from '@angular/forms';

@Component({
	selector: 'app-eventos',
	templateUrl: './eventos.html',
	styleUrl: './eventos.scss',
	imports: [
		CollapseDirective,
		FormsModule
	]
})

export class Eventos implements OnInit {
	public eventos: any;

	public eventosFiltrados: any;
	private _filtro: string = '';

	public largImagem: number = 100;
	public margImagem: number = 2;
	public mostrarImagens = false;

	constructor(private http: HttpClient, private cd: ChangeDetectorRef) { }

	ngOnInit(): void {
		this.getEventos();
	}

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

	public filtrarEventos(filtro: string): any {
		return this.eventos.filter((evento: any) =>
			this.normalizarString(evento.tema).indexOf(filtro) !== -1
			|| this.normalizarString(evento.local).indexOf(filtro) !== -1);
	}

	public alterarImagens(): void {
		this.mostrarImagens = !this.mostrarImagens;
	}

	public getEventos(): void {
		this.http.get("https://localhost:5058/api/eventos").subscribe({
			next: (res: any) => {
				this.eventos = res;
				this.eventosFiltrados = this.eventos;
				this.cd.detectChanges();
			},
			error: err => console.log(err)
		});
	}
}
