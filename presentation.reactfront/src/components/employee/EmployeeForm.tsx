import React, { useState, useEffect } from 'react';
import { Employee, Phone, Role } from '../../services/employeeService';
import employeeService from '../../services/employeeService';
import './EmployeeForm.css';

interface EmployeeFormProps {
    employee?: Employee;
    onSubmit: (employee: Employee) => void;
    onCancel: () => void;
}

const EmployeeForm: React.FC<EmployeeFormProps> = ({ employee, onSubmit, onCancel }) => {
    const [formData, setFormData] = useState<Employee>({
        firstName: '',
        lastName: '',
        email: '',
        password: '',
        docNumber: '',
        birthDate: '',
        address: '',
        city: '',
        managerId: '',
        roleId: 4, // Default to Employee role
        phones: [{ phoneNumber: '', phoneType: 'Mobile', employeeId: '', id: '' }]
    });
    const [passwordConfirm, setPasswordConfirm] = useState('');
    const [errors, setErrors] = useState<string[]>([]);
    const [managers, setManagers] = useState<Employee[]>([]);
    const [roles] = useState<Role[]>(employeeService.getRoles());
    const maxBirthDate = new Date();
    maxBirthDate.setFullYear(maxBirthDate.getFullYear() - 18);

    useEffect(() => {
        if (employee) {
            const formattedEmployee = {
                ...employee,
                birthDate: employee.birthDate ? new Date(employee.birthDate).toISOString().split('T')[0] : ''
            };
            setFormData(formattedEmployee);
        }
        loadManagers();
    }, [employee]);

    const loadManagers = async () => {
        try {
            const data = await employeeService.getAll();
            setManagers(data);
        } catch (error) {
            console.error('Failed to load managers:', error);
        }
    };

    const handleChange = (e: React.ChangeEvent<HTMLInputElement | HTMLSelectElement>) => {
        const { name, value } = e.target;
        
        if (name === 'managerId') {
            const selectedManager = managers.find(m => m.id === value);
            setFormData(prev => ({
                ...prev,
                [name]: value,
                managerName: selectedManager ? `${selectedManager.firstName} ${selectedManager.lastName}` : ''
            }));
        } else {
            setFormData(prev => ({
                ...prev,
                [name]: value
            }));
        }
    };

    const handlePhoneChange = (index: number, field: keyof Phone, value: string) => {
        setFormData(prev => ({
            ...prev,
            phones: prev.phones?.map((phone, i) => 
                i === index ? { ...phone, [field]: value } : phone
            )
        }));
    };

    const addPhone = () => {
        setFormData(prev => ({
            ...prev,
            phones: [...(prev.phones || []), { phoneNumber: '', phoneType: 'Mobile' }]
        }));
    };

    const removePhone = (index: number) => {
        setFormData(prev => ({
            ...prev,
            phones: prev.phones?.filter((_, i) => i !== index)
        }));
    };

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        const validationErrors = employeeService.validateEmployee(formData, passwordConfirm);
        
        if (validationErrors.length > 0) {
            setErrors(validationErrors);
            return;
        }

        onSubmit(formData);
    };

    return (
        <form onSubmit={handleSubmit} className="employee-form">
            <h2>{employee ? 'Edit Employee' : 'Add Employee'}</h2>
            {errors.length > 0 && (
                <div className="error-message">
                    {errors.map((error, index) => (
                        <div key={index}>{error}</div>
                    ))}
                </div>
            )}
            
            <div className="form-section">
                <h3>Personal Information</h3>
                <div className="form-row">
                    <div className="form-group">
                        <label htmlFor="firstName">First Name</label>
                        <input
                            type="text"
                            id="firstName"
                            name="firstName"
                            value={formData.firstName}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <div className="form-group">
                        <label htmlFor="lastName">Last Name</label>
                        <input
                            type="text"
                            id="lastName"
                            name="lastName"
                            value={formData.lastName}
                            onChange={handleChange}
                            required
                        />
                    </div>
                </div>

                <div className="form-row">
                    <div className="form-group">
                        <label htmlFor="email">Email</label>
                        <input
                            type="email"
                            id="email"
                            name="email"
                            value={formData.email}
                            onChange={handleChange}
                            required
                        />
                    </div>
                    <div className="form-group">
                        <label htmlFor="docNumber">Document Number</label>
                        <input
                            type="text"
                            id="docNumber"
                            name="docNumber"
                            value={formData.docNumber}
                            onChange={handleChange}
                        />
                    </div>
                </div>

                <div className="form-row">
                    <div className="form-group">
                        <label htmlFor="birthDate">Birth Date</label>
                        <input
                            type="date"
                            id="birthDate"
                            name="birthDate"
                            value={formData.birthDate}
                            onChange={handleChange}
                            max={maxBirthDate.toISOString().split('T')[0]}
                        />
                    </div>
                    <div className="form-group">
                        <label htmlFor="roleId">Role</label>
                        <select
                            id="roleId"
                            name="roleId"
                            value={formData.roleId}
                            onChange={handleChange}
                            required
                        >
                            {roles.map(role => (
                                <option key={role.id} value={role.id}>
                                    {role.name}
                                </option>
                            ))}
                        </select>
                    </div>
                </div>
            </div>

            <div className="form-section">
                <h3>Address</h3>
                <div className="form-row">
                    <div className="form-group full-width">
                        <label htmlFor="address">Address</label>
                        <input
                            type="text"
                            id="address"
                            name="address"
                            value={formData.address}
                            onChange={handleChange}
                        />
                    </div>
                </div>
                <div className="form-row">
                    <div className="form-group">
                        <label htmlFor="city">City</label>
                        <input
                            type="text"
                            id="city"
                            name="city"
                            value={formData.city}
                            onChange={handleChange}
                        />
                    </div>
                    <div className="form-group">
                        <label htmlFor="managerId">Manager</label>
                        <select
                            id="managerId"
                            name="managerId"
                            value={formData.managerId}
                            onChange={handleChange}
                            required
                        >
                            <option value="">Select a manager</option>
                            {managers.map(manager => (
                                <option key={manager.id} value={manager.id}>
                                    {manager.firstName} {manager.lastName}
                                </option>
                            ))}
                        </select>
                    </div>
                </div>
            </div>

            {!employee ? (
                <div className="form-section">
                    <h3>Account Information</h3>
                    <div className="form-row">
                        <div className="form-group">
                            <label htmlFor="password">Password</label>
                            <input
                                type="password"
                                id="password"
                                name="password"
                                value={formData.password}
                                onChange={handleChange}
                                required
                            />
                        </div>
                        <div className="form-group">
                            <label htmlFor="passwordConfirm">Confirm Password</label>
                            <input
                                type="password"
                                id="passwordConfirm"
                                value={passwordConfirm}
                                onChange={(e) => setPasswordConfirm(e.target.value)}
                                required
                            />
                        </div>
                    </div>
                </div>
            ) : (
                <div className="form-section">
                    <h3>Account Information</h3>
                    <div className="form-row">
                        <div className="form-group">
                            <label htmlFor="password">New Password</label>
                            <input
                                type="password"
                                id="password"
                                name="password"
                                value={formData.password}
                                onChange={handleChange}
                                required
                            />
                        </div>
                        <div className="form-group">
                            <label htmlFor="passwordConfirm">Confirm New Password</label>
                            <input
                                type="password"
                                id="passwordConfirm"
                                value={passwordConfirm}
                                onChange={(e) => setPasswordConfirm(e.target.value)}
                                required
                            />
                        </div>
                    </div>
                </div>
            )}

            <div className="form-section">
                <h3>Contact Information</h3>
                <div className="phones-section">
                    {formData.phones?.map((phone, index) => (
                        <div key={index} className="phone-form-group">
                            <div className="form-group">
                                <label>Type</label>
                                <select
                                    value={phone.phoneType}
                                    onChange={(e) => handlePhoneChange(index, 'phoneType', e.target.value)}
                                >
                                    <option value="Mobile">Mobile</option>
                                    <option value="Home">Home</option>
                                    <option value="Work">Work</option>
                                </select>
                            </div>
                            <div className="form-group">
                                <label>Number</label>
                                <input
                                    type="tel"
                                    value={phone.phoneNumber}
                                    onChange={(e) => handlePhoneChange(index, 'phoneNumber', e.target.value)}
                                    required
                                />
                            </div>
                            {formData.phones && formData.phones.length > 1 && (
                                <button
                                    type="button"
                                    onClick={() => removePhone(index)}
                                    className="remove-phone-btn"
                                >
                                    Remove
                                </button>
                            )}
                        </div>
                    ))}
                    <button type="button" onClick={addPhone} className="add-phone-btn">
                        Add Phone
                    </button>
                </div>
            </div>

            <div className="form-actions">
                <button type="submit" className="submit-btn">
                    {employee ? 'Update' : 'Add'} Employee
                </button>
                <button type="button" onClick={onCancel} className="cancel-btn">
                    Cancel
                </button>
            </div>
        </form>
    );
};

export default EmployeeForm; 