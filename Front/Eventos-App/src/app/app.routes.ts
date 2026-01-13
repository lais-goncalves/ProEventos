import {Routes} from '@angular/router';
import {Eventos} from './components/eventos/eventos';
import {Contatos} from './components/contatos/contatos';
import {Palestrantes} from './components/palestrantes/palestrantes';
import {Perfil} from './components/perfil/perfil';
import {Dashboard} from './components/dashboard/dashboard';

export const routes: Routes = [
	{path: 'eventos', component: Eventos },
	{path: 'contatos', component: Contatos },
	{path: 'palestrantes', component: Palestrantes },
	{path: 'perfil', component: Perfil },
	{path: 'dashboard', component: Dashboard },
	{path: '', redirectTo: 'dashboard', pathMatch: 'full' },
	{path: '**', redirectTo: 'dashboard', pathMatch: 'full' },
];
