import {Component, signal} from '@angular/core';
import {RouterOutlet} from '@angular/router';
import {Eventos} from './eventos/eventos';
import {Palestrantes} from './palestrantes/palestrantes';

@Component({
	selector: 'app-root',
	imports: [RouterOutlet, Eventos, Palestrantes],
	templateUrl: './app.html',
	styleUrl: './app.scss'
})
export class App {
	protected readonly title = signal('Eventos-App');
}
