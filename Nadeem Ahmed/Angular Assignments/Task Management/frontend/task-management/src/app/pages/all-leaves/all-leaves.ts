import { Component, OnInit, inject, signal } from '@angular/core';

import { CommonModule } from '@angular/common';

import { LeaveService } from '../../services/leave.service';

import { Leave } from '../../models/leave.model';

@Component({
  selector: 'app-all-leaves',

  standalone: true,

  imports: [CommonModule],

  templateUrl: './all-leaves.html',

  styleUrl: './all-leaves.css',
})
export class AllLeaves implements OnInit {
  private leaveService = inject(LeaveService);

  leaves = signal<Leave[]>([]);

  ngOnInit(): void {
    this.loadLeaves();
  }

  loadLeaves(): void {
    this.leaveService.getAllLeaves().subscribe({
      next: (res) => {
        this.leaves.set(res.data);
      },

      error: (error) => {
        console.error(error);
      },
    });
  }
}
