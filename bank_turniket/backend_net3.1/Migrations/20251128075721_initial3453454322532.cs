using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiAll.Migrations
{
    public partial class initial3453454322532 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tegirmon_user_hisob_access_tegirmon_skladlar_hisobid",
                table: "tegirmon_user_hisob_access");

            migrationBuilder.DropIndex(
                name: "IX_tegirmon_user_hisob_access_hisobid",
                table: "tegirmon_user_hisob_access");

            migrationBuilder.DropColumn(
                name: "hisobid",
                table: "tegirmon_user_hisob_access");

            migrationBuilder.RenameColumn(
                name: "TegirmonHisobId",
                table: "tegirmon_user_hisob_access",
                newName: "TegirmonHisoblarId");

            migrationBuilder.UpdateData(
                table: "tegirmon_close",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_date_time", "updated_date_time" },
                values: new object[] { new DateTime(2025, 11, 28, 12, 57, 16, 347, DateTimeKind.Local).AddTicks(1739), new DateTime(2025, 11, 28, 12, 57, 16, 347, DateTimeKind.Local).AddTicks(6076) });

            migrationBuilder.CreateIndex(
                name: "IX_tegirmon_user_hisob_access_TegirmonHisoblarId",
                table: "tegirmon_user_hisob_access",
                column: "TegirmonHisoblarId");

            migrationBuilder.AddForeignKey(
                name: "FK_tegirmon_user_hisob_access_tegirmon_hisoblar_TegirmonHisobl~",
                table: "tegirmon_user_hisob_access",
                column: "TegirmonHisoblarId",
                principalTable: "tegirmon_hisoblar",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tegirmon_user_hisob_access_tegirmon_hisoblar_TegirmonHisobl~",
                table: "tegirmon_user_hisob_access");

            migrationBuilder.DropIndex(
                name: "IX_tegirmon_user_hisob_access_TegirmonHisoblarId",
                table: "tegirmon_user_hisob_access");

            migrationBuilder.RenameColumn(
                name: "TegirmonHisoblarId",
                table: "tegirmon_user_hisob_access",
                newName: "TegirmonHisobId");

            migrationBuilder.AddColumn<long>(
                name: "hisobid",
                table: "tegirmon_user_hisob_access",
                type: "bigint",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "tegirmon_close",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_date_time", "updated_date_time" },
                values: new object[] { new DateTime(2025, 11, 28, 12, 47, 35, 250, DateTimeKind.Local).AddTicks(8262), new DateTime(2025, 11, 28, 12, 47, 35, 251, DateTimeKind.Local).AddTicks(2199) });

            migrationBuilder.CreateIndex(
                name: "IX_tegirmon_user_hisob_access_hisobid",
                table: "tegirmon_user_hisob_access",
                column: "hisobid");

            migrationBuilder.AddForeignKey(
                name: "FK_tegirmon_user_hisob_access_tegirmon_skladlar_hisobid",
                table: "tegirmon_user_hisob_access",
                column: "hisobid",
                principalTable: "tegirmon_skladlar",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
