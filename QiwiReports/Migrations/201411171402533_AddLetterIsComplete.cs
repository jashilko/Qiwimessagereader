namespace QiwiReports.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLetterIsComplete : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Letters", "IsComplete", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Letters", "IsComplete");
        }
    }
}
