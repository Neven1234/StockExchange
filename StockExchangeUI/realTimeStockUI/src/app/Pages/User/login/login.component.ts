import { Component, OnInit } from '@angular/core';
import { User } from '../../../Core/interfaces/User';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../../../Core/Services/Auth/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent implements OnInit {
  user:User={
    id:'',
    username:'',
    password:'',
    email:''
  }
  LoginForm!: FormGroup;
  constructor(private fb:FormBuilder,private router:Router,private Auth:AuthService){}
  ngOnInit(): void {
    this.createLoginForm()
  }
  //register forem
  createLoginForm(){
    this.LoginForm= this.fb.group({
      username:['',Validators.required],
      password:['', [Validators.required,Validators.minLength(11)]],
    })
  }
  Login(){
    if(this.LoginForm.valid){
      this.user.username=this.LoginForm.get('username')?.value
      this.user.password=this.LoginForm.get('password')?.value
      this.Auth.Login(this.user).subscribe({
        next:(res)=>{
          this.router.navigate([''])
        },
        error:(error)=>[
          console.log(error)
        ]
      })
    }
  }
  Cancel(){
    this.router.navigate([''])
  }
}
