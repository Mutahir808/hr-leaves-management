import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { LeaveBalance, LeaveRequest, LeaveSettlement, LeaveType } from '../models/leave.models';

@Injectable({ providedIn: 'root' })
export class ApiService {
  private baseUrl = 'https://localhost:5001/api';
  constructor(private http: HttpClient) {}

  getLeaveTypes() { return this.http.get<LeaveType[]>(`${this.baseUrl}/leave-types`); }
  createLeaveType(body: Partial<LeaveType>) { return this.http.post<LeaveType>(`${this.baseUrl}/leave-types`, body); }
  updateLeaveType(id: number, body: Partial<LeaveType>) { return this.http.put<LeaveType>(`${this.baseUrl}/leave-types/${id}`, body); }
  deleteLeaveType(id: number) { return this.http.delete(`${this.baseUrl}/leave-types/${id}`); }

  getBalances(employeeId = 1) { return this.http.get<LeaveBalance[]>(`${this.baseUrl}/leave-balances/employee/${employeeId}`); }

  getRequests(filters: any = {}) {
    let params = new HttpParams();
    Object.keys(filters).forEach(k => filters[k] && (params = params.set(k, filters[k])));
    return this.http.get<LeaveRequest[]>(`${this.baseUrl}/leave-requests`, { params });
  }
  getPendingRequests() { return this.http.get<LeaveRequest[]>(`${this.baseUrl}/leave-requests/pending`); }
  createRequest(body: any) { return this.http.post<LeaveRequest>(`${this.baseUrl}/leave-requests`, body); }
  approve(id: number) { return this.http.post<LeaveRequest>(`${this.baseUrl}/leave-requests/${id}/approve`, {}); }
  reject(id: number, rejectionComment?: string) { return this.http.post<LeaveRequest>(`${this.baseUrl}/leave-requests/${id}/reject`, { rejectionComment }); }
  bulkApprove(ids: number[]) { return this.http.post(`${this.baseUrl}/leave-requests/bulk-approve`, { requestIds: ids }); }
  bulkReject(ids: number[], rejectionComment?: string) { return this.http.post(`${this.baseUrl}/leave-requests/bulk-reject`, { requestIds: ids, rejectionComment }); }
  exportCsv() { window.open(`${this.baseUrl}/leave-requests/export`, '_blank'); }

  getSettlements() { return this.http.get<LeaveSettlement[]>(`${this.baseUrl}/leave-settlements`); }
  createSettlement(body: any) { return this.http.post<LeaveSettlement>(`${this.baseUrl}/leave-settlements`, body); }
}
