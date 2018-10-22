using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace WebAPIsTrainingProject.Migrations
{
    public partial class Migration_2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Visible_Till",
                table: "ProductTable",
                newName: "VisibleTill");

            migrationBuilder.RenameColumn(
                name: "Product_Description",
                table: "ProductTable",
                newName: "ProductName");

            migrationBuilder.RenameColumn(
                name: "Prod_Name",
                table: "ProductTable",
                newName: "ProductDescription");

            migrationBuilder.RenameColumn(
                name: "No_Of_Products",
                table: "ProductTable",
                newName: "Quantity");

            migrationBuilder.RenameColumn(
                name: "Product_ID",
                table: "ProductTable",
                newName: "ProductID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "VisibleTill",
                table: "ProductTable",
                newName: "Visible_Till");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "ProductTable",
                newName: "No_Of_Products");

            migrationBuilder.RenameColumn(
                name: "ProductName",
                table: "ProductTable",
                newName: "Product_Description");

            migrationBuilder.RenameColumn(
                name: "ProductDescription",
                table: "ProductTable",
                newName: "Prod_Name");

            migrationBuilder.RenameColumn(
                name: "ProductID",
                table: "ProductTable",
                newName: "Product_ID");
        }
    }
}
