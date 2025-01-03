using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ProductGroupProductTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductGroupProduct_ProductGroups_ProductGroupId",
                table: "ProductGroupProduct");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductGroupProduct_Products_ProductId",
                table: "ProductGroupProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductGroupProduct",
                table: "ProductGroupProduct");

            migrationBuilder.RenameTable(
                name: "ProductGroupProduct",
                newName: "ProductGroupProducts");

            migrationBuilder.RenameIndex(
                name: "IX_ProductGroupProduct_ProductId",
                table: "ProductGroupProducts",
                newName: "IX_ProductGroupProducts_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductGroupProduct_ProductGroupId",
                table: "ProductGroupProducts",
                newName: "IX_ProductGroupProducts_ProductGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductGroupProducts",
                table: "ProductGroupProducts",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductGroupProducts_ProductGroups_ProductGroupId",
                table: "ProductGroupProducts",
                column: "ProductGroupId",
                principalTable: "ProductGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductGroupProducts_Products_ProductId",
                table: "ProductGroupProducts",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductGroupProducts_ProductGroups_ProductGroupId",
                table: "ProductGroupProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductGroupProducts_Products_ProductId",
                table: "ProductGroupProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductGroupProducts",
                table: "ProductGroupProducts");

            migrationBuilder.RenameTable(
                name: "ProductGroupProducts",
                newName: "ProductGroupProduct");

            migrationBuilder.RenameIndex(
                name: "IX_ProductGroupProducts_ProductId",
                table: "ProductGroupProduct",
                newName: "IX_ProductGroupProduct_ProductId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductGroupProducts_ProductGroupId",
                table: "ProductGroupProduct",
                newName: "IX_ProductGroupProduct_ProductGroupId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductGroupProduct",
                table: "ProductGroupProduct",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductGroupProduct_ProductGroups_ProductGroupId",
                table: "ProductGroupProduct",
                column: "ProductGroupId",
                principalTable: "ProductGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductGroupProduct_Products_ProductId",
                table: "ProductGroupProduct",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
