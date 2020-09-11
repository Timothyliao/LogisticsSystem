namespace LogisticsSystem.Model.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class migration2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Orders", "EndWebsiteId", "dbo.WebsiteInfoes");
            DropForeignKey("dbo.Orders", "StartWebsiteId", "dbo.WebsiteInfoes");
            DropIndex("dbo.Orders", new[] { "StartWebsiteId" });
            DropIndex("dbo.Orders", new[] { "EndWebsiteId" });
            RenameColumn(table: "dbo.AuthCodes", name: "char", newName: "Code");
            CreateTable(
                "dbo.InsuranceInfoes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OrderId = c.Guid(nullable: false),
                        InsurerId = c.Guid(nullable: false),
                        Value = c.String(nullable: false, maxLength: 10, unicode: false),
                        Risk = c.String(nullable: false, maxLength: 5, unicode: false),
                        ProveImage1 = c.String(maxLength: 100, unicode: false),
                        ProveImage2 = c.String(maxLength: 100, unicode: false),
                        ProveImage3 = c.String(maxLength: 100, unicode: false),
                        IsProblem = c.Boolean(nullable: false),
                        ProblemDiscription = c.String(storeType: "ntext"),
                        CreatTime = c.DateTime(nullable: false),
                        IsRemove = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId)
                .ForeignKey("dbo.CargoSenders", t => t.InsurerId)
                .Index(t => t.OrderId)
                .Index(t => t.InsurerId);
            
            CreateTable(
                "dbo.Sections",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(),
                        ChargeMan = c.String(),
                        CreatTime = c.DateTime(nullable: false),
                        IsRemove = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.CargoReceivers", "Location", c => c.String(maxLength: 50, unicode: false));
            AddColumn("dbo.Orders", "Status", c => c.String(maxLength: 1, fixedLength: true, unicode: false));
            AddColumn("dbo.CargoSenders", "Location", c => c.String(maxLength: 50, unicode: false));
            AddColumn("dbo.OrderDetails", "CargoName", c => c.String(nullable: false, maxLength: 8000, unicode: false));
            AddColumn("dbo.OrderDetails", "CargoWeight", c => c.String(nullable: false, maxLength: 15, unicode: false));
            AddColumn("dbo.OrderDetails", "UitNum", c => c.String(nullable: false, maxLength: 5, unicode: false));
            AddColumn("dbo.OrderDetails", "CargoVolume", c => c.String(maxLength: 20, unicode: false));
            AddColumn("dbo.OrderDetails", "PayType", c => c.Boolean(nullable: false));
            AddColumn("dbo.OrderDetails", "GetGoodsTime", c => c.String(maxLength: 8000, unicode: false));
            AddColumn("dbo.OrderDetails", "Mark", c => c.String(storeType: "ntext"));
            AddColumn("dbo.OrderDetails", "SenderId", c => c.Guid(nullable: false));
            AddColumn("dbo.OrderDetails", "ReceiverId", c => c.Guid(nullable: false));
            AlterColumn("dbo.StaffInfoes", "Password", c => c.String(nullable: false, maxLength: 70, unicode: false));
            AlterColumn("dbo.StaffInfoes", "ImagePath", c => c.String(maxLength: 100, unicode: false));
            AlterColumn("dbo.StaffInfoes", "IdCard", c => c.String(nullable: false, maxLength: 100, unicode: false));
            AlterColumn("dbo.AuthCodes", "Code", c => c.String(nullable: false, maxLength: 40, unicode: false));
            AlterColumn("dbo.Orders", "BarCode", c => c.String(nullable: false, maxLength: 20, unicode: false));
            CreateIndex("dbo.OrderDetails", "SenderId");
            CreateIndex("dbo.OrderDetails", "ReceiverId");
            AddForeignKey("dbo.OrderDetails", "ReceiverId", "dbo.CargoReceivers", "Id");
            AddForeignKey("dbo.OrderDetails", "SenderId", "dbo.CargoSenders", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderDetails", "SenderId", "dbo.CargoSenders");
            DropForeignKey("dbo.OrderDetails", "ReceiverId", "dbo.CargoReceivers");
            DropForeignKey("dbo.InsuranceInfoes", "InsurerId", "dbo.CargoSenders");
            DropForeignKey("dbo.InsuranceInfoes", "OrderId", "dbo.Orders");
            DropIndex("dbo.OrderDetails", new[] { "ReceiverId" });
            DropIndex("dbo.OrderDetails", new[] { "SenderId" });
            DropIndex("dbo.InsuranceInfoes", new[] { "InsurerId" });
            DropIndex("dbo.InsuranceInfoes", new[] { "OrderId" });
            AlterColumn("dbo.Orders", "BarCode", c => c.String(nullable: false, maxLength: 15, unicode: false));
            AlterColumn("dbo.AuthCodes", "Code", c => c.String(nullable: false, maxLength: 6));
            AlterColumn("dbo.StaffInfoes", "IdCard", c => c.String(nullable: false, maxLength: 30, unicode: false));
            AlterColumn("dbo.StaffInfoes", "ImagePath", c => c.String(maxLength: 30, unicode: false));
            AlterColumn("dbo.StaffInfoes", "Password", c => c.String(nullable: false, maxLength: 50, unicode: false));
            DropColumn("dbo.OrderDetails", "ReceiverId");
            DropColumn("dbo.OrderDetails", "SenderId");
            DropColumn("dbo.OrderDetails", "Mark");
            DropColumn("dbo.OrderDetails", "GetGoodsTime");
            DropColumn("dbo.OrderDetails", "PayType");
            DropColumn("dbo.OrderDetails", "CargoVolume");
            DropColumn("dbo.OrderDetails", "UitNum");
            DropColumn("dbo.OrderDetails", "CargoWeight");
            DropColumn("dbo.OrderDetails", "CargoName");
            DropColumn("dbo.CargoSenders", "Location");
            DropColumn("dbo.Orders", "Status");
            DropColumn("dbo.CargoReceivers", "Location");
            DropTable("dbo.Sections");
            DropTable("dbo.InsuranceInfoes");
            RenameColumn(table: "dbo.AuthCodes", name: "Code", newName: "char");
            CreateIndex("dbo.Orders", "EndWebsiteId");
            CreateIndex("dbo.Orders", "StartWebsiteId");
            AddForeignKey("dbo.Orders", "StartWebsiteId", "dbo.WebsiteInfoes", "Id");
            AddForeignKey("dbo.Orders", "EndWebsiteId", "dbo.WebsiteInfoes", "Id");
        }
    }
}
