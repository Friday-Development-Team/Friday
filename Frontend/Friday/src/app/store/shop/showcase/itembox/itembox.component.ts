import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Item } from 'src/app/models/models';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Observable } from 'rxjs';

@Component({
  selector: 'friday-itembox',
  templateUrl: './itembox.component.html',
  styleUrls: ['./itembox.component.scss']
})
export class ItemboxComponent implements OnInit {

  @Input() item: Item
  @Input() refresher: Observable<any>
  @Output() onAdd: EventEmitter<{ item: Item, amount: number }> = new EventEmitter()
  form: FormGroup

  constructor(builder: FormBuilder) {
    this.form = builder.group({
      amount: builder.control('1', Validators.pattern("[0-9]+"))
    })

  }

  ngOnInit() {
    this.refresher.subscribe(s => this.sanitizeForm())
  }

  getURL() {
    return `assets/${this.item.normalizedImageName}.jpg`
  }

  onAddToCart() {
    if (this.form.invalid) {
      this.sanitizeForm()
      return
    }
    var amount = this.form.get('amount').value
    this.onAdd.emit({ item: this.item, amount: amount })
  }

  sanitizeForm() {
    this.form.get('amount').setValue('1')
    this.form.updateValueAndValidity()
  }

}
