using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ForgeERP.Catalog.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "catalog");

            migrationBuilder.CreateTable(
                name: "Boms",
                schema: "catalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Boms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                schema: "catalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ItemCode = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    ItemType = table.Column<string>(type: "text", nullable: false),
                    UnitOfMeasure = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BomLines",
                schema: "catalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BomId = table.Column<Guid>(type: "uuid", nullable: false),
                    ComponentItemId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<decimal>(type: "numeric", nullable: false),
                    Position = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BomLines", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BomLines_Boms_BomId",
                        column: x => x.BomId,
                        principalSchema: "catalog",
                        principalTable: "Boms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BomLines_BomId",
                schema: "catalog",
                table: "BomLines",
                column: "BomId");

            migrationBuilder.CreateIndex(
                name: "IX_BomLines_ComponentItemId",
                schema: "catalog",
                table: "BomLines",
                column: "ComponentItemId");

            migrationBuilder.CreateIndex(
                name: "IX_Boms_ItemId",
                schema: "catalog",
                table: "Boms",
                column: "ItemId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Items_ItemCode",
                schema: "catalog",
                table: "Items",
                column: "ItemCode",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BomLines",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "Items",
                schema: "catalog");

            migrationBuilder.DropTable(
                name: "Boms",
                schema: "catalog");
        }
    }
}
