using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisualProgramming.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixEfNodePortConnectionModelChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ports_BaseNode_NodeId",
                table: "Ports");

            migrationBuilder.DropIndex(
                name: "IX_Ports_NodeId",
                table: "Ports");

            migrationBuilder.DropColumn(
                name: "NodeId",
                table: "Ports");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "NodeId",
                table: "Ports",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Ports_NodeId",
                table: "Ports",
                column: "NodeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ports_BaseNode_NodeId",
                table: "Ports",
                column: "NodeId",
                principalTable: "BaseNode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
