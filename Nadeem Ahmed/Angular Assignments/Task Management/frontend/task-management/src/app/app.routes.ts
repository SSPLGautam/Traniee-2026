import { Routes } from '@angular/router';
import { Login } from './pages/login/login';
import { Register } from './pages/register/register';
import { Dashboard } from './pages/dashboard/dashboard';
import { authGuard } from './core/guards/auth.guard';
import { roleGuard } from './core/guards/role-guard';
import { MyTasks } from './pages/my-tasks/my-tasks';
import { CreateTask } from './pages/create-task/create-task';
import { UserList } from './pages/user-list/user-list';
import { TaskList } from './pages/task-list/task-list';
import { EditTask } from './pages/edit-task/edit-task';
import { ViewTask } from './pages/view-task/view-task';
import { ViewUser } from './pages/view-user/view-user';
import { EditUser } from './pages/edit-user/edit-user';
import { ApplyLeave } from './pages/apply-leave/apply-leave';
import { MyLeaves } from './pages/my-leaves/my-leaves';
import { LeaveRequests } from './pages/leave-requests/leave-requests';
import { AllLeaves } from './pages/all-leaves/all-leaves';
import { Attendance } from './pages/attendance/attendance';
import { ManagerAttendance } from './pages/manager-attendance/manager-attendance';

export const routes: Routes = [
  {
    path: 'login',
    component: Login,
  },
  {
    path: 'register',
    component: Register,
  },
  {
    path: '',
    component: Dashboard,
    canActivate: [authGuard],
  },
  {
    path: 'my-tasks',
    component: MyTasks,
    canActivate: [authGuard],
  },

  {
    path: 'tasks',
    component: TaskList,
    canActivate: [authGuard, roleGuard],
    data: {
      roles: ['Admin', 'Manager'],
    },
  },

  {
    path: 'tasks/edit/:id',
    component: EditTask,
    canActivate: [authGuard, roleGuard],
    data: {
      roles: ['Admin', 'Manager'],
    },
  },

  {
    path: 'create-task',
    component: CreateTask,
    canActivate: [authGuard, roleGuard],
    data: {
      roles: ['Admin', 'Manager'],
    },
  },

  {
    path: 'users',
    component: UserList,
    canActivate: [authGuard, roleGuard],
    data: {
      roles: ['Admin'],
    },
  },

  {
    path: 'tasks/view/:id',

    component: ViewTask,

    canActivate: [authGuard, roleGuard],

    data: {
      roles: ['Admin', 'Manager'],
    },
  },

  {
    path: 'users/view/:id',
    component: ViewUser,
    canActivate: [authGuard, roleGuard],
    data: {
      roles: ['Admin'],
    },
  },

  {
    path: 'users/edit/:id',
    component: EditUser,
    canActivate: [authGuard, roleGuard],
    data: {
      roles: ['Admin'],
    },
  },

  {
    path: 'apply-leave',
    component: ApplyLeave,
    canActivate: [authGuard],
  },

  {
    path: 'my-leaves',
    component: MyLeaves,
    canActivate: [authGuard],
  },

  {
    path: 'leave-requests',

    component: LeaveRequests,

    canActivate: [authGuard, roleGuard],

    data: {
      roles: ['Manager'],
    },
  },

  {
    path: 'all-leaves',
    component: AllLeaves,
    canActivate: [authGuard, roleGuard],
    data: {
      roles: ['Admin'],
    },
  },

  {
    path: 'attendance',
    component: Attendance,

    canActivate: [authGuard],
  },

  {
    path: 'attendance-list',
    component: ManagerAttendance,

    canActivate: [authGuard, roleGuard],

    data: {
      roles: ['Admin', 'Manager'],
    },
  },
];
