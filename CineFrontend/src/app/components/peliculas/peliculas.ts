import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { CineService } from '../../services/cine.service';

@Component({
  selector: 'app-peliculas',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './peliculas.html'
})
export class PeliculasComponent implements OnInit {
  peliculas: any[] = [];
  peliculaActual = { idPelicula: 0, nombre: '', duracion: 120 };
  modoEdicion = false;

  private cineService = inject(CineService);
  private cd = inject(ChangeDetectorRef); 

  ngOnInit(): void {
    this.cargarPeliculas();
  }

  cargarPeliculas() {
    this.cineService.getPeliculas().subscribe(data => {
      this.peliculas = [...data]; 
      this.cd.detectChanges();    
    });
  }

  guardar() {
    if (this.modoEdicion) {
      this.cineService.actualizarPelicula(this.peliculaActual.idPelicula, this.peliculaActual)
        .subscribe(() => { this.resetForm(); this.cargarPeliculas(); });
    } else {
      this.cineService.crearPelicula(this.peliculaActual)
        .subscribe(() => { this.resetForm(); this.cargarPeliculas(); });
    }
  }

  editar(p: any) {
    this.peliculaActual = { ...p };
    this.modoEdicion = true;
  }

  eliminar(id: number) {
    if (confirm('¿Desea eliminar esta película?')) {
      this.cineService.eliminarPelicula(id).subscribe(() => this.cargarPeliculas());
    }
  }

  resetForm() {
    this.peliculaActual = { idPelicula: 0, nombre: '', duracion: 120 };
    this.modoEdicion = false;
  }
}