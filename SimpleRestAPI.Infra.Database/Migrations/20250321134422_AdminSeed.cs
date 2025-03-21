using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleRestAPI.Infra.Database.Migrations
{
    /// <inheritdoc />
    public partial class AdminSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "Id", "Active", "Address", "BirthDate", "City", "CreatedAt", "CreatedBy", "Deleted", "DocNumber", "Email", "FirstName", "LastName", "ManagerId", "ManagerName", "Password", "RoleId", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("f18534d4-8679-47f4-9b37-25e85bc35f2f"), true, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin City", new DateTime(2025, 3, 21, 10, 44, 22, 253, DateTimeKind.Local).AddTicks(8369), null, false, "admin", "ispadoto@gmail.com", "Admin", "Admin", null, "Himself", "admin", 1, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: new Guid("f18534d4-8679-47f4-9b37-25e85bc35f2f"));
        }
    }
}
