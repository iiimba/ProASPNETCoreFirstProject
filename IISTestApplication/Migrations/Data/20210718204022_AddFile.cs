using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IISTestApplication.Migrations.Data
{
    public partial class AddFile : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "FileId",
                table: "FileMetadatas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Files",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FileBody = table.Column<byte[]>(type: "varbinary(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Files", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileMetadatas_FileId",
                table: "FileMetadatas",
                column: "FileId");

            migrationBuilder.AddForeignKey(
                name: "FK_FileMetadatas_Files_FileId",
                table: "FileMetadatas",
                column: "FileId",
                principalTable: "Files",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileMetadatas_Files_FileId",
                table: "FileMetadatas");

            migrationBuilder.DropTable(
                name: "Files");

            migrationBuilder.DropIndex(
                name: "IX_FileMetadatas_FileId",
                table: "FileMetadatas");

            migrationBuilder.DropColumn(
                name: "FileId",
                table: "FileMetadatas");
        }
    }
}
