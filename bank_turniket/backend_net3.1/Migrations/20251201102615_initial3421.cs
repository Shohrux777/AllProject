using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ApiAll.Migrations
{
    public partial class initial3421 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tegirmon_sklad_diff",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    TegirmonSkladId = table.Column<long>(type: "bigint", nullable: false),
                    TegirmonProductId = table.Column<long>(type: "bigint", nullable: false),
                    ostatka = table.Column<double>(type: "double precision", nullable: false),
                    qty = table.Column<double>(type: "double precision", nullable: false),
                    diff = table.Column<double>(type: "double precision", nullable: false),
                    unit = table.Column<string>(type: "text", nullable: true),
                    product_name = table.Column<string>(type: "text", nullable: true),
                    create_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    active_status = table.Column<bool>(type: "boolean", nullable: false),
                    created_date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    inv_accepted_status = table.Column<bool>(type: "boolean", nullable: false),
                    auth_user_creator_id = table.Column<long>(type: "bigint", nullable: true),
                    auth_user_updator_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tegirmon_sklad_diff", x => x.id);
                    table.ForeignKey(
                        name: "FK_tegirmon_sklad_diff_tegirmon_product_TegirmonProductId",
                        column: x => x.TegirmonProductId,
                        principalTable: "tegirmon_product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tegirmon_sklad_diff_tegirmon_skladlar_TegirmonSkladId",
                        column: x => x.TegirmonSkladId,
                        principalTable: "tegirmon_skladlar",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "tegirmon_close",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_date_time", "updated_date_time" },
                values: new object[] { new DateTime(2025, 12, 1, 15, 26, 8, 853, DateTimeKind.Local).AddTicks(3000), new DateTime(2025, 12, 1, 15, 26, 8, 853, DateTimeKind.Local).AddTicks(7896) });

            migrationBuilder.CreateIndex(
                name: "IX_tegirmon_sklad_diff_TegirmonProductId",
                table: "tegirmon_sklad_diff",
                column: "TegirmonProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tegirmon_sklad_diff_TegirmonSkladId",
                table: "tegirmon_sklad_diff",
                column: "TegirmonSkladId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tegirmon_sklad_diff");

            migrationBuilder.UpdateData(
                table: "tegirmon_close",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_date_time", "updated_date_time" },
                values: new object[] { new DateTime(2025, 11, 28, 12, 57, 16, 347, DateTimeKind.Local).AddTicks(1739), new DateTime(2025, 11, 28, 12, 57, 16, 347, DateTimeKind.Local).AddTicks(6076) });
        }
    }
}
