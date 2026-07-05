import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { ApiService } from '../../services/api.service';
import { LeaveBalance, LeaveType } from '../../models/leave.models';

@Component({ selector:'app-apply-leave', standalone:true, imports:[CommonModule,FormsModule], templateUrl:'./apply-leave.component.html' })
export class ApplyLeaveComponent implements OnInit {
  types:LeaveType[]=[]; balances:LeaveBalance[]=[]; message=''; error='';
  form:any = JSON.parse(localStorage.getItem('leaveDraft') || '{"employeeId":1,"leaveTypeId":"","startDate":"","endDate":"","reason":""}');
  constructor(private api:ApiService){}
  ngOnInit(){ this.api.getLeaveTypes().subscribe(x=>this.types=x); this.api.getBalances().subscribe(x=>this.balances=x); }
  saveDraft(){ localStorage.setItem('leaveDraft', JSON.stringify(this.form)); }
  selectedBalance(){ return this.balances.find(b=>b.leaveTypeId == this.form.leaveTypeId); }
  submit(){ this.saveDraft(); this.message=''; this.error=''; this.api.createRequest({...this.form, leaveTypeId:+this.form.leaveTypeId}).subscribe({next:()=>{this.message='Leave request submitted successfully'; localStorage.removeItem('leaveDraft'); this.form={employeeId:1,leaveTypeId:'',startDate:'',endDate:'',reason:''};}, error:e=>this.error=e.error?.message || 'Unable to submit leave request'}); }
}
