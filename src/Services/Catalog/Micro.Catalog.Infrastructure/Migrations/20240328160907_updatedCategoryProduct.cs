using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Micro.Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedCategoryProduct : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_category_products",
                table: "category_products");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "category_products",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "pk_category_products",
                table: "category_products",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "ix_category_products_category_id",
                table: "category_products",
                column: "category_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "pk_category_products",
                table: "category_products");

            migrationBuilder.DropIndex(
                name: "ix_category_products_category_id",
                table: "category_products");

            migrationBuilder.AlterColumn<string>(
                name: "id",
                table: "category_products",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "pk_category_products",
                table: "category_products",
                columns: new[] { "category_id", "product_id" });
        }
    }
}
