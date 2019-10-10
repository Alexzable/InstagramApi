﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApi.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserContexts",
                columns: table => new
                {
                    Username = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    ClientId = table.Column<string>(nullable: true),
                    ClientSecret = table.Column<string>(nullable: true),
                    ID = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    ProfePicture = table.Column<string>(nullable: true),
                    LinkBio = table.Column<string>(nullable: true),
                    Comments = table.Column<double>(nullable: false),
                    Likes = table.Column<double>(nullable: false),
                    IDInsta = table.Column<double>(nullable: false),
                    Location = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserContexts", x => x.Username);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserContexts");
        }
    }
}
