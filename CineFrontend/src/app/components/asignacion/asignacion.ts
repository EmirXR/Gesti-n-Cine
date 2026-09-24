import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CineService } from '../../services/cine.service';

@Component({
  selector: 'app-asignacion',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './asignacion.html'
})
export class AsignacionComponent implements OnInit {
  salas: any[] = [];
  peliculas: any[] = [];
  asignaciones: any[] = [];

  idSalaCine: number = 0;
  idPelicula: number = 0;
  fechaPublicacion: string = '';
  fechaFin: string = '';

  nombreSalaBuscar: string = '';
  resultadoConsultaSala: any = null;

  private cineService = inject(CineService);
  private cd = inject(ChangeDetectorRef);

  ngOnInit(): void {
    this.cineService.getSalas().subscribe({
      next: (res) => {
        this.salas = [...res]; 
        this.cd.detectChanges();
      },
      error: (err) => console.error('Error al obtener salas:', err)
    });

    this.cineService.getPeliculas().subscribe({
      next: (res) => {
        this.peliculas = [...res]; 
        this.cd.detectChanges();
      },
      error: (err) => console.error('Error al obtener películas:', err)
    });

    this.cargarAsignaciones();
  }

  cargarAsignaciones(): void {
    this.cineService.getAsignaciones().subscribe({
      next: (res) => {
        this.asignaciones = [...res];
        this.cd.detectChanges();
      },
      error: (err) => console.error('Error al obtener asignaciones:', err)
    });
  }

  consultarSala(): void {
    if (!this.nombreSalaBuscar) return;
    
    this.cineService.consultarDisponibilidadSala(this.nombreSalaBuscar)
      .subscribe({
        next: (res) => {
          this.resultadoConsultaSala = res;
          this.cd.detectChanges(); 
        },
        error: () => alert('Error al consultar el estado de la sala')
      });
  }

  consultarSalaDirecta(nombreSala: string): void {
    this.nombreSalaBuscar = nombreSala;
    this.consultarSala();
  }

  asignar(): void {
    if (!this.idSalaCine || !this.idPelicula || !this.fechaPublicacion || !this.fechaFin) {
      alert('Por favor complete todos los campos');
      return;
    }

    const payload = {
      idSalaCine: Number(this.idSalaCine),
      idPelicula: Number(this.idPelicula),
      fechaPublicacion: this.fechaPublicacion,
      fechaFin: this.fechaFin
    };

    this.cineService.asignarPelicula(payload).subscribe({
      next: () => {
        alert('Película asignada correctamente.');
        this.cargarAsignaciones(); 
        
        if (this.nombreSalaBuscar) {
          this.consultarSala(); 
        }
        
        this.resetForm();
      },
      error: (err) => alert(err.error?.mensaje || 'Error al asignar la película')
    });
  }

  eliminarAsignacion(idSalaCine: number, idPelicula: number): void {
    if (confirm('¿Deseas eliminar esta asignación?')) {
      this.cineService.eliminarAsignacion(idSalaCine, idPelicula).subscribe({
        next: () => {
          alert('Asignación eliminada correctamente.');
          this.cargarAsignaciones();
          if (this.nombreSalaBuscar) {
            this.consultarSala();
          }
        },
        error: () => alert('Error al eliminar la asignación')
      });
    }
  }

  resetForm(): void {
    this.idSalaCine = 0;
    this.idPelicula = 0;
    this.fechaPublicacion = '';
    this.fechaFin = '';
    this.cd.detectChanges();
  }
}