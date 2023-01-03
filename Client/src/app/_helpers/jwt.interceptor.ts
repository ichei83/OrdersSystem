import { Injectable } from '@angular/core';
import { HttpRequest, HttpHandler, HttpEvent, HttpInterceptor } from '@angular/common/http';
import { Observable } from 'rxjs';

import { environment } from '@environments/environment';
import { AccountService } from '@app/_services';

@Injectable()
export class JwtInterceptor implements HttpInterceptor {
    constructor(private accountService: AccountService) { }

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        // add auth header with jwt if user is logged in and request is to the api url
        const user = this.accountService.userValue;
        var stringifyUser = JSON.stringify(this.accountService.userValue);
        var parsedUser = JSON.parse(stringifyUser);  
        if(parsedUser){
            const isLoggedIn = user;// && user.token;
            const isApiUrl = request.url.startsWith(environment.apiUrl);
            if (isLoggedIn && isApiUrl) {
                request = request.clone({
                    setHeaders: {
                        Authorization: parsedUser.user.username
                        //`Bearer fake-jwt-token`
                        // `Bearer ${user.token}`
                    }
                });
            }
        }


        return next.handle(request);
    }
}