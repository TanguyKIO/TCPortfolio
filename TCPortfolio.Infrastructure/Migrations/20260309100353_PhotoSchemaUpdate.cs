using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace TCPortfolio.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class PhotoSchemaUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "File_FileName",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "File_FileSize",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "File_Type",
                table: "Photos");

            migrationBuilder.DropColumn(
                name: "File_Url",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "Format_Width",
                table: "Photos",
                newName: "Width");

            migrationBuilder.RenameColumn(
                name: "Format_Height",
                table: "Photos",
                newName: "Height");

            migrationBuilder.RenameColumn(
                name: "File_PublicId",
                table: "Photos",
                newName: "PublicId");

            migrationBuilder.AlterColumn<int>(
                name: "Width",
                table: "Photos",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Height",
                table: "Photos",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(double),
                oldType: "double precision",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MainUrl",
                table: "Photos",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.DropForeignKey(
                name: "FK_Locations_Countries_CountryId",
                table: "Locations");

            migrationBuilder.DropForeignKey(
                name: "FK_CountryTranslations_Countries_CountryId", 
                table: "CountryTranslations");

            migrationBuilder.Sql(@"
                ALTER TABLE ""Locations"" 
                ALTER COLUMN ""CountryId"" TYPE uuid 
                USING (CASE 
                    WHEN ""CountryId"" IS NULL THEN NULL 
                    ELSE ('00000000-0000-0000-0000-' || LPAD(""CountryId""::text, 12, '0'))::uuid
                END)");

            migrationBuilder.Sql(@"
                ALTER TABLE ""Countries"" ALTER COLUMN ""Id"" DROP IDENTITY IF EXISTS;
                ALTER TABLE ""Countries"" ALTER COLUMN ""Id"" TYPE uuid USING (""Id""::text::uuid);
                ALTER TABLE ""Countries"" ALTER COLUMN ""Id"" SET NOT NULL;
            ");

            migrationBuilder.Sql(@"
                ALTER TABLE ""CountryTranslations"" ALTER COLUMN ""CountryId"" TYPE uuid 
                USING (('00000000-0000-0000-0000-' || LPAD(""CountryId""::text, 12, '0'))::uuid)");
            
            migrationBuilder.Sql(@"
                ALTER TABLE ""CountryTranslations"" ALTER COLUMN ""Id"" DROP IDENTITY IF EXISTS;
                ALTER TABLE ""CountryTranslations"" ALTER COLUMN ""Id"" TYPE uuid USING (""Id""::text::uuid);
                ALTER TABLE ""CountryTranslations"" ALTER COLUMN ""Id"" SET NOT NULL;
            ");

            migrationBuilder.AddForeignKey(
                name: "FK_Locations_Countries_CountryId",
                table: "Locations",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            
            migrationBuilder.AddForeignKey(
                name: "FK_CountryTranslations_Countries_CountryId",
                table: "CountryTranslations",
                column: "CountryId",
                principalTable: "Countries",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainUrl",
                table: "Photos");

            migrationBuilder.RenameColumn(
                name: "Width",
                table: "Photos",
                newName: "Format_Width");

            migrationBuilder.RenameColumn(
                name: "PublicId",
                table: "Photos",
                newName: "File_PublicId");

            migrationBuilder.RenameColumn(
                name: "Height",
                table: "Photos",
                newName: "Format_Height");

            migrationBuilder.AlterColumn<double>(
                name: "Format_Width",
                table: "Photos",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AlterColumn<double>(
                name: "Format_Height",
                table: "Photos",
                type: "double precision",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "File_FileName",
                table: "Photos",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "File_FileSize",
                table: "Photos",
                type: "bigint",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "File_Type",
                table: "Photos",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "File_Url",
                table: "Photos",
                type: "text",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "Locations",
                type: "integer",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "CountryTranslations",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "CountryTranslations",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Countries",
                type: "integer",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uuid")
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);
        }
    }
}
