import { inject, Injectable } from '@angular/core';

import { HttpClient } from '@angular/common/http';

import { environment } from '../../environments/environment';

import { LeaveCreateRequest, LeaveResponse, LeavesResponse } from '../models/leave.model';

@Injectable({
  providedIn: 'root',
})
export class LeaveService {
  private http = inject(HttpClient);

  createLeave(data: LeaveCreateRequest) {
    return this.http.post<LeaveResponse>(`${environment.apiUrl}/leaves`, data);
  }

  getManagerLeaves() {
    return this.http.get<LeavesResponse>(`${environment.apiUrl}/leaves/manager`);
  }

  updateLeaveStatus(id: number, status: string) {
    return this.http.put(`${environment.apiUrl}/leaves/${id}/status`, { status });
  }

  getMyLeaves() {
    return this.http.get<LeavesResponse>(`${environment.apiUrl}/leaves/my-leaves`);
  }

  getAllLeaves() {
    return this.http.get<LeavesResponse>(`${environment.apiUrl}/leaves`);
  }

  getManagers() {
    return this.http.get<LeavesResponse>(`${environment.apiUrl}/users/managers`);
  }
}
