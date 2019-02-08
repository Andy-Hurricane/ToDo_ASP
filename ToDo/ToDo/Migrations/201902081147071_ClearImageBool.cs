namespace ToDo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ClearImageBool : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tasks", "ClearImage", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Tasks", "ClearImage");
        }
    }
}
