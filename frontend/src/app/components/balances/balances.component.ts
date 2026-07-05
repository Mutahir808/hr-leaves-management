import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApiService } from '../../services/api.service';
import { LeaveBalance } from '../../models/leave.models';

@Component({ selector:'app-balances', standalone:true, imports:[CommonModule], template:`<div class="page"><h2>Leave Balance Widget</h2><div class="grid"><div class="card" *ngFor="let b of balances"><h3>{{b.leaveTypeName}}</h3><p>{{b.remainingDays}} days remaining</p><progress [value]="b.remainingDays" [max]="b.totalDays"></progress><p class="muted">Used {{b.usedDays}} of {{b.totalDays}}</p></div></div></div>` })
export class BalancesComponent implements OnInit { balances:LeaveBalance[]=[]; constructor(private api:ApiService){} ngOnInit(){this.api.getBalances().subscribe(x=>this.balances=x);} }
