import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { CateringOrder } from '../models/models';
import { environment } from 'src/environments/environment';
import { map, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class CateringService {

  constructor(private http: HttpClient) { }

  getCateringOrders(isKitchen: boolean): Observable<CateringOrder[]> {
    return this.http.get<CateringOrder[]>(`${environment.apiUrl}/order/catering`,
      {
        params: { isKitchen: `${isKitchen}` }
      })
  }

  /**
   * Sets the given Order as accepted. Already subscribes to it, returns success or not.
   * @param id ID of the Order
   * @param toKitchen Whether it should be send to the kitchen (food only, depends on config)
   * @param isAccept Accept or unaccept it.
   */
  setAccepted(id: number, toKitchen: boolean, isAccept: boolean) {
    this.http.put(`${environment.apiUrl}/Order/accept/${id}/${toKitchen}`, isAccept).pipe(
      map(s => true),
      catchError(s => map(t => false))

    ).subscribe(s => { return s })
  }
}