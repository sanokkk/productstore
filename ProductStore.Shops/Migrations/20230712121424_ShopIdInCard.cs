using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductStore.Shops.Migrations
{
    /// <inheritdoc />
    public partial class ShopIdInCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "shops",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.AddColumn<int>(
                name: "ShopId",
                table: "cards",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "cards");

            migrationBuilder.InsertData(
                table: "shops",
                columns: new[] { "Id", "Address", "Name" },
                values: new object[] { 1, "Балашиха, бульвар Нестерова 9, к.2", "Пятерочка" });
        }
    }
}
