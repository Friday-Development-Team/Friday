import { Pipe, PipeTransform } from '@angular/core';
import { Item } from '../models/models';

@Pipe({
  name: 'search'
})
export class SearchPipe implements PipeTransform {

  transform(value: Item[], args?: string): any {
    if (!args)
      return value
    return value.filter(s => s.name.toLowerCase().includes(args.toLowerCase()))
  }

}
