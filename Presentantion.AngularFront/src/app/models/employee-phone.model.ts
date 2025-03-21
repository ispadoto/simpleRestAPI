import { Guid } from "guid-typescript";

export interface EmployeePhone {
  id?: string; // Optional ID
  phoneNumber?: string; // Phone number (optional)
  phoneType?: string; // Type of phone (optional)
  employeeId?: string; // ID of the related employee
}
