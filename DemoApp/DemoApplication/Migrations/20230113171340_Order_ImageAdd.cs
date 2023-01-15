using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DemoApplication.Migrations
{
    public partial class Order_ImageAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "BookImages");

            migrationBuilder.AddColumn<int>(
                name: "Order",
                table: "BookImages",
                type: "int",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Order",
                table: "BookImages");

            migrationBuilder.AddColumn<int>(
                name: "Type",
                table: "BookImages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
