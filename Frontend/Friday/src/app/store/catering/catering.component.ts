import { Component } from '@angular/core';
import { ConfigService } from 'src/app/services/config.service';
import { CateringOrder } from 'src/app/models/models';
import { DataService } from 'src/app/services/data.service';
import { CateringService } from 'src/app/services/catering.service';
import { RefService } from 'src/app/services/ref.service';

@Component({
  selector: 'friday-catering',
  templateUrl: './catering.component.html',
  styleUrls: ['./catering.component.scss']
})
export class CateringComponent {

  private readonly ref: string = 'catering'

  combined: boolean = false

  orders: CateringOrder[] = []

  ordersActive: { order: CateringOrder, active: boolean }[]

  constructor(private config: ConfigService, private refS: RefService, private catering: CateringService) {
    this.refS.sendRef(this.ref)//Send reference

    this.combined = this.config.config.combinedCateringKitchen
    this.catering.getCateringOrders(false).subscribe(s => {
      console.log(s)
      this.orders = s
      this.ordersActive = s.map(t => { return { order: t, active: this.isActive(t) } })
      console.log(this.ordersActive)
    })
  }

  setActive(i: number) {
    let order = this.orders[i]

    if (order.statusBeverage !== "None")
      this.orders[i].statusBeverage = "Accepted"

    if (order.statusFood !== "None")
      this.orders[i].statusFood = "Accepted"

    this.ordersActive[i].active = true
    console.log(this.ordersActive)

    this.catering.setAccepted(order.id, false, true)
  }

  setNotActive(i) {
    let order = this.orders[i]

    if (order.statusBeverage !== "None")
      this.orders[i].statusBeverage = "Pending"

    if (order.statusFood !== "None")
      this.orders[i].statusFood = "Pending"
    this.ordersActive[i].active = false

    this.catering.setAccepted(order.id, false, false)
  }

  isActive(order: CateringOrder): boolean {
    const bool = order.statusBeverage === 'Accepted' || order.statusBeverage === 'Accepted' || order.statusFood === 'SentToKitchen'
    return bool
  }

  hasFood(i: number) {
    let order = this.orders[i]

    return order.statusFood !== "None"
  }

  sendToKitchen(i: number) {
    let order = this.orders[i]

    if (order.statusFood !== "None" && (order.statusFood === "Pending" || order.statusFood === "Accepted"))
      order.statusFood = "SentToKitchen"

    this.catering.setAccepted(order.id, true, true)
  }

  unSendToKitchen(i: number) {
    let order = this.orders[i]

    if (order.statusFood !== "None" && order.statusFood === "SentToKitchen")
      order.statusFood = order.statusBeverage//Pending if not accepted, else accepted but not sent to kitchen

    this.catering.setAccepted(order.id, true, false)
  }

}
