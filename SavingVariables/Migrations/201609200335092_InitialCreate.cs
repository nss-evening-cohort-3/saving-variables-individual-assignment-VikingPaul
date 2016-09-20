namespace SavingVariables.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Variables",
                c => new
                    {
                        VariableId = c.Int(nullable: false, identity: true),
                        Var = c.String(nullable: false),
                        Value = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.VariableId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Variables");
        }
    }
}
