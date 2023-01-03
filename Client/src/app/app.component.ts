import { Component, OnInit  } from '@angular/core';

import { AccountService } from './_services';
import { User } from './_models';

@Component({ selector: 'app-root', templateUrl: 'app.component.html' })
export class AppComponent implements OnInit {
    user?: User | null;
    isAdmin:any = false;
    constructor(private accountService: AccountService) {

            
        this.accountService.user.subscribe(x => this.user = x);
    }

    logout() {
        this.accountService.logout();
    }
    ngOnInit() {
        //var userLocalStorage = localStorage.getItem('user');
        if(this.accountService.userValue){

            var parsedUser = JSON.parse(JSON.stringify(this.accountService.userValue));    
            this.isAdmin = parsedUser.user.role == 'Admin'; 
        }
    }
}