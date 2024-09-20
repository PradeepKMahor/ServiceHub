using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServiceHub.Domain.Migrations
{
    /// <inheritdoc />
    public partial class NewColInUserClint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.AddColumn<string>(
                name: "MiddleName",
                table: "TblUserClint",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            _ = migrationBuilder.DropColumn(
                name: "MiddleName",
                table: "TblUserClint");
        }
    }
}
