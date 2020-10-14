import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import {FormsModule} from '@angular/forms';
import {CommonModule} from '@angular/common';
import {HttpClientModule} from '@angular/common/http';
import {MatRadioModule} from '@angular/material/radio';
import {MatInputModule} from '@angular/material/input';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import {MatButtonModule} from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import {MatDialogModule, MAT_DIALOG_DEFAULT_OPTIONS} from '@angular/material/dialog';
import {NgbModule} from '@ng-bootstrap/ng-bootstrap';
import { UserContentComponent } from './user-content/user-content.component';
import { MatTableModule } from '@angular/material/table'
import { MatFormFieldModule } from '@angular/material/form-field';
import { PaginationModule } from 'ngx-bootstrap/pagination';


@NgModule({
  declarations: [
    AppComponent,
    UserContentComponent    
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    CommonModule,
    HttpClientModule,
    MatRadioModule,
    MatInputModule,
    BrowserAnimationsModule,
    MatButtonModule,
    MatSelectModule,
    MatDialogModule,
    NgbModule,
    MatTableModule,
    MatFormFieldModule,
    PaginationModule
    ],
  providers: [{provide: MAT_DIALOG_DEFAULT_OPTIONS, useValue: {hasBackdrop: true}}],
  entryComponents: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
