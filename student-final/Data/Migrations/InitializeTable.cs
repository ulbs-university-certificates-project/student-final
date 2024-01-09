using FluentMigrator;

namespace student_final.Data.Migrations;

[Migration(131220231)]
public class InitializeTable:Migration
{
    public override void Up()
    {
        Create.Table("students")
            .WithColumn("nr_matricol").AsInt32().PrimaryKey().Identity()
            .WithColumn("nume").AsString(128).NotNullable()
            .WithColumn("an").AsInt32().NotNullable()
            .WithColumn("sectie").AsString(128).NotNullable();
    }

    public override void Down()
    {
        Delete.Table("products");
    }
}