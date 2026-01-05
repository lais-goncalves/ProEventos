import { Component, HostListener } from '@angular/core';
import {CollapseDirective} from 'ngx-bootstrap/collapse';

@Component({
  selector: 'app-nav',
	imports: [
		CollapseDirective
	],
  templateUrl: './nav.html',
  styleUrl: './nav.scss',
})

export class Nav {
	public isCollapsed = true;

	@HostListener('window:resize', ['$event'])
	onResize(event: any) {
		if (event.target.innerWidth > 992) {
			this.isCollapsed = false;
		}

		else {
			this.isCollapsed = true;
		}
	}
}
