using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace YourNamespace.Migrations
{
    public partial class SeedAdminEmployee : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Verifica se já existe um funcionário com DocNumber 'Admin'
            var sql = @"
                IF NOT EXISTS (SELECT 1 FROM Employees WHERE DocNumber = 'admin')
                BEGIN
                    INSERT INTO Employees (
                        FirstName, LastName, DocNumber, RoleId, Password,
                        Email, Phone, BirthDate, Address, City,
                        State, Country, ZipCode, Active, CreatedAt,
                        UpdatedAt
                    )
                    VALUES (
                        'Admin', 'Admin', 'admin', 1, '$2b$10$UgbfxfnbHRmTggYq6TUB3.HlbsYHQe.4Jgvt0uc5hi60tIw.LJ7ym',
                        'admin@admin.com', '0000000000', '1990-01-01', 'Admin Address', 'Admin City',
                        'AS', 'Admin Country', '00000000', 1, GETDATE(),
                        GETDATE()
                    )
                END";

            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            var sql = "DELETE FROM Employees WHERE DocNumber = 'Admin'";
            migrationBuilder.Sql(sql);
        }
    }
} 