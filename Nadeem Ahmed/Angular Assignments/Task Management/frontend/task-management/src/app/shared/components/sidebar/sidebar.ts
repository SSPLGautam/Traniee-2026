import { Component, inject, OnInit } from '@angular/core';
import { Router, RouterLink } from '@angular/router';

@Component({
  selector: 'app-sidebar',
  standalone: true,
  templateUrl: './sidebar.html',
  styleUrl: './sidebar.css'
})
export class Sidebar implements OnInit {
  private router = inject(Router)

  userRole = '';

  ngOnInit(): void {

    const user = JSON.parse(
      localStorage.getItem('user') || '{}'
    );

    console.log('USER');
    console.log(user);

    console.log('ROLE');
    console.log(user.Role);

    console.log('ROLE NAME');
    console.log(user.Role?.name);

    this.userRole = user.Role?.name ?? '';

  }


  logout(): void {

    localStorage.removeItem('token');

    localStorage.removeItem('user');

    this.router.navigate(['/login']);

  }

}