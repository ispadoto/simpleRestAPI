import { Component, OnInit } from '@angular/core';
import { EmployeeService } from '../services/employee.service';
import { Employee } from '../models/employee.model';
import { MatDatepickerInputEvent } from '@angular/material/datepicker';
import { ToastrService } from 'ngx-toastr';
import { catchError } from 'rxjs/operators';
import { of } from 'rxjs';
import { Router } from '@angular/router';

interface Role {
  id: number;
  name: string;
}

@Component({
  selector: 'app-employee',
  templateUrl: './employee.component.html',
  styleUrls: ['./employee.component.css'],
  styles: [`
    .employee-container {
      padding: 16px;
      width: 95%;
      margin: 0 auto;
      overflow-x: auto;
      position: relative;
      top: 0;
    }

    h2 {
      position: sticky;
      top: 0;
      background: white;
      margin-top: 0;
      padding: 16px 0;
      z-index: 1;
    }

    .employee-form {
      display: flex;
      flex-direction: column;
      gap: 10px;
      min-width: 320px;
      margin-top: 16px;
    }

    .form-section {
      background: #f5f5f5;
      padding: 16px;
      border-radius: 8px;
      margin-bottom: 16px;
      width: 100%;
      box-sizing: border-box;
    }

    .form-section h3 {
      margin-top: 0;
      color: #333;
      margin-bottom: 16px;
    }

    .form-row {
      display: flex;
      flex-wrap: wrap;
      gap: 16px;
      margin-bottom: 16px;
    }

    .form-row mat-form-field {
      flex: 1 1 300px;
      min-width: 250px;
    }

    .full-width {
      width: 100%;
    }

    .phone-list {
      margin-top: 16px;
    }

    .phone-item {
      display: flex;
      flex-wrap: wrap;
      gap: 16px;
      align-items: center;
      margin-bottom: 16px;
    }

    .phone-item mat-form-field {
      flex: 1 1 200px;
      min-width: 200px;
    }

    .form-actions {
      display: flex;
      gap: 10px;
      justify-content: flex-end;
      margin-top: 20px;
      flex-wrap: wrap;
    }

    .table-container {
      margin-top: 30px;
      overflow-x: auto;
    }

    table {
      width: 100%;
      min-width: 600px;
    }

    @media (max-width: 600px) {
      .employee-container {
        padding: 8px;
        width: 100%;
      }

      .form-section {
        padding: 12px;
      }

      .form-row {
        flex-direction: column;
        gap: 8px;
      }

      .form-row mat-form-field {
        width: 100%;
      }
    }
  `]
})
export class EmployeeComponent implements OnInit {
  employees: Employee[] = [];
  employeeForm: Employee = {
    managerId: '',
    roleId: 1,
    phones: [],
    password: ''
  };
  isEditing = false;
  editId?: string;
  maxBirthDate: Date;
  isValidAge = true;
  managers: Employee[] = [];
  passwordConfirm: string = '';

  roles: Role[] = [
    { id: 1, name: 'Admin' },
    { id: 2, name: 'Director' },
    { id: 3, name: 'Leader' },
    { id: 4, name: 'Employee' }
  ];

  constructor(
    private employeeService: EmployeeService,
    private toastr: ToastrService,
    private router: Router
  ) {
    // Calculate max birth date (18 years ago from today)
    const today = new Date();
    this.maxBirthDate = new Date(
      today.getFullYear() - 18,
      today.getMonth(),
      today.getDate()
    );
  }

  ngOnInit() {
    this.loadEmployees();
    this.loadManagers();
  }

  loadEmployees() {
    this.employeeService.getEmployees()
      .pipe(
        catchError(error => {
          this.toastr.error(`Erro ${error.status}: ${error.message}`, 'Falha na Comunicação com a API');
          return of([]);
        })
      )
      .subscribe(data => {
        this.employees = data;
      });
  }

