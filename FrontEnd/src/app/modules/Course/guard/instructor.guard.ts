import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Observable } from 'rxjs';
import { Roles } from 'src/app/shared/models/enums/token-const.enum';
import { LocalStorageService } from 'src/app/shared/services/local-storage.service';

@Injectable({
  providedIn: 'root'
})
export class InstructorGuard implements CanActivate {

  constructor(private jwtTokenService: JwtHelperService,
    private localStorage: LocalStorageService,
    private route: Router) {
  }
  
  canActivate(
    route: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      var redirectedPath: boolean | UrlTree = this.route.createUrlTree([`/core`]);
      var token = this.localStorage.getToken()!;
      if (token) {
        var tokenDecoded = this.jwtTokenService.decodeToken(token);
        if (tokenDecoded) {
          var role = tokenDecoded[Roles.RolePropertyName];
          if (role == Roles.InstructorRole) {
            redirectedPath = true;
          }
        }
      }
      return redirectedPath;
  }
  
}
