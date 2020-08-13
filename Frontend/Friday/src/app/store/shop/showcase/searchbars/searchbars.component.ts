import { Component, OnInit } from '@angular/core';
import { FilterService } from 'src/app/services/filter.service';

@Component({
  selector: 'friday-searchbars',
  templateUrl: './searchbars.component.html',
  styleUrls: ['./searchbars.component.scss']
})
export class SearchbarsComponent implements OnInit {

  orderFilters: string[] = [
    "Price: low to high", "Price: high to low", "Name", "Calories: low to high", "Calories: high to low", "Type"
  ]

  constructor(private filter: FilterService) { }

  ngOnInit() {
  }

  searchName(event: any) {
    console.log(event.target.value)
    this.filter.searchName(event.target.value)
  }

  changeOrder(event: any) {
    console.log(event.target.value)
    this.filter.changeOrder(event.target.value)
  }

}
