import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { CourseModel } from 'src/app/shared/models/classes/courseMode';
import { FormControlService } from 'src/app/shared/services/form-control.service';
import { CourseService } from '../../services/course.service';

@Component({
  selector: 'app-create-edit-course',
  templateUrl: './create-edit-course.component.html',
  styleUrls: ['./create-edit-course.component.css']
})
export class CreateEditCourseComponent implements OnInit {

  constructor(public courseService: CourseService,private route:Router, private router:ActivatedRoute, 
    public formControlService:FormControlService, private toastrService: ToastrService) { }
  courseId:string|undefined;
  isEdit:boolean = false;
  course:CourseModel|undefined;
  courseForm = new FormGroup({
    courseName: new FormControl('', Validators.required),
    courseHours: new FormControl('', Validators.required),
    courseDepartment: new FormControl('', Validators.required),
    courseDescription: new FormControl('', Validators.required),
  });

  ngOnInit(): void {
    var id = this.router.snapshot.paramMap.get('id');
    if (id) {
      this.getCourse(id);
    }
  }

  AddCourse(){
    var newCourse = new CourseModel();
      newCourse.courseName = this.courseForm.value.courseName;
      newCourse.courseHours = this.courseForm.value.courseHours;
      newCourse.department = this.courseForm.value.courseDepartment;
      newCourse.courseDescription = this.courseForm.value.courseDescription;
      newCourse.instructorId = 1;
    this.courseService.AddCourse(newCourse).subscribe(result =>{
      this.toastrService.success(`${result.message}`, 'Create New Course', {
        timeOut: 3000,
      });
      this.route.navigate(['/courses'])
    });
  }
  getCourse(courseId:string){
    this.courseService.GetCourseDetails(Number(courseId)).subscribe(result =>{
      this.setValuesToFormGroup(result as CourseModel);
      this.isEdit = !this.isEdit;
    })
  }
  setValuesToFormGroup(course:CourseModel){
    this.course = course;
    this.courseForm.controls["courseName"].setValue(course.courseName);
    this.courseForm.controls["courseHours"].setValue(Number(course.courseHours));
    this.courseForm.controls["courseDepartment"].setValue(course.department);
    this.courseForm.controls["courseDescription"].setValue(course.courseDescription);

  }
  editCourse(){
      this.course!.courseName = this.courseForm.value.courseName;
      this.course!.courseHours = this.courseForm.value.courseHours;
      this.course!.department = this.courseForm.value.courseDepartment;
      this.course!.courseDescription = this.courseForm.value.courseDescription;
      this.courseService.EditCourse(this.course!).subscribe(result => {
        if (result.status) {
          this.toastrService.success(`${result.message}`, 'Edit Course', {
            timeOut: 3000,
          });
        }
        else{
          this.toastrService.error(`${result.message}`, 'Edit Course', {
            timeOut: 3000,
          });
        }
        this.route.navigate(['/courses'])
      })
  }

  
}
