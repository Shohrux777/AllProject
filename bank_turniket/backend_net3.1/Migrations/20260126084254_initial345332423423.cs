using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ApiAll.Migrations
{
    public partial class initial345332423423 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tegirmon_tortilgan_bugdoy_royxati_image",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    TegirmonTortilganBugdoyRoyxatiGroupDetailid = table.Column<long>(type: "bigint", nullable: true),
                    TegirmonTortilganBugdoyRoyxatiid = table.Column<long>(type: "bigint", nullable: true),
                    image_url = table.Column<string>(type: "text", nullable: true),
                    image_url_2 = table.Column<string>(type: "text", nullable: true),
                    active_status = table.Column<bool>(type: "boolean", nullable: false),
                    created_date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    inv_accepted_status = table.Column<bool>(type: "boolean", nullable: false),
                    auth_user_creator_id = table.Column<long>(type: "bigint", nullable: true),
                    auth_user_updator_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tegirmon_tortilgan_bugdoy_royxati_image", x => x.id);
                    table.ForeignKey(
                        name: "FK_tegirmon_tortilgan_bugdoy_royxati_image_tegirmon_tortilgan_~",
                        column: x => x.TegirmonTortilganBugdoyRoyxatiGroupDetailid,
                        principalTable: "tegirmon_tortilgan_bugdoy_royxati_group_detail",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tegirmon_tortilgan_bugdoy_royxati_image_tegirmon_tortilgan~1",
                        column: x => x.TegirmonTortilganBugdoyRoyxatiid,
                        principalTable: "tegirmon_tortilgan_bugdoy_royxati",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tegirmon_user_avans_oylik",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    userid = table.Column<long>(type: "bigint", nullable: true),
                    salary_id = table.Column<long>(type: "bigint", nullable: true),
                    sum = table.Column<double>(type: "double precision", nullable: false),
                    dollor = table.Column<double>(type: "double precision", nullable: false),
                    num = table.Column<double>(type: "double precision", nullable: false),
                    b_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    e_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    note = table.Column<string>(type: "text", nullable: true),
                    image_url = table.Column<string>(type: "text", nullable: true),
                    auth_name = table.Column<string>(type: "text", nullable: true),
                    auth_id = table.Column<long>(type: "bigint", nullable: true),
                    avans = table.Column<bool>(type: "boolean", nullable: false),
                    salary = table.Column<bool>(type: "boolean", nullable: false),
                    hisoblan_sum = table.Column<double>(type: "double precision", nullable: true),
                    qarz_sum = table.Column<double>(type: "double precision", nullable: true),
                    additional_qarz_sum = table.Column<double>(type: "double precision", nullable: true),
                    hisoblan_dollor = table.Column<double>(type: "double precision", nullable: true),
                    qarz_dollor = table.Column<double>(type: "double precision", nullable: true),
                    additional_qarz_dollor = table.Column<double>(type: "double precision", nullable: true),
                    old_qarz = table.Column<double>(type: "double precision", nullable: true),
                    old_debt = table.Column<double>(type: "double precision", nullable: true),
                    num1 = table.Column<double>(type: "double precision", nullable: true),
                    active_status = table.Column<bool>(type: "boolean", nullable: false),
                    created_date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    inv_accepted_status = table.Column<bool>(type: "boolean", nullable: false),
                    auth_user_creator_id = table.Column<long>(type: "bigint", nullable: true),
                    auth_user_updator_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tegirmon_user_avans_oylik", x => x.id);
                });

            migrationBuilder.UpdateData(
                table: "tegirmon_close",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_date_time", "updated_date_time" },
                values: new object[] { new DateTime(2026, 1, 26, 13, 42, 49, 72, DateTimeKind.Local).AddTicks(8159), new DateTime(2026, 1, 26, 13, 42, 49, 73, DateTimeKind.Local).AddTicks(2383) });

            migrationBuilder.CreateIndex(
                name: "IX_tegirmon_tortilgan_bugdoy_royxati_image_TegirmonTortilganB~1",
                table: "tegirmon_tortilgan_bugdoy_royxati_image",
                column: "TegirmonTortilganBugdoyRoyxatiid");

            migrationBuilder.CreateIndex(
                name: "IX_tegirmon_tortilgan_bugdoy_royxati_image_TegirmonTortilganBu~",
                table: "tegirmon_tortilgan_bugdoy_royxati_image",
                column: "TegirmonTortilganBugdoyRoyxatiGroupDetailid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tegirmon_tortilgan_bugdoy_royxati_image");

            migrationBuilder.DropTable(
                name: "tegirmon_user_avans_oylik");

            migrationBuilder.UpdateData(
                table: "tegirmon_close",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_date_time", "updated_date_time" },
                values: new object[] { new DateTime(2025, 12, 20, 16, 58, 38, 572, DateTimeKind.Local).AddTicks(970), new DateTime(2025, 12, 20, 16, 58, 38, 572, DateTimeKind.Local).AddTicks(6755) });
        }
    }
}
