using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
namespace GUDB.Model
{
    public class GUDBContext:DbContext
    {
        public GUDBContext()
                    : base("name=GUDBContext")
        {
            //Database.SetInitializer<GUDBContext>(null);
            //自动创建表，如果Entity有改到就更新到表结构
            Database.SetInitializer<GUDBContext>(new MigrateDatabaseToLatestVersion<GUDBContext, GUDB.Model.Migrations.Configuration>());
        }

        //重写
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //表名为类名，不是上面带s的名字  //移除复数表名的契约
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            ////不创建EdmMetadata表  //防止黑幕交易 要不然每次都要访问 EdmMetadata这个表
            //modelBuilder.Conventions.Remove<IncludeMetadataConvention>();    
            modelBuilder.Conventions.Remove<OneToOneConstraintIntroductionConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

        }

        //public void SaveChanges()
        //{
        //    throw new NotImplementedException();
        //}


        //所有的类在上下文注册一下

        public DbSet<User> Users { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<Event> Events { get; set; }

        public DbSet<Damage> Damages { get; set; }

        
        public DbSet<DamageBuilding> DamageBuildings { set; get; }

        public DbSet<DamageOther> DamageOthers { set; get; }
        
        public DbSet<Admin> Admins { get; set; }
        public DbSet<DamagePeople> DamagePeoples { set; get; }

        public DbSet<DamageResource> DamageResources { set; get; }
        public DbSet<Investigator> Investigators { set; get; }
        public DbSet<Unit> Units { set; get; }

        public DbSet<UserLogin> UserLogins { set; get; }

        


    }
}
