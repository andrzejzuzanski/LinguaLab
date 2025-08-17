import { Component, inject, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { DashboardService } from '../../services/dashboard.service';

@Component({
  selector: 'app-dashboard-home',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './dashboard-home.component.html',
  styleUrl: './dashboard-home.component.scss',
})
export class DashboardHomeComponent implements OnInit {
  private dashboardService = inject(DashboardService);

  heatmapData: any[] = [];

  ngOnInit(): void {
    this.dashboardService.getActivityHeatmap().subscribe({
      next: (data) => {
        console.log('Heatmap data:', data);
        this.heatmapData = data;
      },
      error: (err) => {
        console.error('Error fetching heatmap data:', err);
      },
    });
  }
}
