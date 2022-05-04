using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace A5.Migrations
{
    public partial class initchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddedBy",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "AddedOn",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Employees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedOn",
                table: "Employees",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Comments_AwardId",
                table: "Comments",
                column: "AwardId");

            migrationBuilder.CreateIndex(
                name: "IX_Awards_AwardeeId",
                table: "Awards",
                column: "AwardeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Awards_AwardTypeId",
                table: "Awards",
                column: "AwardTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Awards_StatusId",
                table: "Awards",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Awards_AwardTypes_AwardTypeId",
                table: "Awards",
                column: "AwardTypeId",
                principalTable: "AwardTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Awards_Employees_AwardeeId",
                table: "Awards",
                column: "AwardeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Awards_Statuses_StatusId",
                table: "Awards",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Awards_AwardId",
                table: "Comments",
                column: "AwardId",
                principalTable: "Awards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Awards_AwardTypes_AwardTypeId",
                table: "Awards");

            migrationBuilder.DropForeignKey(
                name: "FK_Awards_Employees_AwardeeId",
                table: "Awards");

            migrationBuilder.DropForeignKey(
                name: "FK_Awards_Statuses_StatusId",
                table: "Awards");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Awards_AwardId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Comments_AwardId",
                table: "Comments");

            migrationBuilder.DropIndex(
                name: "IX_Awards_AwardeeId",
                table: "Awards");

            migrationBuilder.DropIndex(
                name: "IX_Awards_AwardTypeId",
                table: "Awards");

            migrationBuilder.DropIndex(
                name: "IX_Awards_StatusId",
                table: "Awards");

            migrationBuilder.DropColumn(
                name: "AddedBy",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "AddedOn",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "UpdatedOn",
                table: "Employees");
        }
    }
}
