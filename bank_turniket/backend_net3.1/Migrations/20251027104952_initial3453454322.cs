using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ApiAll.Migrations
{
    public partial class initial3453454322 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "all_qty",
                table: "tegirmon_order_item",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "new_qty",
                table: "tegirmon_order_item",
                type: "double precision",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "zaxira",
                table: "tegirmon_order_item",
                type: "boolean",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tegirmon_category",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    color = table.Column<string>(type: "text", nullable: true),
                    bot_id = table.Column<long>(type: "bigint", nullable: true),
                    active_status = table.Column<bool>(type: "boolean", nullable: false),
                    created_date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    inv_accepted_status = table.Column<bool>(type: "boolean", nullable: false),
                    auth_user_creator_id = table.Column<long>(type: "bigint", nullable: true),
                    auth_user_updator_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tegirmon_category", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tegirmon_category");

            migrationBuilder.DropColumn(
                name: "all_qty",
                table: "tegirmon_order_item");

            migrationBuilder.DropColumn(
                name: "new_qty",
                table: "tegirmon_order_item");

            migrationBuilder.DropColumn(
                name: "zaxira",
                table: "tegirmon_order_item");
        }
    }
}
