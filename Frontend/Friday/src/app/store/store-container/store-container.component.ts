import { Component, OnInit, AfterViewInit, ViewChild, TemplateRef, Renderer2, ElementRef } from '@angular/core';
import { UserService, ShopUser } from 'src/app/services/user.service';
import { RefService } from 'src/app/services/ref.service';
import { Observable, of } from 'rxjs';
import { AuthService } from 'src/app/services/auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'friday-store-container',
  templateUrl: './store-container.component.html',
  styleUrls: ['./store-container.component.scss']
})
export class StoreContainerComponent implements OnInit {


  currentPage: string
  currentUser: ShopUser
  userRoles: string[]

  hasRunningOrders: boolean = true
  canActivateTools: Observable<boolean>


  constructor(private userService: UserService, private refService: RefService, private ren: Renderer2, private auth: AuthService, private router: Router) {
    this.userService.user.subscribe(s => {
      this.currentUser = s
    })
    this.userService.startUserPolling()

    this.currentPage = this.refService.ref.getValue()

    this.refService.ref.subscribe(s => {
      this.currentPage = s
    })

    this.canActivateTools = this.auth.hasRole(['admin', 'catering'])

    this.auth.getRoles().subscribe(s => this.userRoles = s.map(t => t.toLowerCase()))
  }

  hasRole(role: string): boolean {
    return !!this.userRoles && !!this.userRoles.find(s => s === role.toLowerCase())
  }

  ngOnInit() {
    this.router.navigate([`/store/${this.currentPage}`])
  }

  logout() {
    this.auth.logout()
    this.router.navigate(['auth'])
  }

}
