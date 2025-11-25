using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SLACalculator.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addslatablesandseeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BusinessClosures",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ClosureDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsFullDayClosure = table.Column<bool>(type: "bit", nullable: false),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessClosures", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Complaints",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    CaptureDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SlaExpirationDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FilePaths = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complaints", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SlaConfigurations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Priority = table.Column<int>(type: "int", nullable: false),
                    ResolutionTimeInHours = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SlaConfigurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkingDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DayOfWeek = table.Column<int>(type: "int", nullable: false),
                    IsWorkingDay = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingDays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkingHours",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    EndTime = table.Column<TimeSpan>(type: "time", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkingHours", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "BusinessClosures",
                columns: new[] { "Id", "ClosureDate", "CreatedAt", "Description", "EndTime", "IsFullDayClosure", "Name", "StartTime" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 2, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 25, 14, 59, 22, 587, DateTimeKind.Utc).AddTicks(6719), "Saudi Founding Day", null, true, "Saudi Founding Day 2026", null },
                    { 2, new DateTime(2026, 9, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 25, 14, 59, 22, 587, DateTimeKind.Utc).AddTicks(7547), "Saudi Nationl Day", null, true, "Saudi Nationl Day 2026", null },
                    { 3, new DateTime(2025, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2025, 11, 25, 14, 59, 22, 587, DateTimeKind.Utc).AddTicks(7549), "Today Event for 4 Hours", new TimeSpan(0, 22, 0, 0, 0), false, "Today event from 6 PM to 10 PM", new TimeSpan(0, 18, 0, 0, 0) }
                });

            migrationBuilder.InsertData(
                table: "SlaConfigurations",
                columns: new[] { "Id", "CreatedAt", "Priority", "ResolutionTimeInHours", "UpdatedAt" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 11, 25, 14, 59, 22, 587, DateTimeKind.Utc).AddTicks(5993), 1, 4, null },
                    { 2, new DateTime(2025, 11, 25, 14, 59, 22, 587, DateTimeKind.Utc).AddTicks(6350), 2, 10, null },
                    { 3, new DateTime(2025, 11, 25, 14, 59, 22, 587, DateTimeKind.Utc).AddTicks(6351), 3, 24, null }
                });

            migrationBuilder.InsertData(
                table: "WorkingDays",
                columns: new[] { "Id", "CreatedAt", "DayOfWeek", "IsWorkingDay" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 11, 25, 14, 59, 22, 587, DateTimeKind.Utc).AddTicks(820), 0, true },
                    { 2, new DateTime(2025, 11, 25, 14, 59, 22, 587, DateTimeKind.Utc).AddTicks(1260), 1, true },
                    { 3, new DateTime(2025, 11, 25, 14, 59, 22, 587, DateTimeKind.Utc).AddTicks(1261), 2, true },
                    { 4, new DateTime(2025, 11, 25, 14, 59, 22, 587, DateTimeKind.Utc).AddTicks(1262), 3, true },
                    { 5, new DateTime(2025, 11, 25, 14, 59, 22, 587, DateTimeKind.Utc).AddTicks(1262), 4, true },
                    { 6, new DateTime(2025, 11, 25, 14, 59, 22, 587, DateTimeKind.Utc).AddTicks(1263), 5, false },
                    { 7, new DateTime(2025, 11, 25, 14, 59, 22, 587, DateTimeKind.Utc).AddTicks(1264), 6, false }
                });

            migrationBuilder.InsertData(
                table: "WorkingHours",
                columns: new[] { "Id", "CreatedAt", "Description", "EndTime", "StartTime" },
                values: new object[] { 1, new DateTime(2025, 11, 25, 14, 59, 22, 587, DateTimeKind.Utc).AddTicks(5053), "Everyday Work Hours form 8 AM to 5 PM", new TimeSpan(0, 17, 0, 0, 0), new TimeSpan(0, 8, 0, 0, 0) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessClosures");

            migrationBuilder.DropTable(
                name: "Complaints");

            migrationBuilder.DropTable(
                name: "SlaConfigurations");

            migrationBuilder.DropTable(
                name: "WorkingDays");

            migrationBuilder.DropTable(
                name: "WorkingHours");
        }
    }
}
