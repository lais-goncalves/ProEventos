import {Evento} from '@app/_models/evento';
import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable, take} from 'rxjs';

@Injectable({
	providedIn: 'root'
})
export class EventoService {
	baseURL = "https://localhost:5058/api/eventos";

	constructor(private http: HttpClient) {}

	public getEventos(): Observable<Evento[]> {
		return this.http
			.get<Evento[]>(this.baseURL)
			.pipe(take(1));
	}

	public getEventosByTema(tema: string): Observable<Evento[]> {
		return this.http
			.get<Evento[]>(`${this.baseURL}/tema/${tema}`)
			.pipe(take(1));
	}

	public getEventoById(id: number): Observable<Evento> {
		return this.http
			.get<Evento>(`${this.baseURL}/id/${id}`)
			.pipe(take(1));
	}

	public postEvento(evento: Evento): Observable<object> {
		return this.http
			.post(`${this.baseURL}`, evento)
			.pipe(take(1));
	}

	public putEvento(id: number, evento: Evento): Observable<object> {
		return this.http
			.put(`${this.baseURL}/id/${id}`, evento)
			.pipe(take(1));
	}

	public deleteEvento(id: number): Observable<string> {
		return this.http
			.delete(`${this.baseURL}/id/${id}`, { responseType: 'text' })
			.pipe(take(1));
	}
}
