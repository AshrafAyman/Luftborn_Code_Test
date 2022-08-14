import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginGuard } from './modules/auth/guard/login.guard';
import { CoursesListComponent } from './modules/Course/components/courses-list/courses-list.component';
import { InstructorGuard } from './modules/Course/guard/instructor.guard';

const routes: Routes = [
  {
    path: 'core',
    canActivate:[LoginGuard,InstructorGuard],
    loadChildren: () => import('./modules/Course/course.module').then(module => module.CourseModule)
  },
  {
    path:'auth',
    loadChildren: () => import('./modules/auth/auth.module').then(module => module.AuthModule)
  },
  {
    path: '',redirectTo:'core'  ,pathMatch:'full'
  }, 
  {
    path: '**', redirectTo:'core'  ,pathMatch:'full'
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes,{useHash:true})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
