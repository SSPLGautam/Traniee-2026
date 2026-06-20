import { Component, inject, signal } from '@angular/core';

import { CommonModule } from '@angular/common';

import { AttendanceService } from '../../services/attendance.service';

@Component({
  selector: 'app-attendance',

  standalone: true,

  imports: [CommonModule],

  templateUrl: './attendance.html',

  styleUrl: './attendance.css',
})
export class Attendance {
  private attendanceService = inject(AttendanceService);

  marked = signal(false);

  markAttendance(): void {
    this.attendanceService.markAttendance().subscribe({
      next: () => {
        this.marked.set(true);

        alert('Attendance Marked Successfully');
      },

      error: (error) => {
        alert(error.error.message);
      },
    });
  }
}
