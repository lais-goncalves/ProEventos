import {Inject, LOCALE_ID, Pipe, PipeTransform} from '@angular/core';
import {Constants} from '../../utils/constants/constants';
import {DatePipe} from '@angular/common';

@Pipe({
  	name: 'dateTimeFormat',
	standalone: true
})
export class DateTimeFormatPipe implements PipeTransform {
	private locale: string;

	constructor(@Inject(LOCALE_ID) locale: string) {
		this.locale = locale;
	}

	transform(value: any, format?: any, timezone?: any, locale?: any): string | null {
		// Create an instance of DatePipe manually just to do the work
		const datePipe = new DatePipe(this.locale);
		return datePipe.transform(value, Constants.DATE_TIME_FMT);
	}
}
