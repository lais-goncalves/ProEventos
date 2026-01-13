import { Component } from '@angular/core';
import {Titulo} from "../../shared/titulo/titulo";

@Component({
  selector: 'app-dashboard',
    imports: [
        Titulo
    ],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.scss',
})
export class Dashboard {

}
