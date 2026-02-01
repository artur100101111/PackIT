using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PackIt.Persistance.EF.Migrations
{
    /// <inheritdoc />
    public partial class Init_Write : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "packing");

            migrationBuilder.CreateTable(
                name: "ItemTypes",
                schema: "packing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                    Code = table.Column<string>(type: "VARCHAR(30)", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                schema: "packing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                    Code = table.Column<string>(type: "VARCHAR(30)", nullable: false),
                    Description = table.Column<string>(type: "VARCHAR(100)", nullable: true),
                    Type = table.Column<string>(type: "VARCHAR(20)", nullable: false),
                    ParentId = table.Column<long>(type: "bigint", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                    table.CheckConstraint("CK_Location_Type_Enum", "Type IN ('Factory', 'Area', 'Line', 'Warehouse')");
                    table.ForeignKey(
                        name: "FK_Locations_Locations_ParentId",
                        column: x => x.ParentId,
                        principalSchema: "packing",
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                schema: "packing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "BIGINT", nullable: false),
                    Requested_Location_Name = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                    Requested_Location_Code = table.Column<string>(type: "VARCHAR(30)", nullable: false),
                    Requested_Location_Type = table.Column<string>(type: "VARCHAR(30)", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Order_Type = table.Column<string>(type: "VARCHAR(20)", maxLength: 13, nullable: false),
                    Order_State = table.Column<string>(type: "NVARCHAR(50)", nullable: true),
                    RequestedDeliveryTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeliveryTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Delivery_Location_Name = table.Column<string>(type: "NVARCHAR(100)", nullable: true),
                    Delivery_Location_Code = table.Column<string>(type: "VARCHAR(30)", nullable: true),
                    Delivery_Location_Type = table.Column<string>(type: "VARCHAR(30)", nullable: true),
                    Name = table.Column<string>(type: "NVARCHAR(100)", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                schema: "packing",
                columns: table => new
                {
                    Id = table.Column<long>(type: "BIGINT", nullable: false),
                    Name = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                    Code = table.Column<string>(type: "VARCHAR(30)", nullable: false),
                    TypeID = table.Column<long>(type: "bigint", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Items_ItemTypes_TypeID",
                        column: x => x.TypeID,
                        principalSchema: "packing",
                        principalTable: "ItemTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderItems",
                schema: "packing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Item_Name = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                    Item_Code = table.Column<string>(type: "VARCHAR(30)", nullable: false),
                    Item_TypeName = table.Column<string>(type: "NVARCHAR(100)", nullable: false),
                    Item_TypeCode = table.Column<string>(type: "NVARCHAR(30)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Order_Id = table.Column<long>(type: "BIGINT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderItems_Orders_Order_Id",
                        column: x => x.Order_Id,
                        principalSchema: "packing",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderStateChangedHistory",
                schema: "packing",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PreviousState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CurrentState = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EventTime = table.Column<DateTime>(type: "DATETIME", nullable: false),
                    OrderId = table.Column<long>(type: "BIGINT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderStateChangedHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderStateChangedHistory_Orders_OrderId",
                        column: x => x.OrderId,
                        principalSchema: "packing",
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Item_Code",
                schema: "packing",
                table: "Items",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_TypeID",
                schema: "packing",
                table: "Items",
                column: "TypeID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemTypes_Code",
                schema: "packing",
                table: "ItemTypes",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_Code",
                schema: "packing",
                table: "Locations",
                column: "Code",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Locations_ParentId",
                schema: "packing",
                table: "Locations",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_ItemVO_Code",
                schema: "packing",
                table: "OrderItems",
                column: "Item_Code");

            migrationBuilder.CreateIndex(
                name: "IX_OrderItems_Order_Id",
                schema: "packing",
                table: "OrderItems",
                column: "Order_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_Requested_Location_Code",
                schema: "packing",
                table: "Orders",
                column: "Requested_Location_Code");

            migrationBuilder.CreateIndex(
                name: "IX_OrderStateChangedHistory_OrderId",
                schema: "packing",
                table: "OrderStateChangedHistory",
                column: "OrderId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items",
                schema: "packing");

            migrationBuilder.DropTable(
                name: "Locations",
                schema: "packing");

            migrationBuilder.DropTable(
                name: "OrderItems",
                schema: "packing");

            migrationBuilder.DropTable(
                name: "OrderStateChangedHistory",
                schema: "packing");

            migrationBuilder.DropTable(
                name: "ItemTypes",
                schema: "packing");

            migrationBuilder.DropTable(
                name: "Orders",
                schema: "packing");
        }
    }
}
