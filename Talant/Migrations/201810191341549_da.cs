namespace Talant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class da : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Intrebares", "Raspuns", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Intrebares", "Raspuns");
        }
    }
}
