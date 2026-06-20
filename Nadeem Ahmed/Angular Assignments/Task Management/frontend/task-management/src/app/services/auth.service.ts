import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import {
  LoginRequest,
  LoginResponse,
  RegisterRequest,
  RegisterResponse,
} from '../models/auth.model';

@Injectable({
  providedIn: 'root',
})
export class AuthService {
  private http = inject(HttpClient);

  login(data: LoginRequest) {
    return this.http.post<LoginResponse>(`${environment.apiUrl}/auth/login`, data, {
      withCredentials: true,
    });
  }

  refreshToken() {
    return this.http.get<{
      success: boolean;
      accessToken: string;
    }>(`${environment.apiUrl}/auth/refresh-token`, {
      withCredentials: true,
    });
  }

  getToken(): string | null {
    return localStorage.getItem('token');
  }

  getUser() {
    const user = localStorage.getItem('user');

    return user ? JSON.parse(user) : null;
  }

  logout(): void {
    localStorage.removeItem('token');

    localStorage.removeItem('user');
  }

  register(data: RegisterRequest) {
    return this.http.post<RegisterResponse>(`${environment.apiUrl}/auth/register`, data);
  }
}
