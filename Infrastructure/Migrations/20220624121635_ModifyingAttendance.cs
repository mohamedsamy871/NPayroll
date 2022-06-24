using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class ModifyingAttendance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AbsenceConditionsId",
                table: "Attendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_AbsenceConditionsId",
                table: "Attendance",
                column: "AbsenceConditionsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attendance_AbsenceConditions_AbsenceConditionsId",
                table: "Attendance",
                column: "AbsenceConditionsId",
                principalTable: "AbsenceConditions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attendance_AbsenceConditions_AbsenceConditionsId",
                table: "Attendance");

            migrationBuilder.DropIndex(
                name: "IX_Attendance_AbsenceConditionsId",
                table: "Attendance");

            migrationBuilder.DropColumn(
                name: "AbsenceConditionsId",
                table: "Attendance");
        }
    }
}
