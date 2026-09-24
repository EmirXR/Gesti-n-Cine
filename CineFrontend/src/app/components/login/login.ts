import { Component, inject } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './login.html'
})
export class LoginComponent {
  usuario = 'admin';
  clave = '12345';
  errorMsg = '';

  private authService = inject(AuthService);
  private router = inject(Router);

  onLogin() {
    if (this.authService.login(this.usuario, this.clave)) {
      this.router.navigate(['/dashboard']);
    } else {
      this.errorMsg = 'Usuario o contraseña incorrectos';
    }
  }
}