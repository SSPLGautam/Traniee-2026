import { inject, Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';

import { environment } from '../../environments/environment';

import { AttendanceResponse, AttendanceResponses } from '../models/attendance.model';

@Injectable({
  providedIn: 'root',
})
export class AttendanceService {
  private http = inject(HttpClient);

  markAttendance() {
    return this.http.post<AttendanceResponse>(
      `${environment.apiUrl}/attendance/mark`,
      {},
    );
  }

  getMyAttendance() {
    return this.http.get<AttendanceResponses>(`${environment.apiUrl}/attendance/my`);
  }

  getAllAttendance() {
    return this.http.get<AttendanceResponses>(`${environment.apiUrl}/attendance`);
  }
}
