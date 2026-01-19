import {ApplicationConfig, importProvidersFrom, provideBrowserGlobalErrorListeners} from '@angular/core';
import {provideRouter} from '@angular/router';

import {routes} from './app.routes';
import {BrowserAnimationsModule, provideAnimations} from '@angular/platform-browser/animations';
import {TooltipModule} from 'ngx-bootstrap/tooltip';
import {ModalModule} from 'ngx-bootstrap/modal';
import {ToastrModule} from 'ngx-toastr';
import {NgxSpinnerModule} from 'ngx-spinner';
import {ReactiveFormsModule} from '@angular/forms';

export const appConfig: ApplicationConfig = {
	providers: [
		provideBrowserGlobalErrorListeners(),
		provideRouter(routes),
		provideAnimations(),
		importProvidersFrom(BrowserAnimationsModule),
		importProvidersFrom(TooltipModule.forRoot()),
		importProvidersFrom(ModalModule.forRoot()),
		importProvidersFrom(ToastrModule.forRoot({
			timeOut: 3000,
			positionClass: 'toast-bottom-right',
			preventDuplicates: true,
			progressBar: true
		})),
		importProvidersFrom(NgxSpinnerModule.forRoot()),
		importProvidersFrom(ReactiveFormsModule)
	]
};
