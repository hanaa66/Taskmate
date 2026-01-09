using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sep_project.Migrations
{
    /// <inheritdoc />
    public partial class ttt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tasks_Users_User_Id",
                table: "Tasks");

            migrationBuilder.DropIndex(
                name: "IX_Tasks_User_Id",
                table: "Tasks");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "Tasks");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "User_Id",
                table: "Tasks",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Tasks_User_Id",
                table: "Tasks",
                column: "User_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tasks_Users_User_Id",
                table: "Tasks",
                column: "User_Id",
                principalTable: "Users",
                principalColumn: "User_Id");
        }
    }
}
