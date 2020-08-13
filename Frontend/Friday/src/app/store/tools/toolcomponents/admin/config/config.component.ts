import { Component, OnInit } from '@angular/core';
import { Configuration } from 'src/app/models/models';
import { AdminService } from 'src/app/services/admin.service';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { timer } from 'rxjs';

@Component({
  selector: 'friday-config',
  templateUrl: './config.component.html',
  styleUrls: ['./config.component.scss']
})
export class ConfigComponent implements OnInit {

  config: Configuration = new Configuration(false, false)
  form: FormGroup

  success: boolean = false
  submitted: boolean = false

  check: boolean = false

  constructor(private admin: AdminService, fb: FormBuilder) {
    this.admin.getConfig().subscribe(s => this.config = s)
    this.form = fb.group({
      cancel: fb.control('', Validators.required),
      combined: fb.control('', Validators.required)
    })

    this.form.get('cancel').valueChanges.subscribe(s => this.config.cancelOnAccepted = s)
    this.form.get('combined').valueChanges.subscribe(s => this.config.combinedCateringKitchen = s)
  }

  ngOnInit() {
  }

  submit() {
    this.submitted = true
    this.admin.setConfig(this.config).subscribe(s => {
      this.check = true
      this.success = true
    }
    )

    timer(5000).subscribe(s => {
      this.submitted = false
      this.check = false
    })
  }

}
