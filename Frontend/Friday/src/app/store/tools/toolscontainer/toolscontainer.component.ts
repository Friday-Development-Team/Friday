import { Component, OnInit } from '@angular/core';
import { RefService } from 'src/app/services/ref.service';
import { UserService } from 'src/app/services/user.service';
import { AuthService } from 'src/app/services/auth.service';
import { BehaviorSubject } from 'rxjs';
import { Router } from '@angular/router';

@Component({
  selector: 'friday-toolscontainer',
  templateUrl: './toolscontainer.component.html',
  styleUrls: ['./toolscontainer.component.scss']
})
export class ToolscontainerComponent implements OnInit {

  private readonly ref: string = 'tools'

  isAdmin: boolean | undefined = false
  isCatering: boolean | undefined = false

  constructor(private refService: RefService, private auth: AuthService, private router: Router) {
    this.refService.sendRef(this.ref)

    this.auth.hasRole(['admin']).subscribe(s => {
      this.isAdmin = s
      if (s)
        this.router.navigate(['/store/tools/admin'])
    })

    this.auth.hasRole(['catering']).subscribe(s => {
      this.isCatering = s
      if (s && !this.isAdmin)//Only if catering but not an admin
        this.router.navigate(['/store/tools/catering'])
    })

  }

  // loadPage() {
  //   if (!this.hasFinishedLoading)
  //     return
  //   if (!this.isAdmin && !this.isCatering)
  //     this.router.navigate(['store'])
  //   if (this.isAdmin)
  //     this.router.navigate(['store/tools/admin'])
  //   if (this.isCatering)
  //     this.router.navigate(['store/tools/catering'])
  // }

  ngOnInit() {

  }

}
