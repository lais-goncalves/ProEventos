import {Evento} from '@app/_models/evento';
import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable} from 'rxjs';

@Injectable({
	providedIn: 'root'
})
export class EventoService {
	baseURL = "https://localhost:5058/api/eventos";

	constructor(private http: HttpClient) {}

	public getEventos(): Observable<Evento[]> {
		return this.http.get<Evento[]>(this.baseURL);
	}

	public getEventosByTema(tema: string): Observable<Evento[]> {
		return this.http.get<Evento[]>(`${this.baseURL}/tema/${tema}`);
	}

	public getEventoById(id: number): Observable<Evento> {
		return this.http.get<Evento>(`${this.baseURL}/id/${id}`);
	}

	// public deleteEvento(id: number): Observable<object> {
	// 	return this.http.delete(`${this.baseURL}/id/${id}`);
	// }
}
