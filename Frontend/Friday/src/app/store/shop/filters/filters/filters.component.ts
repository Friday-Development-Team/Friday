import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { PriceFilterDisplay } from '../filterscontainer/filterscontainer.component';


@Component({
  selector: 'friday-filters',
  templateUrl: './filters.component.html',
  styleUrls: ['./filters.component.scss']
})
export class FiltersComponent implements OnInit {

  @Input() priceFilters: PriceFilterDisplay[]
  @Output() price: EventEmitter<{ type: string, amount: number }> = new EventEmitter()

  constructor() { }

  ngOnInit() {
  }

  applyFilter(filterType: string, id: number) {
    this.changeChecked(filterType, id)
    switch (filterType.toLowerCase()) {
      case 'price':
        var filter: PriceFilterDisplay = this.priceFilters[id]
        this.price.emit({ type: filter.type, amount: filter.amount })
        break;
      default:
        break;
    }
  }

  private changeChecked(type: string, id: number) {
    switch (type.toLowerCase()) {
      case 'price':
        this.priceFilters.forEach(s => s.checked = false)
        this.priceFilters[id].checked = true;
    }
  }

}
