import {Lote} from './lote';
import {RedeSocial} from './redesocial';

export interface Evento {
	id: number;
	local: string;
	dataEvento?: Date;
	tema: string;
	qtdPessoas: number;
	imagemURL: string;
	lotes: Lote[];
	redesSociais: RedeSocial[];
	//PalestrantesEventos: PalestrantesEvento[];
}
