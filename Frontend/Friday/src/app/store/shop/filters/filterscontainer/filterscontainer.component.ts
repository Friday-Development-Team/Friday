import { Component, OnInit } from '@angular/core';
import { FilterService } from 'src/app/services/filter.service';
import { PriceFilter } from 'src/app/models/pricefilter';

@Component({
  selector: 'friday-filterscontainer',
  templateUrl: './filterscontainer.component.html',
  styleUrls: ['./filterscontainer.component.scss']
})
export class FilterscontainerComponent implements OnInit {

  private priceFilters: PriceFilterDisplay[] = [new PriceFilterDisplay('none', 0, true), new PriceFilterDisplay('<=', 1.5), new PriceFilterDisplay('>', 1.5)]

  constructor(private service: FilterService) { }

  ngOnInit() {
  }

  changePriceFilter(filter: { type: string, amount: number }) {
    this.service.changePriceFilter(new PriceFilter(filter.type, filter.amount))
  }



}

export class PriceFilterDisplay {
  public text: string
  constructor(public type: string, public amount: number, public checked: boolean = false) {
    this.text = this.getText()
  }

  private getText(): string {
    if (this.type.toLowerCase() === 'none')
      return "No filter"
    var dir: boolean = this.type.includes("<")
    var including: boolean = this.type.includes("=")
    return `${(dir ? 'Greater' : 'Smaller')} than ${(including ? 'or equal to:' : '')} ${this.amount}`
  }
}
