import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { User } from '../../../Core/interfaces/User';
import { Router } from '@angular/router';
import { AuthService } from '../../../Core/Services/Auth/auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent implements OnInit{
  
  registerForm!: FormGroup;
  user:User={
    id:'',
    username:'',
    email:'',
    password:''
  }
  constructor(private fb:FormBuilder,private router:Router,private auth:AuthService){}
  ngOnInit() {
    this.createREgisterForm()
  }


   //register forem
   createREgisterForm(){
    this.registerForm= this.fb.group({
      
      username:['',Validators.required],
      email:['',Validators.required],
      password:['', [Validators.required,Validators.minLength(12)]],
      confirmPassword:['',Validators.required]
    },{validator:this.passwordMatchValidator})
  }
   //custom validation
   passwordMatchValidator(g:AbstractControl){
    return g.get('password')?.value===g.get('confirmPassword')?.value ? null:{'mismatch':true}
  }

  regiser(){
    if(this.registerForm.valid){
      this.user.username=this.registerForm.get('username')?.value
      this.user.email=this.registerForm.get('email')?.value
      this.user.password=this.registerForm.get('password')?.value
      this.auth.Register(this.user).subscribe({
        next:(response)=>{
          console.log(response)
          this.router.navigate(['Login'])
        },
        error:(error)=>{
          console.log(error)
        }
      })
    }
  }
  Cancel(){
    this.router.navigate([""])
  }
}
