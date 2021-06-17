import { SmartHome } from './../../Model/smartHome';
import { Component, OnInit } from '@angular/core';
import { NgbModalConfig, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { IoTService } from 'src/app/Service/iot.service';
import * as signalR from '@aspnet/signalr';
import {URL_DATA_SERVICE} from "../../API/api";

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  constructor(config: NgbModalConfig, private modalService: NgbModal, private IoTService: IoTService) {
    config.backdrop = 'static';
    config.keyboard = false;

   }
  
  public _hubConnection: signalR.HubConnection;


  public disableSelect:boolean = false;
  public currentTimeOfReading:number = 0; 
  public setTimeOfReading:number = 1;

  public valueFromDataSelect:string = "";
  public valueFromOrderBySelect:string = "";
  public valueFromInput:number = 0;
  
  public SmartHomeArray:Array<SmartHome> = [];
  public tempSmartHomeArray:Array<SmartHome> = [];
  public logsSmartHomeArray:Array<SmartHome> = [];
  public templogsSmartHomeArray:Array<SmartHome> = [];

  public modalSmartHome:SmartHome = new SmartHome();


  itemsPerPage:number = 3;
  itemsCount:number = 15;
  page:number = 1;
  from = 0;
  to = this.itemsPerPage;

  ngOnDestroy(): void {
    this._hubConnection.stop().then(() => console.log("Connection stopped."))
  }

  ngOnInit(): void {  
    this.IoTService.getTimeInterval().subscribe((timeOfReadong:any) =>{
      this.currentTimeOfReading = timeOfReadong;
    });
    this.IoTService.getAllData(0,3).subscribe((sensorDataArray:Array<SmartHome>)=>{
      this.SmartHomeArray = sensorDataArray;
    })

    this._hubConnection = new signalR.HubConnectionBuilder()
    .withUrl(URL_DATA_SERVICE+"/sensorData")
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
      this.logsSmartHomeArray.push(newSensorData);
      this.templogsSmartHomeArray = [];
      this.templogsSmartHomeArray = this.logsSmartHomeArray;
      this.templogsSmartHomeArray.reverse();
    });

 
    
  }

  joinRoomDataSensor()
  {
    this._hubConnection.invoke("JoinRoom", "dataFromSensor").catch((err)=>{
      console.log(err)
    })
  }

 

  handleGetDataBy(valueFromDataSelect: string):void
  {
    if(valueFromDataSelect === "4")
      this.disableSelect = true;
    else
      this.disableSelect = false;

    this.valueFromDataSelect = valueFromDataSelect;
  }

  handleShowSensorDataInModal(clickedSmartHomeObject:SmartHome,content:any)
  {
   
    this.modalSmartHome.time = clickedSmartHomeObject.time;
    this.modalSmartHome.use = clickedSmartHomeObject.use;
    this.modalSmartHome.gen = clickedSmartHomeObject.gen;
    this.modalSmartHome.houseOverall = clickedSmartHomeObject.houseOverall;
    this.modalSmartHome.dishwasher = clickedSmartHomeObject.dishwasher;
    this.modalSmartHome.furnace1 = clickedSmartHomeObject.furnace1;
    this.modalSmartHome.furnace2 = clickedSmartHomeObject.furnace2;
    this.modalSmartHome.homeOffice = clickedSmartHomeObject.homeOffice;
    this.modalSmartHome.fridge = clickedSmartHomeObject.fridge;
    this.modalSmartHome.wineCellar = clickedSmartHomeObject.wineCellar;
    this.modalSmartHome.garageDoor = clickedSmartHomeObject.garageDoor;
    this.modalSmartHome.kitchen1 = clickedSmartHomeObject.kitchen1;
    this.modalSmartHome.kitchen2 = clickedSmartHomeObject.kitchen2;
    this.modalSmartHome.kitchen3 = clickedSmartHomeObject.kitchen3;
    this.modalSmartHome.barn = clickedSmartHomeObject.barn;
    this.modalSmartHome.well = clickedSmartHomeObject.well;
    this.modalSmartHome.microwave = clickedSmartHomeObject.microwave;
    this.modalSmartHome.livingRoom = clickedSmartHomeObject.livingRoom;
    this.modalSmartHome.solar = clickedSmartHomeObject.solar;
    this.modalSmartHome.temperature = clickedSmartHomeObject.temperature;
    this.modalSmartHome.icon = clickedSmartHomeObject.icon;
    this.modalSmartHome.humidity = clickedSmartHomeObject.humidity;
    this.modalSmartHome.visibility = clickedSmartHomeObject.visibility;
    this.modalSmartHome.summary = clickedSmartHomeObject.summary;
    this.modalSmartHome.apparentTemperature = clickedSmartHomeObject.apparentTemperature;
    this.modalSmartHome.pressure = clickedSmartHomeObject.pressure;
    this.modalSmartHome.windSpeed = clickedSmartHomeObject.windSpeed;
    this.modalSmartHome.cloudCover = clickedSmartHomeObject.cloudCover;
    this.modalSmartHome.windBearing = clickedSmartHomeObject.windBearing;
    this.modalSmartHome.precipIntensity = clickedSmartHomeObject.precipIntensity;
    this.modalSmartHome.dewPoint = clickedSmartHomeObject.dewPoint;
    this.modalSmartHome.precipProbability = clickedSmartHomeObject.precipProbability;

    this.open(content);
  }

  handleGetOrderBy(valueFromOrderBy:string):void
  {
    this.valueFromOrderBySelect = valueFromOrderBy;
  }

  handleSetTimeReading()
  {
    if(isNaN(this.setTimeOfReading))return;

    this.IoTService.setTimeInterval(this.setTimeOfReading).subscribe((currentTime:number)=>{
      this.currentTimeOfReading = currentTime;
      this.setTimeOfReading = 1;
    })
  }

  handleFindData()
  {
    //if(isNaN(this.valueFromInput) || this.valueFromInput<1)return;
    if(this.valueFromDataSelect === "1")
    {
      this.IoTService.getUsage(this.valueFromInput, this.valueFromOrderBySelect).subscribe((sensorDataArray:Array<SmartHome>)=>{
        this.SmartHomeArray = [];
        this.tempSmartHomeArray = sensorDataArray;
        this.SmartHomeArray = this.tempSmartHomeArray.slice(this.from,this.to);
        this.itemsCount = this.tempSmartHomeArray.length;
      })
    }
    if(this.valueFromDataSelect === "2")
    {
      this.IoTService.getGenerated(this.valueFromInput, this.valueFromOrderBySelect).subscribe((sensorDataArray:Array<SmartHome>)=>{
        this.SmartHomeArray = [];
        this.tempSmartHomeArray = sensorDataArray;
        this.SmartHomeArray = this.tempSmartHomeArray.slice(this.from,this.to);
        this.itemsCount = this.tempSmartHomeArray.length;
      })
    }
    if(this.valueFromDataSelect === "3")
    {
      this.IoTService.getTemperature(this.valueFromInput, this.valueFromOrderBySelect).subscribe((sensorDataArray:Array<SmartHome>)=>{
        this.SmartHomeArray = [];
        this.tempSmartHomeArray = sensorDataArray;
        this.SmartHomeArray = this.tempSmartHomeArray.slice(this.from,this.to);
        this.itemsCount = this.tempSmartHomeArray.length;
      })
    }
    if(this.valueFromDataSelect === "4")
    {
      this.IoTService.getAllData(0,3).subscribe((sensorDataArray:Array<SmartHome>)=>{
        this.SmartHomeArray = [];
        this.SmartHomeArray = sensorDataArray;
        this.itemsCount = 15;
        console.log(this.SmartHomeArray)
      })
    }
    this.valueFromDataSelect = "";
    this.valueFromOrderBySelect = "";
    this.valueFromInput = 0;
  }

  handlePageNumber=(pageNumber)=>{
    
    if(!pageNumber || pageNumber===undefined || pageNumber === null) {
        this.from = 0;
        this.to = this.itemsPerPage;
    } else {
        if(pageNumber === Math.floor(this.itemsCount / this.itemsPerPage) + 1) {
          this.from = (pageNumber-1)*this.itemsPerPage;
          this.to = this.from+(this.itemsCount-this.from);
           

        } else {
          this.from = (pageNumber - 1) * this.itemsPerPage;
          this.to = this.from + this.itemsPerPage;
        }
    }
    if(this.valueFromDataSelect==="" || this.valueFromDataSelect =="1")
    {
      this.IoTService.getAllData(this.from,this.to).subscribe((sensorDataArray:Array<SmartHome>)=>{
        this.SmartHomeArray = [];
        this.SmartHomeArray = sensorDataArray;
      })
    }
    else
      this.SmartHomeArray = this.tempSmartHomeArray.slice(this.from,this.to);
  }

  open(content) {
    this.modalService.open(content);
  }

}
