using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Centenary.Mvc.Data.Migrations
{
    public partial class CreatedRename : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UploadedOn",
                table: "Document",
                newName: "CreatedOn");

            migrationBuilder.RenameColumn(
                name: "UploadedBy",
                table: "Document",
                newName: "CreatedBy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CreatedOn",
                table: "Document",
                newName: "UploadedOn");

            migrationBuilder.RenameColumn(
                name: "CreatedBy",
                table: "Document",
                newName: "UploadedBy");
        }
    }
}
