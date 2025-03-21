import { Guid } from "guid-typescript";
import { EmployeePhone } from "./employee-phone.model";

export interface Employee {
  id?: string; // Optional ID
  firstName?: string; // Employee's first name
  lastName?: string; // Employee's last name
  email?: string; // Employee's email
  password: string; // Agora é obrigatório (removido o ?)
  docNumber?: string; // Document number
  birthDate?: string; // Birthdate (ISO format)
  address?: string; // Address
  city?: string; // City
  managerId: string; // Mandatory manager ID
  managerName?: string; // Name of the manager
  roleId: number; // Role ID
  phones?: EmployeePhone[]; // Array of employee phones
}
