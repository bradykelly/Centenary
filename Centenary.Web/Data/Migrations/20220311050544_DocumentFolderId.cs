using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Centenary.Mvc.Data.Migrations
{
    public partial class DocumentFolderId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Documents_Name_Folder",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "Folder",
                table: "Documents");

            migrationBuilder.AddColumn<int>(
                name: "FolderId",
                table: "Documents",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documents_Name_FolderId",
                table: "Documents",
                columns: new[] { "Name", "FolderId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Documents_Name_FolderId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "FolderId",
                table: "Documents");

            migrationBuilder.AddColumn<string>(
                name: "Folder",
                table: "Documents",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Documents_Name_Folder",
                table: "Documents",
                columns: new[] { "Name", "Folder" },
                unique: true);
        }
    }
}
