namespace Talant.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Intrebares",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Tip = c.Short(nullable: false),
                        Categorie = c.Short(nullable: false),
                        Enunt = c.String(),
                        Punctaj = c.Short(nullable: false),
                        Editie = c.Int(nullable: false),
                    Raspuns = c.String(),
                })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Referintas",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        Carte = c.String(),
                        Capitol = c.Int(nullable: false),
                        verset = c.Int(nullable: false),
                        Altele = c.String(),
                        Intrebare_Id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.Intrebares", t => t.Intrebare_Id)
                .Index(t => t.Intrebare_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Referintas", "Intrebare_Id", "dbo.Intrebares");
            DropIndex("dbo.Referintas", new[] { "Intrebare_Id" });
            DropTable("dbo.Referintas");
            DropTable("dbo.Intrebares");
        }
    }
}
