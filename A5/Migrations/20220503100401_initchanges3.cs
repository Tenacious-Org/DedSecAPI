using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace A5.Migrations
{
    public partial class initchanges3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HRId",
                table: "Awards",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HRId",
                table: "Awards");
        }
    }
}
