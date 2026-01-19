import {IconDefinition} from '@fortawesome/angular-fontawesome';
import {faCalendarAlt} from '@fortawesome/free-solid-svg-icons';
import {Component} from '@angular/core';
import {Titulo} from '@app/shared/titulo/titulo';
import {RouterOutlet} from '@angular/router';

@Component({
	selector: 'app-eventos',
	templateUrl: './eventos.html',
	styleUrl: './eventos.scss',
	providers: [],
	imports: [
		Titulo,
		RouterOutlet
	]
})

export class Eventos {
	public faCalendar: IconDefinition = faCalendarAlt;
}
