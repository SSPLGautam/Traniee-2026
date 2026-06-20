import { Component, OnInit, inject, signal } from '@angular/core';

import { CommonModule } from '@angular/common';

import { LeaveService } from '../../services/leave.service';

import { Leave } from '../../models/leave.model';

@Component({
  selector: 'app-my-leaves',

  standalone: true,

  imports: [CommonModule],

  templateUrl: './my-leaves.html',

  styleUrl: './my-leaves.css',
})
export class MyLeaves implements OnInit {
  private leaveService = inject(LeaveService);

  leaves = signal<Leave[]>([]);

  ngOnInit(): void {
    this.loadLeaves();
  }

  loadLeaves(): void {
    this.leaveService.getMyLeaves().subscribe({
      next: (response) => {
        this.leaves.set(response.data);
      },

      error: (error) => {
        console.error(error);
      },
    });
  }
}
