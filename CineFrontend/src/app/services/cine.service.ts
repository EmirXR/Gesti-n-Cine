import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({ providedIn: 'root' })
export class CineService {
  private http = inject(HttpClient);
  private apiUrl = 'http://localhost:5268/api'; // Asegúrate de que coincida con el puerto de tu API .NET

  getDashboard(): Observable<any> {
    return this.http.get(`${this.apiUrl}/SalaCine/dashboard-indicadores`);
  }

  getPeliculas(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/Pelicula`);
  }

  crearPelicula(pelicula: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/Pelicula`, pelicula);
  }

  actualizarPelicula(id: number, pelicula: any): Observable<any> {
    return this.http.put(`${this.apiUrl}/Pelicula/${id}`, pelicula);
  }

  eliminarPelicula(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/Pelicula/${id}`);
  }

  getSalas(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/SalaCine`);
  }

  consultarDisponibilidadSala(nombreSala: string): Observable<any> {
    return this.http.get(`${this.apiUrl}/SalaCine/estado-disponibilidad?nombreSala=${encodeURIComponent(nombreSala)}`);
  }

  asignarPelicula(payload: any): Observable<any> {
    return this.http.post(`${this.apiUrl}/SalaCine/asignar-pelicula`, payload);
  }

  getAsignaciones(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/SalaCine/asignaciones`);
  }

  eliminarAsignacion(idSalaCine: number, idPelicula: number): Observable<any> {
  return this.http.delete(`${this.apiUrl}/SalaCine/asignaciones?idSalaCine=${idSalaCine}&idPelicula=${idPelicula}`);
  }
}