import { Injectable } from '@angular/core';
import { Subject, BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RefService {

  ref: BehaviorSubject<string> = new BehaviorSubject('shop')//Sends shop

  constructor() { }

  sendRef(ref: string) {
    this.ref.next(ref)
  }
}
