import { Pipe, PipeTransform } from '@angular/core';
import { PriceFilter } from '../models/pricefilter';
import { Item } from '../models/models';

@Pipe({
  name: 'price'
})
export class PricefilterPipe implements PipeTransform {

  transform(value: Item[], args: PriceFilter): any {
    if (!value || !args)
      return []
    return value.filter(s => args.compareTo(s.price));
  }

}
