namespace GUDB.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Event",
                c => new
                    {
                        EId = c.String(nullable: false, maxLength: 128),
                        TId = c.String(),
                        ETime = c.DateTime(nullable: false),
                        ELong = c.Double(nullable: false),
                        ELat = c.Double(nullable: false),
                        ELocation = c.String(),
                        ERange = c.String(),
                        EDamageDes = c.String(),
                        EEarthDec = c.String(),
                        Type_TId = c.Int(),
                    })
                .PrimaryKey(t => t.EId)
                .ForeignKey("dbo.Type", t => t.Type_TId)
                .Index(t => t.Type_TId);
            
            CreateTable(
                "dbo.Type",
                c => new
                    {
                        TId = c.Int(nullable: false, identity: true),
                        TName = c.String(nullable: false),
                        TCharact = c.String(),
                        TReason = c.String(),
                        TDamageCharact = c.String(),
                    })
                .PrimaryKey(t => t.TId);
            
            CreateTable(
                "dbo.User",
                c => new
                    {
                        UId = c.Int(nullable: false, identity: true),
                        UName = c.String(),
                        UIdName = c.String(),
                        UPhone = c.String(),
                        UMail = c.String(),
                        UOtherConnect = c.String(),
                        UIdNumber = c.String(),
                    })
                .PrimaryKey(t => t.UId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Event", "Type_TId", "dbo.Type");
            DropIndex("dbo.Event", new[] { "Type_TId" });
            DropTable("dbo.User");
            DropTable("dbo.Type");
            DropTable("dbo.Event");
        }
    }
}
