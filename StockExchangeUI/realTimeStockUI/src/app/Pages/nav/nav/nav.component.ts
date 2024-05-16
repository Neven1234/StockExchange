import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../Core/Services/Auth/auth.service';

@Component({
  selector: 'app-nav',
  templateUrl: './nav.component.html',
  styleUrl: './nav.component.css'
})
export class NavComponent implements OnInit{

  Loggedin:boolean=false
  constructor(private Auth:AuthService){}
  ngOnInit(): void {
    this.isLoggedIn()
  }
  isLoggedIn(){
    this.Loggedin=this.Auth.LoggedIn()
  }
}
