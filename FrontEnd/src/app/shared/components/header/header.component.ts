import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { LocalStorageService } from '../../services/local-storage.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {

  constructor(private route:Router, private tokenService: LocalStorageService) { }

  ngOnInit(): void {
  }
  addCourse(){
    this.route.navigate(['core','add-course'])
  }

  CoursesPage(){
    this.route.navigate(['core','courses'])
  }
  logout(){
    this.tokenService.removeToken();
    this.route.navigate(['auth']);
  }
}
