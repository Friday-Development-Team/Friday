import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { RefService } from 'src/app/services/ref.service';

@Component({
  selector: 'friday-shopcontainer',
  templateUrl: './shopcontainer.component.html',
  styleUrls: ['./shopcontainer.component.scss']
})
export class ShopcontainerComponent implements OnInit {

  private readonly ref: string = 'shop'

  constructor(private refS: RefService) {
    this.refS.sendRef(this.ref)
  }

  ngOnInit() {

  }

}
