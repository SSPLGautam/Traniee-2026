import { Component, inject, signal } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { LoginResponse } from '../../models/auth.model';
import { HttpErrorResponse } from '@angular/common/http';

@Component({
  selector: 'app-login',
  imports: [ReactiveFormsModule, RouterLink],
  templateUrl: './login.html',
  styleUrl: './login.css',
})
export class Login {
  private fb = inject(FormBuilder);

  private authService = inject(AuthService);

  private router = inject(Router);

  errorMessage = signal('');

  loginForm = this.fb.group({

    email: [
      '',
      [
        Validators.required,
        Validators.email
      ]
    ],

    password: [
      '',
      Validators.required
    ]

  });

  login(): void {

    this.errorMessage.set('');

    if (this.loginForm.invalid) {
      return;
    }

    this.authService
      .login({

        email:
          this.loginForm.value.email!,

        password:
          this.loginForm.value.password!

      })
      .subscribe({

        next: (
          res: LoginResponse
        ) => {

          localStorage.setItem(
            'token',
            res.accessToken
          );

          localStorage.setItem(
            'user',
            JSON.stringify(
              res.user
            )
          );

         

          this.router.navigate(['/']);

        },

        error: (
          error: HttpErrorResponse
        ) => {

          console.error(error);

          this.errorMessage.set(
            error.error?.message ??
            'Login Failed'
          )


        }

      });

  }
}

