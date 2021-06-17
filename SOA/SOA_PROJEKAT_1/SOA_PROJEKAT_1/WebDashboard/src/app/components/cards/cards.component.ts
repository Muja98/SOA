import { realTimeChartData } from './../../Model/realTimeChartData';
import { Component,OnInit } from '@angular/core';
import {  NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { SmartHome } from './../../Model/smartHome';
import { RealtimeChartOptions } from 'ngx-graph';
import * as signalR from '@aspnet/signalr';
import { subSeconds } from 'date-fns';


@Component({
  selector: 'app-cards',
  templateUrl: './cards.component.html',
  styleUrls: ['./cards.component.css']
})
export class CardsComponent implements OnInit {

  public propertyArray:Array<String> =["Time",
  "use",
  "gen",
  "houseOverall",
  "dishwasher",
  "furnace1",
  "furnace2",
  "homeOffice",
  "fridge",
  "wineCellar",
  "garageDoor",
  "kitchen1",
  "kitchen2",
  "kitchen3",
  "barn",
  "well",
  "microwave",
  "livingRoom",
  "solar",
  "temperature",
  "icon",
  "humidity",
  "visibility",
  "summary",
  "apparentTemperature",
  "pressure",
  "windSpeed",
  "cloudCover",
  "windBearing",
  "precipIntensity",
  "precipProbability",
  "dewPoint" ]

  cardsArray: Array<realTimeChartData> = [];

  realtimeChartOptions: RealtimeChartOptions = {
    height: 230,
    width:400,
    margin: { left: 40 },
    lines: [
      { color: '#34B77C', lineWidth: 3, area: true, areaColor: '#34B77C', areaOpacity: .2 }
    ],
    xGrid: { tickPadding: 15, tickNumber: 5 },
    yGrid: { min: 0, max: 100, tickNumber: 5, tickFormat: (v: number) => `${v}C`, tickPadding: 25 }
  };

  realtimeChartData = [[]];
 
  generateRandomRealtimeData(
    n: number = 10,
    step: number = 1,
    min: number = 0,
    max: number = 100,
    date = new Date()
  ): { date: Date; value: number }[] {
    return Array.from(Array(n).keys())
      .map((_, i) => ({
        date: subSeconds(date, i * step),
        value: this.randomInt(min, max)
      }))
      .reverse();
  }

  randomInt(min: number = 0, max: number = 100): number {
    return Math.floor(Math.random() * (max - min + 1) + min);
  }

  checkIfIsChart(realTimeChartData:realTimeChartData):boolean
  {
    if(realTimeChartData.type==="chart")return true;
    else return false;
  }


  public _hubConnection: signalR.HubConnection;

  constructor(private modalService: NgbModal) { }

  min:number = 0;
  max:number = 0;
  propertyName:string = "";
  typeData:string = "";
  liveOrChart:string = "";

  ngOnInit(): void {

   

    this._hubConnection = new signalR.HubConnectionBuilder()
    .withUrl("http://localhost:55542/sensorData")//DataService
    .build()
  
    this._hubConnection
    .start()
    .then(() => {
      console.log('Connection started! :)')
      this.joinRoomDataSensor();
    }
        
    )
    .catch(err => console.log('Error while establishing connection :('));
    this._hubConnection.on('ReceiveMessage', (newSensorData:SmartHome) => {
      console.log(newSensorData)
      for(let i=0; i<this.cardsArray.length; i++)
      {
        if(this.cardsArray[i].type === "chart")
          this.cardsArray[i].realtimeChartData[0].push({date: new Date(), value: this.getValue(newSensorData,this.cardsArray[i])})
        else
          this.cardsArray[i].value = this.getValue(newSensorData,this.cardsArray[i])
      }
     
    });
  }

 

 getValue(newSensorData:SmartHome, dataFromArray:realTimeChartData)
 {
    if(dataFromArray.name === "use")
      return newSensorData.use;
    if(dataFromArray.name === "gen")
      return newSensorData.gen;
    if(dataFromArray.name === "houseOverall")
      return newSensorData.houseOverall;
    if(dataFromArray.name === "dishwasher")
      return newSensorData.dishwasher;
    if(dataFromArray.name === "furnace1")
      return newSensorData.furnace1;
    if(dataFromArray.name === "furnace2")
      return newSensorData.furnace2;
    if(dataFromArray.name === "homeOffice")
      return newSensorData.homeOffice;
    if(dataFromArray.name === "fridge")
      return newSensorData.fridge;
    if(dataFromArray.name === "wineCellar")
      return newSensorData.wineCellar;
    if(dataFromArray.name === "garageDoor")
      return newSensorData.garageDoor;
    if(dataFromArray.name === "kitchen1")
      return newSensorData.kitchen1;
    if(dataFromArray.name === "kitchen2")
      return newSensorData.kitchen2;
    if(dataFromArray.name === "kitchen3")
      return newSensorData.kitchen3;
    if(dataFromArray.name === "barn")
      return newSensorData.barn;
    if(dataFromArray.name === "well")
      return newSensorData.well;
    if(dataFromArray.name === "microwave")
      return newSensorData.microwave;
    if(dataFromArray.name === "livingRoom")
      return newSensorData.livingRoom;
    if(dataFromArray.name === "solar")
      return newSensorData.solar;
    if(dataFromArray.name === "temperature")
      return newSensorData.temperature;
    if(dataFromArray.name === "icon")
      return newSensorData.icon;
    if(dataFromArray.name === "humidity")
      return newSensorData.humidity;
    if(dataFromArray.name === "visibility")
      return newSensorData.visibility;
    if(dataFromArray.name === "summary")
      return newSensorData.summary;
    if(dataFromArray.name === "apparentTemperature")
      return newSensorData.apparentTemperature;
    if(dataFromArray.name === "pressure")
      return newSensorData.pressure;
    if(dataFromArray.name === "windSpeed")
      return newSensorData.windSpeed;
    if(dataFromArray.name === "cloudCover")
      return newSensorData.cloudCover;
    if(dataFromArray.name === "windBearing")
      return newSensorData.windBearing;
    if(dataFromArray.name === "precipIntensity")
      return newSensorData.precipIntensity;
    if(dataFromArray.name === "precipProbability")
      return newSensorData.precipProbability;
    if(dataFromArray.name === "dewPoint")
      return newSensorData.dewPoint;
 }

  joinRoomDataSensor()
  {
    this._hubConnection.invoke("JoinRoom", "dataFromSensor").catch((err)=>{
      console.log(err)
    })
  }


  open(content) {
    this.modalService.open(content);
  }

  handleGetData(value, type:number)
  {
    if(type===1)
      this.propertyName = value;
    if(type===2)
      this.typeData = value;
    if(type===3)
      this.liveOrChart = value;
  }

  handleAddNewCard()
  {
    let newCardObject: realTimeChartData = new realTimeChartData();
    newCardObject.realtimeChartOptions.yGrid.min = this.min;
    newCardObject.realtimeChartOptions.yGrid.max = this.max;
    newCardObject.type = this.liveOrChart
    newCardObject.typeData = this.typeData;
    newCardObject.name = this.propertyName;

    if(this.liveOrChart === "chart")
    {
      newCardObject.realtimeChartOptions.yGrid.tickFormat =  (v: number) => `${v}${this.typeData}`
    }
    this.cardsArray.push(newCardObject);

    this.min = 0;
    this.max = 0;
    this.liveOrChart = "";
    this.typeData = "";
    this.propertyName = "";
  }

}
