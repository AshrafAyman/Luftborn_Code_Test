import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { CreateEditCourseComponent } from './components/create-edit-course/create-edit-course.component';
import { PreviewCourseDetailsComponent } from './components/preview-course-details/preview-course-details.component';
import { CoursesListComponent } from './components/courses-list/courses-list.component';
import { CourseRoutingModule } from './course-routing.module';
import { CoursesMainPageComponent } from './components/courses-main-page/courses-main-page.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { BrowserModule } from '@angular/platform-browser';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';
import { SharedModule } from 'src/app/shared/shared.module';
import { ModalModule } from 'ngx-bootstrap/modal';


@NgModule({
  declarations: [

    CreateEditCourseComponent,
    PreviewCourseDetailsComponent,
    CoursesListComponent,
    CoursesMainPageComponent
  ],
  imports: [
    CommonModule,
    CourseRoutingModule,
    FormsModule,
    ReactiveFormsModule,
    HttpClientModule,
    ToastrModule.forRoot(),
    SharedModule,
    ModalModule.forRoot()
  ]
})
export class CourseModule { }
