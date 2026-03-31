using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ApiAll.Migrations
{
    public partial class initial345339871312 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "skud_user_bonus",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    note = table.Column<string>(type: "text", nullable: true),
                    count = table.Column<double>(type: "double precision", nullable: false),
                    reg_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    num_1 = table.Column<int>(type: "integer", nullable: true),
                    num_2 = table.Column<int>(type: "integer", nullable: true),
                    str_1 = table.Column<string>(type: "text", nullable: true),
                    str_2 = table.Column<string>(type: "text", nullable: true),
                    userid = table.Column<long>(type: "bigint", nullable: true),
                    user_name = table.Column<string>(type: "text", nullable: true),
                    active_status = table.Column<bool>(type: "boolean", nullable: true),
                    created_date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_skud_user_bonus", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tegirmon_hikvision_camera",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    ip = table.Column<string>(type: "text", nullable: true),
                    username = table.Column<string>(type: "text", nullable: true),
                    password = table.Column<string>(type: "text", nullable: true),
                    active_status = table.Column<bool>(type: "boolean", nullable: false),
                    created_date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    inv_accepted_status = table.Column<bool>(type: "boolean", nullable: false),
                    auth_user_creator_id = table.Column<long>(type: "bigint", nullable: true),
                    auth_user_updator_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tegirmon_hikvision_camera", x => x.id);
                });

            migrationBuilder.UpdateData(
                table: "tegirmon_close",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_date_time", "updated_date_time" },
                values: new object[] { new DateTime(2026, 3, 24, 16, 36, 31, 396, DateTimeKind.Local).AddTicks(7013), new DateTime(2026, 3, 24, 16, 36, 31, 397, DateTimeKind.Local).AddTicks(1420) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "skud_user_bonus");

            migrationBuilder.DropTable(
                name: "tegirmon_hikvision_camera");

            migrationBuilder.UpdateData(
                table: "tegirmon_close",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_date_time", "updated_date_time" },
                values: new object[] { new DateTime(2026, 1, 26, 13, 42, 49, 72, DateTimeKind.Local).AddTicks(8159), new DateTime(2026, 1, 26, 13, 42, 49, 73, DateTimeKind.Local).AddTicks(2383) });
        }
    }
}
