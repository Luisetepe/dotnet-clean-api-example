using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NodaTime;

#nullable disable

namespace Artema.Platform.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial_Migration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "PRODUCT_CATEGORIES",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    IsService = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAt = table.Column<Instant>(type: "timestamptz", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedAt = table.Column<Instant>(type: "timestamptz", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCT_CATEGORIES", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PRODUCTS",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "varchar(100)", nullable: false),
                    Pvp = table.Column<long>(type: "bigint", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<Instant>(type: "timestamptz", nullable: false, defaultValueSql: "NOW()"),
                    UpdatedAt = table.Column<Instant>(type: "timestamptz", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRODUCTS", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PRODUCTS_PRODUCT_CATEGORIES_CategoryId",
                        column: x => x.CategoryId,
                        principalSchema: "public",
                        principalTable: "PRODUCT_CATEGORIES",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PRODUCTS_CategoryId",
                schema: "public",
                table: "PRODUCTS",
                column: "CategoryId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PRODUCTS",
                schema: "public");

            migrationBuilder.DropTable(
                name: "PRODUCT_CATEGORIES",
                schema: "public");
        }
    }
}
