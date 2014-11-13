namespace QiwiReports.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Letters",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        txn_id = c.String(),
                        account = c.String(),
                        amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        trm_id = c.String(),
                        trm_txn_id = c.String(),
                        from_amount = c.Decimal(nullable: false, precision: 18, scale: 2),
                        account1 = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Letters");
        }
    }
}
