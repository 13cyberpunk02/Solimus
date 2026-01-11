import { TuiRoot } from "@taiga-ui/core";
import { Component, signal } from '@angular/core';
import { Layout } from "./shared";

@Component({
  selector: 'app-root',
  imports: [TuiRoot, Layout],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {}
