import { Component } from '@angular/core';
import {FaIconComponent} from '@fortawesome/angular-fontawesome';
import {faUsers} from '@fortawesome/free-solid-svg-icons';
import {RouterLink} from '@angular/router';

@Component({
  selector: 'app-login',
	imports: [
		FaIconComponent,
		RouterLink
	],
  templateUrl: './login.html',
  styleUrl: './login.scss',
})
export class Login {
	public faUsers = faUsers;
}
