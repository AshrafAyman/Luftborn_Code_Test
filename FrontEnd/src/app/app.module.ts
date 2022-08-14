import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { AuthModule } from './modules/auth/auth.module';
import { CourseModule } from './modules/Course/course.module';
import { SharedModule } from './shared/shared.module';
import { ModalModule } from 'ngx-bootstrap/modal';
import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { TokenInjectorInterceptor } from './shared/interceptors/token-injector.interceptor';
import { JwtModule } from '@auth0/angular-jwt';
import JwtModuleOptionsConst from './shared/models/constants/jwt-options.const';

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    CommonModule,
    BrowserAnimationsModule,
    AppRoutingModule,
    CourseModule,
    AuthModule,
    SharedModule,
    ModalModule.forRoot(),
    JwtModule.forRoot(JwtModuleOptionsConst()),
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: TokenInjectorInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
