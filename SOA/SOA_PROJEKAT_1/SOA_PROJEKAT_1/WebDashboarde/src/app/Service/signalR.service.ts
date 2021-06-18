import { SmartHome } from './../Model/smartHome';
import { Injectable } from '@angular/core';

import {HttpClient} from '@angular/common/http';
import * as signalR from '@aspnet/signalr';

@Injectable({
  providedIn: 'root'
})

export class signalRService {

  public hubConnection: signalR.HubConnection;
  URL:string = "http://localhost:1112"
  constructor(private http:HttpClient) {

    this.startConnection();
   }

  public startConnection =()=>{
      this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(URL+"/sensorData")
      .build()

      this.hubConnection
      .start()
      .then(()=> console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: '+err))


  }

  



}