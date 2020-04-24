namespace KrankenhausSjukhuset.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Patients",
                c => new
                    {
                        SSN = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                        Age = c.Int(nullable: false),
                        IllnessLevel = c.Int(nullable: false),
                        StatusID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SSN)
                .ForeignKey("dbo.Status", t => t.StatusID, cascadeDelete: true)
                .Index(t => t.StatusID);
            
            CreateTable(
                "dbo.Status",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        PatientStatus = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Patients", "StatusID", "dbo.Status");
            DropIndex("dbo.Patients", new[] { "StatusID" });
            DropTable("dbo.Status");
            DropTable("dbo.Patients");
        }
    }
}
