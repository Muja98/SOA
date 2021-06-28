import { Injectable } from '@angular/core';
import { URL } from '../API/api';
import {HttpClient} from '@angular/common/http';
import { Router } from '@angular/router';
import { SmartHome } from '../Model/smartHome';

@Injectable({
  providedIn: 'root'
})

export class IoTService{

    constructor(private http:HttpClient, private router:Router) { }

    setTimeInterval(timeInterval:number) {
        return this.http.post(URL + "/api/commandGateway/setTimeInterval", timeInterval);
    } 
    
    getTimeInterval()
    {
        return this.http.get<number>(URL + "/api/commandGateway/getTimeInterval");
    }

    getLastAction()
    {
        return this.http.get<String>(URL+"/api/commandGateway/getLastAction");
    }

    getUsage(use:number, grSm:string)
    {
        return this.http.get<SmartHome[]>(URL+"/api/dataGateway/usage?use="+use+"&grSmUse="+grSm);
    }

    getGenerated(gen:number, grSm:string)
    {
        return this.http.get<SmartHome[]>(URL+"/api/dataGateway/generated?gen="+gen+"&grSmGen="+grSm);
    }

    getTemperature(temp:number, grSm:string)
    {
        return this.http.get<SmartHome[]>(URL+"/api/dataGateway/temperature?temp="+temp+"&grSmTemp="+grSm);
    }

    getAllData(from:number, to:number)
    {
        return this.http.get<SmartHome[]>(URL+"/api/dataGateway/allData?from="+from+"&to="+to);
    }

}