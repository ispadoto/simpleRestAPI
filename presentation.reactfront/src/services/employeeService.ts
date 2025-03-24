import axios from 'axios';
import authService from './authService';

const API_URL = 'https://localhost:7251/api/Employees';

export interface Phone {
    id?: string;
    phoneNumber: string;
    phoneType: string;
    employeeId?: string;
}

export interface Employee {
    id?: string;
    firstName: string;
    lastName: string;
    email: string;
    password: string;
    docNumber?: string;
    birthDate?: string;
    address?: string;
    city?: string;
    managerId: string;
    managerName?: string;
    roleId: number;
    phones?: Phone[];
}

export interface Role {
    id: number;
    name: string;
}

class EmployeeService {
    private getHeaders() {
        const token = authService.getToken();
        return {
            Authorization: `Bearer ${token}`
        };
    }

    async getAll(): Promise<Employee[]> {
        const response = await axios.get<Employee[]>(API_URL, { headers: this.getHeaders() });
        return response.data;
    }

    async getById(id: string): Promise<Employee> {
        const response = await axios.get<Employee>(`${API_URL}/${id}`, { headers: this.getHeaders() });
        return response.data;
    }

    async create(employee: Employee): Promise<Employee> {
        const response = await axios.post<Employee>(API_URL, employee, { headers: this.getHeaders() });
        return response.data;
    }

    async update(id: string, employee: Employee): Promise<Employee> {
        const response = await axios.put<Employee>(`${API_URL}/${id}`, employee, { headers: this.getHeaders() });
        return response.data;
    }

    async delete(id: number): Promise<void> {
        await axios.delete(`${API_URL}/${id}`);
    }

    getRoles(): Role[] {
        return [
            { id: 1, name: 'Admin' },
            { id: 2, name: 'Director' },
            { id: 3, name: 'Leader' },
            { id: 4, name: 'Employee' }
        ];
    }

    validateEmployee(employee: Employee, passwordConfirm: string): string[] {
        const errors: string[] = [];

        // Validação de idade (18 anos)
        if (employee.birthDate) {
            const birthDate = new Date(employee.birthDate);
            const today = new Date();
            let calculatedAge = today.getFullYear() - birthDate.getFullYear();
            const monthDiff = today.getMonth() - birthDate.getMonth();
            
            if (monthDiff < 0 || (monthDiff === 0 && today.getDate() < birthDate.getDate())) {
                calculatedAge--;
            }
            
            if (calculatedAge < 18) {
                errors.push('O funcionário deve ter pelo menos 18 anos');
            }
        }

        // Validação de senha para novos funcionários
        if (!employee.id && employee.password !== passwordConfirm) {
            errors.push('As senhas não coincidem');
        }

        // Validação de campos obrigatórios
        if (!employee.firstName) errors.push('Nome é obrigatório');
        if (!employee.lastName) errors.push('Sobrenome é obrigatório');
        if (!employee.email) errors.push('Email é obrigatório');
        if (!employee.managerId) errors.push('Gerente é obrigatório');
        if (!employee.roleId) errors.push('Cargo é obrigatório');

        // Validação de permissões
        const currentRoleId = parseInt(authService.getRoleId() || '0', 10);
        if (employee.roleId <= currentRoleId) {
            errors.push('Você só pode adicionar cargos abaixo do seu');
        }

        return errors;
    }
}

export default new EmployeeService(); 