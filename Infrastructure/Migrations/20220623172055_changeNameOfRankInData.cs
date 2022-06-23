using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class changeNameOfRankInData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Rank_SalaryManagementId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rank",
                table: "Rank");

            migrationBuilder.RenameTable(
                name: "Rank",
                newName: "SalaryManagement");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SalaryManagement",
                table: "SalaryManagement",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_SalaryManagement_SalaryManagementId",
                table: "Employees",
                column: "SalaryManagementId",
                principalTable: "SalaryManagement",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_SalaryManagement_SalaryManagementId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SalaryManagement",
                table: "SalaryManagement");

            migrationBuilder.RenameTable(
                name: "SalaryManagement",
                newName: "Rank");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rank",
                table: "Rank",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Rank_SalaryManagementId",
                table: "Employees",
                column: "SalaryManagementId",
                principalTable: "Rank",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
