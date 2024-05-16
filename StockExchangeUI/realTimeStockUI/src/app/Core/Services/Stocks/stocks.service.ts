import { Injectable } from '@angular/core';
import { environment } from '../../../../environments/environment';
import { MasterService } from '../Master/master.service';
import { Stock } from '../../interfaces/Stock';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class StocksService {

  constructor(private master:MasterService,private http:HttpClient) { }
  baseURL=environment.baseUrl
  getAllStocks(stockName:string[]):Observable<Stock[]>{
    let params = new HttpParams();
    params = params.append('StocksName', 'googl');
    params = params.append('StocksName', 'tsla');
    params = params.append('StocksName', 'aapl');
    return this.http.get<Stock[]>(this.baseURL+'api/Stock/getAllStocks',{ params: params })
  }
}
