using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class RemovingabsenceConditionsFromAttendance : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Absence",
                table: "Attendance");

            migrationBuilder.DropColumn(
                name: "DeductionAmount",
                table: "Attendance");

            migrationBuilder.DropColumn(
                name: "IncentiveAmount",
                table: "Attendance");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Absence",
                table: "Attendance",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "DeductionAmount",
                table: "Attendance",
                type: "float",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "IncentiveAmount",
                table: "Attendance",
                type: "float",
                nullable: true);
        }
    }
}
