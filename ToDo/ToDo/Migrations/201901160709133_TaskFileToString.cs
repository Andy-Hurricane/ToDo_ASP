namespace ToDo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class TaskFileToString : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "File", c => c.String());
            DropColumn("dbo.Tasks", "File_IsReadOnly");
            DropColumn("dbo.Tasks", "File_CreationTime");
            DropColumn("dbo.Tasks", "File_CreationTimeUtc");
            DropColumn("dbo.Tasks", "File_LastAccessTime");
            DropColumn("dbo.Tasks", "File_LastAccessTimeUtc");
            DropColumn("dbo.Tasks", "File_LastWriteTime");
            DropColumn("dbo.Tasks", "File_LastWriteTimeUtc");
            DropColumn("dbo.Tasks", "File_Attributes");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tasks", "File_Attributes", c => c.Int(nullable: false));
            AddColumn("dbo.Tasks", "File_LastWriteTimeUtc", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tasks", "File_LastWriteTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tasks", "File_LastAccessTimeUtc", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tasks", "File_LastAccessTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tasks", "File_CreationTimeUtc", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tasks", "File_CreationTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Tasks", "File_IsReadOnly", c => c.Boolean(nullable: false));
            DropColumn("dbo.Tasks", "File");
        }
    }
}
