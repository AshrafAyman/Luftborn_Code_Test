import { Component, OnInit, ViewChild } from '@angular/core';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { CourseModel } from 'src/app/shared/models/classes/courseMode';
import { LocalStorageService } from 'src/app/shared/services/local-storage.service';
import { CourseService } from '../../services/course.service';

@Component({
  selector: 'app-courses-list',
  templateUrl: './courses-list.component.html',
  styleUrls: ['./courses-list.component.css']
})
export class CoursesListComponent implements OnInit {

  coursesList:Array<CourseModel> = [];
  modalRef?: BsModalRef;
  courseId:number|undefined;
  pageSize:number = 10;
  collectionSize = 0 ;
  @ViewChild("template") dialogTemplate:any
  constructor(private courseService: CourseService,private modalService: BsModalService,private route:Router,
    private toastrService: ToastrService,private jwtTokenService: JwtHelperService,private localStorage: LocalStorageService) { }
  ngOnInit(): void {
    this.getAllCourses();
  }

  getAllCourses(){
    this.courseService.GetCoursesList().subscribe(result => {
      this.coursesList = [...result];
      this.collectionSize = this.coursesList.length
    })
  }

  previewCourse(id:number){
    this.route.navigate(['core','preview-course',id])
  }

  editCourse(id:number){
    this.route.navigate(['core','edit-course',id])
  }

  openDeleteCourseModal(id:number){
    this.courseId = id;
    this.modalRef = this.modalService.show(this.dialogTemplate);
  }

  confirm(){
    this.courseService.DeleteCourse(this.courseId!).subscribe(result => {
      this.modalRef?.hide();
      this.courseId = undefined;
      if (result.status) {
        this.toastrService.success(`${result.message}`, 'Delete Course', {
          timeOut: 3000,
        });
        this.getAllCourses();
      }
      else{
        this.toastrService.error(`${result.message}`, 'Delete Course', {
          timeOut: 3000,
        });
      }
    })
  }

  decline(){
    this.modalRef?.hide()
  }
}
