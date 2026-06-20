export interface Attendance {
  id: number;

  attendanceDate: string;

  attendanceTime: string;

  status: 'Present' | 'Absent' | 'Leave';

  employeeId: number;

  Employee?: {
    id: number;

    username: string;

    email: string;
  };
}

export interface AttendanceResponse {
  success: boolean;
  data: Attendance;
}

export interface AttendanceResponses {
  success: boolean;
  data: Attendance[];
}
