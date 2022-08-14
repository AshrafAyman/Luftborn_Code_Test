import { Injectable } from '@angular/core';
import { Token } from '../models/enums/token-const.enum';

@Injectable({
  providedIn: 'root'
})
export class LocalStorageService {

  constructor() { }
  
  public setToken(value: string) {
    localStorage.removeItem(Token.TokenKey);
    localStorage.setItem(Token.TokenKey, value);
  }

  public getToken() {
    var token = localStorage.getItem(Token.TokenKey);
    return token;
  }

  public removeToken() {
    localStorage.removeItem(Token.TokenKey);
  }
}
