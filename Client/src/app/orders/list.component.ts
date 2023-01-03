import { Component, OnInit, ViewChild  } from '@angular/core';
import { first } from 'rxjs/operators';
import { DatatableComponent } from '@swimlane/ngx-datatable/lib/components/datatable.component';


import { AccountService } from '@app/_services';
import { User } from '@app/_models';

@Component({ templateUrl: 'list.component.html' })
export class ListComponent implements OnInit {
    orders?: any[];
    columns = [{ prop: 'id' },{prop: 'user'}, { prop: 'date' },{ prop: 'status' }, { name: 'agent' }, { name: 'customer' }, { name: 'customersite' }, { prop: 'details' }];
   // @ViewChild(DatatableComponent) table: DatatableComponent;
   startDate: any;
   endDate: any;
    constructor(private accountService: AccountService) {
     var stringifyUser = JSON.stringify(this.accountService.userValue);
     var parsedUser = JSON.parse(stringifyUser); 
     if(parsedUser && this.accountService.isAdmin==false){
       this.accountService.selectedUserForFiltering = parsedUser.user;
        //this.getOrders();
     }
    //  this.setInitialDates();
    }

    ngOnInit() {
      this.setInitialDates();
     var stringifyUser = JSON.stringify(this.accountService.userValue);
     var parsedUser = JSON.parse(stringifyUser); 
     if(parsedUser && this.accountService.isAdmin==false){
       this.accountService.selectedUserForFiltering = parsedUser.user;
        this.getOrders();
     }
    }

  private setInitialDates() {
    let date = new Date();
    this.startDate = new Date(date.getFullYear(), date.getMonth(), date.getDate());
    this.endDate = new Date(date.getFullYear(), date.getMonth(), date.getDate() + 9);
    this.accountService.GetOrdersByDate(this.startDate, this.endDate).subscribe((result) => {
      console.log('Data: ' + result);

    });
  }

    getOrders(){ 
        this.accountService.GetOrdersByDate(this.startDate , this.endDate)
        .pipe(first())
        .subscribe(orders => this.orders = orders);   
            // this.accountService.getOrders(this.accountService.selectedUserForFiltering.id)
            // .pipe(first())
            //  .subscribe(orders => this.orders = orders);      
    }

    onClick(){
      console.log(this.startDate);
      console.log(this.endDate);
      this.accountService.GetOrdersByDate(this.startDate , this.endDate)
      .pipe(first())
      .subscribe(orders => this.orders = orders);   
      // this.accountService.GetOrdersByDate(this.startDate , this.endDate).subscribe((result) => {
      //    console.log('Data: ' + result);    
      //  });
    }

}