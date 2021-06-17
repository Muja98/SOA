import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { RouterModule } from "@angular/router";
import {HttpClientModule} from '@angular/common/http'

import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { CardsComponent } from './components/cards/cards.component';
import { PageNotFoundComponent } from './components/page-not-found/page-not-found.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { FormsModule } from '@angular/forms';
import { ToastsContainer } from './components/toast-container/toast-container.component';
import { LineChartModule, RealtimeChartModule, PieChartModule } from 'ngx-graph';



@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    CardsComponent,
    PageNotFoundComponent,
    ToastsContainer
  ],
  imports: [
    BrowserModule,
    FormsModule,
    LineChartModule, RealtimeChartModule, PieChartModule,
    HttpClientModule,
    RouterModule.forRoot([
      {path: '', component: HomeComponent},
      {path: 'home', component: HomeComponent},
      {path: 'cards', component: CardsComponent},
      {path: '**', component: PageNotFoundComponent}
    ]),
    NgbModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
