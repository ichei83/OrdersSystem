import { Component, OnInit } from '@angular/core';
import { first } from 'rxjs/operators';
import {SelectionType} from '@swimlane/ngx-datatable';
import { AccountService } from '@app/_services';
import { User } from '@app/_models';
import { Router, ActivatedRoute } from '@angular/router';

@Component({ 
    templateUrl: 'list.component.html'
 }
)
export class ListComponent implements OnInit {
    users?: any[];
    // { checkboxable: true, headerCheckboxable: true },
    columns = [    
        { prop: 'firstName' }, { name: 'lastName' }, { name: 'username', sortable: true }, { name: 'role' }];
    index:any;
    isAdmin: any = false;
    type = SelectionType.single;

    constructor(        private route: ActivatedRoute,
        private router: Router,private accountService: AccountService) {
        
    }

    ngOnInit() {
        this.index = 0;
        this.isAdmin = this.accountService.isAdmin;
        var filter:any = this.getSelectedRoleAccordingToTabIndex();
        this.accountService.getAll(filter)
           .pipe(first())
            .subscribe(users => this.users = users);
    }

    private getSelectedRoleAccordingToTabIndex(): any {
        return this.index == 0 ? 'Admin' : 'User';
    }

    async select(event:any) {
        if(event != null && event.selected != null && event.selected[0] != null){
            console.log(event.selected[0].role);
            console.log(event.selected[0].username);
            var user = event.selected[0];
            const returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';
            console.log(returnUrl);
            this.accountService.selectedUserForFiltering = user;
            await this.router.navigateByUrl(returnUrl + "orders");
            // var stringifyUser = JSON.stringify(this.accountService.userValue);
            // var parsedUser = JSON.parse(stringifyUser); 
            // if(parsedUser){
            //     this.accountService.selectedUserForFiltering = parsedUser.user;
            // }
        }
      }

    async getUsers(){
        var filter:any = this.getSelectedRoleAccordingToTabIndex();
        this.accountService.getAll(filter)
        .pipe(first())

        .subscribe(users => this.users = users);
    }

    async topTabChange(event:any)//role tab change
    {   
        await this.getUsers();
    }
}