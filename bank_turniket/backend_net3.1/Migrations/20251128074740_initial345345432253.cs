using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ApiAll.Migrations
{
    public partial class initial345345432253 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tegirmon_user_hisob_access",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    text_2 = table.Column<string>(type: "text", nullable: true),
                    TegirmonUserid = table.Column<long>(type: "bigint", nullable: false),
                    TegirmonHisobId = table.Column<long>(type: "bigint", nullable: false),
                    hisobid = table.Column<long>(type: "bigint", nullable: true),
                    active_status = table.Column<bool>(type: "boolean", nullable: false),
                    created_date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    inv_accepted_status = table.Column<bool>(type: "boolean", nullable: false),
                    auth_user_creator_id = table.Column<long>(type: "bigint", nullable: true),
                    auth_user_updator_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tegirmon_user_hisob_access", x => x.id);
                    table.ForeignKey(
                        name: "FK_tegirmon_user_hisob_access_tegirmon_skladlar_hisobid",
                        column: x => x.hisobid,
                        principalTable: "tegirmon_skladlar",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tegirmon_user_hisob_access_tegirmon_user_TegirmonUserid",
                        column: x => x.TegirmonUserid,
                        principalTable: "tegirmon_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

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

            migrationBuilder.CreateIndex(
                name: "IX_tegirmon_user_hisob_access_TegirmonUserid",
                table: "tegirmon_user_hisob_access",
                column: "TegirmonUserid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tegirmon_user_hisob_access");

            migrationBuilder.UpdateData(
                table: "tegirmon_close",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_date_time", "updated_date_time" },
                values: new object[] { new DateTime(2025, 11, 28, 10, 15, 35, 353, DateTimeKind.Local).AddTicks(1710), new DateTime(2025, 11, 28, 10, 15, 35, 353, DateTimeKind.Local).AddTicks(6219) });
        }
    }
}
