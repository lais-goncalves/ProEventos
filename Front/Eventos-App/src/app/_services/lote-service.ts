import {Lote} from '@app/_models/lote';
import {HttpClient} from '@angular/common/http';
import {Injectable} from '@angular/core';
import {Observable, take} from 'rxjs';

@Injectable({
	providedIn: 'root'
})
export class LoteService {
	baseURL = "https://localhost:5058/api/lotes";

	constructor(private http: HttpClient) {}

	public getLotesByEventoId(eventoId: number): Observable<Lote[]> {
		return this.http
			.get<Lote[]>(`${this.baseURL}/evento_id/${eventoId}`)
			.pipe(take(1));
	}

	public getLoteById(eventoId: number, loteId: number): Observable<Lote> {
		return this.http
			.get<Lote>(`${this.baseURL}/evento_id/${eventoId}/lote_id/${loteId}`)
			.pipe(take(1));
	}

	public saveLotes(eventoId: number, lotes: Lote[]): Observable<object> {
		return this.http
			.put(`${this.baseURL}/evento_id/${eventoId}`, lotes)
			.pipe(take(1));
	}

	public deleteLote(eventoId: number, loteId: number): Observable<string> {
		return this.http
			.delete(`${this.baseURL}/evento_id/${eventoId}/lote_id/${loteId}`, { responseType: 'text' })
			.pipe(take(1));
	}
}
