import { Injectable } from '@angular/core';

import { Observable, of, Subject, timer } from 'rxjs';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from 'src/environments/environment';
import { OrderDTO } from './cart.service';
import { RefreshService } from './refresh.service';
import { Item, OrderHistory, CateringOrder } from '../models/models';
import { map } from 'rxjs/operators';


@Injectable({
  providedIn: 'root'
})
export class DataService {

  constructor(private http: HttpClient, private refresh: RefreshService) { }

  getAllItems(): Observable<Item[]> {
    return this.http.get<Item[]>(`${environment.apiUrl}/item/`)
  }

  placeOrder(dto: OrderDTO): Observable<any> {
    const headers = new HttpHeaders().set('content-type', 'application/json')
    this.refresh.trigger(5000)
    return this.http.post(`${environment.apiUrl}/order/`, dto, { headers })
  }

  getHistory() {
    return this.http.get(`${environment.apiUrl}/order/history`).pipe(map(s => {
      let temp = new OrderHistory()
      temp.fromJson(s)
      return temp
    }))
  }

  getRunning() {
    return this.http.get<CateringOrder[]>(`${environment.apiUrl}/order/running`)
  }

  

}
