import {Component, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {NgClass} from '@angular/common';

@Component({
  selector: 'app-evento-detalhe',
	imports: [
		ReactiveFormsModule,
		NgClass
	],
  templateUrl: './evento-detalhe.html',
  styleUrl: './evento-detalhe.scss',
})
export class EventoDetalhe implements OnInit {
	public form: FormGroup = new FormGroup({});

	get f() : any {
		return this.form.controls;
	}

	constructor(private formBuilder: FormBuilder) { }

	ngOnInit() {
		this.validarCampos();
	}

	validarCampos(): void {
		this.form = this.formBuilder.group(
		{
			local: ['', Validators.required],
			dataEvento: ['', [
				Validators.required
			]],
			tema: ['', [
				Validators.required,
				Validators.minLength(4),
				Validators.maxLength(50)
			]],
			qtdPessoas: ['', [
				Validators.required,
				Validators.max(120000)
			]],
			telefone: ['', Validators.required],
			email: ['', [
				Validators.required,
				Validators.email
			]],
			imagemURL: ['', Validators.required],
		});
	}

	resetarCampos(): void {
		this.form.reset();
	}

	protected readonly console = console;
}
