using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Micro.Catalog.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatedCategoryProduct2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "category_products");

            migrationBuilder.CreateTable(
                name: "category_product",
                columns: table => new
                {
                    categories_id = table.Column<string>(type: "text", nullable: false),
                    products_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_category_product", x => new { x.categories_id, x.products_id });
                    table.ForeignKey(
                        name: "fk_category_product_categories_categories_id",
                        column: x => x.categories_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_category_product_products_products_id",
                        column: x => x.products_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_category_product_products_id",
                table: "category_product",
                column: "products_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "category_product");

            migrationBuilder.CreateTable(
                name: "category_products",
                columns: table => new
                {
                    id = table.Column<string>(type: "text", nullable: false),
                    category_id = table.Column<string>(type: "text", nullable: false),
                    created = table.Column<long>(type: "bigint", nullable: false),
                    created_by = table.Column<string>(type: "text", nullable: true),
                    last_modified = table.Column<long>(type: "bigint", nullable: true),
                    last_modified_by = table.Column<string>(type: "text", nullable: true),
                    product_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_category_products", x => x.id);
                    table.ForeignKey(
                        name: "fk_category_products_categories_category_id",
                        column: x => x.category_id,
                        principalTable: "categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_category_products_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_category_products_category_id",
                table: "category_products",
                column: "category_id");

            migrationBuilder.CreateIndex(
                name: "ix_category_products_product_id",
                table: "category_products",
                column: "product_id");
        }
    }
}
