import {Component, OnInit} from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { ChangeDetectorRef } from '@angular/core';
import {CollapseDirective} from 'ngx-bootstrap/collapse';

@Component({
	selector: 'app-eventos',
	templateUrl: './eventos.html',
	styleUrl: './eventos.scss',
	imports: [
		CollapseDirective
	]
})

export class Eventos implements OnInit {
	public eventos: any;
	public largImagem: number = 100;
	public margImagem: number = 2;
	public mostrarImagens = true;

	constructor(private http: HttpClient, private cd: ChangeDetectorRef) { }

	ngOnInit() {
		this.getEventos();
	}

	alterarImagens() {
		this.mostrarImagens = !this.mostrarImagens;
	}

	public getEventos(): void {
		this.http.get("https://localhost:5058/api/eventos").subscribe({
			next: (res: any) => {
				this.eventos = res;
				console.log(this.eventos);
				this.cd.detectChanges();
			},
			error: err => console.log(err)
		});
	}
}
