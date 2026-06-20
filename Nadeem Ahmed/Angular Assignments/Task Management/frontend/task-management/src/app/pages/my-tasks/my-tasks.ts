import { Component, OnInit, inject, signal } from '@angular/core';

import { CommonModule } from '@angular/common';

import { TaskService } from '../../services/task.service';

import { Task } from '../../models/task.model';

@Component({
  selector: 'app-my-tasks',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './my-tasks.html',
  styleUrls: ['./my-tasks.css'],
})
export class MyTasks implements OnInit {
  private taskService = inject(TaskService);

  tasks = signal<Task[]>([]);

  ngOnInit(): void {
    this.loadTasks();
  }

  loadTasks(): void {
    this.taskService.getMyTasks().subscribe({
      next: (res) => {
        this.tasks.set(res.data);
      },

      error: (error) => {
        console.error(error);
      },
    });
  }
  changeStatus(taskId: number, status: string): void {
    this.taskService.updateTaskStatus(taskId, status).subscribe({
      next: () => {
        this.loadTasks();
      },

      error: (error) => {
        console.error(error);
      },
    });
  }

  onStatusChange(event: Event, taskId: number): void {
    const target = event.target as HTMLSelectElement;

    this.changeStatus(taskId, target.value);
  }
}
