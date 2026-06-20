import { Component, inject, OnInit, signal } from '@angular/core';

import { CommonModule } from '@angular/common';

import { LeaveService } from '../../services/leave.service';

import { Leave } from '../../models/leave.model';

@Component({
  selector: 'app-leave-requests',

  standalone: true,

  imports: [CommonModule],

  templateUrl: './leave-requests.html',

  styleUrl: './leave-requests.css',
})
export class LeaveRequests implements OnInit {
  private leaveService = inject(LeaveService);

  leaves = signal<Leave[]>([]);

  ngOnInit(): void {
    this.loadLeaves();
  }

  loadLeaves(): void {
    this.leaveService.getManagerLeaves().subscribe({
      next: (res) => {
        this.leaves.set(res.data);
      },
    });
  }

  approve(id: number): void {
    this.leaveService.updateLeaveStatus(id, 'Approved').subscribe({
      next: () => {
        this.loadLeaves();
      },
    });
  }

  reject(id: number): void {
    this.leaveService.updateLeaveStatus(id, 'Rejected').subscribe({
      next: () => {
        this.loadLeaves();
      },
    });
  }
}
