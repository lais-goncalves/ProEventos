import {Component, CUSTOM_ELEMENTS_SCHEMA, signal} from '@angular/core';
import {RouterOutlet} from '@angular/router';
import {EventoService} from './_services/evento-service';
import {Nav} from './shared/nav/nav';
import {NgxSpinnerComponent, NgxSpinnerService} from 'ngx-spinner';

@Component({
	selector: 'app-root',
	imports: [RouterOutlet, Nav, NgxSpinnerComponent],
	templateUrl: './app.html',
	styleUrl: './app.scss',
	providers: [EventoService],
	schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class App {
	protected readonly title = signal('Eventos-App');

	constructor(private spinner: NgxSpinnerService) { }

	public ngOnInit(): void {
		this.spinner.show();

		setTimeout(() => {
			this.spinner.hide();
		}, 1000);
	}
}
