import { Injectable, inject } from '@angular/core';
import { Router } from '@angular/router';
import { BehaviorSubject, Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class AuthService {
  private router = inject(Router);

  
  private isLoggedInSubject = new BehaviorSubject<boolean>(this.isLoggedIn());
  public isLoggedIn$: Observable<boolean> = this.isLoggedInSubject.asObservable();

  login(usuario: string, clave: string): boolean {
    if (usuario === 'admin' && clave === '12345') {
      localStorage.setItem('usuario_cine', usuario);
      this.isLoggedInSubject.next(true); 
      return true;
    }
    return false;
  }

  logout(): void {
    localStorage.removeItem('usuario_cine');
    this.isLoggedInSubject.next(false); 
    this.router.navigate(['/login']);
  }

  isLoggedIn(): boolean {
    return !!localStorage.getItem('usuario_cine');
  }
}