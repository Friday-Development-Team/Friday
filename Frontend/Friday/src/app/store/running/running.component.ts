import { Component, OnInit } from '@angular/core';
import { RefService } from 'src/app/services/ref.service';
import { DataService } from 'src/app/services/data.service';
import { CateringOrder } from 'src/app/models/models';
import { UserService } from 'src/app/services/user.service';

@Component({
  selector: 'friday-running',
  templateUrl: './running.component.html',
  styleUrls: ['./running.component.scss']
})
export class RunningComponent implements OnInit {

  private readonly ref: string = 'running'

  orders: CateringOrder[] = []

  constructor(private refService: RefService, private data: DataService, private user: UserService) {
    this.refService.sendRef(this.ref)

    this.data.getRunning().subscribe(s => {
      this.orders = s
    })

    this.user.startOrderPolling()

    this.user.running.subscribe(s => {

      this.orders = s
      if (!!s)
        this.user.stopOrderPolling()//If no more running orders, stop polling
    })

  }

  ngOnInit() {
  }

}
