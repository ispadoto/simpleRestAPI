import { ComponentFixture, TestBed, fakeAsync, tick } from '@angular/core/testing';
import { FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatTableModule } from '@angular/material/table';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { EmployeeComponent } from './employee.component';
import { EmployeeService } from '../services/employee.service';
import { of } from 'rxjs';
import { Employee } from '../models/employee.model';

describe('EmployeeComponent', () => {
  let component: EmployeeComponent;
  let fixture: ComponentFixture<EmployeeComponent>;
  let employeeService: jasmine.SpyObj<EmployeeService>;

  const mockEmployees: Employee[] = [
    {
      id: '1',
      firstName: 'John',
      lastName: 'Doe',
      email: 'john@example.com',
      managerId: '2',
      managerName: 'Jane Manager',
      roleId: 4
    },
    {
      id: '2',
      firstName: 'Jane',
      lastName: 'Manager',
      email: 'jane@example.com',
      managerId: '3',
      managerName: 'Super Manager',
      roleId: 3
    }
  ];

  beforeEach(async () => {
    const spy = jasmine.createSpyObj('EmployeeService', ['getEmployees', 'createEmployee', 'updateEmployee', 'deleteEmployee']);
    spy.getEmployees.and.returnValue(of(mockEmployees));
    spy.createEmployee.and.returnValue(of({}));
    spy.updateEmployee.and.returnValue(of({}));
    spy.deleteEmployee.and.returnValue(of({}));

    await TestBed.configureTestingModule({
      declarations: [ EmployeeComponent ],
      imports: [
        FormsModule,
        MatFormFieldModule,
        MatInputModule,
        MatSelectModule,
        MatDatepickerModule,
        MatNativeDateModule,
        MatTableModule,
        MatIconModule,
        MatButtonModule,
        BrowserAnimationsModule
      ],
      providers: [
        { provide: EmployeeService, useValue: spy }
      ]
    }).compileComponents();

    employeeService = TestBed.inject(EmployeeService) as jasmine.SpyObj<EmployeeService>;
  });

  beforeEach(() => {
    fixture = TestBed.createComponent(EmployeeComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should load employees on init', () => {
    expect(employeeService.getEmployees).toHaveBeenCalled();
    expect(component.employees).toEqual(mockEmployees);
  });

  it('should add phone to employee form', () => {
    component.addPhone();
    expect(component.employeeForm.phones?.length).toBe(1);
    expect(component.employeeForm.phones?.[0]).toEqual({ phoneNumber: '', phoneType: 'Mobile' });
  });

  it('should remove phone from employee form', () => {
    component.addPhone();
    component.addPhone();
    expect(component.employeeForm.phones?.length).toBe(2);

    component.removePhone(0);
    expect(component.employeeForm.phones?.length).toBe(1);
  });

  it('should validate age correctly', () => {
    const event = {
      value: new Date(2000, 0, 1) // Someone born in 2000
    } as any;

    component.onBirthDateChange(event);
    expect(component.isValidAge).toBeTrue();

    const invalidEvent = {
      value: new Date(2010, 0, 1) // Someone born in 2010
    } as any;

    component.onBirthDateChange(invalidEvent);
    expect(component.isValidAge).toBeFalse();
  });

  it('should update manager name when manager is selected', () => {
    const event = {
      value: '1'
    };

    component.onManagerChange(event);
    expect(component.employeeForm.managerName).toBe('John Doe');
  });

  it('should reset form correctly', () => {
    component.employeeForm = {
      firstName: 'Test',
      lastName: 'User',
      email: 'test@example.com',
      managerId: '1',
      roleId: 2,
      phones: [{ phoneNumber: '123', phoneType: 'Work' }]
    };
    component.isEditing = true;
    component.editId = '1';

    component.resetForm();

    expect(component.employeeForm).toEqual({
      managerId: '',
      roleId: 1,
      phones: []
    });
    expect(component.isEditing).toBeFalse();
    expect(component.editId).toBeUndefined();
  });

  it('should save new employee', fakeAsync(() => {
    component.employeeForm = {
      firstName: 'New',
      lastName: 'Employee',
      email: 'new@example.com',
      managerId: '1',
      roleId: 4
    };

    component.saveEmployee();
    tick();

    expect(employeeService.createEmployee).toHaveBeenCalledWith(component.employeeForm);
    expect(employeeService.getEmployees).toHaveBeenCalled();
  }));

  it('should update existing employee', fakeAsync(() => {
    component.isEditing = true;
    component.editId = '1';
    component.employeeForm = {
      firstName: 'Updated',
      lastName: 'Employee',
      email: 'updated@example.com',
      managerId: '1',
      roleId: 4
    };

    component.saveEmployee();
    tick();

    expect(employeeService.updateEmployee).toHaveBeenCalledWith('1', component.employeeForm);
    expect(employeeService.getEmployees).toHaveBeenCalled();
  }));

  it('should delete employee', fakeAsync(() => {
    component.deleteEmployee('1');
    tick();

    expect(employeeService.deleteEmployee).toHaveBeenCalledWith('1');
    expect(employeeService.getEmployees).toHaveBeenCalled();
  }));
});
