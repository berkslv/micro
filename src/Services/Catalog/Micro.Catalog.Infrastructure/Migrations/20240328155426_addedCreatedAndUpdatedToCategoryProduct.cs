using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Micro.Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedCreatedAndUpdatedToCategoryProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "created",
                table: "category_products",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "created_by",
                table: "category_products",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "id",
                table: "category_products",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "last_modified",
                table: "category_products",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "last_modified_by",
                table: "category_products",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "created",
                table: "category_products");

            migrationBuilder.DropColumn(
                name: "created_by",
                table: "category_products");

            migrationBuilder.DropColumn(
                name: "id",
                table: "category_products");

            migrationBuilder.DropColumn(
                name: "last_modified",
                table: "category_products");

            migrationBuilder.DropColumn(
                name: "last_modified_by",
                table: "category_products");
        }
    }
}
