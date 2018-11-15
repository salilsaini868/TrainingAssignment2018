import { Component, OnInit } from '@angular/core';
import { DashboardService } from '../services/dashboard.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  statistics: any;

  constructor(private dashboardService: DashboardService) {
    this.statistics = {
      categoryCount: 0,
      productCount: 0
    };
  }

  ngOnInit() {
    this.dashboard();
  }

  dashboard() {
    this.dashboardService.Dashboard().
      subscribe((result: any) => {
        console.log(result);
        this.statistics.categoryCount = result.categoryCount;
        this.statistics.productCount = result.productCount;
      });
  }
}
