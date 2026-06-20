export enum LeaveType {
  Sick = 'Sick',

  Casual = 'Casual',

  Annual = 'Earned',
}

export enum LeaveStatus {
  Pending = 'Pending',

  Approved = 'Approved',

  Rejected = 'Rejected',
}

export interface Leave {
  id: number;

  leaveType: string;

  fromDate: string;

  toDate: string;

  reason: string;

  status: string;

  employeeId: number;

  managerId: number;

  Employee?: {
    id: number;

    username: string;

    email: string;
  };

  Manager?: {
    id: number;

    username: string;

    email: string;
  };
}

export interface LeaveCreateRequest {
  leaveType: LeaveType;

  fromDate: string;

  toDate: string;

  reason: string;

  managerId: number;
}

export interface LeaveResponse {
  success: boolean;

  data: Leave;
}

export interface LeavesResponse {
  success: boolean;

  data: Leave[];
}
