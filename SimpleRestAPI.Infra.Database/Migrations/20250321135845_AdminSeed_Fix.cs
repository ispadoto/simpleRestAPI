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
            migrationBuilder.UpdateData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: new Guid("f18534d4-8679-47f4-9b37-25e85bc35f2f"),
                column: "Password",
                value: "$2b$10$UgbfxfnbHRmTggYq6TUB3.HlbsYHQe.4Jgvt0uc5hi60tIw.LJ7ym");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Employee",
                keyColumn: "Id",
                keyValue: new Guid("f18534d4-8679-47f4-9b37-25e85bc35f2f"),
                column: "Password",
                value: "admin");
        }
    }
}
