import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../../services/api.service';
import { LeaveRequest } from '../../models/leave.models';

@Component({ selector:'app-approval', standalone:true, imports:[CommonModule,FormsModule], templateUrl:'./approval.component.html' })
export class ApprovalComponent implements OnInit {
  requests:LeaveRequest[]=[]; selected:number[]=[]; comment=''; message='';
  constructor(private api:ApiService){}
  ngOnInit(){ this.load(); }
  load(){ this.api.getPendingRequests().subscribe(x=>this.requests=x); }
  toggle(id:number, checked:boolean){ this.selected = checked ? [...this.selected,id] : this.selected.filter(x=>x!==id); }
  approve(id:number){ this.api.approve(id).subscribe(()=>{this.message='Request approved'; this.load();}); }
  reject(id:number){ this.api.reject(id,this.comment).subscribe(()=>{this.message='Request rejected'; this.load();}); }
  bulkApprove(){ this.api.bulkApprove(this.selected).subscribe(()=>{this.message='Selected requests approved'; this.selected=[]; this.load();}); }
  bulkReject(){ this.api.bulkReject(this.selected,this.comment).subscribe(()=>{this.message='Selected requests rejected'; this.selected=[]; this.load();}); }
}
