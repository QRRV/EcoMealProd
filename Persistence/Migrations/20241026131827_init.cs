using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Canteens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HotMeals = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Canteens", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    containsAlcohol = table.Column<bool>(type: "bit", nullable: false),
                    ImageLink = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StudentNumber = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudyCity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EmployeeNumber = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CanteenId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Canteens_CanteenId",
                        column: x => x.CanteenId,
                        principalTable: "Canteens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MealBoxes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CanteenId = table.Column<int>(type: "int", nullable: false),
                    PickUpTimeMin = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PickUpTimeMax = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Adult = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StudentId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealBoxes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MealBoxes_Canteens_CanteenId",
                        column: x => x.CanteenId,
                        principalTable: "Canteens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealBoxes_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MealboxProducts",
                columns: table => new
                {
                    MealBoxId = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealboxProducts", x => new { x.MealBoxId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_MealboxProducts_MealBoxes_MealBoxId",
                        column: x => x.MealBoxId,
                        principalTable: "MealBoxes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MealboxProducts_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Canteens",
                columns: new[] { "Id", "City", "HotMeals", "Location" },
                values: new object[,]
                {
                    { 1, "Breda", false, "la" },
                    { 2, "Breda", true, "ld" },
                    { 3, "Tilburg", true, "Ha" },
                    { 4, "Tilburg", true, "Dc" },
                    { 5, "'s-Hertogenbosch", false, "Bk" },
                    { 6, "'s-Hertogenbosch", true, "Lc" }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "ImageLink", "Name", "containsAlcohol" },
                values: new object[,]
                {
                    { 1, "https://images.pexels.com/photos/1093038/pexels-photo-1093038.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1", "Banaan", false },
                    { 2, "https://images.pexels.com/photos/1633578/pexels-photo-1633578.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1", "Burger", false },
                    { 3, "https://images.pexels.com/photos/52994/beer-ale-bitter-fermented-52994.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1", "Bier", true },
                    { 4, "https://images.pexels.com/photos/3743389/pexels-photo-3743389.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1", "Hertenbiefstuk", false },
                    { 5, "https://images.pexels.com/photos/1841506/pexels-photo-1841506.jpeg?auto=compress&cs=tinysrgb&w=1260&h=750&dpr=1", "Champagnefles", true },
                    { 6, "https://images.unsplash.com/photo-1580822184713-fc5400e7fe10?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=687&q=80", "Sushi", false },
                    { 7, "https://images.unsplash.com/photo-1598679253544-2c97992403ea?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=687&q=80", "Patat", false },
                    { 8, "https://images.unsplash.com/photo-1474722883778-792e7990302f?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=691&q=80", "Wijn", true },
                    { 9, "https://images.unsplash.com/photo-1621852004158-f3bc188ace2d?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1470&q=80", "Panini", false },
                    { 10, "https://images.unsplash.com/photo-1581636625402-29b2a704ef13?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=688&q=80", "Frisdrank", false },
                    { 11, "https://images.unsplash.com/photo-1613844237701-8f3664fc2eff?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=764&q=80", "Soep", false },
                    { 12, "https://images.unsplash.com/photo-1569718212165-3a8278d5f624?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=880&q=80", "Noodles", false },
                    { 13, "https://images.unsplash.com/photo-1627308595216-439c00ade0fe?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=735&q=80", "Belegde broodje", false },
                    { 14, "https://images.unsplash.com/photo-1595787572590-545171362a1c?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=1470&q=80", "Yoghurt", false },
                    { 15, "https://images.unsplash.com/photo-1634141510639-d691d86f47be?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=764&q=80", "Melk", false },
                    { 16, "https://images.unsplash.com/photo-1553909489-ec2175ef3f52?ixlib=rb-4.0.3&ixid=MnwxMjA3fDB8MHxwaG90by1wYWdlfHx8fGVufDB8fHx8&auto=format&fit=crop&w=765&q=80", "Chocomel", false }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "BirthDate", "Email", "FirstName", "LastName", "PhoneNumber", "StudentNumber", "StudyCity" },
                values: new object[,]
                {
                    { 1, new DateTime(2002, 11, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "quinnie@hotmail.nl", "Quinn", "Verschoor", "0612345678", 2168424, "Breda" },
                    { 2, new DateTime(2008, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "jaron@gmail.com", "Jaron", "van Well", "0687654321", 2168253, "Tilburg" }
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CanteenId", "Email", "EmployeeNumber", "FirstName", "LastName" },
                values: new object[,]
                {
                    { 1, 1, "lucas@gmail.com", 12, "Lucas", "het Kleijn" },
                    { 2, 2, "hans@gmail.com", 51, "Hans Gerard", "Karremans" },
                    { 3, 3, "arno@gmail.com", 34, "Arno", "Broeders" },
                    { 4, 4, "pascal@gmail.com", 123, "Pascal", "van Gastel" },
                    { 5, 5, "robin@gmail.com", 535, "Robin", "Schellius" },
                    { 6, 6, "dion@gmail.com", 143, "Dion", "Koeze" }
                });

            migrationBuilder.InsertData(
                table: "MealBoxes",
                columns: new[] { "Id", "Adult", "CanteenId", "City", "Name", "PickUpTimeMax", "PickUpTimeMin", "Price", "StudentId", "Type" },
                values: new object[,]
                {
                    { 1, true, 1, "Breda", "Lucas Meal", new DateTime(2024, 10, 28, 15, 18, 27, 191, DateTimeKind.Local).AddTicks(8833), new DateTime(2024, 10, 27, 21, 18, 27, 191, DateTimeKind.Local).AddTicks(8801), 52.00m, 2, "Alcoholische drank" },
                    { 2, true, 2, "Breda", "Hans Meal", new DateTime(2024, 10, 28, 8, 6, 27, 191, DateTimeKind.Local).AddTicks(8838), new DateTime(2024, 10, 27, 11, 18, 27, 191, DateTimeKind.Local).AddTicks(8836), 200.00m, 1, "Luxemaaltijd" },
                    { 3, false, 1, "Breda", "Jaron Meal", new DateTime(2024, 10, 27, 15, 18, 27, 191, DateTimeKind.Local).AddTicks(8842), new DateTime(2024, 10, 27, 1, 18, 27, 191, DateTimeKind.Local).AddTicks(8840), 1.00m, null, "Budgetmeal" },
                    { 4, false, 2, "Breda", "Best Meal", new DateTime(2024, 10, 27, 10, 30, 27, 191, DateTimeKind.Local).AddTicks(8895), new DateTime(2024, 10, 26, 19, 18, 27, 191, DateTimeKind.Local).AddTicks(8843), 10.53m, null, "Avondmaaltijd" },
                    { 5, false, 3, "'Tilburg", "De goed bezig meal", new DateTime(2024, 10, 27, 20, 6, 27, 191, DateTimeKind.Local).AddTicks(8899), new DateTime(2024, 10, 26, 20, 18, 27, 191, DateTimeKind.Local).AddTicks(8898), 9.35m, null, "Bewust eten" },
                    { 6, true, 3, "Tilburg", "De Luxury meal", new DateTime(2024, 10, 28, 15, 18, 27, 191, DateTimeKind.Local).AddTicks(8902), new DateTime(2024, 10, 26, 20, 18, 27, 191, DateTimeKind.Local).AddTicks(8901), 35.0m, null, "Luxemaaltijd" },
                    { 7, false, 4, "Tilburg", "Japanese meal", new DateTime(2024, 10, 27, 22, 30, 27, 191, DateTimeKind.Local).AddTicks(8906), new DateTime(2024, 10, 26, 19, 18, 27, 191, DateTimeKind.Local).AddTicks(8904), 15.0m, null, "Bewust eten" },
                    { 8, false, 4, "Tilburg", "De frituur meal", new DateTime(2024, 10, 27, 20, 6, 27, 191, DateTimeKind.Local).AddTicks(8909), new DateTime(2024, 10, 26, 19, 18, 27, 191, DateTimeKind.Local).AddTicks(8908), 6.99m, null, "Avondmaaltijd" },
                    { 9, false, 5, "'s-Hertogenbosch", "De Avans box", new DateTime(2024, 10, 27, 15, 18, 27, 191, DateTimeKind.Local).AddTicks(8912), new DateTime(2024, 10, 26, 17, 18, 27, 191, DateTimeKind.Local).AddTicks(8911), 8.00m, null, "Brood" },
                    { 10, true, 5, "'s-Hertogenbosch", "Studenten box", new DateTime(2024, 10, 27, 15, 18, 27, 191, DateTimeKind.Local).AddTicks(8916), new DateTime(2024, 10, 26, 17, 18, 27, 191, DateTimeKind.Local).AddTicks(8914), 7.21m, null, "Alcoholische drank" },
                    { 11, false, 6, "'s-Hertogenbosch", "Soda box", new DateTime(2024, 10, 27, 12, 54, 27, 191, DateTimeKind.Local).AddTicks(8999), new DateTime(2024, 10, 26, 17, 18, 27, 191, DateTimeKind.Local).AddTicks(8997), 3.10m, null, "Drinken" },
                    { 12, false, 6, "'s-Hertogenbosch", "Zuivel box", new DateTime(2024, 10, 28, 10, 30, 27, 191, DateTimeKind.Local).AddTicks(9002), new DateTime(2024, 10, 26, 17, 18, 27, 191, DateTimeKind.Local).AddTicks(9001), 4.10m, null, "Zuivel producten" }
                });

            migrationBuilder.InsertData(
                table: "MealboxProducts",
                columns: new[] { "MealBoxId", "ProductId", "Id" },
                values: new object[,]
                {
                    { 1, 1, 1 },
                    { 1, 3, 2 },
                    { 2, 4, 3 },
                    { 2, 5, 4 },
                    { 5, 1, 5 },
                    { 5, 13, 6 },
                    { 6, 4, 7 },
                    { 6, 5, 8 },
                    { 6, 8, 9 },
                    { 7, 6, 10 },
                    { 7, 10, 12 },
                    { 7, 12, 11 },
                    { 8, 2, 13 },
                    { 8, 7, 14 },
                    { 8, 10, 15 },
                    { 9, 10, 18 },
                    { 9, 11, 16 },
                    { 9, 13, 17 },
                    { 10, 2, 19 },
                    { 10, 3, 20 },
                    { 10, 7, 21 },
                    { 10, 9, 22 },
                    { 10, 13, 23 },
                    { 11, 10, 24 },
                    { 11, 13, 25 },
                    { 12, 13, 26 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Employees_CanteenId",
                table: "Employees",
                column: "CanteenId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Email",
                table: "Employees",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MealBoxes_CanteenId",
                table: "MealBoxes",
                column: "CanteenId");

            migrationBuilder.CreateIndex(
                name: "IX_MealBoxes_StudentId",
                table: "MealBoxes",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_MealboxProducts_ProductId",
                table: "MealboxProducts",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "MealboxProducts");

            migrationBuilder.DropTable(
                name: "MealBoxes");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Canteens");

            migrationBuilder.DropTable(
                name: "Students");
        }
    }
}
