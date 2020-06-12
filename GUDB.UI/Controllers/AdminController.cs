using GUDB.BLL;
using GUDB.DAL;
using GUDB.Model;
using GUDB.UI.Models;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts; //四个方面： 事件发生 类型时间地点（经纬度、地名：村/   山头）、自然属性强度（强度（管理员自定义）/细节）、社会造成的影响（经济、人口伤亡、建筑物）  

//以上是数据库核心   

    /*非核心信息*/
    //查询：
    //用户类型：注册用户（可以提供修改  提供身份证号  记录修改日期 ip）、
    
    //数据演示： 经纬度、时间范围

    
    //附加功能; 十年前以来所有的数据（列表、图形 //五分钟快速显示（按时间排序）  时间近的颜色比较深旧的久远//）


    //重要是设计思想  自己展示尽量短
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;
using System.Web.UI.WebControls;

namespace GUDB.UI.Controllers
{
    public class AdminController : Controller
    {

        public AdminService adminService = new AdminService();

        public UserService userService = new UserService();

        public UnitService unitService = new UnitService();


        TypeService typeService = new TypeService();

        /*********************************首页*************************** */
        //公 用
        #region 首页

        [Authorize]
        // GET: Admin
        public ActionResult Index()
        {

            HttpContext context = System.Web.HttpContext.Current;

            //3.根据FormsAuthentication.FormsCookieName从上下文请求中获取到Cookie
            var cookie = context.Request.Cookies[FormsAuthentication.FormsCookieName];



            //  如果票据不为空 证明已经登录
            if (cookie != null)
            {
                //cookies
                if (!string.IsNullOrWhiteSpace(cookie.Value))
                {

                    //反  解
                    var ticket = FormsAuthentication.Decrypt(cookie.Value);
                    //// if(ticket.UserData..ToString()==null)
                    ////{
                    ////    Response.Write("<scritp>alert()</script>");
                    ////}

                   // string naod = String.Format("<script>console.log('geshu：{0}个数：用户{1} {2}  ')",ticket,ticket.UserData,ticket.Name); Response.Write(naod);
                    if (ticket.UserData == "Admin")
                    {

                        return View();
                    }



                    else
                    {

                        Response.Write("<script>alert('您没有权限');location='../Home/Index'</script>");
                        return View();
                    }



                    //5.判断票据的UserData，如果不为空则反序列化为实体


                    #region 得出全部人
                    ////if (!string.IsNullOrWhiteSpace(ticket.UserData))
                    ////{
                    ////    var dtoModel = ticket.UserData;

                    ////    Response.Write(dtoModel);

                    ////    ////6.将上下文中的User数据实例化，通过MyFormsPrincipal的构造函数 ticket，userData

                    ////    //context.User = new MyFormsPrincipal<LoginUserDTOModel>(ticket, dtoModel);
                    ////}
                    #endregion
                }









                //为空
                else
                {
                    Response.Write("<script>alert('您还未登陆)</script>");
                    return View("Login");
                }


            }




            else
            {
                Response.Write("<script>alert('您还未登陆')</script>");

                return View("Login");

            }

        }


  

        #region 登录


        [HttpGet]
        public ActionResult Login()
        {

            //Response.Write(User.Identity.GetUserName());
            //首先查看管理员是否登录 不登录继续

            //对票据进行解
            System.Web.HttpContext context = System.Web.HttpContext.Current;

            //3.根据FormsAuthentication.FormsCookieName从上下文请求中获取到Cookie
            var cookie = context.Request.Cookies[FormsAuthentication.FormsCookieName];


            #region  登录了
            //  如果票据不为空 证明已经登录
            if (cookie != null)
            {
                //4.解密cookie.Value获得票据
                if (!string.IsNullOrWhiteSpace(cookie.Value))
                {

                    //反  解
                    var ticket = FormsAuthentication.Decrypt(cookie.Value);

                    Response.Write(ticket.Name);
                    //5.判断票据的UserData，如果不为空则反序列化为实体

                    return RedirectToAction("Index", "Admin");
                    #region 得出全部人
                    ////if (!string.IsNullOrWhiteSpace(ticket.UserData))
                    ////{
                    ////    var dtoModel = ticket.UserData;

                    ////    Response.Write(dtoModel);

                    ////    ////6.将上下文中的User数据实例化，通过MyFormsPrincipal的构造函数 ticket，userData

                    ////    //context.User = new MyFormsPrincipal<LoginUserDTOModel>(ticket, dtoModel);
                    ////}
                    #endregion
                }

                //为空
                else { return View(); }
            }

            #endregion


            else
            {
                return View();

            }


        }



