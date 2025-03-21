import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { MatTableModule } from '@angular/material/table'; // Import MatTableModule
import { MatButtonModule } from '@angular/material/button'; // Import MatButtonModule for buttons
import { MatCardModule } from '@angular/material/card'; // Import MatCardModule for cards
import { MatFormFieldModule } from '@angular/material/form-field'; // Import MatFormFieldModule for form fields
import { MatInputModule } from '@angular/material/input'; // Import MatInputModule for inputs
import { MatIconModule } from '@angular/material/icon'; // Import MatIconModule for icons
import { MatSelectModule } from '@angular/material/select'; // Import MatSelectModule for select
import { MatDatepickerModule } from '@angular/material/datepicker'; // Import MatDatepickerModule for datepicker
import { MatNativeDateModule } from '@angular/material/core'; // Import MatNativeDateModule for datepicker
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import { EmployeeComponent } from './employee/employee.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations'; // Import here
import { ToastrModule } from 'ngx-toastr';

@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    EmployeeComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    HttpClientModule,
    FormsModule,
    MatTableModule, // Add MatTableModule here
    MatButtonModule, // Add if using buttons
    MatCardModule, // Add if using cards
    MatFormFieldModule, // Add if using form fields
    MatInputModule,  // Add if using input
    MatIconModule, // Add MatIconModule here
    MatSelectModule, // Add MatSelectModule here
    MatDatepickerModule, // Add MatDatepickerModule here
    MatNativeDateModule, // Add MatNativeDateModule here
    BrowserAnimationsModule,
    ToastrModule.forRoot({
      timeOut: 3000,
      positionClass: 'toast-top-right',
      preventDuplicates: true,
    })
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
