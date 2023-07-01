using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductStore.Shops.Migrations
{
    /// <inheritdoc />
    public partial class shopswithdata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "shops",
                columns: new[] { "Id", "Address", "Name" },
                values: new object[] { 1, "Балашиха, бульвар Нестерова 9, к.2", "Пятерочка" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "shops",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
