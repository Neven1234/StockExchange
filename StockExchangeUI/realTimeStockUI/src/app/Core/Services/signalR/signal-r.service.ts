import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr'
import { environment } from '../../../../environments/environment';
@Injectable({
  providedIn: 'root'
})
export class SignalRService {

  constructor() { }
  baseUrl=environment.baseUrl
  hubConnection : signalR.HubConnection | undefined
  startConnection=async ()=>{
    this.hubConnection=new signalR.HubConnectionBuilder()
    
    .withUrl(this.baseUrl+'stocks'
      ,{
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets
      }
    )
    .configureLogging(signalR.LogLevel.Information)
    .build()
    await this.hubConnection.start()
    .then(()=>{
      console.log('Hub Connection Started')
    })
    .catch(err=>console.log('error wile starting connection ', err))
  }
  // baseURL=environment.baseUrl
  //  hubConnection: signalR.HubConnection | undefined
  // startConnection=async ()=>{
  //   this.hubConnection=new signalR.HubConnectionBuilder()
    
  //   .withUrl(this.baseURL+'stocks'
  //     ,{
  //       skipNegotiation: true,
  //       transport: signalR.HttpTransportType.WebSockets
  //     }
  //   )
  //   .configureLogging(signalR.LogLevel.Information)
  //   .build()
  //   await this.hubConnection.start()
  //   .then(()=>{
  //     console.log('Hub Connection Started')
  //   })
  //   .catch(err=>console.log('error wile starting connection ', err))
  // }
  // async StocksHub(){

  // }
}
