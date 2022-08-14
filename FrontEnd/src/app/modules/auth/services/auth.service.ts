import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { catchError, throwError } from 'rxjs';
import { UserModel } from 'src/app/shared/models/classes/userModel';
import { AuthPaths } from 'src/app/shared/models/enums/authPathes.enum';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(private http:HttpClient) { }
  url = environment.apiPath;
  controllerName = "Auth"
  login(user:UserModel){
    return this.http.post<any>(`${this.url}${this.controllerName}/${AuthPaths.Login}`,user)
    .pipe(catchError(error => throwError(() => this.handleError(error))));
  }

  private handleError(error: any): any {
    console.log(error);
    if (error.error) {
      return error.error;
    } else {
      return error;
    }
  }
}
