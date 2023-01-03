import { NgModule } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

import { UsersRoutingModule } from './users-routing.module';
import { LayoutComponent } from './layout.component';
import { ListComponent } from './list.component';
import { NgxDatatableModule, SelectionType } from '@swimlane/ngx-datatable';

import { MatTabsModule } from '@angular/material/tabs';

@NgModule({
    imports: [
        CommonModule,
        ReactiveFormsModule,
        UsersRoutingModule,
        NgxDatatableModule,
        MatTabsModule
    ],
    declarations: [
        LayoutComponent,
        ListComponent
        
    ]
})
export class UsersModule { }