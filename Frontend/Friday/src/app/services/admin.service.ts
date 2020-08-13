import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { ShopUser } from './user.service';
import { environment } from 'src/environments/environment';
import { Log, ItemAmount, Configuration } from '../models/models';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class AdminService {

  constructor(private http: HttpClient) { }

  getAllUsers(): Observable<ShopUser[]> {
    return this.http.get<ShopUser[]>(`${environment.apiUrl}/user/all`)
  }

  getLogs(route: string, param?: any): Observable<Log[]> {
    let params = new HttpParams().set('param', param)
    return this.http.get<Log[]>(`${environment.apiUrl}/logs/${route}`, { params })
      .pipe(
        map(s => s.map(t => {
          let temp = t
          temp.type = temp.type.toLowerCase()
          return temp
        })))
  }

  getItemAmounts(isSoldOnly: boolean): Observable<ItemAmount[]> {
    let route = 'logs/stock/'.concat(isSoldOnly ? 'sold' : 'remaining')
    return this.http.get<ItemAmount[]>(`${environment.apiUrl}/${route}`)
  }

  getTotalIncome(): Observable<number> {
    return this.http.get<number>(`${environment.apiUrl}/logs/total`)
  }

  getConfig(): Observable<Configuration> {
    return this.http.get<Configuration>(`${environment.apiUrl}/configuration`)
  }

  setConfig(config: Configuration): Observable<any> {
    return this.http.put(`${environment.apiUrl}/configuration`, config)
    
    //subscribe to use http call, response not needed
  }
}
