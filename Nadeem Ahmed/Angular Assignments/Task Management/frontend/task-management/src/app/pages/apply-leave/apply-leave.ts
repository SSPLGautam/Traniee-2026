import { Component, inject, OnInit, signal } from '@angular/core';

import { CommonModule } from '@angular/common';

import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';

import { Router } from '@angular/router';

import { UserService } from '../../services/user.service';

import { LeaveService } from '../../services/leave.service';

import { Employee } from '../../models/user.model';

import { LeaveCreateRequest, LeaveType } from '../../models/leave.model';

@Component({
  selector: 'app-apply-leave',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './apply-leave.html',
  styleUrl: './apply-leave.css',
})
export class ApplyLeave implements OnInit {
  private fb = inject(FormBuilder);

  private userService = inject(UserService);

  private leaveService = inject(LeaveService);

  private router = inject(Router);

  managers = signal<Employee[]>([]);

  today = new Date().toISOString().split('T')[0];

  loading = signal(false);
  leaveTypes = Object.values(LeaveType);

  leaveForm = this.fb.group({
    leaveType: ['', Validators.required],

    fromDate: ['', Validators.required],

    toDate: ['', Validators.required],

    reason: ['', Validators.required],

    managerId: ['', Validators.required],
  });

  ngOnInit(): void {
    this.loadManagers();
  }

  loadManagers(): void {
    this.userService.getManagers().subscribe({
      next: (res) => {
        this.managers.set(res.data);
      },

      error: (error) => {
        console.error(error);
      },
    });
  }

  applyLeave(): void {
    if (this.leaveForm.invalid) {
      this.leaveForm.markAllAsTouched();

      return;
    }

    const fromDate = new Date(this.leaveForm.value.fromDate!);

    const toDate = new Date(this.leaveForm.value.toDate!);

    if (toDate < fromDate) {
      alert('To Date cannot be earlier than From Date');

      return;
    }

    const leaveData: LeaveCreateRequest = {
      leaveType: this.leaveForm.value.leaveType as LeaveType,

      fromDate: this.leaveForm.value.fromDate!,

      toDate: this.leaveForm.value.toDate!,

      reason: this.leaveForm.value.reason!,

      managerId: Number(this.leaveForm.value.managerId),
    };

    this.loading.set(true);

    this.leaveService.createLeave(leaveData).subscribe({
      next: () => {
        this.loading.set(false);

        alert('Leave Applied Successfully');

        this.router.navigate(['/my-leaves']);
      },

      error: (error) => {
        this.loading.set(false);

        console.error(error);
      },
    });
  }
}
