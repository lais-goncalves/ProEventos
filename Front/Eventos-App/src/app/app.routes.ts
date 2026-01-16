import {Routes} from '@angular/router';
import {Eventos} from './components/eventos/eventos';
import {Contatos} from './components/contatos/contatos';
import {Palestrantes} from './components/palestrantes/palestrantes';
import {Perfil} from './components/user/perfil/perfil';
import {Dashboard} from './components/dashboard/dashboard';
import {EventoLista} from './components/eventos/evento-lista/evento-lista';
import {EventoDetalhe} from './components/eventos/evento-detalhe/evento-detalhe';
import {User} from './components/user/user';
import {Login} from './components/user/login/login';
import {Registration} from './components/user/registration/registration';

export const routes: Routes = [
	{path: 'eventos', redirectTo: '/eventos/lista', pathMatch: 'full' },
	{
		path: 'eventos', component: Eventos,
		children: [
			{ path: 'detalhe/:id', component: EventoDetalhe },
			{ path: 'detalhe', component: EventoDetalhe },
			{ path: 'lista', component: EventoLista },
		]
	},
	{ path: 'usuario/perfil', component: Perfil },
	{
		path: 'usuario', component: User,
		children: [
			{ path: 'login', component: Login },
			{ path: 'registro', component: Registration }
		]
	},
	{path: 'contatos', component: Contatos },
	{path: 'palestrantes', component: Palestrantes },
	{path: 'dashboard', component: Dashboard },
	{path: '', redirectTo: 'dashboard', pathMatch: 'full' },
	{path: '**', redirectTo: 'dashboard', pathMatch: 'full' },
];
