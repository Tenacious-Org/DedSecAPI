using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace A5.Migrations
{
    public partial class NavigationEmp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CommentId",
                table: "Comments",
                newName: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_HRId",
                table: "Employees",
                column: "HRId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_ReportingPersonId",
                table: "Employees",
                column: "ReportingPersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_HRId",
                table: "Employees",
                column: "HRId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_ReportingPersonId",
                table: "Employees",
                column: "ReportingPersonId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_HRId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_ReportingPersonId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_HRId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_ReportingPersonId",
                table: "Employees");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Comments",
                newName: "CommentId");
        }
    }
}
