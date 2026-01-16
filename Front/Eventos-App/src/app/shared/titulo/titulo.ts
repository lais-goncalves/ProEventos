import {Component, input, Input} from '@angular/core';
import {FaIconComponent, IconDefinition} from '@fortawesome/angular-fontawesome';
import {faUser} from '@fortawesome/free-solid-svg-icons';
import {Router} from '@angular/router';

@Component({
	selector: 'app-titulo',
	imports: [
		FaIconComponent
	],
	templateUrl: './titulo.html',
	styleUrl: './titulo.scss',
	standalone: true,
})
export class Titulo {
	@Input() titulo: string = '';
	@Input() subtitulo: string = 'Desde 2021';
	@Input() icone: IconDefinition = faUser;
	@Input() botaoListar: boolean = false;

	constructor(private router: Router) { }

	public listar(): void {
		this.router.navigate([`/${this.titulo.toLowerCase()}/lista`]);
	}
}
