import { Pipe, PipeTransform } from '@angular/core';
import { Item } from '../models/models';

@Pipe({
  name: 'order'
})
export class OrderPipe implements PipeTransform {

  transform(value: Item[], args: string): any {
    if (!args)
      return value
    if (!this)
      return

    var temp = value
    switch (args) {
      case "Name":
        //return value.sort((a, b) => (a.name > b.name) ? 1 : ((b.name > a.name) ? -1 : 0))
        temp.sort((a, b) => sortByName(a, b, false))
        break
      case "Price: low to high":
        //return value.sort(s => s.price)
        //temp.sort(function (a, b) { return +a - +b; })
        temp.sort(function (a, b) {
          return a.price > b.price ? 1 : b.price > a.price ? -1 : sortByName(a, b, false);
        })
        break
      case "Price: high to low":
        //return value.sort(s => s.price).reverse()
        temp.sort(function (a, b) {
          return a.price > b.price ? -1 : b.price > a.price ? 1 : sortByName(a, b, true);
        }).reverse()
        break
      case "Calories: low to high":
        //return value.sort(s => s.itemDetails.calories)
        temp.sort(function (a, b) {
          return a.itemDetails.calories > b.itemDetails.calories ? 1 : b.itemDetails.calories > a.itemDetails.calories ? -1 : sortByName(a, b, false)
        })
        break
      case "Calories: high to low":
        //return value.sort(s => s.itemDetails.calories).reverse()
        temp.sort(function (a, b) {
          return a.itemDetails.calories > b.itemDetails.calories ? 1 : b.itemDetails.calories > a.itemDetails.calories ? -1 : sortByName(a, b, true);
        }).reverse()
        break
      case "Type":
        //return value.sort((a, b) => (a.type > b.type) ? 1 : ((b.type > a.type) ? -1 : 0))
        temp.sort((a, b) => (a.type > b.type) ? 1 : ((b.type > a.type) ? -1 : sortByName(a, b, false)))
        break
      default:
        break
      //return value;
    }
    return temp
  }



}
/**
 * Sorts by name
 * @param a Item 1
 * @param b Item 2
 * @param isReverse Reverses the order if needed. Is used when comparing by other property first but alphabetical order has to be the same regardless of the other order type
 */
function sortByName(a, b, isReverse: boolean) {
  return (a.name > b.name) ? (isReverse ? -1 : 1) : ((b.name > a.name) ? (isReverse ? 1 : -1) : 0)
}
