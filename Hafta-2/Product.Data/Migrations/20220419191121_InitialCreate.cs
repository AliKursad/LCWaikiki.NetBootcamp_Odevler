using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ProductAPI.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Price = table.Column<decimal>(type: "decimal(8,2)", precision: 8, scale: 2, nullable: false),
                    UnitsInStock = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "NVARCHAR(MAX)", nullable: true)
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
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "Description", "ModifiedDate", "Name" },
                values: new object[] { 1, new DateTime(2022, 4, 19, 22, 11, 20, 983, DateTimeKind.Local).AddTicks(6646), "Kışlık kaliteli kazaklar", new DateTime(2022, 4, 19, 22, 11, 20, 983, DateTimeKind.Local).AddTicks(6659), "Kazak" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "Description", "ModifiedDate", "Name" },
                values: new object[] { 2, new DateTime(2022, 4, 19, 22, 11, 20, 983, DateTimeKind.Local).AddTicks(6681), "Tişört çeşitleri", new DateTime(2022, 4, 19, 22, 11, 20, 983, DateTimeKind.Local).AddTicks(6682), "Tişört" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "CreatedDate", "Description", "ModifiedDate", "Name" },
                values: new object[] { 3, new DateTime(2022, 4, 19, 22, 11, 20, 983, DateTimeKind.Local).AddTicks(6687), "Ayakkabı çeşitleri", new DateTime(2022, 4, 19, 22, 11, 20, 983, DateTimeKind.Local).AddTicks(6689), "Ayakkabı" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "CreatedDate", "Description", "ModifiedDate", "Name", "Price", "UnitsInStock" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2022, 4, 19, 22, 11, 20, 979, DateTimeKind.Local).AddTicks(6007), "Kalın %100 yün kazak.", new DateTime(2022, 4, 19, 22, 11, 20, 979, DateTimeKind.Local).AddTicks(6797), "Yünlü Kazak", 350m, 2500 },
                    { 2, 1, new DateTime(2022, 4, 19, 22, 11, 20, 979, DateTimeKind.Local).AddTicks(9655), "Kalın %100 yün boğazlı kazak.", new DateTime(2022, 4, 19, 22, 11, 20, 979, DateTimeKind.Local).AddTicks(9656), "Boğazlı Kazak", 300m, 1800 },
                    { 3, 2, new DateTime(2022, 4, 19, 22, 11, 20, 979, DateTimeKind.Local).AddTicks(9664), "Polo yaka yazlık tişört.", new DateTime(2022, 4, 19, 22, 11, 20, 979, DateTimeKind.Local).AddTicks(9665), "Polo Yaka Tişört", 150m, 3200 },
                    { 4, 2, new DateTime(2022, 4, 19, 22, 11, 20, 979, DateTimeKind.Local).AddTicks(9671), "V Yaka yazlık tişört.", new DateTime(2022, 4, 19, 22, 11, 20, 979, DateTimeKind.Local).AddTicks(9673), "V Yaka Tişört", 120m, 3900 },
                    { 5, 3, new DateTime(2022, 4, 19, 22, 11, 20, 979, DateTimeKind.Local).AddTicks(9678), "Esnek, rahat tabanlı spor ayakkabı.", new DateTime(2022, 4, 19, 22, 11, 20, 979, DateTimeKind.Local).AddTicks(9679), "Spor Ayakkabı", 200m, 1500 },
                    { 6, 3, new DateTime(2022, 4, 19, 22, 11, 20, 979, DateTimeKind.Local).AddTicks(9685), "Su geçirmez, soğuğa dayanıklı bağcıklı bot.", new DateTime(2022, 4, 19, 22, 11, 20, 979, DateTimeKind.Local).AddTicks(9686), "Bot", 450m, 2300 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
