import { CanActivateFn, Router } from '@angular/router';
import { AuthService } from '../Services/Auth/auth.service';
import { inject } from '@angular/core';

export const authGuardGuard: CanActivateFn = (route, state) => {
  const userService=inject(AuthService)
  const router=inject(Router)
  if(userService.LoggedIn())
  {
    return true;
  }

  else{
    alert("login first");
    router.navigate([''])
    return false
  }
};
