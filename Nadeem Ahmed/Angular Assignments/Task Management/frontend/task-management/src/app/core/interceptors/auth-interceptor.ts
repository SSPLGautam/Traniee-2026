import { HttpInterceptorFn, HttpErrorResponse } from '@angular/common/http';

import { inject } from '@angular/core';

import { Router } from '@angular/router';

import { catchError, switchMap, throwError } from 'rxjs';

import { AuthService } from '../../services/auth.service';

export const authInterceptor: HttpInterceptorFn = (req, next) => {
  const authService = inject(AuthService);

  const router = inject(Router);

  const token = authService.getToken();

  const authReq = token
    ? req.clone({
      setHeaders: {
        Authorization: `Bearer ${token}`,
      },
    })
    : req;

  return next(authReq).pipe(
    catchError((error: HttpErrorResponse) => {
      if (error.status === 401) {
        return authService.refreshToken().pipe(
          switchMap((response) => {
            localStorage.setItem(
              'token',

              response.accessToken,
            );

            const retryReq = req.clone({
              setHeaders: {
                Authorization: `Bearer ${response.accessToken}`,
              },
            });

            return next(retryReq);
          }),

          catchError(() => {
            authService.logout();

            router.navigate(['/login']);

            return throwError(() => error);
          }),
        );
      }

      return throwError(() => error);
    }),
  );
};
