import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subject, Observable, timer, BehaviorSubject, interval } from 'rxjs';
import { environment } from 'src/environments/environment';
import { switchMap } from 'rxjs/operators';
import { DataService } from './data.service';
import { RefreshService } from './refresh.service';
import { CateringOrder } from '../models/models';

@Injectable({
  providedIn: 'root'
})
export class UserService {


  userclock: Observable<any> = interval(300000)
  orderclock: Observable<any> = interval(30000)

  user: Subject<ShopUser> = new BehaviorSubject(new ShopUser('Loading ....', 0))
  running: Subject<CateringOrder[]> = new Subject()

  constructor(private http: HttpClient, private refresh: RefreshService, private data: DataService) {
    //Gets the first user information when this service is loaded in. Polling is started on component side to allow component to load
    this.getUserInformation().subscribe(s => this.user.next(s))
    //Triggers an instant refresh of information
    this.refresh.refresh.subscribe(s => this.getUserInformation().subscribe(t => this.user.next(t)))
  }

  startUserPolling() {
    if ((!this.userclock))
      this.userclock = interval(300000)
    this.userclock.pipe(switchMap(() => this.getUserInformation())).subscribe(s => {
      this.user.next(s)
    }
    )
  }
  stopUserPolling() {
    this.userclock = null
  }

  startOrderPolling() {
    if (!this.orderclock)
      this.orderclock = interval(30000)
    this.userclock.pipe(switchMap(() => this.data.getRunning())).subscribe(s => {
      this.running.next(s)
    })
  }

  stopOrderPolling() {
    this.orderclock = null
  }

  getUserInformation(): Observable<ShopUser> {
    return this.http.get<ShopUser>(`${environment.apiUrl}/user`)
  }

  updateBalance(name: string, amount: any) {
    this.refresh.trigger(3000)
    return this.http.put(`${environment.apiUrl}/user/updatebalance`, new BalanceUpdateDTO(name, amount))
  }




}

export class ShopUser {
  constructor(public name: string, public balance: number) { }
}

export class BalanceUpdateDTO{
  constructor(public name: string, public amount: number){}
}
