import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { SlaCalculatorComponent } from './components/sla-calculator/sla-calculator.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, SlaCalculatorComponent],
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  title = 'SLA Calculator';
}