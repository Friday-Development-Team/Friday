import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpEvent, HttpHandler, HttpRequest } from '@angular/common/http';
import { AuthService } from '../services/auth.service';
import { Observable } from 'rxjs';

@Injectable()
export class AuthenticationInterceptor implements HttpInterceptor {
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        if (this.authService.token.length) {
           
            const clonedRequest = req.clone({
                headers: req.headers.set('Authorization', `Bearer ${this.authService.token}`
                    
                )
            });
            return next.handle(clonedRequest)
        }
        return next.handle(req)
    }
    constructor(private authService: AuthService) { }


}