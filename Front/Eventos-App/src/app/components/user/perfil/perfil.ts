import {Component, OnInit} from '@angular/core';
import {Titulo} from "@app/shared/titulo/titulo";
import {
	AbstractControl,
	AbstractControlOptions,
	FormBuilder,
	FormGroup,
	ReactiveFormsModule,
	Validators
} from '@angular/forms';
import {NgClass} from '@angular/common';
import {ValidadoresForm} from '@app/utils/validadores/validadores-form';

@Component({
  selector: 'app-perfil',
	imports: [
		Titulo,
		ReactiveFormsModule,
		NgClass
	],
  templateUrl: './perfil.html',
  styleUrl: './perfil.scss',
})
export class Perfil implements OnInit {
	public form: FormGroup = new FormGroup({});

	get f(): any {
		return this.form.controls;
	}

	constructor(private formBuilder: FormBuilder) {}

	ngOnInit() {
		this.validarCampos();
	}

	validarCampos(): void {
		const opcoes: AbstractControlOptions = {
			validators: [
				ValidadoresForm.senhasIguais(
					'senha',
					'confirmarSenha'
				)
			]
		};

		this.form = this.formBuilder.group({
			titulo: ['', Validators.required],
			primeiroNome: ['', Validators.required],
			ultimoNome: ['', Validators.required],
			email: ['', [
				Validators.required,
				Validators.email
			]],
			telefone: ['', Validators.required],
			funcao: ['', Validators.required],
			descricao: [''],
			senha: ['', [
				Validators.required,
				Validators.minLength(6)
			]],
			confirmarSenha: ['', Validators.required]
		}, opcoes)
	}

	resetarForm(): void {
		this.form.reset();
	}
}
