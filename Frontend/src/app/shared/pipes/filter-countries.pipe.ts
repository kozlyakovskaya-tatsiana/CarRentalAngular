import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
  name: 'filterArray'
})
export class FilterArrayPipe implements PipeTransform {

  transform(value: string[], startWith: string): string[] {
    if (!startWith) {
      return new Array<string>();
    }
    return value.filter(val => val.toLowerCase().startsWith(startWith.toLowerCase()));
  }
}
