import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { debounceTime, Subject, switchMap } from 'rxjs';
import { ApiService } from '../../services/api.service';
import { LeaveBalance, LeaveRequest, LeaveType } from '../../models/leave.models';

@Component({ selector:'app-dashboard', standalone:true, imports:[CommonModule,FormsModule], templateUrl:'./dashboard.component.html' })
export class DashboardComponent implements OnInit {
  requests: LeaveRequest[]=[]; types: LeaveType[]=[]; balances: LeaveBalance[]=[];
  filters:any={ status:'', leaveTypeId:'', fromDate:'', toDate:''}; sortField:keyof LeaveRequest='createdAt'; sortDir:1|-1=-1; private filter$=new Subject<void>();
  constructor(private api:ApiService){}
  ngOnInit(){ this.api.getLeaveTypes().subscribe(x=>this.types=x); this.api.getBalances().subscribe(x=>this.balances=x); this.filter$.pipe(debounceTime(300),switchMap(()=>this.api.getRequests(this.filters))).subscribe(x=>{this.requests=x; this.applySort();}); this.reload(); }
  reload(){ this.filter$.next(); }
  totalRemaining(){ return this.balances.reduce((s,b)=>s+b.remainingDays,0); }
  sort(field:keyof LeaveRequest){ this.sortDir=this.sortField===field ? (this.sortDir===1?-1:1) : 1; this.sortField=field; this.applySort(); }
  applySort(){ this.requests=[...this.requests].sort((a:any,b:any)=> a[this.sortField] > b[this.sortField] ? this.sortDir : a[this.sortField] < b[this.sortField] ? -this.sortDir : 0); }
  export(){ this.api.exportCsv(); }
}
