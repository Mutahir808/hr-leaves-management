import { Component } from '@angular/core';
import { RouterLink, RouterLinkActive, RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root', standalone: true, imports: [RouterOutlet, RouterLink, RouterLinkActive],
  template: `<nav class="nav"><a routerLink="/" routerLinkActive="active" [routerLinkActiveOptions]="{exact:true}">Dashboard</a><a routerLink="/apply" routerLinkActive="active">Apply</a><a routerLink="/approval" routerLinkActive="active">Approval</a><a routerLink="/balances" routerLinkActive="active">Balances</a><a routerLink="/leave-types" routerLinkActive="active">Leave Types</a><a routerLink="/settlements" routerLinkActive="active">Settlements</a></nav><router-outlet />`
})
export class AppComponent {}
