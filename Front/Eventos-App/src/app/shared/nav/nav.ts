import { Component, HostListener } from '@angular/core';
import {CollapseDirective} from 'ngx-bootstrap/collapse';
import {BsDropdownDirective, BsDropdownMenuDirective, BsDropdownToggleDirective} from 'ngx-bootstrap/dropdown';
import {Router, RouterLink, RouterLinkActive} from '@angular/router';

@Component({
  selector: 'app-nav',
	imports: [
		CollapseDirective,
		BsDropdownDirective,
		BsDropdownToggleDirective,
		BsDropdownMenuDirective,
		RouterLink,
		RouterLinkActive
	],
  templateUrl: './nav.html',
  styleUrl: './nav.scss',
})

export class Nav {
	public isCollapsed = true;


	constructor(protected router: Router) {
	}

	@HostListener('window:resize', ['$event'])
	onResize(event: any) {
		if (event.target.innerWidth > 992) {
			this.isCollapsed = false;
		}

		else {
			this.isCollapsed = true;
		}
	}

	public mostrarMenu(): boolean {
		return this.router.url !== '/usuario/login' && this.router.url !== '/usuario/registro';
	}
}
