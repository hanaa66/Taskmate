using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sep_project.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "First_Name",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "Salary",
                table: "Employees",
                newName: "Department_Id");

            migrationBuilder.RenameColumn(
                name: "Last_Name",
                table: "Employees",
                newName: "Employee_Country");

            migrationBuilder.AddColumn<string>(
                name: "Employee_Email",
                table: "Employees",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Employee_Name",
                table: "Employees",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Employee_Salary",
                table: "Employees",
                type: "decimal(10,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "Hire_Date",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    Department_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Department_Name = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Department_Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Department_Id",
                table: "Employees",
                column: "Department_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_Department_Id",
                table: "Employees",
                column: "Department_Id",
                principalTable: "Departments",
                principalColumn: "Department_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_Department_Id",
                table: "Employees");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropIndex(
                name: "IX_Employees_Department_Id",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Employee_Email",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Employee_Name",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Employee_Salary",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Hire_Date",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "Employee_Country",
                table: "Employees",
                newName: "Last_Name");

            migrationBuilder.RenameColumn(
                name: "Department_Id",
                table: "Employees",
                newName: "Salary");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "First_Name",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
