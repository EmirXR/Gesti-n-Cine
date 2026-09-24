import { Component, OnInit, inject, ChangeDetectorRef } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CineService } from '../../services/cine.service';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard.html'
})
export class DashboardComponent implements OnInit {
  indicadores: any = { 
    totalSalas: 0, 
    totalSalasDisponibles: 0, 
    totalPeliculas: 0 
  };

  private cineService = inject(CineService);
  private cd = inject(ChangeDetectorRef);

  ngOnInit(): void {
    this.cineService.getDashboard().subscribe({
      next: (res) => {
        this.indicadores = res;
        this.cd.detectChanges(); 
      },
      error: (err) => console.error('Error cargando indicadores', err)
    });
  }
}