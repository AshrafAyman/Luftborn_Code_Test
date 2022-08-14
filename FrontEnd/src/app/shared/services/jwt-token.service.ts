import { Injectable } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';
import { LocalStorageService } from './local-storage.service';

@Injectable({
  providedIn: 'root'
})
export class JwtTokenService {

  constructor(private helper: JwtHelperService,private token: LocalStorageService) { }

  decodeToken(): any {
    var _token: any = this.token.getToken();
    return this.helper.decodeToken(_token);
  }

  tokenExpirationDate(): Date | null {
    var _token: any = this.token.getToken();
    return this.helper.getTokenExpirationDate(_token);
  }

  isTokenExpired(deleteToken = false): boolean {
    var _token: any = this.token.getToken();
    var isTokenExpired = this.helper.isTokenExpired(_token);
    if (isTokenExpired && deleteToken) {
      this.token.removeToken();
    }
    return isTokenExpired;
  }
}
