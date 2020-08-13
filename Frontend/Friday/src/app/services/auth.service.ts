import { Injectable } from '@angular/core'
import { BehaviorSubject, Observable, of } from 'rxjs'
import { HttpClient } from '@angular/common/http'
import { environment } from 'src/environments/environment'
import { map, catchError } from 'rxjs/operators'


@Injectable({
  providedIn: 'root'
})
export class AuthService {

  private readonly tokenKey = 'currentUser'
  private user: BehaviorSubject<string>

  private roles: string[]

  public redirectUrl: string

  constructor(private http: HttpClient) {
    let parsedToken = parseJwt(localStorage.getItem(this.tokenKey))
    if (parsedToken) {
      const expires = new Date(parseInt(parsedToken.exp, 10) * 1000) < new Date()
      if (expires) {
        localStorage.removeItem(this.tokenKey)
        parsedToken = null
      }
    }
    this.user = new BehaviorSubject<string>(parsedToken && parsedToken.unique_name)
  }

  get user$(): BehaviorSubject<string> {
    return this.user
  }

  get token(): string {
    const localToken = localStorage.getItem(this.tokenKey)
    return !!localToken ? localToken : ''
  }

  login(username: string, password: string): Observable<boolean> {
    return this.http.post(
      `${environment.apiUrl}/user/login`,
      { username, password },
      { responseType: 'text' }
    ).pipe(
      map((token: string) => {
        if (token) {
          localStorage.setItem(this.tokenKey, token.replace(/^"(.*)"$/, '$1'))
          this.user.next(username)
          return true
        } else {
          return false
        }
      })
    )
  }

  register(
    name: string, password: string
  ): Observable<boolean> {
    return this.http
      .post(`${environment.apiUrl}/user/register`, { name, password })
      .pipe(catchError(s => null),
        map((token: any) => {
          return !!token
        })
      )
  }

  logout() {
    if (this.user.getValue()) {
      localStorage.removeItem('currentUser')
      this.user.next(null)
    }
    this.roles = null
  }

  checkUserNameAvailability = (name: string): Observable<boolean> => {
    return this.http.get<boolean>(
      `${environment.apiUrl}/user/checkusername`,
      {
        params: { name }
      }
    )
  }
  /**
   * Checks if the user has a certain role or at least one of the required roels (only used for UI, always in conjunction with backend auth)
   * @param role Roles to be checked
   */
  hasRole(role: string[]): Observable<boolean> {
    let response =
      ((!!this.roles)
        ? of(this.roles)
        : this.getRoles())

    return response.pipe(map(s => {
      this.roles = s
      return s.map(t => t.toLowerCase())
    }), map(t => <boolean>
      (role.length === 1
        ? t.includes(role[0].toLowerCase())//if only 1 role
        : role.some(u => t.includes(u)))
    ))
  }

  getRoles(): Observable<string[]> {
    return this.http.get<string[]>(`${environment.apiUrl}/user/roles`)
  }

}


function parseJwt(token) {
  if (!token) {
    return null
  }
  const base64Token = token.split('.')[1]
  const base64 = base64Token.replace(/-/g, '+').replace(/_/g, '/')
  return JSON.parse(window.atob(base64))
}

