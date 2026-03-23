using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiAll.Migrations
{
    public partial class initial34534543225 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "tegirmon_close",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_date_time", "updated_date_time" },
                values: new object[] { new DateTime(2025, 11, 28, 10, 15, 35, 353, DateTimeKind.Local).AddTicks(1710), new DateTime(2025, 11, 28, 10, 15, 35, 353, DateTimeKind.Local).AddTicks(6219) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "tegirmon_close",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_date_time", "updated_date_time" },
                values: new object[] { new DateTime(2025, 11, 28, 10, 9, 17, 956, DateTimeKind.Local).AddTicks(9588), new DateTime(2025, 11, 28, 10, 9, 17, 957, DateTimeKind.Local).AddTicks(3766) });
        }
    }
}
