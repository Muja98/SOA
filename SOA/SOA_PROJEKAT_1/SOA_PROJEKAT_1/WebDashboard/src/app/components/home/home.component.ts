import { SmartHome } from './../../Model/smartHome';
import { Component, OnInit } from '@angular/core';
import { NgbModalConfig, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { IoTService } from 'src/app/Service/iot.service';


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

  public disableSelect:boolean = false;
  public currentTimeOfReading:number = 0; 
  public setTimeOfReading:number = 1;

  public valueFromDataSelect:string = "";
  public valueFromOrderBySelect:string = "";
  public valueFromInput:number = 0;
  
  public SmartHomeArray:Array<SmartHome> = [];
  public tempSmartHomeArray:Array<SmartHome> = [];

  itemsPerPage:number = 3;
  itemsCount:number = 15;
  page:number = 1;
  from = 0;
  to = this.itemsPerPage;
  todoHabbit = [];
 

  ngOnInit(): void {  
    this.IoTService.getTimeInterval().subscribe((timeOfReadong:any) =>{
      this.currentTimeOfReading = timeOfReadong;
    });
    this.IoTService.getAllData(0,3).subscribe((sensorDataArray:Array<SmartHome>)=>{
      this.SmartHomeArray = sensorDataArray;
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
