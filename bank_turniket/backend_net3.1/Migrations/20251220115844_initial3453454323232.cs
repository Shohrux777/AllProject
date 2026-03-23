using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ApiAll.Migrations
{
    public partial class initial3453454323232 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "address",
                table: "tegirmon_ticket",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "closed_date_time",
                table: "tegirmon_ticket",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "hisob_name",
                table: "tegirmon_ticket",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "invoice_massa",
                table: "tegirmon_ticket",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "is_closed",
                table: "tegirmon_ticket",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<double>(
                name: "percentage",
                table: "tegirmon_ticket",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "price",
                table: "tegirmon_ticket",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ticket_date",
                table: "tegirmon_ticket",
                type: "timestamp without time zone",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "total_price",
                table: "tegirmon_ticket",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<long>(
                name: "tegirmonticketid",
                table: "tegirmon_invoice_new",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tegirmon_sklad_qty_log",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    TegirmonSkladId = table.Column<long>(type: "bigint", nullable: false),
                    TegirmonProductId = table.Column<long>(type: "bigint", nullable: false),
                    qty = table.Column<double>(type: "double precision", nullable: false),
                    type = table.Column<long>(type: "bigint", nullable: false),
                    text = table.Column<string>(type: "text", nullable: true),
                    price = table.Column<double>(type: "double precision", nullable: false),
                    userId = table.Column<long>(type: "bigint", nullable: true),
                    authId = table.Column<long>(type: "bigint", nullable: true),
                    user_name = table.Column<string>(type: "text", nullable: true),
                    photo_url = table.Column<string>(type: "text", nullable: true),
                    create_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    note = table.Column<string>(type: "text", nullable: true),
                    operation_id = table.Column<long>(type: "bigint", nullable: true),
                    operation_type = table.Column<string>(type: "text", nullable: true),
                    active_status = table.Column<bool>(type: "boolean", nullable: false),
                    created_date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    inv_accepted_status = table.Column<bool>(type: "boolean", nullable: false),
                    auth_user_creator_id = table.Column<long>(type: "bigint", nullable: true),
                    auth_user_updator_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tegirmon_sklad_qty_log", x => x.id);
                    table.ForeignKey(
                        name: "FK_tegirmon_sklad_qty_log_tegirmon_product_TegirmonProductId",
                        column: x => x.TegirmonProductId,
                        principalTable: "tegirmon_product",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tegirmon_sklad_qty_log_tegirmon_skladlar_TegirmonSkladId",
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
                values: new object[] { new DateTime(2025, 12, 20, 16, 58, 38, 572, DateTimeKind.Local).AddTicks(970), new DateTime(2025, 12, 20, 16, 58, 38, 572, DateTimeKind.Local).AddTicks(6755) });

            migrationBuilder.CreateIndex(
                name: "IX_tegirmon_invoice_new_tegirmonticketid",
                table: "tegirmon_invoice_new",
                column: "tegirmonticketid");

            migrationBuilder.CreateIndex(
                name: "IX_tegirmon_sklad_qty_log_TegirmonProductId",
                table: "tegirmon_sklad_qty_log",
                column: "TegirmonProductId");

            migrationBuilder.CreateIndex(
                name: "IX_tegirmon_sklad_qty_log_TegirmonSkladId",
                table: "tegirmon_sklad_qty_log",
                column: "TegirmonSkladId");

            migrationBuilder.AddForeignKey(
                name: "FK_tegirmon_invoice_new_tegirmon_ticket_tegirmonticketid",
                table: "tegirmon_invoice_new",
                column: "tegirmonticketid",
                principalTable: "tegirmon_ticket",
                principalColumn: "id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tegirmon_invoice_new_tegirmon_ticket_tegirmonticketid",
                table: "tegirmon_invoice_new");

            migrationBuilder.DropTable(
                name: "tegirmon_sklad_qty_log");

            migrationBuilder.DropIndex(
                name: "IX_tegirmon_invoice_new_tegirmonticketid",
                table: "tegirmon_invoice_new");

            migrationBuilder.DropColumn(
                name: "address",
                table: "tegirmon_ticket");

            migrationBuilder.DropColumn(
                name: "closed_date_time",
                table: "tegirmon_ticket");

            migrationBuilder.DropColumn(
                name: "hisob_name",
                table: "tegirmon_ticket");

            migrationBuilder.DropColumn(
                name: "invoice_massa",
                table: "tegirmon_ticket");

            migrationBuilder.DropColumn(
                name: "is_closed",
                table: "tegirmon_ticket");

            migrationBuilder.DropColumn(
                name: "percentage",
                table: "tegirmon_ticket");

            migrationBuilder.DropColumn(
                name: "price",
                table: "tegirmon_ticket");

            migrationBuilder.DropColumn(
                name: "ticket_date",
                table: "tegirmon_ticket");

            migrationBuilder.DropColumn(
                name: "total_price",
                table: "tegirmon_ticket");

            migrationBuilder.DropColumn(
                name: "tegirmonticketid",
                table: "tegirmon_invoice_new");

            migrationBuilder.UpdateData(
                table: "tegirmon_close",
                keyColumn: "id",
                keyValue: 1L,
                columns: new[] { "created_date_time", "updated_date_time" },
                values: new object[] { new DateTime(2025, 12, 12, 13, 22, 48, 105, DateTimeKind.Local).AddTicks(2118), new DateTime(2025, 12, 12, 13, 22, 48, 105, DateTimeKind.Local).AddTicks(6993) });
        }
    }
}
