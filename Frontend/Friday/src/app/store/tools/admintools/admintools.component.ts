import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'friday-admintools',
  templateUrl: './admintools.component.html',
  styleUrls: ['./admintools.component.scss']
})
export class AdmintoolsComponent implements OnInit {

  isTouched: boolean = false

  constructor(private router: Router) { }

  ngOnInit() {
    //this.router.getCurrentNavigation().finalUrl.toString().endsWith('admin')
  }

  setTouched() {
    this.isTouched = true
  }

}
