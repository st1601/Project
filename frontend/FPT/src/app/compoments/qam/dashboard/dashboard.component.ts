import { HttpClient } from '@angular/common/http';
import { Component, ViewChild } from '@angular/core';
import {
  ApexAxisChartSeries,
  ApexChart,
  ChartComponent,
  ApexDataLabels,
  ApexPlotOptions,
  ApexYAxis,
  ApexTitleSubtitle,
  ApexXAxis,
  ApexFill,
} from 'ng-apexcharts';

import { ApexNonAxisChartSeries, ApexResponsive } from 'ng-apexcharts';

export type ChartOptionsCol = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  dataLabels: ApexDataLabels;
  plotOptions: ApexPlotOptions;
  yaxis: ApexYAxis;
  xaxis: ApexXAxis;
  fill: ApexFill;
  title: ApexTitleSubtitle;
};

export type ChartOptionsPie = {
  series: ApexNonAxisChartSeries;
  chart: ApexChart;
  responsive: ApexResponsive[];
  labels: any;
};

export type ChartOptionsBar = {
  series: ApexAxisChartSeries;
  chart: ApexChart;
  dataLabels: ApexDataLabels;
  plotOptions: ApexPlotOptions;
  xaxis: ApexXAxis;
};

export type Data = {
  [PlayerName: string]: any;
  [Run: symbol]: any;
};

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss'],
})
export class DashboardComponent {
  url = 'http://dungmdung-001-site1.atempurl.com/api/Ideas/most-popular-idea';
  Player = [];
  Run = [];
  @ViewChild('chart') chart!: ChartComponent;
  public chartOptionsCol: Partial<ChartOptionsCol> | any;
  public chartOptionsPie: Partial<ChartOptionsPie> | any;
  public chartOptionsBar: Partial<ChartOptionsBar> | any;

  constructor(private httpClient: HttpClient) {
    //Pie chart
    this.chartOptionsPie = {
      series: [20, 15, 5, 38],
      chart: {
        width: 380,
        type: 'pie',
      },
      labels: ['Admin', 'Staff', 'QAC', 'QAM'],
      responsive: [
        {
          breakpoint: 480,
          options: {
            chart: {
              width: 200,
            },
            legend: {
              position: 'bottom',
            },
          },
        },
      ],
    };

    //Column chart
    this.chartOptionsCol = {
      series: [
        {
          name: 'Inflation',
          data: [2.3, 3.1, 4.0, 10.1, 4.0, 3.6],
        },
      ],
      chart: {
        height: 350,
        type: 'bar',
      },
      plotOptions: {
        bar: {
          dataLabels: {
            position: 'top', // top, center, bottom
          },
        },
      },
      dataLabels: {
        enabled: true,
        formatter: function (val: string) {
          return val + '%';
        },
        offsetY: -20,
        style: {
          fontSize: '12px',
          colors: ['#304758'],
        },
      },
      xaxis: {
        categories: ['Jan', 'Feb', 'Mar', 'Apr', 'May'],
        position: 'top',
        labels: {
          offsetY: -18,
        },
        axisBorder: {
          show: false,
        },
        axisTicks: {
          show: false,
        },
        crosshairs: {
          fill: {
            type: 'gradient',
            gradient: {
              colorFrom: '#D8E3F0',
              colorTo: '#BED1E6',
              stops: [0, 100],
              opacityFrom: 0.4,
              opacityTo: 0.5,
            },
          },
        },
        tooltip: {
          enabled: true,
          offsetY: -35,
        },
      },
      fill: {
        type: 'gradient',
        gradient: {
          shade: 'light',
          type: 'horizontal',
          shadeIntensity: 0.25,
          gradientToColors: undefined,
          inverseColors: true,
          opacityFrom: 1,
          opacityTo: 1,
          stops: [50, 0, 100, 100],
        },
      },
      yaxis: {
        axisBorder: {
          show: false,
        },
        axisTicks: {
          show: false,
        },
      },
    };

    //Bar chart
    this.chartOptionsBar = {
      series: [
        {
          name: 'basic',
          data: [5, 10, 20, 30, 45, 50],
        },
      ],
      chart: {
        type: 'bar',
        height: 350,
      },
      plotOptions: {
        bar: {
          horizontal: true,
        },
      },
      dataLabels: {
        enabled: false,
      },
      xaxis: {
        categories: [
          'Top idea 1',
          'Top idea 5',
          'Top idea 2',
          'Top idea 4',
          'Top idea 3',
        ],
      },
    };
  }

  ngOnInit(): void {
    this.httpClient.get(this.url).subscribe((result: Data) => {
      result['forEach']((x: { PlayerName: any }) => {});
    });
  }
}
