import { Component } from '@angular/core';
import { RouterModule } from '@angular/router';
import { MaterialModule } from './material.module';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  standalone: true,
  imports: [RouterModule, MaterialModule],
  styleUrl: './app.component.scss',
})
export class AppComponent {
  title = 'LinguaLab.Client';
}
