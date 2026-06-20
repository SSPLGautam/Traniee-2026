import { Component, inject, OnInit, signal } from '@angular/core';

import { CommonModule } from '@angular/common';

import { AttendanceService } from '../../services/attendance.service';

import { Attendance } from '../../models/attendance.model';

@Component({
  selector: 'app-manager-attendance',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './manager-attendance.html',
})
export class ManagerAttendance implements OnInit {
  private attendanceService = inject(AttendanceService);

  attendances = signal<Attendance[]>([]);

  ngOnInit(): void {
    this.loadAttendance();
  }

  loadAttendance(): void {
    this.attendanceService.getAllAttendance().subscribe({
      next: (res) => {
        this.attendances.set(res.data);
      },
    });
  }
}
