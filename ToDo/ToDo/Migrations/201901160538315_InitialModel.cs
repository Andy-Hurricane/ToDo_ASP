namespace ToDo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Tasks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Topic = c.String(nullable: false, maxLength: 255),
                        Action = c.String(nullable: false, maxLength: 255),
                        Status = c.Int(nullable: false),
                        Start = c.DateTime(nullable: false),
                        End = c.DateTime(nullable: false),
                        Priority = c.Int(nullable: false),
                        Progress = c.Int(nullable: false),
                        Description = c.String(maxLength: 255),
                        File_IsReadOnly = c.Boolean(nullable: false),
                        File_CreationTime = c.DateTime(nullable: false),
                        File_CreationTimeUtc = c.DateTime(nullable: false),
                        File_LastAccessTime = c.DateTime(nullable: false),
                        File_LastAccessTimeUtc = c.DateTime(nullable: false),
                        File_LastWriteTime = c.DateTime(nullable: false),
                        File_LastWriteTimeUtc = c.DateTime(nullable: false),
                        File_Attributes = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Topic, unique: true);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.Tasks", new[] { "Topic" });
            DropTable("dbo.Tasks");
        }
    }
}
