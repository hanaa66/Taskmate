using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace sep_project.Migrations
{
    /// <inheritdoc />
    public partial class addhmg : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image_Name",
                table: "Employees",
                type: "nvarchar(max)",
                nullable: true,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image_Name",
                table: "Employees");
        }
    }
}
