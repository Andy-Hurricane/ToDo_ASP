namespace ToDo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FileString : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "ImageFile", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tasks", "ImageFile");
        }
    }
}
