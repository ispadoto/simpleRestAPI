import React, { useState } from 'react';
import { Employee, Role } from '../../services/employeeService';
import './EmployeeList.css';

interface EmployeeListProps {
  employees: Employee[];
  onEdit: (employee: Employee) => void;
  onDelete: (id: number) => void;
  roles: Role[];
}

const EmployeeList: React.FC<EmployeeListProps> = ({ employees = [], onEdit, onDelete, roles }) => {
  const [error] = useState<string | null>(null);

  const getRoleName = (roleId: number): string => {
    const role = roles.find(r => r.id === roleId);
    return role ? role.name : 'Unknown Role';
  };

  if (error) {
    return <div className="error-message">{error}</div>;
  }

  if (!employees || employees.length === 0) {
    return <div className="no-data-message">No employees found</div>;
  }

  return (
    <div className="employee-grid-container">
      <table className="employee-table">
        <thead>
          <tr>
            <th>Name</th>
            <th>Email</th>
            <th>Role</th>
            <th>Document</th>
            <th>Birth Date</th>
            <th>Address</th>
            <th>City</th>
            <th>Manager</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {employees.map((employee) => (
            <tr key={employee.id}>
              <td>{`${employee.firstName} ${employee.lastName}`}</td>
              <td>{employee.email}</td>
              <td>{getRoleName(employee.roleId)}</td>
              <td>{employee.docNumber || 'N/A'}</td>
              <td>{employee.birthDate ? new Date(employee.birthDate).toLocaleDateString() : 'N/A'}</td>
              <td>{employee.address || 'N/A'}</td>
              <td>{employee.city || 'N/A'}</td>
              <td>{employee.managerName || 'N/A'}</td>
              <td className="employee-actions">
                <button
                  className="edit-btn"
                  onClick={() => onEdit(employee)}
                >
                  Edit
                </button>
                <button
                  className="delete-btn"
                  onClick={() => onDelete(Number(employee.id))}
                >
                  Delete
                </button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default EmployeeList; 