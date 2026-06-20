import { Component, inject, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';

import { TaskService } from '../../services/task.service';

import { DashboardStats } from '../../models/dashboard.model';

import { Task } from '../../models/task.model';

import { StoredUser } from '../../models/auth.model';
import { Router, RouterLink } from '@angular/router';
import { LeaveService } from '../../services/leave.service';
import { Leave } from '../../models/leave.model';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './dashboard.html',
  styleUrl: './dashboard.css',
})
export class Dashboard implements OnInit {
  private taskService = inject(TaskService);
  private router = inject(Router);
  private leaveService = inject(LeaveService);

  tasks = signal<Task[]>([]);

  stats = signal<DashboardStats>({
    totalTasks: 0,
    pendingTasks: 0,
    inProgressTasks: 0,
    completedTasks: 0,
    totalLeaves: 0,
    pendingLeaves: 0,
    approvedLeaves: 0,
    rejectedLeaves: 0,
  });

  userName = signal('');
  userRole = signal('');

  ngOnInit(): void {
    const userString = localStorage.getItem('user');

    if (userString) {
      const user: StoredUser = JSON.parse(userString);

      this.userName.set(user.username);

      this.userRole.set(user.Role?.name ?? 'Employee');
    }

    this.loadTasks();
    this.loadLeaves();
  }

  loadTasks(): void {
    switch (this.userRole()) {
      case 'Admin':
        this.taskService.getAllTasks().subscribe({
          next: (res) => {
            this.tasks.set(res.data);
            this.calculateStats(res.data);
          },
        });

        break;

      case 'Manager':
        this.taskService.getAllTasks().subscribe({
          next: (res) => {
            this.tasks.set(res.data);
            this.calculateStats(res.data);
          },
        });

        break;

      case 'Employee':
        this.taskService.getMyTasks().subscribe({
          next: (res) => {
            this.tasks.set(res.data);
            this.calculateStats(res.data);
          },
        });

        break;
    }
  }

  loadLeaves(): void {
    switch (this.userRole()) {
      case 'Admin':
        this.leaveService.getAllLeaves().subscribe({
          next: (res) => {
            this.calculateLeaveStats(res.data);
          },
        });

        break;

      case 'Manager':
        this.leaveService.getManagerLeaves().subscribe({
          next: (res) => {
            this.calculateLeaveStats(res.data);
          },

          error: (error) => {
            console.error(error);
          },
        });

        break;

      case 'Employee':
        this.leaveService.getMyLeaves().subscribe({
          next: (res) => {
            this.calculateLeaveStats(res.data);
          },
        });

        break;
    }
  }
  private calculateStats(tasks: Task[]): void {
    this.stats.update((current) => ({
      ...current,

      totalTasks: tasks.length,

      pendingTasks: tasks.filter((task) => task.status === 'Pending').length,

      inProgressTasks: tasks.filter((task) => task.status === 'In Progress').length,

      completedTasks: tasks.filter((task) => task.status === 'Completed').length,
    }));
  }
  private calculateLeaveStats(leaves: Leave[]): void {
    this.stats.update((current) => ({
      ...current,

      totalLeaves: leaves.length,

      pendingLeaves: leaves.filter((leave) => leave.status === 'Pending').length,

      approvedLeaves: leaves.filter((leave) => leave.status === 'Approved').length,

      rejectedLeaves: leaves.filter((leave) => leave.status === 'Rejected').length,
    }));
  }

  logout(): void {
    localStorage.removeItem('token');

    localStorage.removeItem('user');

    this.router.navigate(['/login']);
  }
}