  loadManagers() {
    this.employeeService.getEmployees()
      .pipe(
        catchError(error => {
          this.toastr.error('Houveram problemas ao comunicar com a API');
          return of([]);
        })
      )
      .subscribe(data => {
        this.managers = data;
      });
  }

  saveEmployee() {
    if (!this.isValidAge) {
      this.toastr.error('Idade inválida', 'Erro de Validação');
      return;
    }

    const currentRoleId = parseInt(localStorage.getItem('roleId') || '0', 10);
    if (this.employeeForm.roleId <= currentRoleId) {
      this.toastr.error('Você só pode adicionar cargos abaixo do seu.', 'Operação não permitida');
      return;
    }
    if (!this.isEditing && this.employeeForm.password !== this.passwordConfirm) {
      this.toastr.error('As senhas não coincidem', 'Erro de Validação');
      return;
    }

    if (this.isEditing) {
      const { password, ...employeeWithoutPassword } = this.employeeForm;
      this.employeeService.updateEmployee(this.editId!, this.employeeForm)
        .pipe(
          catchError(error => {
            this.toastr.error(`Erro ${error.status}: ${error.message}`, 'Falha na Atualização');
            return of(null);
          })
        )
        .subscribe(response => {
          if (response) {
            this.toastr.success('Funcionário atualizado com sucesso');
            this.resetForm();
            this.loadEmployees();
          }
        });
    } else {
      this.employeeService.createEmployee(this.employeeForm)
        .pipe(
          catchError(error => {
            this.toastr.error(`Erro ${error.status}: ${error.message}`, 'Falha na Criação');
            return of(null);
          })
        )
        .subscribe(response => {
          if (response) {
            this.toastr.success('Funcionário criado com sucesso');
            this.resetForm();
            this.loadEmployees();
          }
        });
    }
  }

  editEmployee(employee: Employee) {
    const { password, ...employeeWithoutPassword } = employee;
    this.employeeForm = { ...employeeWithoutPassword, password: '' };
    this.isEditing = true;
    this.editId = employee.id;
  }

  deleteEmployee(id: string) {
    this.employeeService.deleteEmployee(id)
      .pipe(
        catchError(error => {
          this.toastr.error('Houveram problemas ao comunicar com a API');
          return of(null);
        })
      )
      .subscribe(response => {
        if (response !== null) {
          this.toastr.success('Sucesso');
          this.loadEmployees();
        }
      });
  }

  resetForm() {
    this.employeeForm = {
      managerId: '',
      roleId: 1,
      phones: [],
      password: ''
    };
    this.passwordConfirm = '';
    this.isEditing = false;
    this.editId = undefined;
  }

  addPhone() {
    if (!this.employeeForm.phones) {
      this.employeeForm.phones = [];
    }
    this.employeeForm.phones.push({ phoneNumber: '', phoneType: 'Mobile' });
  }

  removePhone(index: number) {
    if (this.employeeForm.phones) {
      this.employeeForm.phones.splice(index, 1);
    }
  }

  onBirthDateChange(event: MatDatepickerInputEvent<Date>) {
    if (event.value) {
      const birthDate = new Date(event.value);
      const today = new Date();
      const age = today.getFullYear() - birthDate.getFullYear();
      const monthDiff = today.getMonth() - birthDate.getMonth();

      if (monthDiff < 0 || (monthDiff === 0 && today.getDate() < birthDate.getDate())) {
        this.isValidAge = age - 1 >= 18;
      } else {
        this.isValidAge = age >= 18;
      }
    }
  }

  onManagerChange(event: any) {
    const selectedManager = this.managers.find(m => m.id === event.value);
    if (selectedManager) {
      this.employeeForm.managerName = `${selectedManager.firstName} ${selectedManager.lastName}`;
    }
  }

  logout(): void {
    localStorage.clear();  // Limpa todo o localStorage
    this.router.navigate(['/login']);  // Redireciona para a tela de login
  }
}
