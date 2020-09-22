using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace GeneralStore.Api.Migrations
{
    public partial class AddedCategory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CategoryId",
                table: "Products",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Products",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Manufacturers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ParentCategoryId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Category_Category_ParentCategoryId",
                        column: x => x.ParentCategoryId,
                        principalTable: "Category",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "IsDeleted", "Name", "ParentCategoryId" },
                values: new object[] { new Guid("8c02b200-10de-40a7-a1c5-e963947f5697"), false, "Toys", null });

            migrationBuilder.InsertData(
                table: "Category",
                columns: new[] { "Id", "IsDeleted", "Name", "ParentCategoryId" },
                values: new object[] { new Guid("ef001e3f-472d-46b1-a8f8-32a15ebbc78b"), false, "Paper products", null });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("4864203a-f343-4f51-ad6a-edeffe2bc77c"),
                column: "CategoryId",
                value: new Guid("8c02b200-10de-40a7-a1c5-e963947f5697"));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("53ce0364-8501-447d-953b-5a24f9a99a8d"),
                column: "CategoryId",
                value: new Guid("ef001e3f-472d-46b1-a8f8-32a15ebbc78b"));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("9e64a226-43df-487e-aeae-883f40cab282"),
                column: "CategoryId",
                value: new Guid("8c02b200-10de-40a7-a1c5-e963947f5697"));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("acb091e9-3440-4e1b-8c72-3f7d07ffdb45"),
                column: "CategoryId",
                value: new Guid("ef001e3f-472d-46b1-a8f8-32a15ebbc78b"));

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumn: "Id",
                keyValue: new Guid("e8aab2b0-3092-4b63-a128-41730f06cb80"),
                column: "CategoryId",
                value: new Guid("ef001e3f-472d-46b1-a8f8-32a15ebbc78b"));

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_ParentCategoryId",
                table: "Category",
                column: "ParentCategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Category_CategoryId",
                table: "Products",
                column: "CategoryId",
                principalTable: "Category",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Products_Category_CategoryId",
                table: "Products");

            migrationBuilder.DropTable(
                name: "Category");

            migrationBuilder.DropIndex(
                name: "IX_Products_CategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Manufacturers");
        }
    }
}
