using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SimpleRestAPI.Infra.Database.Migrations
{
    /// <inheritdoc />
    public partial class AdminSeed_Fix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Employee",
                columns: new[] { "Id", "Active", "Address", "BirthDate", "City", "CreatedAt", "CreatedBy", "Deleted", "DocNumber", "Email", "FirstName", "LastName", "ManagerId", "ManagerName", "Password", "RoleId", "UpdatedAt", "UpdatedBy" },
                values: new object[] { new Guid("f18534d4-8679-47f4-9b37-25e85bc35f2f"), true, null, new DateTime(1981, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Admin City", new DateTime(1981, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), null, false, "admin", "ispadoto@gmail.com", "Admin", "Admin", null, "Himself", "$2b$10$UgbfxfnbHRmTggYq6TUB3.HlbsYHQe.4Jgvt0uc5hi60tIw.LJ7ym", 1, null, null });
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
