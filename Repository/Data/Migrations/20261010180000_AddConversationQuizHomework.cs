using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Repository.Data;

#nullable disable

namespace Repository.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20261010180000_AddConversationQuizHomework")]
    public partial class AddConversationQuizHomework : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
DECLARE @moduleId int;
SELECT TOP 1 @moduleId = m.Id
FROM CourseModules m
INNER JOIN Courses c ON c.Id = m.CourseId
WHERE c.Slug = N'conversation'
ORDER BY m.[Order];

IF @moduleId IS NOT NULL
BEGIN
    IF NOT EXISTS (SELECT 1 FROM Lessons WHERE CourseModuleId = @moduleId AND Title = N'Quiz - Speaking check')
        INSERT INTO Lessons (CourseModuleId, Title, Kind, Seconds, [Order], CreatedAt)
        VALUES (@moduleId, N'Quiz - Speaking check', 3, 600, 4, GETUTCDATE());

    IF NOT EXISTS (SELECT 1 FROM Lessons WHERE CourseModuleId = @moduleId AND Title = N'Homework - Record a 1-minute talk')
        INSERT INTO Lessons (CourseModuleId, Title, Kind, Seconds, [Order], CreatedAt)
        VALUES (@moduleId, N'Homework - Record a 1-minute talk', 4, 0, 5, GETUTCDATE());
END
");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
DELETE FROM Lessons
WHERE Title IN (N'Quiz - Speaking check', N'Homework - Record a 1-minute talk')
  AND CourseModuleId IN (
      SELECT m.Id
      FROM CourseModules m
      INNER JOIN Courses c ON c.Id = m.CourseId
      WHERE c.Slug = N'conversation'
  );
");
        }
    }
}
