import { Component, OnInit } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { CourseModel } from 'src/app/shared/models/classes/courseMode';
import { CourseService } from '../../services/course.service';

@Component({
  selector: 'app-preview-course-details',
  templateUrl: './preview-course-details.component.html',
  styleUrls: ['./preview-course-details.component.css']
})
export class PreviewCourseDetailsComponent implements OnInit {

  constructor(private courseService: CourseService,private route:Router, private router:ActivatedRoute) { }
  course:CourseModel|undefined;
  ngOnInit(): void {
    var id = this.router.snapshot.paramMap.get('id');
    if (id) {
      this.getCourse(id);
    }
  }
  getCourse(courseId:string){
    this.courseService.GetCourseDetails(Number(courseId)).subscribe(result =>{
      if (result) {
        this.course = result;
        console.log(this.course);
      }
    })
  }
}
