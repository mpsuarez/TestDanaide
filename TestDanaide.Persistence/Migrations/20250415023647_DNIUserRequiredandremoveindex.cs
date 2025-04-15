using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestDanaide.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class DNIUserRequiredandremoveindex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CartProducts_CartId_ProductId",
                table: "CartProducts");

            migrationBuilder.CreateIndex(
                name: "IX_CartProducts_CartId",
                table: "CartProducts",
                column: "CartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CartProducts_CartId",
                table: "CartProducts");

            migrationBuilder.CreateIndex(
                name: "IX_CartProducts_CartId_ProductId",
                table: "CartProducts",
                columns: new[] { "CartId", "ProductId" },
                unique: true);
        }
    }
}
