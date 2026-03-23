using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace ApiAll.Migrations
{
    public partial class initial34534543121212 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "tegirmon_user_sklad_access",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    name = table.Column<string>(type: "text", nullable: true),
                    text_2 = table.Column<string>(type: "text", nullable: true),
                    TegirmonUserid = table.Column<long>(type: "bigint", nullable: false),
                    TegirmonSkladId = table.Column<long>(type: "bigint", nullable: false),
                    active_status = table.Column<bool>(type: "boolean", nullable: false),
                    created_date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    updated_date_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    inv_accepted_status = table.Column<bool>(type: "boolean", nullable: false),
                    auth_user_creator_id = table.Column<long>(type: "bigint", nullable: true),
                    auth_user_updator_id = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tegirmon_user_sklad_access", x => x.id);
                    table.ForeignKey(
                        name: "FK_tegirmon_user_sklad_access_tegirmon_skladlar_TegirmonSkladId",
                        column: x => x.TegirmonSkladId,
                        principalTable: "tegirmon_skladlar",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tegirmon_user_sklad_access_tegirmon_user_TegirmonUserid",
                        column: x => x.TegirmonUserid,
                        principalTable: "tegirmon_user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tegirmon_user_sklad_access_TegirmonSkladId",
                table: "tegirmon_user_sklad_access",
                column: "TegirmonSkladId");

            migrationBuilder.CreateIndex(
                name: "IX_tegirmon_user_sklad_access_TegirmonUserid",
                table: "tegirmon_user_sklad_access",
                column: "TegirmonUserid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "tegirmon_user_sklad_access");
        }
    }
}
