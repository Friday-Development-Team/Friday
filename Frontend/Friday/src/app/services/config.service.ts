import { Injectable } from '@angular/core';
import { Configuration } from '../models/models';
import { AdminService } from './admin.service';
import { Observable, interval } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ConfigService {

  private _config: Configuration=new Configuration(false, false)
  private timer: Observable<any>

  constructor(private service: AdminService) {
    this.getConfig()
    this.startTimer()
    this.timer.subscribe(s => this.getConfig())
  }


  get config() {
    return this._config
  }

  private getConfig() {
    this.service.getConfig().subscribe(s => this._config = s)
  }

  private startTimer() {
    this.timer = interval(30000)//emit every 5 minutes
  }

}
