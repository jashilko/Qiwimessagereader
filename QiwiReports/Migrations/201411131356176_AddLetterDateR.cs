namespace QiwiReports.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLetterDateR : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Letters", "dateReceive", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Letters", "dateReceive");
        }
    }
}
