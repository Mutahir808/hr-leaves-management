export type LeaveStatus = 'Pending' | 'Approved' | 'Rejected' | 'Cancelled';
export interface LeaveType { id:number; name:string; defaultDays:number; isAccrued:boolean; accrualRatePerMonth:number; }
export interface LeaveBalance { id:number; employeeId:number; leaveTypeId:number; leaveTypeName:string; totalDays:number; usedDays:number; remainingDays:number; }
export interface LeaveRequest { id:number; employeeId:number; employeeName:string; leaveTypeId:number; leaveTypeName:string; startDate:string; endDate:string; daysRequested:number; reason:string; status:LeaveStatus; rejectionComment?:string; createdAt:string; }
export interface LeaveSettlement { id:number; employeeId:number; employeeName:string; leaveTypeId:number; leaveTypeName:string; adjustmentDays:number; reason:string; createdAt:string; }
