namespace ToDo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FileContent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "ImageContent", c => c.Binary());
            DropColumn("dbo.Tasks", "ImageFile");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Tasks", "ImageFile", c => c.String());
            DropColumn("dbo.Tasks", "ImageContent");
        }
    }
}
