using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GeneralStore.Api.Migrations
{
    public partial class First : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    ManufacturerId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Manufacturers",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("42727d34-66d1-41da-9f5b-6991b869d140"), "Timmy" });

            migrationBuilder.InsertData(
                table: "Manufacturers",
                columns: new[] { "Id", "Name" },
                values: new object[] { new Guid("6ced6baf-1da9-499b-96c2-db3f6e3cf062"), "Papierex" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "Description", "ManufacturerId", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("9e64a226-43df-487e-aeae-883f40cab282"), "Very good, very fast", new Guid("42727d34-66d1-41da-9f5b-6991b869d140"), "Toy car", 120.0m },
                    { new Guid("4864203a-f343-4f51-ad6a-edeffe2bc77c"), "Very pretty doll", new Guid("42727d34-66d1-41da-9f5b-6991b869d140"), "Doll", 15.5m },
                    { new Guid("acb091e9-3440-4e1b-8c72-3f7d07ffdb45"), "White paper for printers", new Guid("6ced6baf-1da9-499b-96c2-db3f6e3cf062"), "500 paper sheets", 40m },
                    { new Guid("e8aab2b0-3092-4b63-a128-41730f06cb80"), "Pretty notepad with 60 pages", new Guid("6ced6baf-1da9-499b-96c2-db3f6e3cf062"), "Pretty A4 notepad", 20m },
                    { new Guid("53ce0364-8501-447d-953b-5a24f9a99a8d"), "Cheap A4 notepad, 50 pages", new Guid("6ced6baf-1da9-499b-96c2-db3f6e3cf062"), "Ugly, but cheap A4 notepad", 8m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_ManufacturerId",
                table: "Products",
                column: "ManufacturerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Manufacturers");
        }
    }
}
