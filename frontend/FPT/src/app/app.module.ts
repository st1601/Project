import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';


import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { UserComponent } from './compoments/admin/user/user.component';
import { RouterModule } from '@angular/router';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { CategoryComponent } from './compoments/staff/category/category.component';
import { IdeasComponent } from './compoments/staff/ideas/ideas.component';
import { EventaComponent } from './compoments/admin/eventa/eventa.component';
import { EventsComponent } from './compoments/staff/events/events.component';
import { FormsModule } from '@angular/forms';
import { LoginComponent } from './compoments/login/login.component';
import { AuthInterceptor } from './auth.interceptor';
import { CreateComponent } from './compoments/staff/create/create.component';
import { MainsComponent } from './compoments/staff/mains/mains.component';
import { MainaComponent } from './compoments/admin/maina/maina.component';
import { NavbarcComponent } from './compoments/qac/navbarc/navbarc.component';
import { MaincComponent } from './compoments/qac/mainc/mainc.component';
import { NavbarsComponent } from './compoments/staff/navbars/navbars.component';
import { NavbarmComponent } from './compoments/qam/navbarm/navbarm.component';
import { MainmComponent } from './compoments/qam/mainm/mainm.component';
import { NavbaraComponent } from './compoments/admin/navbara/navbara.component';
import { FilterPipe } from './filter.pipe';
import { CategorymComponent } from './compoments/qam/categorym/categorym.component';
import { DashboardComponent } from './compoments/qam/dashboard/dashboard.component';
import { NgApexchartsModule } from 'ng-apexcharts';
import { IdeaComponent } from './compoments/qam/idea/idea.component';


@NgModule({
  declarations: [
    AppComponent,
    UserComponent,
    CategoryComponent,
    IdeasComponent,
    EventaComponent,
    EventsComponent,
    LoginComponent,
    CreateComponent,
    MainsComponent,
    MainaComponent,
    NavbarcComponent,
    MaincComponent,
    NavbarsComponent,
    NavbarmComponent,
    MainmComponent,
    NavbaraComponent,
    FilterPipe,
    CategorymComponent,
    DashboardComponent,
    IdeaComponent,

  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    NgApexchartsModule,
    RouterModule.forRoot([
      {
        path: 'category',
        component: CategoryComponent,
      },
      {
        path: 'categorym',
        component: CategorymComponent,
      },
      {
        path: 'dashboard',
        component: DashboardComponent,
      },
      {
        path: '',
        component: LoginComponent,
      },
      {
        path: 'ideas',
        component: IdeasComponent,
      },
      {
        path: 'idea',
        component: IdeaComponent,
      },
      {
        path: 'login',
        component: LoginComponent,
      },
      {
        path: 'user',
        component: UserComponent,
      },
      {
        path: 'create',
        component: CreateComponent,
      },
      {
        path: 'mains',
        component: MainsComponent,
      },
      {
        path: 'mainc',
        component: MaincComponent,
      },
      {
        path: 'mainm',
        component: MainmComponent,
      },
      {
        path: 'maina',
        component: MainaComponent,
      },
      {
        path: 'eventa',
        component: EventaComponent,
      },
      {
        path: 'events',
        component: EventsComponent,
      },
    ]),
    NgbModule,
  ],
  providers: [
    {
    provide: HTTP_INTERCEPTORS,
    useClass: AuthInterceptor,
    multi: true,
  },
],
  bootstrap: [AppComponent]
})
export class AppModule { }
