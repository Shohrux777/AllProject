using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ApiAll.Migrations
{
    public partial class add_default_close_row : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "TegirmonProductid",
                table: "TegirmonClientOstatakaSumInfo",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "image_url",
                table: "tegirmon_tortilgan_bugdoy_royxati",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "phone_number",
                table: "tegirmon_tortilgan_bugdoy_royxati",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "photo_url",
                table: "tegirmon_sklad_history",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "enough_qty",
                table: "tegirmon_invoice_item",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "for_money",
                table: "tegirmon_invoice_item",
                type: "boolean",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "kassa_check_id",
                table: "tegirmon_invoice",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "zaxira_extra_amount",
                table: "tegirmon_invoice",
                type: "double precision",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tegirmon_close",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    status = table.Column<bool>(type: "boolean", nullable: false),
                    active_status = table.Column<bool>(type: "boolean", nullable: false),
                    created_date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    inv_accepted_status = table.Column<bool>(type: "boolean", nullable: false),
                    auth_user_creator_id = table.Column<long>(type: "bigint", nullable: true),
                    auth_user_updator_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tegirmon_close", x => x.id);
                });

            migrationBuilder.InsertData(
                table: "tegirmon_close",
                columns: new[] { "id", "active_status", "auth_user_creator_id", "auth_user_updator_id", "created_date_time", "inv_accepted_status", "status", "updated_date_time" },
                values: new object[] { 1L, true, 0L, 0L, new DateTime(2025, 11, 28, 10, 9, 17, 956, DateTimeKind.Local).AddTicks(9588), false, false, new DateTime(2025, 11, 28, 10, 9, 17, 957, DateTimeKind.Local).AddTicks(3766) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tegirmon_close");

            migrationBuilder.DropColumn(
                name: "TegirmonProductid",
                table: "TegirmonClientOstatakaSumInfo");

            migrationBuilder.DropColumn(
                name: "image_url",
                table: "tegirmon_tortilgan_bugdoy_royxati");

            migrationBuilder.DropColumn(
                name: "phone_number",
                table: "tegirmon_tortilgan_bugdoy_royxati");

            migrationBuilder.DropColumn(
                name: "photo_url",
                table: "tegirmon_sklad_history");

            migrationBuilder.DropColumn(
                name: "enough_qty",
                table: "tegirmon_invoice_item");

            migrationBuilder.DropColumn(
                name: "for_money",
                table: "tegirmon_invoice_item");

            migrationBuilder.DropColumn(
                name: "kassa_check_id",
                table: "tegirmon_invoice");

            migrationBuilder.DropColumn(
                name: "zaxira_extra_amount",
                table: "tegirmon_invoice");
        }
    }
}
