import { Component, OnInit } from '@angular/core';
import { MasterService } from '../../../Core/Services/Master/master.service';
import { environment } from '../../../../environments/environment';
import { StocksService } from '../../../Core/Services/Stocks/stocks.service';
import { Stock } from '../../../Core/interfaces/Stock';
import { error } from 'console';
import { response } from 'express';
import { SignalRService } from '../../../Core/Services/signalR/signal-r.service';

@Component({
  selector: 'app-stocks',
  templateUrl: './stocks.component.html',
  styleUrl: './stocks.component.css'
})
export class StocksComponent implements OnInit {
  stocksNames:string[]=[
    'googl',
    'aapl',
    'tsla'
  ]
  stocks:Stock[]=[]
  stock:Stock={
    symbol: '',
    currency: '',
    regularMarketPrice: 0,
    fiftyTwoWeekHigh: 0,
    fiftyTwoWeekLow: 0,
    regularMarketDayHigh: 0,
    regularMarketDayLow: 0,
    regularMarketVolume: 0,
    chartPreviousClose: 0,
    timestamp: []
  }
  constructor(private stockService:StocksService,private signalR:SignalRService){}
  ngOnInit(): void {
    this.signalR.startConnection()
    // this.signalR.hubConnection?.on("ReceiveMessage",(stocksUpdate:Stock[])=>{
    //   this.stocks=stocksUpdate
    // })
    // this.getStocks()
  }
  getStocks(){
   this.stockService.getAllStocks(this.stocksNames).subscribe({
    next:(response)=>{
      console.log(response)
      this.stocks=response
    },
    error:(error)=>{
      console.log(error)
    }
   })
  }
}
