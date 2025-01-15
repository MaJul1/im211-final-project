using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PLSPEduView.Migrations
{
    /// <inheritdoc />
    public partial class FixAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Address",
                table: "Students",
                newName: "Province");

            migrationBuilder.AddColumn<string>(
                name: "Barangay",
                table: "Students",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Municipality",
                table: "Students",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Barangay",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Municipality",
                table: "Students");

            migrationBuilder.RenameColumn(
                name: "Province",
                table: "Students",
                newName: "Address");
        }
    }
}
