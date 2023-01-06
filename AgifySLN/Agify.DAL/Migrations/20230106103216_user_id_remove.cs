using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Agify.DAL.Migrations
{
    public partial class user_id_remove : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Avg",
                table: "Users",
                newName: "Count");

            migrationBuilder.AlterColumn<string>(
                name: "StageOfLife",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Count",
                table: "Users",
                newName: "Avg");

            migrationBuilder.AlterColumn<int>(
                name: "StageOfLife",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
