import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../../services/api.service';
import { LeaveSettlement, LeaveType } from '../../models/leave.models';

@Component({ selector:'app-settlements', standalone:true, imports:[CommonModule,FormsModule], templateUrl:'./settlements.component.html' })
export class SettlementsComponent implements OnInit {
  types:LeaveType[]=[]; settlements:LeaveSettlement[]=[]; form:any={employeeId:1,leaveTypeId:'',adjustmentDays:0,reason:''}; message='';
  constructor(private api:ApiService){}
  ngOnInit(){ this.api.getLeaveTypes().subscribe(x=>this.types=x); this.load(); }
  load(){ this.api.getSettlements().subscribe(x=>this.settlements=x); }
  save(){ this.api.createSettlement({...this.form, leaveTypeId:+this.form.leaveTypeId}).subscribe(()=>{this.message='Settlement saved'; this.form={employeeId:1,leaveTypeId:'',adjustmentDays:0,reason:''}; this.load();}); }
}
