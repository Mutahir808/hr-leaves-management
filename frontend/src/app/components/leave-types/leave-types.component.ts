import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../../services/api.service';
import { LeaveType } from '../../models/leave.models';

@Component({ selector:'app-leave-types', standalone:true, imports:[CommonModule,FormsModule], templateUrl:'./leave-types.component.html' })
export class LeaveTypesComponent implements OnInit {
  types:LeaveType[]=[]; editing:LeaveType|null=null; form:any={name:'',defaultDays:0,isAccrued:false,accrualRatePerMonth:0}; message='';
  constructor(private api:ApiService){}
  ngOnInit(){ this.load(); }
  load(){ this.api.getLeaveTypes().subscribe(x=>this.types=x); }
  edit(t:LeaveType){ this.editing=t; this.form={...t}; }
  reset(){ this.editing=null; this.form={name:'',defaultDays:0,isAccrued:false,accrualRatePerMonth:0}; }
  save(){ const req=this.editing?this.api.updateLeaveType(this.editing.id,this.form):this.api.createLeaveType(this.form); req.subscribe(()=>{this.message='Leave type saved'; this.reset(); this.load();}); }
  remove(id:number){ this.api.deleteLeaveType(id).subscribe(()=>this.load()); }
}
