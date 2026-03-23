using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ApiAll.Migrations
{
    public partial class initial3453454312121 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "optom_load",
                table: "tegirmon_user_access",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "optom_pay",
                table: "tegirmon_user_access",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "optom_statis",
                table: "tegirmon_user_access",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<long>(
                name: "auth_id",
                table: "tegirmon_sklad_diff",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<double>(
                name: "product_price",
                table: "tegirmon_sklad_diff",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "user_name",
                table: "tegirmon_sklad_diff",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "sklad_diff_id",
                table: "tegirmon_invoice",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tegirmon_ticket",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ticket_number = table.Column<string>(type: "text", nullable: true),
                    massa = table.Column<double>(type: "double precision", nullable: false),
                    organization_name = table.Column<string>(type: "text", nullable: true),
                    tegirmonHisoblar_id = table.Column<long>(type: "bigint", nullable: true),
                    note = table.Column<string>(type: "text", nullable: true),
                    tegirmonAuthId = table.Column<long>(type: "bigint", nullable: true),
                    active_status = table.Column<bool>(type: "boolean", nullable: false),
                    created_date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    inv_accepted_status = table.Column<bool>(type: "boolean", nullable: false),
                    auth_user_creator_id = table.Column<long>(type: "bigint", nullable: true),
                    auth_user_updator_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tegirmon_ticket", x => x.id);
                });

            migrationBuilder.UpdateData(
                table: "tegirmon_close",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_date_time", "updated_date_time" },
                values: new object[] { new DateTime(2025, 12, 12, 13, 22, 48, 105, DateTimeKind.Local).AddTicks(2118), new DateTime(2025, 12, 12, 13, 22, 48, 105, DateTimeKind.Local).AddTicks(6993) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tegirmon_ticket");

            migrationBuilder.DropColumn(
                name: "optom_load",
                table: "tegirmon_user_access");

            migrationBuilder.DropColumn(
                name: "optom_pay",
                table: "tegirmon_user_access");

            migrationBuilder.DropColumn(
                name: "optom_statis",
                table: "tegirmon_user_access");

            migrationBuilder.DropColumn(
                name: "auth_id",
                table: "tegirmon_sklad_diff");

            migrationBuilder.DropColumn(
                name: "product_price",
                table: "tegirmon_sklad_diff");

            migrationBuilder.DropColumn(
                name: "user_name",
                table: "tegirmon_sklad_diff");

            migrationBuilder.DropColumn(
                name: "sklad_diff_id",
                table: "tegirmon_invoice");

            migrationBuilder.UpdateData(
                table: "tegirmon_close",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_date_time", "updated_date_time" },
                values: new object[] { new DateTime(2025, 12, 1, 15, 26, 8, 853, DateTimeKind.Local).AddTicks(3000), new DateTime(2025, 12, 1, 15, 26, 8, 853, DateTimeKind.Local).AddTicks(7896) });
        }
    }
}