        [HttpPost]
        public ActionResult Login(Admin admin)
        {

            // Response.Write(admin.AName);
            //AdminService adminService = new AdminService();

            //判断是否存在
            int IsExist = adminService.GetEntities(u => u.AName == admin.AName).Count();
            if (IsExist == 0)
            {
                string recommand = string.Format("<script>alert('管理员{0}不存在')</script>", admin.AName);
                Response.Write(recommand);
                return View(admin);
            }

            else
            {
                Admin AdminUser = adminService.GetEntities(u => u.AName == admin.AName).FirstOrDefault();
                //区处对象
                //string password = adminService.GetEntities(u => u.AName == admin.AName).FirstOrDefault().APassword;
                if (AdminUser.APassword != admin.APassword)
                {
                    string recommand = string.Format("<script>alert('管理员{0}密码错误')</script>", admin.AName);
                    Response.Write(recommand);
                    return View(admin);
                }

                //登陆成功
                else
                {

                    // 第一种方法设置 
                    // FormsAuthentication.SetAuthCookie(admin.AName, true);

                    //第二种


                    HttpContext context = System.Web.HttpContext.Current;
                    //加载上下文
                    #region 加密

                    ////
                    ////2.用它来序列化要对象
                    //JavaScriptSerializer serial = new JavaScriptSerializer();


                    ////5. 生成初始化凭据
                    //FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                    //    1,
                    //    admin.AName,
                    //    DateTime.Now,
                    //    DateTime.Now.AddMinutes(30),
                    //    false,
                    //    serial.Serialize(adminService.GetEntities(u => u.AName == admin.AName).FirstOrDefault())
                    //    );


                    ////6. 加密
                    //string encryptedTicket = FormsAuthentication.Encrypt(authTicket);



                    ////7. 响应到客户端
                    //System.Web.HttpCookie authCookie = new System.Web.HttpCookie(FormsAuthentication.FormsCookieName*,encryptedTicket);
                    //System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);

                    #endregion

                    JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();


                    //string data = javaScriptSerializer.Serialize(AdminUser);
                    //对 用户信息加密   Json序列

                    //存这个 
                    string data = "Admin";


                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2,
                         admin.AName,
                         DateTime.Now,
                         DateTime.Now.AddMinutes(30),
                         false, data);
                    //创建 初始化票据

                    //垃圾 //Session["TYPE"] = "Admin";



                    var EncryptionTicket = FormsAuthentication.Encrypt(ticket);

                    //对票据进行加密



                    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, EncryptionTicket);//读取配置文件Forms 的name值 
                    cookie.HttpOnly = true;
                    cookie.Secure = FormsAuthentication.RequireSSL;
                    cookie.Domain = FormsAuthentication.CookieDomain;
                    cookie.Path = FormsAuthentication.FormsCookiePath;
                    cookie.Expires = DateTime.Now.Add(FormsAuthentication.Timeout);

                    if (context == null)
                    {
                        throw new ArgumentNullException("context为空");
                    }



                    //写入Cookie     先删除再添加
                    context.Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
                    context.Response.Cookies.Add(cookie);
                    Response.Write("<script>alert('登陆成功');location='../Admin/Index'</script>");
                }



            }
            return View();
        }

        #endregion

        #endregion

        /**********************************用户管理*******************************************/

        #region 用户


        [Authorize]
        #region 用户


        [Authorize]
        public ActionResult UserList(string key, string type, int? page)
        {


            //UserService userService = new UserService();

            //正常访问全都为空
            if (key == "" || key == null || type == null || type == "")
            {
                IQueryable<User> users = userService.GetEntities(u => true);
                //全部

                //int pageNumber = 1;
                int pageNumber = page ?? 1; //默认为

                //每页显示多少条  
                //int pageSize = 5;
                int pageSize = 10;
                users = users.OrderBy(x => x.UId);
                //damagePeoples = damagePeoples.OrderBy(x => x.DPId);
                IPagedList<User> UserListPagedList = users.ToPagedList(pageNumber, pageSize);
                return View(UserListPagedList);
            }



            else
            {

                IQueryable<User> users;

                //处理
                switch (type)
                {
                    case "0":
                        {
                            users = userService.GetEntities(u => u.UId.ToString() == key);
                            break;
                        }
                    case "1":
                        {
                            users = userService.GetEntities(u => u.UName == key);
                            break;
                        }
                    case "2":
                        { users = userService.GetEntities(u => u.UPhone == key); break; }
                    case "3":
                        { users = userService.GetEntities(u => u.UMail == key); break; }
                    case "4":
                        { users = userService.GetEntities(u => u.UIdName == key); break; }
                    case "5":
                        { users = userService.GetEntities(u => u.UIdNumber == key); break; }

                    default:
                        {
                            users = userService.GetEntities(u => true); break;
                        }

                }


                //int pageNumber = 1;
                int pageNumber = page ?? 1; //默认为

                //每页显示多少条  
                //int pageSize = 5;
                int pageSize = 10;
                users = users.OrderBy(x => x.UId);
                //damagePeoples = damagePeoples.OrderBy(x => x.DPId);
                IPagedList<User> UserListPagedList = users.ToPagedList(pageNumber, pageSize);
                return View(UserListPagedList);
            }
        }

        #endregion




        [Authorize]
        #region 修改

        [HttpGet]
        // 返回表
        public ActionResult UserChange(int id)
        {
            ////UserService userService = new UserService();
            User user = userService.GetEntities(m => m.UId == id).FirstOrDefault();
            return View(user);
        }


        [HttpPost]
        //修改具体

        public ActionResult UserChange(User user)
        {
            //UserService userService = new UserService();

            userService.Update(user);
            userService.dbSession.SaveChanges();

            Response.Write("<Script>alert('修改成功')</Script>");
            return View(user);
        }


        #endregion



        public ActionResult Welcome()
        {
            return View();
        }

        #region 删除—————用来显示和真删除
        public ActionResult UserDelete(int id)
        {
            //////UserService userService = new UserService();
            User user = userService.GetEntities(m => m.UId == id).FirstOrDefault();
            return View(user);
        }


        [HttpPost]
        public ActionResult UserDelete(User user)
        {

            //////////UserService userService = new UserService();

            userService.Delete(user);

            Response.Write("<script>alert('删除成功')</script>");
            return View("Welcome");

        }

        #endregion



        #region  创建用户

        [Authorize]



        public ActionResult UserCreate()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserCreate(User user)
        {

            if (!ModelState.IsValid) { return View(); }
            else
            {
                userService.Add(user);
                string recommend = string.Format("<script>alert('创建用户{0}成功')</script>", user.UName);
                Response.Write(recommend);

                return View("UserLook", user);
            }
        }
        #endregion



        #region 查看用户

        public ActionResult UserLook(int Id)
        {



            User user = userService.GetEntities(m => m.UId == Id).FirstOrDefault();

            return View(user);
        }


        #endregion




        #region 登录记录
        public ActionResult UserLoginIn(string id, int? page)
        {
            if (id == "" || id == null) { Response.Write("<Script>alert('参数错误')</script>"); return View("Welcome"); }

            //
            UserLoginService userLoginService = new UserLoginService();

            IQueryable<UserLogin> userLogins = userLoginService.GetEntities(x => x.UId.ToString() == id);

            //int pageNumber = 1;
            int pageNumber = page ?? 1; //默认为

            //每页显示多少条  
            //int pageSize = 5;
            int pageSize = 10;
            userLogins = userLogins.OrderBy(u => u.UId);
            //damagePeoples = damagePeoples.OrderBy(x => x.DPId);
            IPagedList<UserLogin> UserListPagedList = userLogins.ToPagedList(pageNumber, pageSize);
            return View(UserListPagedList);
        }
        #endregion
        #endregion

       
        /*********************************地震管理********************************************/
        #region 地震

        [Authorize]
        public ActionResult EarthQuake()
        {
            return View();
        }

        #region 地震数据显示
        [Authorize]    //直接跳转
        public ActionResult EarthQuakeList(string key,int? tid, string type, int? page) 
        {
            ///参数说明： key是关键词  tid是那灾害   type是选择的那种类型进行搜索
            EventService eventService = new EventService();

            //if() //默认跳转地震
            //第一次跳转
            //if (ViewBag.TId != null ) { tid = Convert.ToInt32(ViewData["TId"]); }


            //第一次跳转
            //else { ViewBag.TId = tid; };

            ViewData["TId"] = tid;
           ViewData["TName"] = eventService.GetEntities(E=>E.TId==tid).First().Type.TName;  //显示地质灾害名称


            //正常访问全都为空
            if (key == "" || key == null || type == null || type == "")
            {
                IQueryable<Event> events = eventService.GetEntities(n => n.Type.TId ==tid);

                //int pageNumber = 1;
                int pageNumber = page ?? 1; //默认为

                //每页显示多少条  
                //int pageSize = 5;
                int pageSize = 10;
                events = events.OrderBy(x => x.EId);
                //damagePeoples = damagePeoples.OrderBy(x => x.DPId);
                IPagedList<Event> EventListPagedList = events.ToPagedList(pageNumber, pageSize);
                return View(EventListPagedList);
            }


            //携带参数
            else
            {
                key = key.Trim(); //处理传过来的数据

                
                IQueryable<Event> events;

                //处理      文字模糊查询
                switch (type)
                {
                    case "0":
                        {
                            events = eventService.GetEntities(u => u.EId.ToString() == key&&u.TId==tid);
                            break;
                        }
                    case "1":
                        {
                            events = eventService.GetEntities(u => u.ETime.ToString() == key&&u.TId==tid);
                            break;
                        }
                    case "2":
                        { events = eventService.GetEntities(u => (u.ELat.ToString()==key&&u.TId==tid)); break; }
                    case "3": //纬度误差不超过5
                        { events = eventService.GetEntities(u => (u.ELong.ToString()==key&& u.TId==tid)); break; }
                    case "4":
                        { events = eventService.GetEntities(u => u.ELocation.Contains(key)&& u.TId == tid); break; }

                    case "8":
                        { events = eventService.GetEntities(u => u.Elevel.ToString() == key&&u.TId == tid); break; }
                    case "5":
                        { events = eventService.GetEntities(u => u.ERange.Contains(key) && u.TId == tid); break; }
                    case "6":
                        { events = eventService.GetEntities(u => u.EEarthDes.Contains(key) && u.TId == tid); break; }

                    case "7":
                        { events = eventService.GetEntities(u => u.EDamageDes.Contains(key) && u.TId == tid); break; }
                    default:
                        {
                            events = eventService.GetEntities(u => u.TId == tid); break;
                        }

                }


                //int pageNumber = 1;
                int pageNumber = page ?? 1; //默认为

                //每页显示多少条  
                //int pageSize = 5;
                int pageSize = 10;
                events = events.OrderBy(x => x.EId);
                //damagePeoples = damagePeoples.OrderBy(x => x.DPId);
                IPagedList<Event> EventListPagedList = events.ToPagedList(pageNumber, pageSize);
                return View(EventListPagedList);
            }



        }

      //  [Authorize]    ///使用了搜索
   



        #endregion




        [Authorize]
        #region 查看地震 

        
        // 根据传过来参数 tid 返回
        public ActionResult EarthQuakeLook(string id)
        {
            EventService eventService = new EventService();

            Event @event = eventService.GetEntities(e => e.EId == id).FirstOrDefault();

            return View(@event);
        }



        #endregion



        #region 修改地震
        [HttpGet]
        public ActionResult EarthQuakeChange(string id)
        {
            EventService eventService = new EventService();

            return View((eventService.GetEntities(e => e.EId == id).FirstOrDefault()));
        }
        
        
        [HttpPost]

        public ActionResult EarthQuakeChange(Event @event)
        {
            //UserService userService = new UserService();

            EventService eventService = new EventService();
            eventService.Update(@event);
            eventService.dbSession.SaveChanges();

            Response.Write("<Script>alert('修改成功')</Script>");
            return View(@event);
        }
        #endregion



        #region 创建地震事


        [Authorize]

        //根据跳转页面来确定是哪一种事

        public ActionResult EarthQuakeCreate(int tid)
        {
            ViewData["Type"] = tid;  //第一次进入  这样就会保存在那个页面来决定这是一个什么样的页面 
            ViewData["TName"] =new EventService().GetEntities(e =>e.TId==tid).First().Type.TName;
            return View();
        }






        [HttpPost]
        public ActionResult EarthQuakeCreate(Event @event)
        {
            //传过来不包含TName
             ////ViewData["TName"] = @event.Type.TName;
            ViewData["Type"] =@event.TId;  //重复创建  如果是第一次那么久已经传过来了
            //////string recom11mend = string.Format("<script>alert('经度为{0} 纬度为{1}的{2}{3}{4} {5}地震事件创建 成功')</script>",@event.TId,@event.TId,@event.TId,@event.TId,@event.TId,@event.TId);
            //////Response.Write(recom11mend);

            //处理数据
            EventService eventService = new EventService();
            @event.ETime = Convert.ToDateTime(@event.ETime);


            ViewData["TName"] = new EventService().GetEntities(e => e.TId == @event.TId).First().Type.TName;

            //eventService.Add(@event);
            //if (@event.EId == "" || @event.EId == null)
            //{
            //    string recommend = string.Format("<script>alert('经度为{0} 纬度为{1}的{2}{3}{4} {5}地震事件创建 成功')</script>", @event.EId, @event.TId, @event.TId, @event.EId, @event.ETime, @event.EId);
            //    Response.Write(recommend);
            //    return View();
            //}


            //数据不合法
            if (@event.EId == "" || @event.EId == null)
            {
                string recommend = string.Format("<script>alert('主键事件Id不能为空')</script>");
                Response.Write(recommend);
                return View(@event);
            }
            else
            {
                if (@event.ETime == null)
                {
                    string recommend = string.Format("<script>alert('时间不能为空')</script>");
                    Response.Write(recommend);
                    return View(@event);
                }


                if (@event.ELat.ToString() == null || @event.ELat.ToString() == "" || @event.ELong.ToString() == "" || @event.ELong.ToString() == null)
                {
                    string recommend = string.Format("<script>alert('经纬度格式错误')</script>");
                    Response.Write(recommend);
                    return View(@event);
                }


                if (@event.Elevel.ToString() == null || @event.Elevel.ToString() == "")
                {
                    string recommend = string.Format("<script>alert('事件强烈等级不能为空')</script>");
                    Response.Write(recommend);
                    return View(@event);
                }
                //数据都可以


                //检测是是否Id  坚持就是胜利

                int Exist = eventService.GetEntities(u => u.EId == @event.EId).Count();

                if (Exist != 0)
                {
                    string recommend = string.Format("<script>alert('事件编码{0}的已经存在   ')</script>", @event.EId);
                    Response.Write(recommend);
                    return View();
                }


                else
                {

                    #region Test
                    /*
                    string recommend1 = string.Format("<script>alert('eivle:{0}')</script>", @event.Elevel);
                    Response.Write(recommend1);
                    @event.Elevel = Convert.ToInt32(@event.Elevel);
                    recommend1 = string.Format("<script>alert('eivle:{0}')</script>", @event.Elevel);
                    Response.Write(recommend1);



                    string recommend2 = string.Format("<script>alert('tid:{0}')</script>", @event.TId); Response.Write(recommend2);
                    @event.TId = Convert.ToInt32(@event.TId);
                    recommend2 = string.Format("<script>alert('tid:{0}')</script>", @event.TId); Response.Write(recommend2);




                    string recommend3 = string.Format("<script>alert('Etime:{0}     ')</script>", @event.ETime);
                       Response.Write(recommend3);



                    //////////Response.Write(recommend);
                    string recommend4 = string.Format("<script>alert('elong:{0}')</script>", @event.ELong); Response.Write(recommend4);
                    @event.ELong =Convert.ToDouble(@event.ELong);
                    recommend4 = string.Format("<script>alert('elong:{0}')</script>", @event.ELong); Response.Write(recommend4);

                    string recommend6 = string.Format("<script>alert('eat:{0}')</script>", @event.ELat);//eventService.Add(@event);
                    Response.Write(recommend6); @event.ELat = Convert.ToDouble(@event.ELat); Response.Write(recommend6);
                    string recommend5 = string.Format("<script>alert('elocation:{0}')</script>", @event.ELocation); Response.Write(recommend5);



                    //Response.Write(recommend6); 

                    string recommend7 = string.Format("<script>alert('eid:{0}')</script>", @event.EId);*/
                    #endregion


                    //插入数据


                    eventService.Add(@event);

                    string recommend = string.Format("<script>alert('Id为{0}经度为{1} 纬度为{2}的{3}地震事件创建 成功')</script>", @event.EId, @event.ELong, @event.ELat, @event.ELocation);
                    Response.Write(recommend);
                    //    return View();
                    return View(@event);
                }

            }

        }

        #endregion

      



        #region   删除
        //浏览    

        public ActionResult EarthQuakeDelete(string id)
        {

            EventService eventService = new EventService();
            Event @event = eventService.GetEntities(e  =>e.EId==id).FirstOrDefault();
            //eventService.Delete(eventService.GetEntities(e=>e.EId==id).FirstOrDefault());

            ViewData["Type"] = @event.TId; //返回事件类型 
            return View(@event);
        }


        [HttpPost]
        public ActionResult EarthQuakeDelete(Event @event)
        {
            //反复提交
            ////if (@event == null||@event.EId==""||@event.EId==null)
            ////{
            ////    string recom11mend = String.Format("<script>alert('该用户已删除')</script>");
            ////    Response.Write(recom11mend); return View();
            ////}

            ////else {
            ///


            //string redicrtion2 = String.Format("<script>alert('删除成功{0}')</script>",@event.TId);
            /////对于重复删除
            ////////Response.Write("<script>alert('删除成功');location='../Admin/Index'</script>");
            //   Response.Write(redicrtion2);
            //int eidmemory = @event.TId;   //记录
                ViewData["Type"] = @event.TId;  //直接返回 ty
                EventService eventService = new EventService();
               

            //删除所有时间相关
            DamagaeBuildingService damagaeBuildingService = new DamagaeBuildingService();
            DamageService damageService = new DamageService();
            DamagePeopleService damagePeopleService = new DamagePeopleService();
            if (damagaeBuildingService.GetEntities(e => e.EId == @event.EId).Count() > 0)
            {
                damagaeBuildingService.Delete(damagaeBuildingService.GetEntities(e => e.EId == @event.EId).First());
            }
            if (damageService.GetEntities(e => e.EId == @event.EId).Count()>0)
            { damageService.Delete(damageService.GetEntities(e => e.EId == @event.EId).First()); }/*}*/
            if (damagePeopleService.GetEntities(e => e.EId == @event.EId).Count() > 0) 
            { 
            damagePeopleService.Delete(damagePeopleService.GetEntities(e => e.EId == @event.EId).FirstOrDefault());
           }

        eventService.Delete(@event);


            /*Response.Write("<script>alert('删除成功')</script>"); */

            string redicrtion = String.Format("<script>alert('删除成功{0}');location='../welcome'</script>", ViewData["Type"]);
            ///对于重复删除
            //////Response.Write("<script>alert('删除成功');location='../Admin/Index'</script>");
            Response.Write(redicrtion);

            return View();
        }

        #endregion

       // #region 建筑详细信息
       //public ActionResult EarthQuakeDetailBuildingList(string Id, int? page)
       // {
       //     if (Id == "" || Id == null)
       //     {
       //         return Redirect("~/Admin/Index");
       //     }

       //     else
       //     {

       //         DamagaeBuildingService damagaeBuildingService = new DamagaeBuildingService();

       //         //多 个值
       //         IQueryable<DamageBuilding> damageBuildings = damagaeBuildingService.GetEntities(u => u.EId == Id);


       //         //string s  = string.Format("<script>alert('{0}')</script>",num);
       //         //Response.Write(s);

       //         //第几页  
       //         //int pageNumber = 1;
       //         int pageNumber = page ?? 1; //默认为

       //         //每页显示多少条  
       //         //int pageSize = 5;
       //         int pageSize = 10;


       //         //根据ID升序排序  
       //         damageBuildings = damageBuildings.OrderBy(x => x.DBId);

       //         //通过ToPagedList扩展方法进行分页  
       //         IPagedList<DamageBuilding> DamageBuildingListPagedList = damageBuildings.ToPagedList(pageNumber, pageSize);

       //         //将分页处理后的列表传给View 
       //         return View(DamageBuildingListPagedList);

       //         //return View(damageBuildings);
       //     }

       // }


       // #endregion


        #region Other
        public ActionResult EarthQuakeDetailOtherList(string Id, int? page)
        {

            if (Id == "" || Id == null)
            {
                return Redirect("~Admin/Index");
             }
            else
            {


                DamageOtherService damageOtherService = new DamageOtherService();
    //获取人
    IQueryable<DamageOther> damageOthers = damageOtherService.GetEntities(u => u.EId == Id);

    //int pageNumber = 1;
    int pageNumber = page ?? 1; //默认为

    //每页显示多少条  
    //int pageSize = 5;
    int pageSize = 10;
    damageOthers = damageOthers.OrderBy(x => x.DOId);
                //damagePeoples = damagePeoples.OrderBy(x => x.DPId);
                IPagedList<DamageOther> DamageOtherListPagedList = damageOthers.ToPagedList(pageNumber, pageSize);
                return View(DamageOtherListPagedList);
    //return View(damagePeoples);
}
        }

        #endregion




        #region  每个事件的图片视频资源

        public ActionResult EarthQuakeDetailResourceList()
        {
            return View();
        }
        #endregion




        #endregion


        /*********************************损失人员管理********************************************/
        #region  人员损失管理
        #region 人员损失详细信息
        /// <summary>
        /// 查看人员损失详细信息
        /// </summary>
        /// <returns></returns>
        public ActionResult EarthQuakeDetailPeopleList(string Id, int? page)
        {
            
            ///传递参数 Id是事件Id
            ///

            if (Id == "" || Id == null)
            {
                return Redirect("~/Admin/Index");
            }
            else
            {


                DamagePeopleService damagePeopleService = new DamagePeopleService();
                //获取人
                IQueryable<DamagePeople> damagePeoples = damagePeopleService.GetEntities(u => u.EId == Id);

                if (damagePeoples.Count() != 0) { ViewData["Type"] = damagePeoples.FirstOrDefault().Event.TId; ViewData["EventId"] = damagePeoples.FirstOrDefault().EId; }    //找到的话返回这个类型的

                //else { ViewData["Type"] = damagePeoples.FirstOrDefault().Event.TId; }
                else { ViewData["Type"] = "1"; }  //默认地震
       

                //int pageNumber = 1;
                int pageNumber = page ?? 1; //默认为

                //每页显示多少条  
                //int pageSize = 5;
                int pageSize = 10;

                damagePeoples = damagePeoples.OrderBy(x => x.DPId);
                IPagedList<DamagePeople> DamageBuildingListPagedList = damagePeoples.ToPagedList(pageNumber, pageSize);

    
                return View(DamageBuildingListPagedList);
                //return View(damagePeoples);
            }
        }
        #endregion


        #region  人员损失修改
        [Authorize]
        public ActionResult EarthQuakeDetailPeopleChange(string id)
        {

            DamagePeopleService damagePeopleService = new DamagePeopleService();

            return View(damagePeopleService.GetEntities(dp => dp.DPId == id).FirstOrDefault());

        }


        [HttpPost]

        public ActionResult EarthQuakeDetailPeopleChange(DamagePeople damagePeople)
        {
            DamagePeopleService damagePeopleService = new DamagePeopleService();
            damagePeopleService.Update(damagePeople);
            damagePeopleService.dbSession.SaveChanges();

            Response.Write("<Script>alert('修改成功')</Script>");
            return View(damagePeople);
        }

        #endregion



        #region 人员损失添加
        
        [HttpGet]
        /// <summary>
        /// 参数 事件Id  
        /// </summary>  第一次进入
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EarthQuakeDetailPeopleCreate(string id)
        {
            if (id == "" || id == null)
            {
                string redicrtion = String.Format("<script>alert('添加失败，请联系管理员');location='../Admin/Welcome'</script>");
                ///对于重复删除
                //////Response.Write("<script>alert('删除成功');location='../Admin/Index'</script>");
                Response.Write(redicrtion);
                return View("Index");
            }

            else
            {
                ViewData["EId"] = id;
               // ViewData["EventId"] = id;  //第一次进入  这样就会保存在那个页面来决定这是一个什么样的页面     这是一个事件Id
                //ViewData["EName"] = new EventService().GetEntities(e => e.TId.ToString() == id).First().Type.TName;

                return View();
            }
        }

        // 返回调查员数
        public JsonResult investigatorData()
        {
            //按照i进入数据库查找
            //UnitService unitService = new UnitService();

            InvestigatorService investigatorService = new InvestigatorService();

            IQueryable<Model.Investigator> investigators =investigatorService.GetEntities(u => true
            );

            List<ShowDataInvestigator> showDataInvestigators = new List<ShowDataInvestigator>();

            int i = 1;
            foreach (var item in investigators) //循
            {
                showDataInvestigators.Add(new ShowDataInvestigator
                {
                    id = item.IId,
                    name = item.IName
                });
                i++;
            }

            //string data;
            ////eventService.GetPageEntities(10,);
            //data = JsonConvert.SerializeObject(unit); 

            string data = JsonConvert.SerializeObject(showDataInvestigators);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 真实创建
        /// </summary>
        /// <param name="damagePeople"></param>
        /// <returns></returns>
       
            [HttpPost]
        public ActionResult EarthQuakeDetailPeopleCreate(DamagePeople damagePeople)
        {

            damagePeople.IId = damagePeople.IId.Replace("\"", "");  //去掉双引号
            ViewData["EId"] = damagePeople.EId;  //第一次进入  这样就会保存在那个页面来决定这是一个什么样的页面 
            //ViewData["EName"] = new EventService().GetEntities(e => e.TId.ToString() == damagePeople.EId).First().Type.TName;

            DamagePeopleService damagePeopleService = new DamagePeopleService();

       




                //检测是是否Id  坚持就是胜利

                int Exist = damagePeopleService.GetEntities(u => u.DPId == damagePeople.DPId).Count();

                if (Exist != 0)
                {
                    string recommend = string.Format("<script>alert('该伤亡人员{0}的已经存在   ')</script>",damagePeople.DPName);
                    Response.Write(recommend);
                    return View();
                }


                else
                {



                    damagePeopleService.Add(damagePeople);

                    string recommend = string.Format("<script>alert('身份证号{0}，名为{1}的伤亡用户添加成功')</script>", damagePeople.DPId,damagePeople.DPName);
                    Response.Write(recommend);
                    //    return View();
                    return View(damagePeople);
                }

            }

        

        #endregion


        #region  人员损失删除

        [Authorize]
        [HttpGet]
        public ActionResult EarthQuakeDetailPeopleDelete(string id)
        {
            //参数id代表身份号
            DamagePeopleService damagePeopleService = new DamagePeopleService();
            DamagePeople damagePeople = damagePeopleService.GetEntities(e => e.DPId==id).FirstOrDefault();
            //eventService.Delete(eventService.GetEntities(e=>e.EId==id).FirstOrDefault());
            if (damagePeople.Event.TId.ToString() == "" || damagePeople.Event.TId.ToString() == null)
            {
                return Redirect("~/Admin/Index");
            }
            else
            {
                ViewData["Type"] = damagePeople.Event.TId; //返回事件类型 
                return View(damagePeople);
            }
            
        }
        [Authorize]
        [HttpPost]
        public ActionResult EarthQuakeDetailPeopleDelete(DamagePeople damagePeople)
        {
            {
                //反复提交
                ////if (@event == null||@event.EId==""||@event.EId==null)
                ////{
                ////    string recom11mend = String.Format("<script>alert('该用户已删除')</script>");
                ////    Response.Write(recom11mend); return View();
                ////}

                ////else {
                ///


                //string redicrtion2 = String.Format("<script>alert('删除成功{0}')</script>",@event.TId);
                /////对于重复删除
                ////////Response.Write("<script>alert('删除成功');location='../Admin/Index'</script>");
                //   Response.Write(redicrtion2);
                //int eidmemory = @event.TId;   //记录
                //ViewData["Type"] = damagePeople.Event.TId;  //直接返回 ty
                DamagePeopleService damagePeopleService = new DamagePeopleService();
                damagePeopleService.Delete(damagePeople);


                /*Response.Write("<script>alert('删除成功')</script>"); */

                string redicrtion = String.Format("<script>alert('删除成功');location='../welcome'</script>");
                ///对于重复删除
                //////Response.Write("<script>alert('删除成功');location='../Admin/Index'</script>");
                Response.Write(redicrtion);
                return View("Welcome");
            }
        }

        #endregion






        #endregion



        /************************************损失建筑管理******************************************/
        #region

        #region  建筑损失详细
        public ActionResult EarthQuakeDetailBuildingList(string Id, int? page)
        {

            ///传递参数 Id是事件Id
            ///

            if (Id == "" || Id == null)
            {
                return Redirect("~/Admin/Index");
            }
            else
            {


                 DamagaeBuildingService damageBuildingService = new DamagaeBuildingService();

                IQueryable<DamageBuilding> damageBuildings = damageBuildingService.GetEntities(u => u.EId== Id); //事件



                

                if (damageBuildings.Count() != 0) {
                    //事件类型
                    ViewData["Type"] = damageBuildings.FirstOrDefault().Event.TId; 
                    ViewData["EventId"] = damageBuildings.FirstOrDefault().EId;
                    ViewBag.EventName = damageBuildings.FirstOrDefault().Event.Type.TName; 
                }    //找到的话返回这个类型的

                else { ViewData["Type"] = "1"; }  //默认地震


                //int pageNumber = 1;
                int pageNumber = page ?? 1; //默认为

                //每页显示多少条  
                //int pageSize = 5;
                int pageSize = 10;

                damageBuildings = damageBuildings.OrderBy(x => x.DBId);
                IPagedList<DamageBuilding> DamageBuildingListPagedList = damageBuildings.ToPagedList(pageNumber, pageSize);


                return View(DamageBuildingListPagedList);
                //return View(damagePeoples);
            }
        }




        #endregion

        #region 创建
        public ActionResult EarthQuakeDetailBuildingCreate(string id)
        {
            if (id == "" || id == null)
            {
                string redicrtion = String.Format("<script>alert('添加失败，请联系管理员');location='../Admin/Welcome'</script>");
                Response.Write(redicrtion);
                return View("Index");
            }

            else
            {
                ViewData["EId"] = id;
                // ViewData["EventId"] = id;  //第一次进入  这样就会保存在那个页面来决定这是一个什么样的页面     这是一个事件Id
                //ViewData["EName"] = new EventService().GetEntities(e => e.TId.ToString() == id).First().Type.TName;



                return View();
            }
        }

        [HttpPost]
        /// <summary>
        /// rEAL
        /// </summary>
        /// <param name="damagePeople"></param>
        /// <returns></returns>

        public ActionResult EarthQuakeDetailBuildingCreate(DamageBuilding damagaeBuilding)
        {

            ViewData["EId"] = damagaeBuilding.EId;  //第一次进入  这样就会保存在那个页面来决定这是一个什么样的页面 
            damagaeBuilding.IId = damagaeBuilding.IId.Replace("\"", "");  //去掉双引号
            DamagaeBuildingService damagaeBuildingService = new DamagaeBuildingService();


            int Exist = damagaeBuildingService.GetEntities(u => u.DBId == damagaeBuilding.DBId).Count();

            if (Exist != 0)
            {
                string recommend = string.Format("<script>alert('建筑损失编号{0}的已经存在   ')</script>", damagaeBuilding.DBId);
                Response.Write(recommend);
                return View();
            }


            else
            {



                damagaeBuildingService.Add(damagaeBuilding);

                string recommend = string.Format("<script>alert('建筑编码为{0}添加成功')</script>",  damagaeBuilding.DBId);
                Response.Write(recommend);
                //    return View();
                return View(damagaeBuilding);
            }

        }
        #endregion

        #region  删除

        [HttpGet]
        public ActionResult EarthQuakeDetailBuildingDelete(string id)
        {
            if (id == null || id == "")
            {
                string redicrtion = String.Format("<script>alert('参数错误');location='../welcome'</script>");
                Response.Write(redicrtion);
                return View("Welcome");
            }
            else {     //}
                DamagaeBuildingService damagaeBuildingService = new DamagaeBuildingService();
            return View(damagaeBuildingService.GetEntities(d => d.DBId.ToString() == id).FirstOrDefault());
        }
        }
        
        
        [HttpPost]
        public ActionResult EarthQuakeDetailBuildingDelete(DamageBuilding damageBuilding)
        {
            {

                DamagaeBuildingService damagaeBuildingService = new DamagaeBuildingService();
                damagaeBuildingService.Delete(damageBuilding);


                string redicrtion = String.Format("<script>alert('删除成功');location='../welcome'</script>");
                Response.Write(redicrtion);
                return View("Welcome");
            }
        }





        #endregion
        #region 修改

        public ActionResult EarthQuakeDetailBuildingChange(string id)
        {

            DamagaeBuildingService damagaeBuildingService = new DamagaeBuildingService();

            return View(damagaeBuildingService.GetEntities(dp => dp.DBId == id).FirstOrDefault());

        }

        [HttpPost]

        public ActionResult       EarthQuakeDetailBuildingChange(DamageBuilding damageBuilding)
        {
            DamagaeBuildingService damagaeBuildingService = new DamagaeBuildingService();
            damagaeBuildingService.Update(damageBuilding);
            damagaeBuildingService.dbSession.SaveChanges();

            Response.Write("<Script>alert('修改成功')</Script>");
            return View(damageBuilding );
        }

        #endregion



        #endregion




        /*******************************其他地质灾害*********************************************/
        #region

        #region 海峡
        ////public ActionResult EarthQuake()
        ////{
        ////    return View();
        ////}

        #endregion





        #region  泥石流

        public ActionResult MudSlide()
    {
        return View();
    }




        #endregion
       
          
        #region

        //        //////Cookies加密
        //        ////public void UserInfoToCookid(LoginUserDTOModel dtomodel)
        //        ////{
        //        ////    //var data = JsonOrObject.ToJson(dtomodel);
        //        ////    var data = dtomodel.ToJson();//获取用户数据转换为Json


        //        ////    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2, "AdminUser", DateTime.Now, DateTime.Now.AddDays(1), false, data);//创建票据FormsAuthenticationTicket
        //        ////    var EncryptionTicket = FormsAuthentication.Encrypt(ticket);//对票据进行加密
        //        ////                                                               //创建Cookie HttpCookie FormsAuthentication.FormsCookieName

        //        ////    HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName, EncryptionTicket);//读取配置文件Forms 的name值 
        //        ////    cookie.HttpOnly = true;
        //        ////    cookie.Secure = FormsAuthentication.RequireSSL;
        //        ////    cookie.Domain = FormsAuthentication.CookieDomain;
        //        ////    cookie.Path = FormsAuthentication.FormsCookiePath;
        //        ////    cookie.Expires = DateTime.Now.Add(FormsAuthentication.Timeout);


        //        ////    HttpContext context = HttpContext.CurrentHandler;
        //        ////    //先删除后 添加
        //        ////    context.Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
        //        ////    context.Response.Cookies.Add(cookie);
        //        ////}

        //        #region 解
        //        public void soe(object sender) {

        //       //1.通过sender获取HttpApplication

        //            HttpApplication app = sender as HttpApplication;


        //            //2.拿到HTTP上下文
        //            HttpContext context = app.Context;


        //            //3.根据FormsAuthentication.FormsCookieName从上下文请求中获取到Cookie
        //            var cookie = context.Request.Cookies[FormsAuthentication.FormsCookieName];
        //            if (cookie != null)
        //            {
        //                //4.解密cookie.Value获得票据
        //                if (!string.IsNullOrWhiteSpace(cookie.Value))
        //                {
        //                    var ticket = FormsAuthentication.Decrypt(cookie.Value);
        //                    //5.判断票据的UserData，如果不为空则反序列化为实体
        //                    if (!string.IsNullOrWhiteSpace(ticket.UserData))
        //                    {
        //                        var dtoModel = ticket.UserData.ToObject<LoginUserDTOModel>();

        //                        //6.将上下文中的User数据实例化，通过MyFormsPrincipal的构造函数 ticket，userData
        //                        context.User = new MyFormsPrincipal<LoginUserDTOModel>(ticket, dtoModel);
        //                    }
        //                }
        //            }
        //        }

        //}
        //        #endregion
        #endregion



        /*******************************岩溶塌陷*********************************************/

        public ActionResult KarstCollapse()
        {
            return View();
        }

        /*******************************土壤侵蚀*********************************************/
        public ActionResult Soilerosion()
        { return View(); }
        /*******************************胀缩土********************************************/
        public ActionResult Shrinkagesoil()
        {
            return View();
        }

        /****************************海岸侵蚀********************************************/
        public ActionResult CoastErosion() {
            return View();
        }



        #endregion




        /**********************************地质灾害调查单位管理**********************************/
        #region
        [Authorize]
        #region 调查单位表
        public ActionResult UnitList(string key, string type, int? page)
        {
            /////UnitService unitService = new UnitService();

            //正常访问全都为空
            if (key == "" || key == null || type == null || type == "")
            {
                IQueryable<Model.Unit> units = unitService.GetEntities(u => true);
                //全部

                //int pageNumber = 1;
                int pageNumber = page ?? 1; //默认为

                //每页显示多少条  
                //int pageSize = 5;
                int pageSize = 10;
                units = units.OrderBy(x => x.UId);
                //damagePeoples = damagePeoples.OrderBy(x => x.DPId);
                IPagedList<Model.Unit> UserListPagedList = units.ToPagedList(pageNumber, pageSize);
                return View(UserListPagedList);
            }



            //如果查询就
            else
            {
                IQueryable < Model.Unit> units;

                //处理
                switch (type)
                {

                    // / 根据Id查找
                    case "0":
                        {
                            units = unitService.GetEntities(u => u.UId.ToString() == key);
                            break;
                        }


                    ///根据单位名称查找
                    case "1":
                        {
                            units = unitService.GetEntities(u => u.UName == key);
                            break;
                        }


                    ///根据单位地址
                    case "2":
                        { units= unitService.GetEntities(u => u.UAddress == key); break; }


                    ///根据单位邮编
                    case "3":
                        { units= unitService.GetEntities(u => u.UEmail== key); break; }


                    ///根据单位性质
                    case "4":
                        { units = unitService.GetEntities(u => u.UProperty == key); break; }
                    default:
                        {
                            units = unitService.GetEntities(u => true); break;
                        }

                }


                //int pageNumber = 1;
                int pageNumber = page ?? 1; //默认为

                //每页显示多少条  
                //int pageSize = 5;
                int pageSize = 10;
                units = units.OrderBy(x => x.UId);
                //damagePeoples = damagePeoples.OrderBy(x => x.DPId);
                IPagedList<Model.Unit> UserListPagedList = units.ToPagedList(pageNumber, pageSize);
                return View(UserListPagedList);

            }



            //return;
        }

        #endregion

        [Authorize]
        #region 修改
        public ActionResult UnitChange(string id)
        {  ////UserService userService = new UserService();
     
            Model.Unit unit = unitService.GetEntities(m => m.UId== id).FirstOrDefault();
            //return View(user);
            return View(unit);
        }



        [HttpPost]
        //修改具体

        public ActionResult UnitChange(Model.Unit unit)
        {
            
            unitService.Update(unit);
            unitService.dbSession.SaveChanges();

            Response.Write("<Script>alert('修改成功')</Script>");
            return View(unit);

        }

        #endregion




        [Authorize]
        #region 查 看
        public ActionResult UnitLook(string id)
        {

            Model.Unit unit = unitService.GetEntities(m => m.UId == id).FirstOrDefault();

            return View(unit);
        }



        #endregion

        [Authorize]
        #region   删  除
        public ActionResult UnitDelete(string id)
        {
            //////UserService userService = new UserService();
            Model.Unit unit = unitService.GetEntities(m => m.UId == id).FirstOrDefault();
            return View(unit);
        }


        [HttpPost]
        public ActionResult UnitDelete(Model.Unit unit)
        {

            //////////UserService userService = new UserService();

            unitService.Delete(unit);

            Response.Write("<script>alert('删除成功')</script>");
            return View("Welcome");

        }
        #endregion


        [Authorize]
         #region    创建 单位

        ////  //[Authorize]
        public ActionResult UnitCreate()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult UnitCreate(Model.Unit unit)
        {

            if (!ModelState.IsValid) { return View(); }

            /*//  if  *///判断是否存在
            else{
                int Exist = unitService.GetEntities(u => u.UId == unit.UId).Count();

            if (Exist != 0)
            {
                string recommend = string.Format("<script>alert('单位ID为{0}的已经存在   ')</script>", unit.UId);
                Response.Write(recommend);
                return View(unit);
            }
            else
            {
                unitService.Add(unit);
                string recommend = string.Format("<script>alert('创建调查单位{0}成功')</script>", unit.UName);
                Response.Write(recommend);

                return View("UnitLook", unit);
            }
        }
        }
        #endregion


        [Authorize]
        #region 单位人员
        public ActionResult UnitPerson(string id,string type,string key, int? page)
        {
            ViewBag.Id = id; ;  
          InvestigatorService investigatorService = new InvestigatorService();
          if (investigatorService.GetEntities(i => i.UId == id).Count()==0)  //显示单位名
        
            {
                ViewData["Name"] = "";
            }

          else
            {
                ViewBag.Name = investigatorService.GetEntities(i => i.UId == id).FirstOrDefault().Unit.UName;  //显示单位名

            }
            //uid
          
          if (key == null || key == ""||type==""||type==null)
            {

                {
                    IQueryable<Investigator> investigators = investigatorService.GetEntities(u => u.UId==id);
                    //全部

                    //int pageNumber = 1;
                    int pageNumber = page ?? 1; //默认为

                    //每页显示多少条  
                    //int pageSize = 5;
                    int pageSize = 10;
                    investigators = investigators.OrderBy(x => x.IId);
                    //damagePeoples = damagePeoples.OrderBy(x => x.DPId);
                    IPagedList<Investigator> InvestigatorListPagedList = investigators.ToPagedList(pageNumber, pageSize);
                    return View(InvestigatorListPagedList);
                }
            }



            else
            { 

                    IQueryable<Investigator> investigators;

                    //处理
                    switch (type)
                    {

                     

                    ///电话
                        case "0":
                            {
                                investigators =investigatorService.GetEntities(u => u.IPhone.ToString() == key);
                                break;
                            }

                        ///身份证号
                        case "1":
                            {
                                investigators = investigatorService.GetEntities(u => u.IId == key);
                                break;
                            }

                       ///电子邮箱
                        case "2":
                            { investigators = investigatorService.GetEntities(U=>U.IEmail==key);
                            
                            break; }

                        
                        /// 姓名
                        case "3":
                            { investigators = investigatorService.GetEntities(u => u.IName == key); break; }
                     
                        default:
                            {
                                investigators = investigatorService.GetEntities(u => true); break;
                            }

                    }






                    //int pageNumber = 1;
                    int pageNumber = page ?? 1; //默认为

                    //每页显示多少条  
                    //int pageSize = 5;
                    int pageSize = 10;
                    investigators = investigators.OrderBy(x => x.IId);
                    //damagePeoples = damagePeoples.OrderBy(x => x.DPId);
                    IPagedList<Investigator>  InvestiagtorsListPagedList = investigators.ToPagedList(pageNumber, pageSize);
                return View(InvestiagtorsListPagedList);
                }
                 //investigatorService.GetEntities(I => I.UId == id).}
        }
        #endregion

         
        [Authorize]
        #region 参与的事件
        public ActionResult UnitEvent(string id,int? page)
        {
            //地质灾害伤害人员
            DamagePeopleService damagePeopleService = new DamagePeopleService();
            //其他
            DamageOtherService damageOtherService = new DamageOtherService();
            //建筑
            DamagaeBuildingService damagaeBuildingService = new DamagaeBuildingService();
            if (damagePeopleService.GetEntities(i => i.IId== id).Count() == 0)  //显示单位名

            {
                ViewData["Name"] = "";
            }

            else
            {
                ViewBag.Name = damagePeopleService.GetEntities(i => i.IId == id).FirstOrDefault().Investigator.IName;  //显示单位名

            }



            ///查找所有并去重


            
            
            EventService eventService = new EventService();
            List<Event> events = new List<Event>();
            Event @event = new Event();


            
            //把所有关于这个机构所有的调查伤亡人数存在来
            
            
            IQueryable<DamagePeople> DPList= damagePeopleService.GetEntities(dp=>dp.Investigator.Unit.UId==id);

            //Response.Write(DPList.FirstOrDefault().Event.ELat+"\n");

            
            foreach (var item in DPList)
            {
                //Response.Write(item.Event.EId + "\n");
                //存取对应的事件
                @event = eventService.GetEntities(e => e.EId == item.Event.EId).FirstOrDefault();
               
                events.Add(@event);
                //Response.Write(@event.EId); //添加每一个

                //Response.Write(events.Count()+ "\n");

            }
            //Response.Write(events.FirstOrDefault().ELocation) ;


            // 把这个机构所有的建筑损失
            IQueryable<DamageBuilding> DBList = damagaeBuildingService.GetEntities(db=>db.Investigator.Unit.UId==id);
            foreach(var item in DBList){
                //Response.Write(item.Event.EId + "\n");
                //存取对应的事件
                @event = eventService.GetEntities(e => e.EId == item.Event.EId).FirstOrDefault();
                events.Add(@event);
                  //Response.Write(@event.EId); //添加每一个

                //Response.Write(events.Count()+ "\n");
            }


            //把所有关于这个机构所有的其他损失存起来
            IQueryable<DamageOther> DOList = damageOtherService.GetEntities( m=>m.Investigator.Unit.UId==id);
            foreach(var item in DOList)
            {
                //Response.Write(item.Event.EId + "\n");
                //存取对应的事件
                @event = eventService.GetEntities(e => e.EId == item.Event.EId).FirstOrDefault();
                events.Add(@event);
                ////Response.Write(@event.EId); //添加每一个

                //Response.Write(events.Count()+ "\n");
            }

            ///总结

            events.Distinct();//去重

            IQueryable<Event> eventsAll = events.AsQueryable();

            eventsAll = eventsAll.Distinct<Event>();  //去重




            
            int pageNumber = page ?? 1; //默认为

       
            int pageSize = 10;
            eventsAll = eventsAll.OrderBy(x=>x.EId);
            IPagedList<Event> EventsListPagedList =eventsAll.ToPagedList(pageNumber, pageSize);
            return View(EventsListPagedList);





        }
        #endregion

        #endregion




        /**********************************调查人员管理**********************************/

        #region 调查人

        [Authorize]
      #region 调查人员列表
        public ActionResult investigatorList(string key, string type, int? page)
        {
            InvestigatorService investigatorService = new InvestigatorService();
            ////Investigator investigator = new Investigator()  ;

            //正常访问全都为空
            if (key == "" || key == null || type == null || type == "")
            {
                IQueryable<Investigator>  investigators = investigatorService.GetEntities(u => true);
                //全部

                //int pageNumber = 1;
                int pageNumber = page ?? 1; //默认为

                //每页显示多少条  
                //int pageSize = 5;
                int pageSize = 10;
              investigators =  investigators.OrderBy(x => x.UId);
                //damagePeoples = damagePeoples.OrderBy(x => x.DPId);
                IPagedList<Investigator> UserListPagedList = investigators.ToPagedList(pageNumber, pageSize);
                return View(UserListPagedList);
            }



            //如果查询就
            else
            {
                IQueryable<Investigator> investigators;

                //处理
                switch (type)
                {

                    // / 根据身份证号码
                    case "0":
                        {
                            investigators = investigatorService.GetEntities(u => u.IId.ToString() == key);
                            break;
                        }


                    ///根据姓名查找
                    case "1":
                        {
                            investigators = investigatorService.GetEntities(u => u.IName == key);
                            break;
                        }


                    ///根据单位地址
                    case "2":
                        { investigators = investigatorService.GetEntities(u => u.IPhone == key); break; }


                    ///根据电邮
                    case "3":
                        { investigators = investigatorService.GetEntities(u => u.IEmail== key); break; }

                    default:
                        {
                           investigators = investigatorService.GetEntities(u => true); break;
                        }

                }


                //int pageNumber = 1;
                int pageNumber = page ?? 1; //默认为

                //每页显示多少条  
                //int pageSize = 5;
                int pageSize = 10;
                investigators =  investigators.OrderBy(x => x.UId);
                //damagePeoples = damagePeoples.OrderBy(x => x.DPId);
                IPagedList<Investigator> UserListPagedList = investigators.ToPagedList(pageNumber, pageSize);
                return View(UserListPagedList);

            }

        }
        #endregion



        [Authorize]
        #region 查看
        public ActionResult investigatorLook(string id)
        {
            InvestigatorService investigatorService = new InvestigatorService();
            Investigator investigator = investigatorService.GetEntities(I=>I.IId==id).FirstOrDefault();
            return View(investigator);
        }
        #endregion



        [Authorize]
        #region  修改调查人员
        [HttpGet]
        public ActionResult investigatorChange(string id)
        {  ////UserService userService = new UserService();

            if (id == "" || id == null) { return View(); }

            else
            {
                InvestigatorService investigatorService = new InvestigatorService();

                Investigator investigator = investigatorService.GetEntities(m => m.IId == id).FirstOrDefault();

                ViewData["UID"] = investigator.UId;
                //return View(user);
                return View(investigator);
            }
            
        }
        [HttpPost]
        [Authorize]
        public ActionResult investigatorChange(Investigator investigator)
        {  ////UserService userService = new UserService();

            //Response.Write(string.Format("<script>alert('{0}')</script>", investigator.UId));
            InvestigatorService investigatorService = new InvestigatorService();




            investigatorService.Update(investigator);
            investigatorService.dbSession.SaveChanges();

            Response.Write("<Script>alert('修改成功')</Script>");
            return View(investigator);

        }



        #endregion





        [Authorize]
        #region 创建调查人员
        public ActionResult investigatorCreate()
        { 


            return View();
        }
        [Authorize]
        [HttpPost]
        public ActionResult investigatorCreate(Investigator investigator)
        {
            //string s = string.Format("alert('{0}')",investigator.UId);
            //Response.Write(s);
            /* return View();*/
            InvestigatorService investigatorService = new InvestigatorService();

            if (!ModelState.IsValid) { return View(); }

            /*//  if  *///判断是否存在
            else
            {
                if (investigator.IId == "" || investigator.IId == null || investigator.IName == "" || investigator.IName == null || investigator.UId == null || investigator.UId == "")
                {
                    string recommend = string.Format("<script>alert('请填写必填项')</script>");
                    Response.Write(recommend);
                    return View(investigator);
                }

                else
                {
                    int Exist = investigatorService.GetEntities(u => u.IId == investigator.IId).Count();

                    if (Exist != 0)
                    {
                        string recommend = string.Format("<script>alert('身份证为{0}的 调查人员已经存在   ')</script>", investigator.IId);
                        Response.Write(recommend);
                        return View(investigator); ;
                    }
                    else
                    {
                        investigator.UId = investigator.UId.Replace("\"", "");  //去掉双引号
                       Response.Write(investigator.UId+investigator.IId+investigator.IName);
                        investigatorService.Add(investigator);
                        string recommend = string.Format("<script>alert('创建身份证号{0}的调查人员成功')</script>", investigator.IName);
                        Response.Write(recommend);

                        return View("investigatorLook", investigator);
                        //return view();
                    }

                }
            }
        }

        #endregion




        /// <summary>
        /// 后天传入前台Json
        /// </summary>
        /// <returns></returns>
        public JsonResult Data()
        {
            //按照单位进入数据库查找
            UnitService unitService = new UnitService();

            IQueryable<Model.Unit> unit = unitService.GetEntities(u => true
            );

            List<ShowDataUnit> showDataUnits = new List<ShowDataUnit>();

            int i = 1;
            foreach(var item in unit)
            {
                showDataUnits.Add(new ShowDataUnit
                {
                    id = i,
                    name = item.UName
                });
               i++;
            }

            //string data;
            ////eventService.GetPageEntities(10,);
            //data = JsonConvert.SerializeObject(unit); 

            string data = JsonConvert.SerializeObject(showDataUnits);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        #region 删除
        public ActionResult investigatorDelete(string id)
        {
            InvestigatorService investigatorService = new InvestigatorService();
            //////UserService userService = new UserService();
            Investigator investigator = investigatorService.GetEntities(m => m.IId == id).FirstOrDefault();
            return View(investigator);
        }


        [HttpPost]
        public ActionResult  investigatorDelete(Investigator investigator)
        {

            //////////UserService userService = new UserService();
            InvestigatorService investigatorService = new InvestigatorService();
            investigatorService.Delete(investigator);

            Response.Write("<script>alert('删除成功')</script>");
            return View("Welcome");

        }


        #endregion

        #endregion



        /**********************************上传管理**********************************/
        #region   
        /// <summary>
        /// 上传Excel
        /// </summary>
        /// <returns></returns>
        public ActionResult UpdateExcel(string type)
          {
            switch (type) //type 0导入用户 type 1导入调人员 2导入地质灾害 3导入调查单位
            {
                case "0": ViewData["type"] = "0"; ViewData["name"] = "用户"; break;

                case "1": ViewData["type"] = "1"; ViewData["name"] = "调查人员"; break;

                case "2": ViewData["type"] = "2"; ViewData["name"] = "地质灾害"; break;

                case "3": ViewData["type"] = "3"; ViewData["name"] = "调查单位"; break;
                default: break;

            }
            return View();
        }


        



        /// <summary>
        /// 批量添加事件
        /// </summary>
        /// <returns></returns>
        /// 



        public JsonResult UpdateEventData()
        {
            #region
            //接受Josn
            System.IO.Stream postData = Request.InputStream;
            System.IO.StreamReader sRead = new System.IO.StreamReader(postData);
            string postContent = sRead.ReadToEnd();
            sRead.Close();


            //忽略
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;
           
            // 反序列化成一个List
            List<Event> models = JsonConvert.DeserializeObject<List<Event>>(postContent, jsonSerializerSettings);


            //eventService.GetPageEntities(10,);
            //data = JsonConvert.SerializeObject(events1);

            //对List对象进行 循环插入
            // UserService userService = new UserService();
            EventService eventService = new EventService();
           List<IsUpdateNum> isUpdateNums= new List<IsUpdateNum>();
            foreach(var item in models)
            {

                //对重复的事件进行处理
                if (eventService.GetEntities(e => e.EId == item.EId).Count() != 0) {
                   // Response.Write("alert('存在重复事件')");
                    isUpdateNums.Add(new IsUpdateNum
                    {
                        Id = item.EId,
                        If = 0
                    }
                      );
                    continue; }

                else
                {
                    //Response.Write("alert('插入成功事件')");
                    isUpdateNums.Add(new IsUpdateNum
                    {
                        Id = item.EId,
                        If = 1
                    }
                    );
                    eventService.Add(item);
                }
            }

            string jsonData = JsonConvert.SerializeObject(isUpdateNums);

            return Json(jsonData, JsonRequestBehavior.AllowGet);
            #endregion
        }

        public JsonResult UpdateInvestigatorData()

        {
            #region
            //接受Josn
            System.IO.Stream postData = Request.InputStream;
            System.IO.StreamReader sRead = new System.IO.StreamReader(postData);
            string postContent = sRead.ReadToEnd();
            sRead.Close();


            //忽略
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;





            // 反序列化成一个List
            List<Investigator> models = JsonConvert.DeserializeObject<List<Investigator>>(postContent, jsonSerializerSettings);


            //eventService.GetPageEntities(10,);
            //data = JsonConvert.SerializeObject(events1);

            //对List对象进行 循环插入
            // UserService userService = new UserService();
            InvestigatorService investigatorService = new InvestigatorService();
            List<IsUpdateNum> isUpdateNums = new List<IsUpdateNum>();
            foreach (var item in models)
            {

                //对重复的事件进行处理
                if (investigatorService.GetEntities(i=>i.IId== item.IId).Count() != 0)
                {
                    // Response.Write("alert('存在重复事件')");
                    isUpdateNums.Add(new IsUpdateNum
                    {
                        Id = item.IId,
                        If = 0
                    }
                      );
                    continue;
                }

                else
                {
                    //Response.Write("alert('插入成功事件')");
                    isUpdateNums.Add(new IsUpdateNum
                    {
                        Id = item.IId,
                        If = 1
                    }
                    );
                    investigatorService.Add(item);
                }
            }

            string jsonData = JsonConvert.SerializeObject(isUpdateNums);

            return Json(jsonData, JsonRequestBehavior.AllowGet);
            #endregion
        }

        public JsonResult UpdateUserData()
        {
            #region
            //接受Josn
            System.IO.Stream postData = Request.InputStream;
            System.IO.StreamReader sRead = new System.IO.StreamReader(postData);
            string postContent = sRead.ReadToEnd();
            sRead.Close();


            //忽略
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;

            // 反序列化成一个List
            List<User> models = JsonConvert.DeserializeObject<List<User>>(postContent, jsonSerializerSettings);


            //eventService.GetPageEntities(10,);
            //data = JsonConvert.SerializeObject(events1);

            //对List对象进行 循环插入
            //UserService userService = new UserService();
            //EventService eventService = new EventService();
            List<IsUpdateNum> isUpdateNums = new List<IsUpdateNum>();
            foreach (var item in models)
            {

                //对重复的事件进行处理
                if (userService.GetEntities(e => e.UId == item.UId).Count() != 0)
                {
                    // Response.Write("alert('存在重复事件')");
                    isUpdateNums.Add(new IsUpdateNum
                    {
                        Id = item.UId.ToString(),
                        If = 0
                    }
                      );
                    continue;
                }

                else
                {
                    //Response.Write("alert('插入成功事件')");
                    isUpdateNums.Add(new IsUpdateNum
                    {
                        Id = item.UId.ToString(),
                        If = 1
                    }
                    );
                    userService.Add(item);
                }
            }

            string jsonData = JsonConvert.SerializeObject(isUpdateNums);

            return Json(jsonData, JsonRequestBehavior.AllowGet);
            #endregion

        }
        public JsonResult UpdateUnitData()

        {
            #region
            //接受Josn
            System.IO.Stream postData = Request.InputStream;
            System.IO.StreamReader sRead = new System.IO.StreamReader(postData);
            string postContent = sRead.ReadToEnd();
            sRead.Close();


            //忽略
            JsonSerializerSettings jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.NullValueHandling = NullValueHandling.Ignore;

            // 反序列化成一个List
            List<Model.Unit> models = JsonConvert.DeserializeObject<List<Model.Unit>>(postContent, jsonSerializerSettings);


            //eventService.GetPageEntities(10,);
            //data = JsonConvert.SerializeObject(events1);

            //对List对象进行 循环插入
            // UserService userService = new UserService();
            //EventService eventService = new EventService();
              UnitService unitService = new UnitService();
            List<IsUpdateNum> isUpdateNums = new List<IsUpdateNum>();
            foreach (var item in models)
            {

                //对重复的事件进行处理
                if (unitService.GetEntities(b=>b.UId==item.UId).Count()!=0)
                {
                    // Response.Write("alert('存在重复事件')");
                    isUpdateNums.Add(new IsUpdateNum
                    {
                        Id = item.UId,
                        If = 0
                    }
                      );
                    continue;
                }

                else
                {
                    //Response.Write("alert('插入成功事件')");
                    isUpdateNums.Add(new IsUpdateNum
                    {
                        Id = item.UId,
                        If = 1
                    }
                    );
                    unitService.Add(item);
                }
            }

            string jsonData = JsonConvert.SerializeObject(isUpdateNums);

            return Json(jsonData, JsonRequestBehavior.AllowGet);
            #endregion

        }
#endregion
        /***********************************下载管理***********************************/

        #region
        public  ActionResult DownLoadExcel()
        {
            return View();
        }


        [Authorize]
        //获取数据
        [HttpGet]
        public JsonResult DownloadAdminData(string type)
        {
            string data;
            switch (type) //type 0导入用户 type 1导入调人员 2导入地质灾害 3导入调查单位
            {
                case "0":{
                       IQueryable<User> users =  userService.GetEntities(u=>true);
                        data = JsonConvert.SerializeObject(users);
                        break;
                }
                case "1":

                    {
                        InvestigatorService investigatorService = new InvestigatorService();
                        IQueryable<Investigator> investigators = investigatorService.GetEntities(u => true);
                        data = JsonConvert.SerializeObject(investigators);
                        break;
                    }

                case "2":
                    {
                        EventService eventService = new EventService();IQueryable<Event> events = eventService.GetEntities(u=>true);
                        data = JsonConvert.SerializeObject(events);   break;
                    }
                case "3": {
                        UnitService unitService = new UnitService();
                        IQueryable<Model.Unit> units = unitService.GetEntities(u => true);
                        data = JsonConvert.SerializeObject(units);
                        break;
                    }

                default:data = "";break;
            }



            return Json(data,JsonRequestBehavior.AllowGet);
        }


        #endregion



        /********************************** 事件类型管理**********************************/
        #region 修改
          [Authorize]
        // 假装
        public ActionResult TypeChange(string id)
        {
            

            return View(typeService.GetEntities(t=>t.TId.ToString()==id).FirstOrDefault());
        }
        //public 

            [Authorize]
        [HttpPost]
        public ActionResult TypeChange(Model.Type type)
        {
            //if (type.TName==null||type.TName=="") { Response.Write("<Script>alert('地质灾害名称不能为空')</Script>"); return View (type); }
            //else
            {typeService.Update(type);

            typeService.dbSession.SaveChanges();

            Response.Write("<Script>alert('修改成功')</Script>");
            return View(type); }
        }

        #region 事件类型管理 
    [Authorize]
        public ActionResult TypeList(int? page,string TId, string key, string type)
        {
            IQueryable<Model.Type> types;

            //TypeService typeService = new TypeService();
            if (key == "" || key == null || type == null || type == "")
            {
               
              types  = typeService.GetEntities(u => true);
                //全部

                //int pageNumber = 1;
                int pageNumber = page ?? 1; //默认为

                //每页显示多少条  
                //int pageSize = 5;
                int pageSize = 10;
                types = types.OrderBy(t => t.TId);
                //damagePeoples = damagePeoples.OrderBy(x => x.DPId);
                IPagedList<Model.Type> UserListPagedList = types.ToPagedList(pageNumber, pageSize);
                return View(UserListPagedList);

            }
            // / return View();

            else 
            {

                //处理
                switch (type)
                {
                    case "0":
                        {
                            types =typeService.GetEntities(u =>u.TId .ToString() == key);
                            break;
                        }
                    case "1":
                        {
                            types = typeService.GetEntities(u => u.TName.Contains (key));
                            break;
                        }
                    case "2":
                        { types = typeService.GetEntities(u => u.TCharact.Contains(key)); break; }
                    case "3":
                        { types = typeService.GetEntities(u => u.TReason.Contains(key)); break; }
                    case "4":
                        { types = typeService.GetEntities(u => u.TDamageCharact.Contains(key)); break; }
                    default:
                        {
                            types = typeService.GetEntities(u => true); break;
                        }

                }


                //int pageNumber = 1;
                int pageNumber = page ?? 1; //默认为

                //每页显示多少条  
                //int pageSize = 5;
                int pageSize = 10;
                types = types.OrderBy(x => x.TId);
                //damagePeoples = damagePeoples.OrderBy(x => x.DPId);
                IPagedList<Model.Type> UserListPagedList = types.ToPagedList(pageNumber, pageSize);
                return View(UserListPagedList);
            }
        
        }
        #endregion
        #endregion

        [Authorize]
        [HttpPost]

        #region 
        public ActionResult TypeAdd(Model.Type type)
        {
            //

            // 设置事件Id
            type.TId = typeService.GetEntities( w =>true).Count()+1;

            typeService.Add(type);
            string recommend = string.Format("<script>alert('创建地质灾害事件{0}成功')</script>", type.TName);
            Response.Write(recommend);

            return View("TypeAdd");
            //return View();
        }
        [Authorize]
        public ActionResult TypeAdd()
        {return View();
        }
        #endregion


        public ActionResult TypeOne(string id)
        {

            return View(typeService.GetEntities(tp=>tp.TId.ToString()==id).FirstOrDefault());
        }
        #region 删除
        /* 真删除*/
        public ActionResult TypeMove(string id)
        {


            return View(typeService.GetEntities(type=>type.TId.ToString()==id).FirstOrDefault());

        }

        [HttpPost]
        public ActionResult TypeMove(Model.Type type)
        {

            typeService.Delete(type);

            Response.Write("<script>alert('删除成功')</script>");
          // return View("Welcome");
            return View("Welcome");
        }
        #endregion
    }





    //返回值
    public class IsUpdateNum
    {
        public string Id { get; set; }
        public int If { get; set; } //0代表  1代表不存在可以
    
    }



}




//123456789101112131415161718192021222324252627282930313233
//public class CookieController : Controller 
//{ public ActionResult Index() 
//    { ViewData["Message"] = "Welcome to ASP.NET MVC!"; 
//        string cookie = "There is no cookie!"; 
//        if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("Cookie")) 
//        { 
//            cookie = "Yeah - Cookie: " + this.ControllerContext.HttpContext.Request.Cookies["Cookie"].Value; } 
//        ViewData["Cookie"] = cookie; return View(); } public ActionResult Create() { HttpCookie cookie = new HttpCookie("Cookie"); cookie.Value = "Hello Cookie! CreatedOn: " + DateTime.Now.ToShortTimeString(); this.ControllerContext.HttpContext.Response.Cookies.Add(cookie); return RedirectToAction("Index", "Home"); } public ActionResult Remove() { if (this.ControllerContext.HttpContext.Request.Cookies.AllKeys.Contains("Cookie")) { HttpCookie cookie = this.ControllerContext.HttpContext.Request.Cookies["Cookie"]; cookie.Expires = DateTime.Now.AddDays(-1); this.ControllerContext.HttpContext.Response.Cookies.Add(cookie); } return RedirectToAction("Index", "Home"); } }







