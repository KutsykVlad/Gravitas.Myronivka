import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LogInComponent } from './components/log-in/log-in.component';
import { TabsComponent } from './components/tabs/tabs.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { ProfileComponent } from './components/profile/profile.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { AuthGuard } from './auth.guard';
import { ContactsComponent } from './components/contacts/contacts.component';

const routes: Routes = [
  {path: '', redirectTo: 'trucks-info', pathMatch: 'full'},
  {path: 'trucks-info', component: TabsComponent},
  {path: 'login', component: LogInComponent},
  {path: 'profile', component: ProfileComponent, canActivate: [AuthGuard]},
  {path: 'registration', component: RegistrationComponent},
  {path: 'contact-us', component: ContactsComponent},
  { path: '**', component: NotFoundComponent}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
