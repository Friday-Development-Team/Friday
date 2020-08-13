import { Injectable } from '@angular/core';
import { Subject, timer } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class RefreshService {

  refresh: Subject<any> = new Subject()
  constructor() { }

  /**
   * Used to trigger a refresh. Services and components that need a refresh should subscribe to the Subject and refresh on next.
   * @param delay Delay before the refresh is triggered (makes sure to leave time for http requests to finish)
   */
  trigger(delay: number) {
    timer(delay).subscribe(s => this.refresh.next())
  }
}
