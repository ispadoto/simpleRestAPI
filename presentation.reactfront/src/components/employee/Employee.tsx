import React, { useState, useEffect } from 'react';
import { Employee, Role } from '../../services/employeeService';
import employeeService from '../../services/employeeService';
import EmployeeList from './EmployeeList';
import EmployeeForm from './EmployeeForm';
import './Employee.css';

const EmployeeComponent: React.FC = () => {
    const [showForm, setShowForm] = useState(false);
    const [editingEmployee, setEditingEmployee] = useState<Employee | undefined>();
    const [error, setError] = useState<string>('');
    const [employees, setEmployees] = useState<Employee[]>([]);
    const [roles] = useState<Role[]>(employeeService.getRoles());

    useEffect(() => {
        const loadEmployees = async () => {
            try {
                const data = await employeeService.getAll();
                setEmployees(data);
            } catch {
                setError('Failed to load employees');
            }
        };
        loadEmployees();
    }, []);

    const handleAdd = () => {
        setEditingEmployee(undefined);
        setShowForm(true);
    };

    const handleEdit = (employee: Employee) => {
        setEditingEmployee(employee);
        setShowForm(true);
    };

    const handleDelete = async (id: number) => {
        if (window.confirm('Are you sure you want to delete this employee?')) {
            try {
                await employeeService.delete(id);
                // Refresh the list
                window.location.reload();
            } catch {
                setError('Failed to delete employee');
            }
        }
    };

    const handleSubmit = async (employee: Employee) => {
        try {
            if (editingEmployee) {
                await employeeService.update(editingEmployee.id!, employee);
            } else {
                await employeeService.create(employee);
            }
            setShowForm(false);
            window.location.reload();
        } catch {
            setError(editingEmployee ? 'Failed to update employee' : 'Failed to create employee');
        }
    };

    const handleCancel = () => {
        setShowForm(false);
        setEditingEmployee(undefined);
    };

    return (
        <div className="employee-container">
            <div className="employee-header">
                <h1>Employees</h1>
                <button onClick={handleAdd} className="add-employee-btn">
                    Add Employee
                </button>
            </div>

            {error && <div className="error-message">{error}</div>}

            {showForm ? (
                <EmployeeForm
                    employee={editingEmployee}
                    onSubmit={handleSubmit}
                    onCancel={handleCancel}
                />
            ) : (
                <EmployeeList
                    employees={employees}
                    roles={roles}
                    onEdit={handleEdit}
                    onDelete={handleDelete}
                />
            )}
        </div>
    );
};

export default EmployeeComponent; 