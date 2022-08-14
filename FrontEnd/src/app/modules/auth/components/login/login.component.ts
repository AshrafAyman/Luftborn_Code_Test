import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { UserModel } from 'src/app/shared/models/classes/userModel';
import { LocalStorageService } from 'src/app/shared/services/local-storage.service';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {
  user:UserModel = new UserModel();
  errorMessage: string | undefined = undefined;
  constructor(private authService:AuthService,private localStorage: LocalStorageService,private toastrService: ToastrService,private router: Router) { }

  ngOnInit(): void {
  }

  login(){
    this.authService.login(this.user).subscribe(response =>{
      if (response.status) {
        this.errorMessage = undefined;
        this.localStorage.setToken(response.message);
        this.toastrService.success('Logged in successfully', 'Login', {
          timeOut: 3000,
        });
        this.router.navigate(['core']);
      }
      else{
        this.errorMessage = response.message
      }
    })
  }
}
