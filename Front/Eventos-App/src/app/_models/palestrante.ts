import {RedeSocial} from './redesocial';

export interface Palestrante {
	id: number;
	nome: string;
	miniCurriculo: string;
	imagemURL: string;
	telefone: string;
	email: string;
	redeSociais: RedeSocial[];
	//PalestranteEventos: PalestranteEvento[];
}
