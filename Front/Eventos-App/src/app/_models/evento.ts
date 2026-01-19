import {Lote} from './lote';
import {RedeSocial} from './redesocial';

export interface Evento {
	id: number;
	telefone: string;
	local: string;
	dataEvento?: Date;
	tema: string;
	qtdPessoas: number;
	imagemURL: string;
	email: string;
	lotes: Lote[];
	redesSociais: RedeSocial[];
	//PalestrantesEventos: PalestrantesEvento[];
}
