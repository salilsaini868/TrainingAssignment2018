import { RouterModule, Routes } from '@angular/router';
import { AppComponent } from '../app.component';
import { RegisterComponent } from 'src/app/register/register.component';
import { LoginComponent } from 'src/app/login/login.component';
import { DashboardComponent } from 'src/app/dashboard/dashboard.component';
import { FeaturesComponent } from 'src/app/features/features.component';
import { AuthService } from '../services/authorization.service';

const routes: Routes = [
    {
        path: 'dashboard',
        component: FeaturesComponent,
        canActivate: [AuthService],
        children: [
            {
                path: '', component: DashboardComponent
            }
        ]
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
