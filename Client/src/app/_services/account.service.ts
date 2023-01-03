import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { BehaviorSubject, Observable } from 'rxjs';
import { map } from 'rxjs/operators';

import { environment } from '@environments/environment';
import { User } from '@app/_models';
import { LoginModel } from '@app/_models/loginmodel';

@Injectable({ providedIn: 'root' })
export class AccountService {
    private userSubject: BehaviorSubject<User | null>;
    public user: Observable<User | null>;
    isAdmin: any = false;
    selectedUserForFiltering: any;

    constructor(
        private router: Router,
        private http: HttpClient
    ) {
        this.userSubject = new BehaviorSubject(JSON.parse(localStorage.getItem('user')!));
        this.user = this.userSubject.asObservable();
    }

    public get userValue() {
        return this.userSubject.value;
    }

    login(username: string, password: string) {
        var obj = {
            "username": username,
            "password": password
          }

        return this.http.post<User>(`${environment.apiUrl}/user/login`, obj)
            .pipe(map(user => {
                // store user details in local storage to keep user logged in between page refreshes
                var stringifyUser = JSON.stringify(user);
                localStorage.setItem('user', stringifyUser);
                var parsedUser = JSON.parse(stringifyUser);    
                this.selectedUserForFiltering = parsedUser.user;
                this.isAdmin = parsedUser.user.role == 'Admin';                 
                this.userSubject.next(user);
                return user;
            }));
    }

    logout() {
        // remove user from local storage and set current user to null
        localStorage.removeItem('user');
        this.userSubject.next(null);
        this.router.navigate(['/account/login']);
    }

    getAll(role:any) {
        return this.http.get<User[]>(`${environment.apiUrl}/user/users/?role=${role}`);
        // .pipe(map(x => {
        //     console.log(x);
        //     return x.users;
        // }))
        // ;
    }
    //orders

    getOrders(id: any) {
        return this.http.get<any[]>(`${environment.apiUrl}/orders/?id=${this.selectedUserForFiltering.id}`);
    }

    // getById(id: string) {
    //     return this.http.get<User>(`${environment.apiUrl}/users/${id}`);
    // }

    

   GetOrdersByDate(_startDate:string, _endDate:string) {
    const startDate = new Date(_startDate);
    const endDate = new Date(_endDate);
    var sdString = startDate.getFullYear() + "-" + (startDate.getMonth()+1) + "-" + startDate.getDate();
    var edString = endDate.getFullYear() + "-" + (endDate.getMonth()+1) + "-" + endDate.getDate();
    return this.http.get<any[]>(`${environment.apiUrl}/GetOrdersByDate/` + this.selectedUserForFiltering.id + "/" + sdString + "/" + edString);
  }

}