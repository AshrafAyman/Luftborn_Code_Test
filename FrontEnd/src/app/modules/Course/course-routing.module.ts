import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { CoursesListComponent } from './components/courses-list/courses-list.component';
import { CreateEditCourseComponent } from './components/create-edit-course/create-edit-course.component';
import { PreviewCourseDetailsComponent } from './components/preview-course-details/preview-course-details.component';

const routes: Routes = [
  { path: 'courses', component: CoursesListComponent },
  { path: 'add-course', component: CreateEditCourseComponent },
  { path: 'edit-course/:id', component: CreateEditCourseComponent },
  { path: 'preview-course/:id', component: PreviewCourseDetailsComponent },
  {path: '', redirectTo:'courses',pathMatch:'full'},
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CourseRoutingModule { }
