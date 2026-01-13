import { Component, HostListener } from '@angular/core';
import {CollapseDirective} from 'ngx-bootstrap/collapse';
import {BsDropdownDirective, BsDropdownMenuDirective, BsDropdownToggleDirective} from 'ngx-bootstrap/dropdown';

@Component({
  selector: 'app-nav',
	imports: [
		CollapseDirective,
		BsDropdownDirective,
		BsDropdownToggleDirective,
		BsDropdownMenuDirective
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
