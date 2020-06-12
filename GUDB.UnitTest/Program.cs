using GUDB.BLL;
using GUDB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using GUDB.IDAL;
using GUDB.DAL;
using System.Data.Entity;
using GUDB.DALFactory;

namespace GUDB.UnitTest
{
    class Program
    {
        static void Main(string[] args)
        {
            //            #region
            //            //UserService userService = new UserService();
            //            ////IQueryable<GUDB.Model.User>  users = userService.GetEntities(u => true);
            //            ////foreach(var user in users)
            //            ////{
            //            ////    Console.WriteLine(user.UName);
            //            ////}
            //            User user = new User();
            //            user.UName = "张三";
            //            //userService.Add(user);

            //            ////UserDal userdal = Assembly.Load("GUDB.DAL").CreateInstance("GUDB.Dal" + "UserDal")
            //            //// as UserDal;
            //            ////try
            //            ////{
            //            ////    ///测试获取上下 
            //            ////    Console.WriteLine(userdal.dbContext);
            //            ////}

            //            ////catch (Exception e)
            //            ////{
            //            ////    Console.WriteLine(e);
            //            ////}
            //            ////userdal.Add(user);

            //            ////GUDBDbContext dbcontext = DbContextFactory.GetCurrentDbContext();
            //            //////GUDBContext context = new GUDBContext();
            //            ////IQueryable<User> users = dbcontext.Users.AsQueryable();
            //            ////foreach (var item in users)
            //            ////{
            //            ////    Console.WriteLine(item.UIdName);
            //            ////}
            //            ///

            //            ////IDbSession dbSession = new DbSession();
            //            //////Console.Write(dbSession.UserDal.ToString());
            //            ////dbSession.UserDal.Add(user);
            //            ////dbSession.SaveChanges();
            //            //////dbSession.UserDal.save
            //            //////UserService userService = new UserService();
            //            //////Console.WriteLine(userService.CurrentDal.ToString());

            //            ////Console.WriteLine(dbSession.UserDal);

            //            //UserService userService = new UserService();
            //            //Console.WriteLine(userService.CurrentDal);

            //            //Console.WriteLine(userService.dbSession);

            //            //Console.WriteLine(userService.Add(user));

            //            //Console.WriteLine("嗯嗯");

            //#endregion

            ////TypeService typeService = new TypeService();
            ////typeService.Add(new Model.Type { TId = 1, TName = "EarthQuake" });
            ///////
            ////UserService userService = new UserService();
            ////User user = new User() { UId = 1, UName = "zja" };
            ////userService.Update(user);

            ////userService.dbSession.SaveChanges();

            ////UserService userService = new UserService();
            //EventService eventService = new EventService();
            ////////datetime2 
            //eventService.Add((new Event { EId = "1233".ToString(), TId = 1, ELong = 12.3, ELat = 12, ETime = DateTime.Now, Elevel = 12 }));


            ////userService.Add(new User { UName = "巨野微山" });

            //userService.Delete(new User { UId=6,UName = "巨野微山" });

            //UserLoginService userLoginService = new UserLoginService();
            //userLoginService.Add(new UserLogin { UId=1,ULIP="123",ULAdress="",ULTime=DateTime.Now});
            //userLoginService.Add(new UserLogin { UId = 1, ULIP = "123", ULAdress = "", ULTime = DateTime.Now });
            //userLoginService.Add(new UserLogin { UId = 2, ULIP = "123", ULAdress = "", ULTime = DateTime.Now });
            //userLoginService.Add(new UserLogin { UId = 4, ULIP = "123", ULAdress = "", ULTime = DateTime.Now });
            //userLoginService.Add(new UserLogin { UId = 9, ULIP = "123", ULAdress = "", ULTime = DateTime.Now });


            //地质灾害表
            TypeService typeService = new TypeService();

            //typeService.Add(new Model.Type { TId=2,TName="MudSlide"});
            //typeService.Add(new Model.Type {TId=3,TName="Karst Collapse" }); //增加岩溶塌陷
            //增加土壤侵蚀
            //typeService.Add(new Model.Type {TId=4,TName="Soil erosion" });
            //胀缩土
            typeService.Add(new Model.Type { TId = 5, TName = "Shrinkage Soil" });
             typeService.Add(new Model.Type { TId=6,TName="Coast erosion"});   //海岸侵蚀
        }
    }
}
