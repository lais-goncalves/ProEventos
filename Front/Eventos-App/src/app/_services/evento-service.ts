import {Evento} from '@app/_models/evento';
import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable, take} from 'rxjs';
import {environment} from '../../environments/environment';

@Injectable({
	providedIn: 'root'
})
export class EventoService {
	baseURL = `${environment.apiURL}api/eventos`;

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

	public uploadImagem(eventoId: number, imagem: File): Observable<Evento> {
		const formData = new FormData();
		formData.append('imagem', imagem);

		return this.http
			.post<Evento>(`${this.baseURL}/imagem/${eventoId}`, formData)
			.pipe(take(1));
	}
}
