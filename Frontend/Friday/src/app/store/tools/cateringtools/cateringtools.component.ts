import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'friday-cateringtools',
  templateUrl: './cateringtools.component.html',
  styleUrls: ['./cateringtools.component.scss']
})
export class CateringtoolsComponent implements OnInit {

  isTouched: boolean = false

  constructor() { }

  ngOnInit() {
  }

  setTouched() {
    this.isTouched = true
  }

}
