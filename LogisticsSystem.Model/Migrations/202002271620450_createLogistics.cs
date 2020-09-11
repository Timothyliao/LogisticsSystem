namespace LogisticsSystem.Model.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class createLogistics : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccountOperateLogs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OperatorId = c.Guid(nullable: false),
                        ModifiedId = c.Guid(nullable: false),
                        CreatTime = c.DateTime(nullable: false),
                        IsRemove = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StaffInfoes", t => t.ModifiedId)
                .ForeignKey("dbo.StaffInfoes", t => t.OperatorId)
                .Index(t => t.OperatorId)
                .Index(t => t.ModifiedId);
            
            CreateTable(
                "dbo.StaffInfoes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20, unicode: false),
                        Tel = c.String(nullable: false, maxLength: 11, fixedLength: true, unicode: false),
                        Password = c.String(nullable: false, maxLength: 50, unicode: false),
                        Email = c.String(nullable: false, maxLength: 50, unicode: false),
                        ImagePath = c.String(maxLength: 30, unicode: false),
                        IdCard = c.String(nullable: false, maxLength: 30, unicode: false),
                        Position = c.String(nullable: false, maxLength: 20, unicode: false),
                        Address = c.String(nullable: false, maxLength: 100, unicode: false),
                        SectionId = c.Guid(nullable: false),
                        Status = c.Boolean(nullable: false),
                        CreatTime = c.DateTime(nullable: false),
                        IsRemove = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AuthCodes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StaffId = c.Guid(nullable: false),
                        _char = c.String(name: "char", nullable: false, maxLength: 6),
                        CreatTime = c.DateTime(nullable: false),
                        IsRemove = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StaffInfoes", t => t.StaffId)
                .Index(t => t.StaffId);
            
            CreateTable(
                "dbo.CargoReceivers",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OrderId = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20, unicode: false),
                        MobliePhone = c.String(nullable: false, maxLength: 11, fixedLength: true, unicode: false),
                        Provinces = c.String(nullable: false, maxLength: 40, unicode: false),
                        DetailAddress = c.String(nullable: false, maxLength: 100, unicode: false),
                        PostCode = c.String(nullable: false, maxLength: 10, unicode: false),
                        TelPhone = c.String(nullable: false, maxLength: 11, unicode: false),
                        FirmName = c.String(nullable: false, maxLength: 40, unicode: false),
                        CreatTime = c.DateTime(nullable: false),
                        IsRemove = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.Orders",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        BarCode = c.String(nullable: false, maxLength: 15, unicode: false),
                        StartWebsiteId = c.Guid(nullable: false),
                        EndWebsiteId = c.Guid(nullable: false),
                        Region = c.String(nullable: false, maxLength: 10, unicode: false),
                        FinishTime = c.DateTime(nullable: false),
                        CreatTime = c.DateTime(nullable: false),
                        IsRemove = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.WebsiteInfoes", t => t.EndWebsiteId)
                .ForeignKey("dbo.WebsiteInfoes", t => t.StartWebsiteId)
                .Index(t => t.StartWebsiteId)
                .Index(t => t.EndWebsiteId);
            
            CreateTable(
                "dbo.WebsiteInfoes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Location = c.String(nullable: false, maxLength: 20, unicode: false),
                        ChargeMan = c.String(nullable: false, maxLength: 20, unicode: false),
                        ChargeManTel = c.String(nullable: false, maxLength: 11, unicode: false),
                        CreatTime = c.DateTime(nullable: false),
                        IsRemove = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CargoSenders",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OrderId = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20, unicode: false),
                        MobliePhone = c.String(nullable: false, maxLength: 11, fixedLength: true, unicode: false),
                        Provinces = c.String(nullable: false, maxLength: 40, unicode: false),
                        DetailAddress = c.String(nullable: false, maxLength: 100, unicode: false),
                        PostCode = c.String(nullable: false, maxLength: 10, unicode: false),
                        TelPhone = c.String(nullable: false, maxLength: 11, unicode: false),
                        FirmName = c.String(nullable: false, maxLength: 40, unicode: false),
                        CreatTime = c.DateTime(nullable: false),
                        IsRemove = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.FileInfoes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20, unicode: false),
                        Url = c.String(nullable: false, maxLength: 100, unicode: false),
                        CreatTime = c.DateTime(nullable: false),
                        IsRemove = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FileOperateLogs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OperatorId = c.Guid(nullable: false),
                        FileId = c.Guid(nullable: false),
                        CreatTime = c.DateTime(nullable: false),
                        IsRemove = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FileInfoes", t => t.FileId)
                .ForeignKey("dbo.StaffInfoes", t => t.OperatorId)
                .Index(t => t.OperatorId)
                .Index(t => t.FileId);
            
            CreateTable(
                "dbo.MenuInfoes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20, unicode: false),
                        Url = c.String(nullable: false, maxLength: 100, unicode: false),
                        FatherId = c.Guid(nullable: false),
                        CreatTime = c.DateTime(nullable: false),
                        IsRemove = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.NoticeInfoes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OperatorId = c.Guid(nullable: false),
                        Title = c.String(nullable: false, maxLength: 50, unicode: false),
                        Content = c.String(nullable: false, storeType: "ntext"),
                        PowerLimit = c.Guid(nullable: false),
                        CreatTime = c.DateTime(nullable: false),
                        IsRemove = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StaffInfoes", t => t.OperatorId)
                .Index(t => t.OperatorId);
            
            CreateTable(
                "dbo.NoticeOperateLogs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OperatorId = c.Guid(nullable: false),
                        NoticeId = c.Guid(nullable: false),
                        CreatTime = c.DateTime(nullable: false),
                        IsRemove = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.NoticeInfoes", t => t.NoticeId)
                .ForeignKey("dbo.StaffInfoes", t => t.OperatorId)
                .Index(t => t.OperatorId)
                .Index(t => t.NoticeId);
            
            CreateTable(
                "dbo.OrderDetails",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OrderId = c.Guid(nullable: false),
                        Freight = c.Double(nullable: false),
                        ServiceCharge = c.Double(nullable: false),
                        IsInsured = c.Boolean(nullable: false),
                        CreatTime = c.DateTime(nullable: false),
                        IsRemove = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId)
                .Index(t => t.OrderId);
            
            CreateTable(
                "dbo.OrderStaffLinks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StaffId = c.Guid(nullable: false),
                        CreatTime = c.DateTime(nullable: false),
                        IsRemove = c.Boolean(nullable: false),
                        StaffInfo_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StaffInfoes", t => t.StaffInfo_Id)
                .Index(t => t.StaffInfo_Id);
            
            CreateTable(
                "dbo.OrderWaybillLinks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OrderId = c.Guid(nullable: false),
                        WaybillId = c.Guid(nullable: false),
                        CreatTime = c.DateTime(nullable: false),
                        IsRemove = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Orders", t => t.OrderId)
                .ForeignKey("dbo.WayBills", t => t.WaybillId)
                .Index(t => t.OrderId)
                .Index(t => t.WaybillId);
            
            CreateTable(
                "dbo.WayBills",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PlanPath = c.String(nullable: false, maxLength: 200, unicode: false),
                        IsFinish = c.Boolean(nullable: false),
                        FinishTime = c.DateTime(nullable: false),
                        CreatTime = c.DateTime(nullable: false),
                        IsRemove = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PowerFileLinks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PowerId = c.Guid(nullable: false),
                        FileId = c.Guid(nullable: false),
                        CreatTime = c.DateTime(nullable: false),
                        IsRemove = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.FileInfoes", t => t.FileId)
                .ForeignKey("dbo.PowerInfoes", t => t.PowerId)
                .Index(t => t.PowerId)
                .Index(t => t.FileId);
            
            CreateTable(
                "dbo.PowerInfoes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        CreatTime = c.DateTime(nullable: false),
                        IsRemove = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PowerMenuLinks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        PowerId = c.Guid(nullable: false),
                        MenuId = c.Guid(nullable: false),
                        CreatTime = c.DateTime(nullable: false),
                        IsRemove = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.MenuInfoes", t => t.MenuId)
                .ForeignKey("dbo.PowerInfoes", t => t.PowerId)
                .Index(t => t.PowerId)
                .Index(t => t.MenuId);
            
            CreateTable(
                "dbo.PowerRoleLinks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RoleId = c.Guid(nullable: false),
                        PowerId = c.Guid(nullable: false),
                        CreatTime = c.DateTime(nullable: false),
                        IsRemove = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PowerInfoes", t => t.PowerId)
                .ForeignKey("dbo.RoleInfoes", t => t.RoleId)
                .Index(t => t.RoleId)
                .Index(t => t.PowerId);
            
            CreateTable(
                "dbo.RoleInfoes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20, unicode: false),
                        CreatTime = c.DateTime(nullable: false),
                        IsRemove = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.StaffGroups",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Name = c.String(nullable: false, maxLength: 20, unicode: false),
                        RoleId = c.Guid(nullable: false),
                        CreatTime = c.DateTime(nullable: false),
                        IsRemove = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoleInfoes", t => t.RoleId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.StaffPowerInfoes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        StaffId = c.Guid(nullable: false),
                        RoleId = c.Guid(nullable: false),
                        CreatTime = c.DateTime(nullable: false),
                        IsRemove = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RoleInfoes", t => t.RoleId)
                .ForeignKey("dbo.StaffInfoes", t => t.StaffId)
                .Index(t => t.StaffId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.TransportInfoes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        RevisorId = c.Guid(nullable: false),
                        CreatTime = c.DateTime(nullable: false),
                        IsRemove = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StaffInfoes", t => t.RevisorId)
                .Index(t => t.RevisorId);
            
            CreateTable(
                "dbo.TruckInfoes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        LicencePlate = c.String(maxLength: 20, unicode: false),
                        Load = c.Int(nullable: false),
                        Volume = c.Int(nullable: false),
                        DriverId = c.Guid(nullable: false),
                        CreatTime = c.DateTime(nullable: false),
                        IsRemove = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StaffInfoes", t => t.DriverId)
                .Index(t => t.DriverId);
            
            CreateTable(
                "dbo.TruckStates",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        TruckId = c.Guid(nullable: false),
                        Location = c.String(nullable: false, maxLength: 20, unicode: false),
                        CreatTime = c.DateTime(nullable: false),
                        IsRemove = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TruckInfoes", t => t.TruckId)
                .Index(t => t.TruckId);
            
            CreateTable(
                "dbo.WaybillTransportLinks",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        WayBillId = c.Guid(nullable: false),
                        TransportInfoId = c.Guid(nullable: false),
                        CreatTime = c.DateTime(nullable: false),
                        IsRemove = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.TruckInfoes", t => t.TransportInfoId)
                .ForeignKey("dbo.WayBills", t => t.WayBillId)
                .Index(t => t.WayBillId)
                .Index(t => t.TransportInfoId);
            
            CreateTable(
                "dbo.WebSiteOperateLogs",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        OperatorId = c.Guid(nullable: false),
                        WebsiteId = c.Guid(nullable: false),
                        CreatTime = c.DateTime(nullable: false),
                        IsRemove = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.StaffInfoes", t => t.OperatorId)
                .ForeignKey("dbo.WebsiteInfoes", t => t.WebsiteId)
                .Index(t => t.OperatorId)
                .Index(t => t.WebsiteId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WebSiteOperateLogs", "WebsiteId", "dbo.WebsiteInfoes");
            DropForeignKey("dbo.WebSiteOperateLogs", "OperatorId", "dbo.StaffInfoes");
            DropForeignKey("dbo.WaybillTransportLinks", "WayBillId", "dbo.WayBills");
            DropForeignKey("dbo.WaybillTransportLinks", "TransportInfoId", "dbo.TruckInfoes");
            DropForeignKey("dbo.TruckStates", "TruckId", "dbo.TruckInfoes");
            DropForeignKey("dbo.TruckInfoes", "DriverId", "dbo.StaffInfoes");
            DropForeignKey("dbo.TransportInfoes", "RevisorId", "dbo.StaffInfoes");
            DropForeignKey("dbo.StaffPowerInfoes", "StaffId", "dbo.StaffInfoes");
            DropForeignKey("dbo.StaffPowerInfoes", "RoleId", "dbo.RoleInfoes");
            DropForeignKey("dbo.StaffGroups", "RoleId", "dbo.RoleInfoes");
            DropForeignKey("dbo.PowerRoleLinks", "RoleId", "dbo.RoleInfoes");
            DropForeignKey("dbo.PowerRoleLinks", "PowerId", "dbo.PowerInfoes");
            DropForeignKey("dbo.PowerMenuLinks", "PowerId", "dbo.PowerInfoes");
            DropForeignKey("dbo.PowerMenuLinks", "MenuId", "dbo.MenuInfoes");
            DropForeignKey("dbo.PowerFileLinks", "PowerId", "dbo.PowerInfoes");
            DropForeignKey("dbo.PowerFileLinks", "FileId", "dbo.FileInfoes");
            DropForeignKey("dbo.OrderWaybillLinks", "WaybillId", "dbo.WayBills");
            DropForeignKey("dbo.OrderWaybillLinks", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.OrderStaffLinks", "StaffInfo_Id", "dbo.StaffInfoes");
            DropForeignKey("dbo.OrderDetails", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.NoticeOperateLogs", "OperatorId", "dbo.StaffInfoes");
            DropForeignKey("dbo.NoticeOperateLogs", "NoticeId", "dbo.NoticeInfoes");
            DropForeignKey("dbo.NoticeInfoes", "OperatorId", "dbo.StaffInfoes");
            DropForeignKey("dbo.FileOperateLogs", "OperatorId", "dbo.StaffInfoes");
            DropForeignKey("dbo.FileOperateLogs", "FileId", "dbo.FileInfoes");
            DropForeignKey("dbo.CargoSenders", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.CargoReceivers", "OrderId", "dbo.Orders");
            DropForeignKey("dbo.Orders", "StartWebsiteId", "dbo.WebsiteInfoes");
            DropForeignKey("dbo.Orders", "EndWebsiteId", "dbo.WebsiteInfoes");
            DropForeignKey("dbo.AuthCodes", "StaffId", "dbo.StaffInfoes");
            DropForeignKey("dbo.AccountOperateLogs", "OperatorId", "dbo.StaffInfoes");
            DropForeignKey("dbo.AccountOperateLogs", "ModifiedId", "dbo.StaffInfoes");
            DropIndex("dbo.WebSiteOperateLogs", new[] { "WebsiteId" });
            DropIndex("dbo.WebSiteOperateLogs", new[] { "OperatorId" });
            DropIndex("dbo.WaybillTransportLinks", new[] { "TransportInfoId" });
            DropIndex("dbo.WaybillTransportLinks", new[] { "WayBillId" });
            DropIndex("dbo.TruckStates", new[] { "TruckId" });
            DropIndex("dbo.TruckInfoes", new[] { "DriverId" });
            DropIndex("dbo.TransportInfoes", new[] { "RevisorId" });
            DropIndex("dbo.StaffPowerInfoes", new[] { "RoleId" });
            DropIndex("dbo.StaffPowerInfoes", new[] { "StaffId" });
            DropIndex("dbo.StaffGroups", new[] { "RoleId" });
            DropIndex("dbo.PowerRoleLinks", new[] { "PowerId" });
            DropIndex("dbo.PowerRoleLinks", new[] { "RoleId" });
            DropIndex("dbo.PowerMenuLinks", new[] { "MenuId" });
            DropIndex("dbo.PowerMenuLinks", new[] { "PowerId" });
            DropIndex("dbo.PowerFileLinks", new[] { "FileId" });
            DropIndex("dbo.PowerFileLinks", new[] { "PowerId" });
            DropIndex("dbo.OrderWaybillLinks", new[] { "WaybillId" });
            DropIndex("dbo.OrderWaybillLinks", new[] { "OrderId" });
            DropIndex("dbo.OrderStaffLinks", new[] { "StaffInfo_Id" });
            DropIndex("dbo.OrderDetails", new[] { "OrderId" });
            DropIndex("dbo.NoticeOperateLogs", new[] { "NoticeId" });
            DropIndex("dbo.NoticeOperateLogs", new[] { "OperatorId" });
            DropIndex("dbo.NoticeInfoes", new[] { "OperatorId" });
            DropIndex("dbo.FileOperateLogs", new[] { "FileId" });
            DropIndex("dbo.FileOperateLogs", new[] { "OperatorId" });
            DropIndex("dbo.CargoSenders", new[] { "OrderId" });
            DropIndex("dbo.Orders", new[] { "EndWebsiteId" });
            DropIndex("dbo.Orders", new[] { "StartWebsiteId" });
            DropIndex("dbo.CargoReceivers", new[] { "OrderId" });
            DropIndex("dbo.AuthCodes", new[] { "StaffId" });
            DropIndex("dbo.AccountOperateLogs", new[] { "ModifiedId" });
            DropIndex("dbo.AccountOperateLogs", new[] { "OperatorId" });
            DropTable("dbo.WebSiteOperateLogs");
            DropTable("dbo.WaybillTransportLinks");
            DropTable("dbo.TruckStates");
            DropTable("dbo.TruckInfoes");
            DropTable("dbo.TransportInfoes");
            DropTable("dbo.StaffPowerInfoes");
            DropTable("dbo.StaffGroups");
            DropTable("dbo.RoleInfoes");
            DropTable("dbo.PowerRoleLinks");
            DropTable("dbo.PowerMenuLinks");
            DropTable("dbo.PowerInfoes");
            DropTable("dbo.PowerFileLinks");
            DropTable("dbo.WayBills");
            DropTable("dbo.OrderWaybillLinks");
            DropTable("dbo.OrderStaffLinks");
            DropTable("dbo.OrderDetails");
            DropTable("dbo.NoticeOperateLogs");
            DropTable("dbo.NoticeInfoes");
            DropTable("dbo.MenuInfoes");
            DropTable("dbo.FileOperateLogs");
            DropTable("dbo.FileInfoes");
            DropTable("dbo.CargoSenders");
            DropTable("dbo.WebsiteInfoes");
            DropTable("dbo.Orders");
            DropTable("dbo.CargoReceivers");
            DropTable("dbo.AuthCodes");
            DropTable("dbo.StaffInfoes");
            DropTable("dbo.AccountOperateLogs");
        }
    }
}
