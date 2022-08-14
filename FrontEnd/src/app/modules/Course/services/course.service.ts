import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { CourseModel } from 'src/app/shared/models/classes/courseMode';
import { environment } from 'src/environments/environment';
import { catchError, throwError } from 'rxjs';
import { CoursePaths } from 'src/app/shared/models/enums/coursePaths.enum';
@Injectable({
  providedIn: 'root'
})
export class CourseService {

  constructor(private http: HttpClient) { }
  categories = [
    'Web Development',
    'Mobile Development',
    'Programing Language',
    'Database Design & Development',
    'Software Testing',
    'Software Engineering',
    'Software Development Tools',
  ]

  url = environment.apiPath;
  controllerName = "Course"

  AddCourse(course:CourseModel){
    return this.http.post<any>(`${this.url}${this.controllerName}/${CoursePaths.AddCourse}`,course)
    .pipe(catchError(error => throwError(() => this.handleError(error))));
  }

  EditCourse(course:CourseModel){
    return this.http.put<any>(`${this.url}${this.controllerName}/${CoursePaths.EditCourse}`,course)
    .pipe(catchError(error => throwError(() => this.handleError(error))));
  }

  GetCoursesList(){
    return this.http.get<any>(`${this.url}${this.controllerName}/${CoursePaths.GetCoursesList}`)
    .pipe(catchError(error => throwError(() => this.handleError(error))));
  }

  GetCourseDetails(courseId:number){
    return this.http.get<any>(`${this.url}${this.controllerName}/${CoursePaths.GetCourseDetails}/${courseId}`)
    .pipe(catchError(error => throwError(() => this.handleError(error))));
  }
  
  DeleteCourse(courseId:number){
    return this.http.delete<any>(`${this.url}${this.controllerName}/${CoursePaths.DeleteCourse}/${courseId}`)
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
