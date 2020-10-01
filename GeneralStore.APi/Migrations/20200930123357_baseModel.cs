using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GeneralStore.Api.Migrations
{
    public partial class baseModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ParentCategoryId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Categories_Categories_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
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
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    ManufacturerId = table.Column<Guid>(nullable: false),
                    CategoryId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "IsDeleted", "Name", "ParentCategoryId" },
                values: new object[,]
                {
                    { new Guid("8c02b200-10de-40a7-a1c5-e963947f5697"), false, "Toys", null },
                    { new Guid("ef001e3f-472d-46b1-a8f8-32a15ebbc78b"), false, "Paper products", null }
                });

            migrationBuilder.InsertData(
                table: "Manufacturers",
                columns: new[] { "Id", "IsDeleted", "Name" },
                values: new object[,]
                {
                    { new Guid("42727d34-66d1-41da-9f5b-6991b869d140"), false, "Timmy" },
                    { new Guid("6ced6baf-1da9-499b-96c2-db3f6e3cf062"), false, "Papierex" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "IsDeleted", "ManufacturerId", "Name", "Price" },
                values: new object[,]
                {
                    { new Guid("9e64a226-43df-487e-aeae-883f40cab282"), new Guid("8c02b200-10de-40a7-a1c5-e963947f5697"), "Very good, very fast", false, new Guid("42727d34-66d1-41da-9f5b-6991b869d140"), "Toy car", 120.0m },
                    { new Guid("4864203a-f343-4f51-ad6a-edeffe2bc77c"), new Guid("8c02b200-10de-40a7-a1c5-e963947f5697"), "Very pretty doll", false, new Guid("42727d34-66d1-41da-9f5b-6991b869d140"), "Doll", 15.5m },
                    { new Guid("acb091e9-3440-4e1b-8c72-3f7d07ffdb45"), new Guid("ef001e3f-472d-46b1-a8f8-32a15ebbc78b"), "White paper for printers", false, new Guid("6ced6baf-1da9-499b-96c2-db3f6e3cf062"), "500 paper sheets", 40m },
                    { new Guid("e8aab2b0-3092-4b63-a128-41730f06cb80"), new Guid("ef001e3f-472d-46b1-a8f8-32a15ebbc78b"), "Pretty notepad with 60 pages", false, new Guid("6ced6baf-1da9-499b-96c2-db3f6e3cf062"), "Pretty A4 notepad", 20m },
                    { new Guid("53ce0364-8501-447d-953b-5a24f9a99a8d"), new Guid("ef001e3f-472d-46b1-a8f8-32a15ebbc78b"), "Cheap A4 notepad, 50 pages", false, new Guid("6ced6baf-1da9-499b-96c2-db3f6e3cf062"), "Ugly, but cheap A4 notepad", 8m }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParentCategoryId",
                table: "Categories",
                column: "ParentCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

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
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Manufacturers");
        }
    }
}
