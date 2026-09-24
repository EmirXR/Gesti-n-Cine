import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterLink } from '@angular/router';
import { AuthService } from '../../services/auth.service'; 

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterLink],
  templateUrl: './navbar.html'
})
export class NavbarComponent implements OnInit {
  isLoggedIn: boolean = false;

  public authService = inject(AuthService);
  private cd = inject(ChangeDetectorRef);

  ngOnInit(): void {
    this.authService.isLoggedIn$.subscribe((status) => {
      this.isLoggedIn = status;
      this.cd.detectChanges(); 
    });
  }

  cerrarSesion(): void {
    this.authService.logout();
  }
}