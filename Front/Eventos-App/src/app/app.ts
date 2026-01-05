import {Component, signal} from '@angular/core';
import {RouterOutlet} from '@angular/router';
import {Eventos} from './eventos/eventos';
import {Palestrantes} from './palestrantes/palestrantes';
import {Nav} from './nav/nav';

@Component({
	selector: 'app-root',
	imports: [RouterOutlet, Eventos, Nav],
	templateUrl: './app.html',
	styleUrl: './app.scss'
})
export class App {
	protected readonly title = signal('Eventos-App');
}
