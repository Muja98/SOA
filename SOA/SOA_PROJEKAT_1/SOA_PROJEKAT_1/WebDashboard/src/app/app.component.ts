import { ToastService } from './Service/toast-service';
import { Component, OnInit } from '@angular/core';
import * as signalR from '@aspnet/signalr';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'WebDashboard';
  public _hubConnectionNotification: signalR.HubConnection;
  
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
    .withUrl("http://localhost:1113/notification")
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

  }

  joinRoomNotification()
  {
    this._hubConnectionNotification.invoke("JoinRoom", "notificationGroup").catch((err)=>{
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
