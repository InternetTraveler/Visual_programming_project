using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisualProgramming.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixModulConfiguration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseNodes_Grafs_id_operation",
                table: "BaseNodes");

            migrationBuilder.DropForeignKey(
                name: "FK_ElementGrafs_BaseNodes_id_node",
                table: "ElementGrafs");

            migrationBuilder.DropForeignKey(
                name: "FK_NodePortConnections_BaseNodes_id_node",
                table: "NodePortConnections");

            migrationBuilder.DropForeignKey(
                name: "FK_Ports_BaseNodes_NodeId",
                table: "Ports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseNodes",
                table: "BaseNodes");

            migrationBuilder.RenameTable(
                name: "BaseNodes",
                newName: "BaseNode");

            migrationBuilder.RenameIndex(
                name: "IX_BaseNodes_id_operation",
                table: "BaseNode",
                newName: "IX_BaseNode_id_operation");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseNode",
                table: "BaseNode",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseNode_Grafs_id_operation",
                table: "BaseNode",
                column: "id_operation",
                principalTable: "Grafs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ElementGrafs_BaseNode_id_node",
                table: "ElementGrafs",
                column: "id_node",
                principalTable: "BaseNode",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NodePortConnections_BaseNode_id_node",
                table: "NodePortConnections",
                column: "id_node",
                principalTable: "BaseNode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ports_BaseNode_NodeId",
                table: "Ports",
                column: "NodeId",
                principalTable: "BaseNode",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseNode_Grafs_id_operation",
                table: "BaseNode");

            migrationBuilder.DropForeignKey(
                name: "FK_ElementGrafs_BaseNode_id_node",
                table: "ElementGrafs");

            migrationBuilder.DropForeignKey(
                name: "FK_NodePortConnections_BaseNode_id_node",
                table: "NodePortConnections");

            migrationBuilder.DropForeignKey(
                name: "FK_Ports_BaseNode_NodeId",
                table: "Ports");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BaseNode",
                table: "BaseNode");

            migrationBuilder.RenameTable(
                name: "BaseNode",
                newName: "BaseNodes");

            migrationBuilder.RenameIndex(
                name: "IX_BaseNode_id_operation",
                table: "BaseNodes",
                newName: "IX_BaseNodes_id_operation");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BaseNodes",
                table: "BaseNodes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseNodes_Grafs_id_operation",
                table: "BaseNodes",
                column: "id_operation",
                principalTable: "Grafs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ElementGrafs_BaseNodes_id_node",
                table: "ElementGrafs",
                column: "id_node",
                principalTable: "BaseNodes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_NodePortConnections_BaseNodes_id_node",
                table: "NodePortConnections",
                column: "id_node",
                principalTable: "BaseNodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Ports_BaseNodes_NodeId",
                table: "Ports",
                column: "NodeId",
                principalTable: "BaseNodes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
