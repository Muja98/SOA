import { ToastService } from './Service/toast-service';
import { Component, OnInit } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import {URL_API_GATEWAY_SERVICE,URL_COMMAND_SERVICE} from "./API/api";
@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'WebDashboard';
  public _hubConnectionNotification: signalR.HubConnection;
  public _hubConnectionNotificationUser: signalR.HubConnection;
  public notificationArray:Array<String> = [];
  public tempnotificationArray:Array<String> = [];

  toasts: any[] = [];

  constructor(public toastService: ToastService){}

  show(header: string, body: string) {
    this.toasts.push({ header, body });
  }

  remove(toast) {
    this.toasts = this.toasts.filter(t => t != toast);
  }

  ngOnInit():void{
    this._hubConnectionNotification = new signalR.HubConnectionBuilder()
    .withUrl(URL_COMMAND_SERVICE+"/notification")
    .build()

    this._hubConnectionNotification
    .start()
    .then(() => {
      console.log('Connection started2! :)')
      this.joinRoomNotification();
    }).catch(err => console.log('Error while establishing connection :('));
    this._hubConnectionNotification.on('ReceiveNotification', (notification:string) => {
      
        this.notificationArray.push(notification);
        this.tempnotificationArray = [];
        this.tempnotificationArray = this.notificationArray;
        this.tempnotificationArray.reverse();

        this.toastService.show(notification, { classname: 'bg-success text-light', delay: 2000 });
    });

    this._hubConnectionNotificationUser = new signalR.HubConnectionBuilder()
    .withUrl(URL_API_GATEWAY_SERVICE+"/notificationUser")
    .build()

    this._hubConnectionNotificationUser
    .start()
    .then(() => {
      console.log('Connection started2! :)')
      this.joinRoomNotificationUser();
    }).catch(err => console.log('Error while establishing connection :('));
    this._hubConnectionNotificationUser.on('ReceiveNotification', (notification:string) => {
      
        this.notificationArray.push(notification);
        this.tempnotificationArray = [];
        this.tempnotificationArray = this.notificationArray;
        this.tempnotificationArray.reverse();

        this.toastService.show(notification, { classname: 'bg-success text-light', delay: 2000 });
    });

  }

  joinRoomNotification()
  {
    this._hubConnectionNotification.invoke("JoinRoom", "notificationGroup").catch((err)=>{
      console.log(err)
    })
  }

  joinRoomNotificationUser()
  {
    this._hubConnectionNotificationUser.invoke("JoinRoom", "notificationGroup").catch((err)=>{
      console.log(err)
    })
  }

  ngOnDestroy(): void {
    this._hubConnectionNotification.stop().then(() => console.log("Connection stopped."))
  }

  getCurrentYear():number
  {
    let currentYear = new Date().getFullYear();
    return currentYear;
  }
}
