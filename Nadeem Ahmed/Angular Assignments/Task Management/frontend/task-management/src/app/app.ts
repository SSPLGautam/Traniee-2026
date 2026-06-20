import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';

import { Dashboard } from './pages/dashboard/dashboard';
import { Sidebar } from './shared/components/sidebar/sidebar';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, Sidebar],
  templateUrl: './app.html',
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('task-management');
}
