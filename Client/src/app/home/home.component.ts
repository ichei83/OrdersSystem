import { Component, OnInit } from '@angular/core';

import { User } from '@app/_models';
import { AccountService } from '@app/_services';

@Component({ templateUrl: 'home.component.html' })
export class HomeComponent implements OnInit  {
    user: any | null;

    constructor(private accountService: AccountService) {
        var stringifyUser = JSON.stringify(this.accountService.userValue);
        var parsedUser = JSON.parse(stringifyUser);  
        if(parsedUser){
            this.user = parsedUser.user; // this.accountService.userValue;
            console.log("Home user: " + this.user);   
        }     

    }
    ngOnInit() {
        var stringifyUser = JSON.stringify(this.accountService.userValue);
        var parsedUser = JSON.parse(stringifyUser);  
        if(parsedUser){
            this.user = parsedUser.user; // this.accountService.userValue;
            console.log("Home user: " + this.user);   
        }    
      }
}