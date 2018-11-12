import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from '../app.component';
import { RegisterComponent } from 'src/app/register/register.component';
import { LoginComponent } from 'src/app/login/login.component';
import { DashboardComponent } from 'src/app/dashboard/dashboard.component';

const routes: Routes = [
    {
        path: 'dashboard',
        component: DashboardComponent
    },
    {
        path: 'register',
        component: RegisterComponent
    },
    {
        path: '',
        component: LoginComponent
    }
];
export const APPROUTERMODULE = RouterModule.forRoot(routes, { useHash: false });
