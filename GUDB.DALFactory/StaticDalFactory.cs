using GUDB.DAL;
using GUDB.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using GUDB.Model;

namespace GUDB.DALFactory
{
    /// <summary> 
    //   依赖接口编程 抽象工厂  进行实例化 new  
    /// </summary>
    public class StaticDalFactory
    {
        //通项 返回值为接口 发送返回值不会产生变化influence
        //public static string assemblyname = System.Configuration.ConfigurationManager.AppSettings["DalAssemblyName"];

        //public string assembname = "GUDB.DAL";

        public static IUserDal GetUserDal()
        {
             return new UserDal();

            //使用反射创建实例 ，这样就不需要修改  加载程序集


            /////把上边new 去掉，改一个配置就可以实现创建实例的变化   不需要改变代码
            //return Assembly.Load("GUDB.DAL").CreateInstance("GUDB.Dal"+"UserDal")
            //  as IUserDal;  //反射实例化


           // return Assembly.Load(assemblyname).CreateInstance(assemblyname + "UserDal") as IUserDal;


        }

        public static ITypeDal GetTypeDal()
        {
            return new TypeDal();

           //return Assembly.Load(assemblyname).CreateInstance(assemblyname + "TypeDal") as ITypeDal;
        }
        /// <summary>      /// <returns></returns>
        public static IEventDal GetEventDal()
        {
            return new EventDal();
            //return Assembly.Load(assemblyname).CreateInstance(assemblyname + "EventDal") as IEventDal;

        }


        public static IDamageDal GetDamageDal()
            { return new DamageDal(); }


        public  static IDamageBuildingDal GetDamageBuildingDal() { return new DamageBuildingDal(); }

        public static IDamageOtherDal GetDamageOtherDal() { return new DamageOtherDal(); }

        public static IDamagePeopleDal GetDamagePeopleDal() { return new DamagePeopleDal(); }


         public static IDamageResourceDal GetDamageResourceDal() { return new DamageResourceDal(); }
        public static IAdminDal AdminDal() { return new AdminDal(); }

        public static IUnitDal UnitDal() { return new UnitDal(); }
        public static IUserLoginDal UserLoginDal() { return new UserLoginDal(); }
        public static IInvestigatorDal InvestigatorDal() { return new InvestigatorDal(); }
    }
}
