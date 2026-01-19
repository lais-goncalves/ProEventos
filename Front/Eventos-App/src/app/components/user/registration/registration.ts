import {Component, OnInit} from '@angular/core';
import {RouterLink} from '@angular/router';
import {AbstractControlOptions, FormBuilder, FormGroup, ReactiveFormsModule, Validators} from '@angular/forms';
import {NgClass} from '@angular/common';
import {ValidadoresForm} from '@app/utils/validadores/validadores-form';

@Component({
  selector: 'app-registration',
	imports: [
		RouterLink,
		ReactiveFormsModule,
		NgClass
	],
  templateUrl: './registration.html',
  styleUrl: './registration.scss',
})
export class Registration implements OnInit {
	public form: FormGroup = new FormGroup({});

	get f(): any {
		return this.form.controls;
	}

	constructor(private formBuilder: FormBuilder) { }

	ngOnInit() {
		this.validarCampos();
	}

	validarCampos(): void {
		const opcoesForm: AbstractControlOptions = {
			validators: ValidadoresForm.senhasIguais(
				'senha',
				'confirmarSenha'
			)
		};

		this.form = this.formBuilder.group({
			primeiroNome: ['', Validators.required],
			ultimoNome: ['', Validators.required],
			email: ['', [
				Validators.required,
				Validators.email
			]],
			usuario: ['', Validators.required],
			senha: ['', [
				Validators.required,
				Validators.minLength(6)
			]],
			confirmarSenha: ['', Validators.required],
			termosServico: ['']
		}, opcoesForm);
	}
}
