using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ApiAll.Migrations
{
    public partial class initial345345431212 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "client_name",
                table: "tegirmon_order_check",
                type: "text",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "tegirmon_qarz_user_type",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    sum = table.Column<double>(type: "double precision", nullable: false),
                    dollor = table.Column<double>(type: "double precision", nullable: false),
                    all_summ = table.Column<double>(type: "double precision", nullable: false),
                    kurs = table.Column<double>(type: "double precision", nullable: false),
                    note = table.Column<string>(type: "text", nullable: true),
                    image_url = table.Column<string>(type: "text", nullable: true),
                    addiotionala_information = table.Column<string>(type: "text", nullable: true),
                    TegirmonQarzUserId = table.Column<long>(type: "bigint", nullable: true),
                    userid = table.Column<long>(type: "bigint", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    reserve = table.Column<string>(type: "text", nullable: true),
                    reserve_val = table.Column<double>(type: "double precision", nullable: false),
                    active_status = table.Column<bool>(type: "boolean", nullable: false),
                    created_date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    inv_accepted_status = table.Column<bool>(type: "boolean", nullable: false),
                    auth_user_creator_id = table.Column<long>(type: "bigint", nullable: true),
                    auth_user_updator_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tegirmon_qarz_user_type", x => x.id);
                    table.ForeignKey(
                        name: "FK_tegirmon_qarz_user_type_tegirmon_qarzdorlik_user_userid",
                        column: x => x.userid,
                        principalTable: "tegirmon_qarzdorlik_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "tegirmon_qarz_user_rasxod_type",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    worker_name = table.Column<string>(type: "text", nullable: true),
                    passport_number = table.Column<string>(type: "text", nullable: true),
                    phone_number = table.Column<string>(type: "text", nullable: true),
                    sum = table.Column<double>(type: "double precision", nullable: false),
                    sum_str = table.Column<string>(type: "text", nullable: true),
                    all_summ = table.Column<double>(type: "double precision", nullable: false),
                    all_summ_string = table.Column<string>(type: "text", nullable: true),
                    status_rasxod = table.Column<long>(type: "bigint", nullable: false),
                    dollor = table.Column<double>(type: "double precision", nullable: false),
                    dollor_string = table.Column<string>(type: "text", nullable: true),
                    kurs = table.Column<double>(type: "double precision", nullable: false),
                    note = table.Column<string>(type: "text", nullable: true),
                    image_url = table.Column<string>(type: "text", nullable: true),
                    addiotionala_information = table.Column<string>(type: "text", nullable: true),
                    TegirmonQarzUserId = table.Column<long>(type: "bigint", nullable: true),
                    userid = table.Column<long>(type: "bigint", nullable: true),
                    TegirmonQarzUserTypeId = table.Column<long>(type: "bigint", nullable: true),
                    created_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    udate_date = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    bot_id = table.Column<long>(type: "bigint", nullable: true),
                    reserve = table.Column<string>(type: "text", nullable: true),
                    reserve1 = table.Column<string>(type: "text", nullable: true),
                    reserve_val = table.Column<double>(type: "double precision", nullable: false),
                    reserve_val_1 = table.Column<double>(type: "double precision", nullable: false),
                    reserve_val_2 = table.Column<double>(type: "double precision", nullable: false),
                    active_status = table.Column<bool>(type: "boolean", nullable: false),
                    created_date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    inv_accepted_status = table.Column<bool>(type: "boolean", nullable: false),
                    auth_user_creator_id = table.Column<long>(type: "bigint", nullable: true),
                    auth_user_updator_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tegirmon_qarz_user_rasxod_type", x => x.id);
                    table.ForeignKey(
                        name: "FK_tegirmon_qarz_user_rasxod_type_tegirmon_qarz_user_type_Tegi~",
                        column: x => x.TegirmonQarzUserTypeId,
                        principalTable: "tegirmon_qarz_user_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_tegirmon_qarz_user_rasxod_type_tegirmon_qarzdorlik_user_use~",
                        column: x => x.userid,
                        principalTable: "tegirmon_qarzdorlik_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tegirmon_qarz_user_rasxod_type_TegirmonQarzUserTypeId",
                table: "tegirmon_qarz_user_rasxod_type",
                column: "TegirmonQarzUserTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_tegirmon_qarz_user_rasxod_type_userid",
                table: "tegirmon_qarz_user_rasxod_type",
                column: "userid");

            migrationBuilder.CreateIndex(
                name: "IX_tegirmon_qarz_user_type_userid",
                table: "tegirmon_qarz_user_type",
                column: "userid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tegirmon_qarz_user_rasxod_type");

            migrationBuilder.DropTable(
                name: "tegirmon_qarz_user_type");

            migrationBuilder.DropColumn(
                name: "client_name",
                table: "tegirmon_order_check");
        }
    }
}
