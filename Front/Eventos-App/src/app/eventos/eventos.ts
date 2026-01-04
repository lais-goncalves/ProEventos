import {Component, OnInit} from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Component({
	selector: 'app-eventos',
	templateUrl: './eventos.html',
	styleUrl: './eventos.scss',
})

export class Eventos implements OnInit {
	public eventos: any;

	constructor(private http: HttpClient) { }

	ngOnInit() {
		this.getEventos();
	}

	public getEventos(): void {
		this.http.get("https://localhost:5058/api/eventos").subscribe({
			next: (res: any) => {
				this.eventos = res;
				console.log(this.eventos);
			},
			error: err => console.log(err)
		});
	}
}
