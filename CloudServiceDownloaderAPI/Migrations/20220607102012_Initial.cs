using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CloudServiceDownloaderAPI.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "share_links",
                columns: table => new
                {
                    share_link_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    link = table.Column<string>(type: "text", nullable: true),
                    cloud_service = table.Column<int>(type: "integer", nullable: false),
                    is_downloaded = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_share_links", x => x.share_link_id);
                });

            migrationBuilder.CreateTable(
                name: "files",
                columns: table => new
                {
                    file_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    file_name = table.Column<string>(type: "text", nullable: true),
                    download_time = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    file_path = table.Column<string>(type: "text", nullable: true),
                    share_link_id = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_files", x => x.file_id);
                    table.ForeignKey(
                        name: "fk_files_share_links_share_link_id",
                        column: x => x.share_link_id,
                        principalTable: "share_links",
                        principalColumn: "share_link_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_files_file_path",
                table: "files",
                column: "file_path",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_files_share_link_id",
                table: "files",
                column: "share_link_id");

            migrationBuilder.CreateIndex(
                name: "ix_share_links_link",
                table: "share_links",
                column: "link",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "files");

            migrationBuilder.DropTable(
                name: "share_links");
        }
    }
}
