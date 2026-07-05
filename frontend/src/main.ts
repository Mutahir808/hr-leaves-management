import { bootstrapApplication } from '@angular/platform-browser';
import { provideAnimations } from '@angular/platform-browser/animations';
import { provideHttpClient } from '@angular/common/http';
import { provideRouter, Routes } from '@angular/router';
import { AppComponent } from './app/app.component';
import { DashboardComponent } from './app/components/dashboard/dashboard.component';
import { ApplyLeaveComponent } from './app/components/apply-leave/apply-leave.component';
import { ApprovalComponent } from './app/components/approval/approval.component';
import { LeaveTypesComponent } from './app/components/leave-types/leave-types.component';
import { BalancesComponent } from './app/components/balances/balances.component';
import { SettlementsComponent } from './app/components/settlements/settlements.component';

const routes: Routes = [
  { path: '', component: DashboardComponent },
  { path: 'apply', component: ApplyLeaveComponent },
  { path: 'approval', component: ApprovalComponent },
  { path: 'leave-types', component: LeaveTypesComponent },
  { path: 'balances', component: BalancesComponent },
  { path: 'settlements', component: SettlementsComponent }
];

bootstrapApplication(AppComponent, { providers: [provideRouter(routes), provideHttpClient(), provideAnimations()] });
