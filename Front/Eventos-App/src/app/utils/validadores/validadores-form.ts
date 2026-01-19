import {AbstractControl, FormControl, ValidatorFn} from '@angular/forms';

export class ValidadoresForm {
	static senhasIguais(campoSenha: string, campoConfirmaSenha: string): any
	{
		return (control: AbstractControl): any =>
		{
			let senha = control.get(campoSenha);
			let confirmaSenha = control.get(campoConfirmaSenha);

			if (confirmaSenha?.errors && !confirmaSenha.errors?.['senhasiguais']) {
				return null;
			}

			if (senha?.value !== confirmaSenha?.value) {
				confirmaSenha?.setErrors({senhasiguais: true});
				return;
			}

			confirmaSenha?.setErrors(null);
		}
	}
}
