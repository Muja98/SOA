import { RealtimeChartOptions, RealtimeChartComponent } from 'ngx-graph';

export class realTimeChartData {
  type: string = "";
  typeData: string = "";
  min: number = 0;
  max: number = 100;
  name: string = "";
  value: any;
  realtimeChartData: any = [[]];
  realtimeChartOptions: RealtimeChartOptions = {
    height: 230,
    width: 400,
    margin: { left: 40 },
    lines: [
      { color: '#34B77C', lineWidth: 3, area: true, areaColor: '#34B77C', areaOpacity: .2 }
    ],
    xGrid: { tickPadding: 15, tickNumber: 5 },
    yGrid: { min: 0, max: 100, tickNumber: 5, tickFormat: (v: number) => `${v}C`, tickPadding: 25 }
  }
  constructor() { }
}
