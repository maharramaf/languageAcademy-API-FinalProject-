using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class SimplifyEntityNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(name: "Eyebrow", table: "Heroes", newName: "Subtitle");
            migrationBuilder.RenameColumn(name: "Lead", table: "Heroes", newName: "Text");
            migrationBuilder.RenameColumn(name: "PrimaryButtonText", table: "Heroes", newName: "Button1");
            migrationBuilder.RenameColumn(name: "SecondaryButtonText", table: "Heroes", newName: "Button2");
            migrationBuilder.RenameColumn(name: "PointOne", table: "Heroes", newName: "Point1");
            migrationBuilder.RenameColumn(name: "PointTwo", table: "Heroes", newName: "Point2");
            migrationBuilder.RenameColumn(name: "PointThree", table: "Heroes", newName: "Point3");
            migrationBuilder.RenameColumn(name: "StudentsStat", table: "Heroes", newName: "Students");
            migrationBuilder.RenameColumn(name: "StudentsLabel", table: "Heroes", newName: "StudentsText");
            migrationBuilder.RenameColumn(name: "RatingStat", table: "Heroes", newName: "Rating");
            migrationBuilder.RenameColumn(name: "RatingLabel", table: "Heroes", newName: "RatingText");

            migrationBuilder.RenameColumn(name: "Eyebrow", table: "Abouts", newName: "Subtitle");
            migrationBuilder.RenameColumn(name: "ParagraphOne", table: "Abouts", newName: "Text1");
            migrationBuilder.RenameColumn(name: "ParagraphTwo", table: "Abouts", newName: "Text2");
            migrationBuilder.RenameColumn(name: "FeatureOne", table: "Abouts", newName: "Feature1");
            migrationBuilder.RenameColumn(name: "FeatureTwo", table: "Abouts", newName: "Feature2");
            migrationBuilder.RenameColumn(name: "FeatureThree", table: "Abouts", newName: "Feature3");
            migrationBuilder.RenameColumn(name: "FeatureFour", table: "Abouts", newName: "Feature4");
            migrationBuilder.RenameColumn(name: "ButtonText", table: "Abouts", newName: "Button");
            migrationBuilder.RenameColumn(name: "YearsStat", table: "Abouts", newName: "Years");
            migrationBuilder.RenameColumn(name: "YearsLabelLineOne", table: "Abouts", newName: "YearsText1");
            migrationBuilder.RenameColumn(name: "YearsLabelLineTwo", table: "Abouts", newName: "YearsText2");

            migrationBuilder.RenameColumn(name: "Eyebrow", table: "AboutHeroes", newName: "Subtitle");
            migrationBuilder.RenameColumn(name: "Lead", table: "AboutHeroes", newName: "Text");

            migrationBuilder.RenameColumn(name: "Eyebrow", table: "WhyChooses", newName: "Subtitle");
            migrationBuilder.RenameColumn(name: "Lead", table: "WhyChooses", newName: "Text");
            migrationBuilder.RenameColumn(name: "SortOrder", table: "WhyChooseCards", newName: "Order");

            migrationBuilder.RenameColumn(name: "Eyebrow", table: "HowWeWorks", newName: "Subtitle");
            migrationBuilder.RenameColumn(name: "SortOrder", table: "HowWeWorkCards", newName: "Order");

            migrationBuilder.RenameColumn(name: "Eyebrow", table: "TeacherSections", newName: "Subtitle");
            migrationBuilder.RenameColumn(name: "Lead", table: "TeacherSections", newName: "Text");
            migrationBuilder.RenameColumn(name: "LinkedInUrl", table: "Teachers", newName: "LinkedIn");
            migrationBuilder.RenameColumn(name: "SocialUrl", table: "Teachers", newName: "Social");
            migrationBuilder.RenameColumn(name: "SortOrder", table: "Teachers", newName: "Order");
            migrationBuilder.DropColumn(name: "ButtonText", table: "Teachers");
            migrationBuilder.DropColumn(name: "LinkedInAriaLabel", table: "Teachers");
            migrationBuilder.DropColumn(name: "SocialAriaLabel", table: "Teachers");

            migrationBuilder.RenameColumn(name: "PreviewVideoUrl", table: "Courses", newName: "Video");
            migrationBuilder.RenameColumn(name: "Meta", table: "CourseModules", newName: "Info");
            migrationBuilder.RenameColumn(name: "SortOrder", table: "CourseModules", newName: "Order");
            migrationBuilder.RenameColumn(name: "SortOrder", table: "CourseOutcomes", newName: "Order");
            migrationBuilder.RenameColumn(name: "VideoUrl", table: "Lessons", newName: "Video");
            migrationBuilder.RenameColumn(name: "DurationSeconds", table: "Lessons", newName: "Seconds");
            migrationBuilder.RenameColumn(name: "SortOrder", table: "Lessons", newName: "Order");

            migrationBuilder.AddColumn<int>(
                name: "Years",
                table: "Stats",
                type: "int",
                nullable: false,
                defaultValue: 10);

            migrationBuilder.Sql(@"
UPDATE s
SET s.Years = ISNULL(i.StoredValue, 10)
FROM Stats s
LEFT JOIN StatItems i ON i.StatsId = s.Id AND i.SourceKey = N'years';
");

            migrationBuilder.DropTable(name: "StatItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StatsId = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Icon = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    Label = table.Column<string>(type: "nvarchar(80)", maxLength: 80, nullable: false),
                    SortOrder = table.Column<int>(type: "int", nullable: false),
                    SourceKey = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    StoredValue = table.Column<int>(type: "int", nullable: false),
                    Suffix = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StatItems_Stats_StatsId",
                        column: x => x.StatsId,
                        principalTable: "Stats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_StatItems_StatsId_SourceKey",
                table: "StatItems",
                columns: new[] { "StatsId", "SourceKey" },
                unique: true);

            migrationBuilder.Sql(@"
INSERT INTO StatItems (StatsId, CreatedAt, Icon, Label, SortOrder, SourceKey, StoredValue, Suffix)
SELECT Id, GETUTCDATE(), N'bi bi-people', N'Students', 1, N'students', 0, N'+' FROM Stats;
INSERT INTO StatItems (StatsId, CreatedAt, Icon, Label, SortOrder, SourceKey, StoredValue, Suffix)
SELECT Id, GETUTCDATE(), N'bi bi-journal-bookmark', N'Courses', 2, N'courses', 0, N'+' FROM Stats;
INSERT INTO StatItems (StatsId, CreatedAt, Icon, Label, SortOrder, SourceKey, StoredValue, Suffix)
SELECT Id, GETUTCDATE(), N'bi bi-person-badge', N'Teachers', 3, N'teachers', 0, N'+' FROM Stats;
INSERT INTO StatItems (StatsId, CreatedAt, Icon, Label, SortOrder, SourceKey, StoredValue, Suffix)
SELECT Id, GETUTCDATE(), N'bi bi-mortarboard', N'Years Experience', 4, N'years', Years, N'+' FROM Stats;
");

            migrationBuilder.DropColumn(name: "Years", table: "Stats");

            migrationBuilder.RenameColumn(name: "Subtitle", table: "Heroes", newName: "Eyebrow");
            migrationBuilder.RenameColumn(name: "Text", table: "Heroes", newName: "Lead");
            migrationBuilder.RenameColumn(name: "Button1", table: "Heroes", newName: "PrimaryButtonText");
            migrationBuilder.RenameColumn(name: "Button2", table: "Heroes", newName: "SecondaryButtonText");
            migrationBuilder.RenameColumn(name: "Point1", table: "Heroes", newName: "PointOne");
            migrationBuilder.RenameColumn(name: "Point2", table: "Heroes", newName: "PointTwo");
            migrationBuilder.RenameColumn(name: "Point3", table: "Heroes", newName: "PointThree");
            migrationBuilder.RenameColumn(name: "Students", table: "Heroes", newName: "StudentsStat");
            migrationBuilder.RenameColumn(name: "StudentsText", table: "Heroes", newName: "StudentsLabel");
            migrationBuilder.RenameColumn(name: "Rating", table: "Heroes", newName: "RatingStat");
            migrationBuilder.RenameColumn(name: "RatingText", table: "Heroes", newName: "RatingLabel");

            migrationBuilder.RenameColumn(name: "Subtitle", table: "Abouts", newName: "Eyebrow");
            migrationBuilder.RenameColumn(name: "Text1", table: "Abouts", newName: "ParagraphOne");
            migrationBuilder.RenameColumn(name: "Text2", table: "Abouts", newName: "ParagraphTwo");
            migrationBuilder.RenameColumn(name: "Feature1", table: "Abouts", newName: "FeatureOne");
            migrationBuilder.RenameColumn(name: "Feature2", table: "Abouts", newName: "FeatureTwo");
            migrationBuilder.RenameColumn(name: "Feature3", table: "Abouts", newName: "FeatureThree");
            migrationBuilder.RenameColumn(name: "Feature4", table: "Abouts", newName: "FeatureFour");
            migrationBuilder.RenameColumn(name: "Button", table: "Abouts", newName: "ButtonText");
            migrationBuilder.RenameColumn(name: "Years", table: "Abouts", newName: "YearsStat");
            migrationBuilder.RenameColumn(name: "YearsText1", table: "Abouts", newName: "YearsLabelLineOne");
            migrationBuilder.RenameColumn(name: "YearsText2", table: "Abouts", newName: "YearsLabelLineTwo");

            migrationBuilder.RenameColumn(name: "Subtitle", table: "AboutHeroes", newName: "Eyebrow");
            migrationBuilder.RenameColumn(name: "Text", table: "AboutHeroes", newName: "Lead");

            migrationBuilder.RenameColumn(name: "Subtitle", table: "WhyChooses", newName: "Eyebrow");
            migrationBuilder.RenameColumn(name: "Text", table: "WhyChooses", newName: "Lead");
            migrationBuilder.RenameColumn(name: "Order", table: "WhyChooseCards", newName: "SortOrder");

            migrationBuilder.RenameColumn(name: "Subtitle", table: "HowWeWorks", newName: "Eyebrow");
            migrationBuilder.RenameColumn(name: "Order", table: "HowWeWorkCards", newName: "SortOrder");

            migrationBuilder.RenameColumn(name: "Subtitle", table: "TeacherSections", newName: "Eyebrow");
            migrationBuilder.RenameColumn(name: "Text", table: "TeacherSections", newName: "Lead");
            migrationBuilder.RenameColumn(name: "LinkedIn", table: "Teachers", newName: "LinkedInUrl");
            migrationBuilder.RenameColumn(name: "Social", table: "Teachers", newName: "SocialUrl");
            migrationBuilder.RenameColumn(name: "Order", table: "Teachers", newName: "SortOrder");
            migrationBuilder.AddColumn<string>(name: "ButtonText", table: "Teachers", type: "nvarchar(80)", maxLength: 80, nullable: false, defaultValue: "View Profile");
            migrationBuilder.AddColumn<string>(name: "LinkedInAriaLabel", table: "Teachers", type: "nvarchar(120)", maxLength: 120, nullable: false, defaultValue: "");
            migrationBuilder.AddColumn<string>(name: "SocialAriaLabel", table: "Teachers", type: "nvarchar(120)", maxLength: 120, nullable: false, defaultValue: "");

            migrationBuilder.RenameColumn(name: "Video", table: "Courses", newName: "PreviewVideoUrl");
            migrationBuilder.RenameColumn(name: "Info", table: "CourseModules", newName: "Meta");
            migrationBuilder.RenameColumn(name: "Order", table: "CourseModules", newName: "SortOrder");
            migrationBuilder.RenameColumn(name: "Order", table: "CourseOutcomes", newName: "SortOrder");
            migrationBuilder.RenameColumn(name: "Video", table: "Lessons", newName: "VideoUrl");
            migrationBuilder.RenameColumn(name: "Seconds", table: "Lessons", newName: "DurationSeconds");
            migrationBuilder.RenameColumn(name: "Order", table: "Lessons", newName: "SortOrder");
        }
    }
}
