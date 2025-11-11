using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace digitalphotoprinting.Data.Migrations
{
    /// <inheritdoc />
    public partial class imageStatusAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Purchase_Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Purchase_Order");
        }
    }
}
