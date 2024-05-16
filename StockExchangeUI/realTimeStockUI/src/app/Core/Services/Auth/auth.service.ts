import { Injectable } from '@angular/core';
import { MasterService } from '../Master/master.service';
import { User } from '../../interfaces/User';
import { environment } from '../../../../environments/environment';
import { JwtHelperService } from '@auth0/angular-jwt';
import { map } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  baseURL=environment.baseUrl
  constructor(private master:MasterService) { }
  jwtHelper= new JwtHelperService();
  decodedToken:any;
  //Login
  Login(user:User){
    return this.master.post(this.baseURL+'api/Auth/login',user)
    .pipe(
      map((response:any)=>{
        const token=response;
        if(user!=undefined){
          localStorage.setItem('token',token)

          console.log('el token: ',token)
          this.decodedToken=this.jwtHelper.decodeToken(token)
          
          console.log('decoded: ', this.decodedToken)
          console.log(this.decodedToken.name)
        }
      })
    )
  }
  LoggedIn(){
    const token=localStorage?.getItem('token')
    return ! this.jwtHelper.isTokenExpired(token)
  }

  //register
  Register(user:User){
    return this.master.post(this.baseURL+'api/Auth/register',user)
  }
}
