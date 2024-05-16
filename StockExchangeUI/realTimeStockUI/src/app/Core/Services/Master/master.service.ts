import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class MasterService {

  constructor(private http :HttpClient) { }
  get(url:string):Observable<any>{
    return this.http.get(url)
  }
  post(url:string,entity:any):Observable<any>{
    const options = {responseType: 'text' as 'json'};
    return this.http.post(url,entity,options);
  }
  put(url:string,entity:any):Observable<any>{
    return this.http.put(url,entity)
  }
  delete(url:string):Observable<any>{
    return this.http.delete(url)
  }
}
