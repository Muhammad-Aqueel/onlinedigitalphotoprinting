using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace digitalphotoprinting.Data.Migrations
{
    /// <inheritdoc />
    public partial class image_titleAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Image_Title",
                table: "Purchase_Order",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image_Title",
                table: "Purchase_Order");
        }
    }
}
