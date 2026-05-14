using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VisualProgramming.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Grafs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    id_project = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Grafs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Grafs_Projects_id_project",
                        column: x => x.id_project,
                        principalTable: "Projects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BaseNodes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    NodeType = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    id_operation = table.Column<Guid>(type: "uuid", nullable: true),
                    TypeOperation = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseNodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaseNodes_Grafs_id_operation",
                        column: x => x.id_operation,
                        principalTable: "Grafs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ElementGrafs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    id_node = table.Column<Guid>(type: "uuid", nullable: true),
                    LevelLevelOfDepthOperation = table.Column<int>(type: "integer", nullable: false),
                    IsModul = table.Column<bool>(type: "boolean", nullable: false),
                    id_element_graf = table.Column<Guid>(type: "uuid", nullable: true),
                    PositionX = table.Column<double>(type: "double precision", nullable: false),
                    PositionY = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElementGrafs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElementGrafs_BaseNodes_id_node",
                        column: x => x.id_node,
                        principalTable: "BaseNodes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ElementGrafs_Grafs_id_element_graf",
                        column: x => x.id_element_graf,
                        principalTable: "Grafs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Ports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    NodeId = table.Column<Guid>(type: "uuid", nullable: false),
                    TypePort = table.Column<int>(type: "integer", nullable: false),
                    Description = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ports", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ports_BaseNodes_NodeId",
                        column: x => x.NodeId,
                        principalTable: "BaseNodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Connections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    in_element_graf_id = table.Column<Guid>(type: "uuid", nullable: false),
                    source_port_id = table.Column<Guid>(type: "uuid", nullable: false),
                    out_element_graf_id = table.Column<Guid>(type: "uuid", nullable: false),
                    target_port_id = table.Column<Guid>(type: "uuid", nullable: false),
                    id_element_graf = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Connections_ElementGrafs_id_element_graf",
                        column: x => x.id_element_graf,
                        principalTable: "ElementGrafs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Connections_ElementGrafs_in_element_graf_id",
                        column: x => x.in_element_graf_id,
                        principalTable: "ElementGrafs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Connections_ElementGrafs_out_element_graf_id",
                        column: x => x.out_element_graf_id,
                        principalTable: "ElementGrafs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Connections_Ports_source_port_id",
                        column: x => x.source_port_id,
                        principalTable: "Ports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Connections_Ports_target_port_id",
                        column: x => x.target_port_id,
                        principalTable: "Ports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NodePortConnections",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    id_node = table.Column<Guid>(type: "uuid", nullable: false),
                    id_port = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NodePortConnections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NodePortConnections_BaseNodes_id_node",
                        column: x => x.id_node,
                        principalTable: "BaseNodes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NodePortConnections_Ports_id_port",
                        column: x => x.id_port,
                        principalTable: "Ports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseNodes_id_operation",
                table: "BaseNodes",
                column: "id_operation");

            migrationBuilder.CreateIndex(
                name: "IX_Connections_id_element_graf",
                table: "Connections",
                column: "id_element_graf");

            migrationBuilder.CreateIndex(
                name: "IX_Connections_in_element_graf_id",
                table: "Connections",
                column: "in_element_graf_id");

            migrationBuilder.CreateIndex(
                name: "IX_Connections_out_element_graf_id",
                table: "Connections",
                column: "out_element_graf_id");

            migrationBuilder.CreateIndex(
                name: "IX_Connections_source_port_id",
                table: "Connections",
                column: "source_port_id");

            migrationBuilder.CreateIndex(
                name: "IX_Connections_target_port_id",
                table: "Connections",
                column: "target_port_id");

            migrationBuilder.CreateIndex(
                name: "IX_ElementGrafs_id_element_graf",
                table: "ElementGrafs",
                column: "id_element_graf");

            migrationBuilder.CreateIndex(
                name: "IX_ElementGrafs_id_node",
                table: "ElementGrafs",
                column: "id_node");

            migrationBuilder.CreateIndex(
                name: "IX_Grafs_id_project",
                table: "Grafs",
                column: "id_project");

            migrationBuilder.CreateIndex(
                name: "IX_NodePortConnections_id_node",
                table: "NodePortConnections",
                column: "id_node");

            migrationBuilder.CreateIndex(
                name: "IX_NodePortConnections_id_port",
                table: "NodePortConnections",
                column: "id_port");

            migrationBuilder.CreateIndex(
                name: "IX_Ports_NodeId",
                table: "Ports",
                column: "NodeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Connections");

            migrationBuilder.DropTable(
                name: "NodePortConnections");

            migrationBuilder.DropTable(
                name: "ElementGrafs");

            migrationBuilder.DropTable(
                name: "Ports");

            migrationBuilder.DropTable(
                name: "BaseNodes");

            migrationBuilder.DropTable(
                name: "Grafs");

            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
