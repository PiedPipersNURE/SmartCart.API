using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartCart.DataProvider.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CartMembers",
                columns: table => new
                {
                    CartMemberID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CartID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    MemberID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartMembers", x => x.CartMemberID);
                });

            migrationBuilder.CreateTable(
                name: "Carts",
                columns: table => new
                {
                    CartID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CartName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CartOwnerID = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Carts", x => x.CartID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    ProductID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CartID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BuyerID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductQuantity = table.Column<int>(type: "int", nullable: false),
                    QuantityType = table.Column<int>(type: "int", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsBought = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.ProductID);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartMembers");

            migrationBuilder.DropTable(
                name: "Carts");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
