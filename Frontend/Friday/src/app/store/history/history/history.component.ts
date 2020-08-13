import { Component, OnInit } from '@angular/core';
import { RefService } from 'src/app/services/ref.service';
import { DataService } from 'src/app/services/data.service';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';
import { OrderHistory } from 'src/app/models/models';
import { RefreshService } from 'src/app/services/refresh.service';
import { Observable, of, empty } from 'rxjs';

@Component({
  selector: 'friday-history',
  templateUrl: './history.component.html',
  styleUrls: ['./history.component.scss']
})
export class HistoryComponent implements OnInit {

  private readonly ref: string = 'history'
  private name: string

  history: OrderHistory


  constructor(private refS: RefService, private data: DataService, private auth: AuthService, private router: Router, private refresh: RefreshService) {
    this.loadHistory()
    this.refresh.refresh.subscribe(s => this.loadHistory())

    this.refS.sendRef(this.ref)
  }

  ngOnInit() {
    this.loadHistory()
  }

  loadHistory() {
    this.name = this.auth.user$.getValue()//Get name current user
    if (!this.name)//You're not logged in
      this.router.navigate(['/auth'])//Go log in
    this.data.getHistory().subscribe(s => this.history = s)
    //Else get history
  }

  showHistory() {
    console.log(this.history)
  }



}
