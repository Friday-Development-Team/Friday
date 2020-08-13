import { Component } from '@angular/core';
import { Item, ItemDTO } from 'src/app/models/models';
import { AdminService } from 'src/app/services/admin.service';
import { FormGroup, Validators, FormBuilder } from '@angular/forms';
import { DataService } from 'src/app/services/data.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';

@Component({
  selector: 'friday-additem',
  templateUrl: './additem.component.html',
  styleUrls: ['./additem.component.scss']
})
export class AdditemComponent {

  item: Item
  form: FormGroup

  images: string[]

  items: string[]

  hasSubmitted: boolean = false
  check: boolean = false
  success: boolean = false

  constructor(private admin: AdminService, private fb: FormBuilder, private data: DataService, private modalService: NgbModal) {
    let decimal = '[0-9]+(\.|,?[0-9]+)?'
    let nodecimal = '[1-9]?[0-9]+'

    this.form = fb.group({
      name: fb.control('', Validators.required),
      price: fb.control('', [Validators.required, Validators.pattern(decimal)]),
      type: fb.control('Beverage', Validators.required),
      count: fb.control('', [Validators.required, Validators.pattern(nodecimal)]),
      image: fb.control('test', [Validators.required, Validators.pattern('[a-z0-9]+(_[a-z0-9]+)*')]),
      size: fb.control('', Validators.required),
      calories: fb.control('', [Validators.required, Validators.pattern(nodecimal)]),
      sugarcontent: fb.control('', [Validators.required, Validators.pattern(decimal)]),
      saltcontent: fb.control('', [Validators.required, Validators.pattern(decimal)]),
      allergens: fb.control('')
    })

    

  }

  open(content) {
    this.modalService.open(content, { ariaLabelledBy: 'modal-basic-title' })
  }

  submit() {
    this.hasSubmitted = true

    console.log(this.form)
    if (this.form.invalid) {
      console.log('invalid')
      console.log(this.form.errors)
      this.check = true
      return
    }

    const item = new ItemDTO(
      this.form.get('name').value,
      this.form.get('price').value,
      this.form.get('type').value,
      this.form.get('count').value,
      this.form.get('image').value,
      this.form.get('size').value,
      this.form.get('calories').value,
      this.form.get('sugarcontent').value,
      this.form.get('saltcontent').value,
      this.form.get('allergens').value)

    console.log(item)
  }




}
