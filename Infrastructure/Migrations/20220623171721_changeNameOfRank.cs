using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class changeNameOfRank : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Rank_RankId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_RankId",
                table: "Employees");

            migrationBuilder.AddColumn<int>(
                name: "SalaryManagementId",
                table: "Employees",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_SalaryManagementId",
                table: "Employees",
                column: "SalaryManagementId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Rank_SalaryManagementId",
                table: "Employees",
                column: "SalaryManagementId",
                principalTable: "Rank",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Rank_SalaryManagementId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_SalaryManagementId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "SalaryManagementId",
                table: "Employees");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_RankId",
                table: "Employees",
                column: "RankId");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Rank_RankId",
                table: "Employees",
                column: "RankId",
                principalTable: "Rank",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
