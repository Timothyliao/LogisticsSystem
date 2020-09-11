using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace LogisticsSystem.Model
{
    public class LogisticsContext : DbContext
    {
        public LogisticsContext()
            : base("name=conn")
        {
            //自动创建表，如果Entity有改到就更新到表结构
            Database.SetInitializer<LogisticsContext>(null);
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
        }

        public void Set<T>(T trucksate)
        {
            throw new NotImplementedException();
        }

        public DbSet<AccountOperateLog> AccountOperateLogs { get; set; }
        public DbSet<AuthCode> AuthCodes { get; set; }
        public DbSet<CargoReceiver> CargoReceivers { get; set; }
        public DbSet<CargoSender> CargoSenders { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<OrderWaybillLink> OrderWaybillLinks { get; set; }
        public DbSet<RoleInfo> RoleInfos { get; set; }
        public DbSet<StaffInfo> StaffInfos { get; set; }
        public DbSet<StaffPowerInfo> StaffPowerInfos { get; set; }
        public DbSet<TruckInfo> TruckInfos { get; set; }
        public DbSet<TruckState> TruckStates { get; set; }
        public DbSet<WayBill> WayBills { get; set; }
        public DbSet<WaybillTransportLink> WaybillTransportLinks { get; set; }
        public DbSet<WebsiteInfo> WebsiteInfos { get; set; }
        public DbSet<WebSiteOperateLog> WebSiteOperateLogs { get; set; }
        public DbSet<Section> Sections { get; set; }
        public DbSet<InsuranceInfo> InsuranceInfos { get; set; }
        public DbSet<TruckTrace> TruckTraces { get; set; }
        public DbSet<OrderTrace> OrderTraces { get; set; }
    }

}