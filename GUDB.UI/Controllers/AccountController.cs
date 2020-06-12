using Newtonsoft.Json;
using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Security;
using GUDB.Model;
using GUDB.BLL;
using GUDB.UI.Models;
using System.Web.Script.Serialization;
using Microsoft.AspNet.Identity;
using Microsoft.Ajax.Utilities;
using System.Threading;
using PagedList;
using System.Text.RegularExpressions;

namespace GUDB.UI.Controllers
{
    public class AccountController : Controller
    {

        public UserService userService = new UserService();



        #region 登录
        public ActionResult Login()
        {

            return View();
        }




        [HttpPost]

         public ActionResult Login(User user)
        {
            //数值过滤
            user.UName = user.UName.Trim();
            user.UPassword = user.UPassword.Trim();



            //判断是否存在
            int IsExist = userService.GetEntities(u => u.UName==user.UName).Count();
            if (IsExist == 0)
            {
                string recommand = string.Format("<script>alert('用户{0}不存在')</script>", user.UName);
                Response.Write(recommand);
                return View(user);
            }


            else
            {
                User loginUser =  userService.GetEntities(u =>u.UName == user.UName).FirstOrDefault();
                //区处对象
                //string password = adminService.GetEntities(u => u.AName == admin.AName).FirstOrDefault().APassword;
                if (user.UPassword != loginUser.UPassword)
                {
                    string recommand = string.Format("<script>alert('用户{0}密码错误')</script>",user.UName);
                    Response.Write(recommand);
                    return View();
                }


                else //登陆成功
                {
                    Ip ip = new Ip();  //初始化页面首先获取ip地址所在的地区
           //new Ip();
                    // 获取返回对象
                    ip = Get_Ip_Address();

                    //string recommand = string.Format("<script>alert('用户{0}密码错误')</script>", ip.ip);
                    //Response.Write(recommand);



                    //设置cookies



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


                   string data = javaScriptSerializer.Serialize(user);
                    //对 用户信息加密   Json序列



                    FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2,
                         user.UName,
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


                    UserLoginService userLoginService = new UserLoginService();


                    UserLogin userLogin = new UserLogin() { UId =loginUser.UId, ULIP = ip.ip, ULTime = DateTime.Now, ULAdress = ip.pro + ip.city };
                    
                    
                    
                    
                    
                    
                    userLoginService.Add(userLogin);
                    //登录历史保存到table

                    string recommand = string.Format("<script>alert('欢迎回来   {0}');location='../Home/Index'</script>",userLogin.User.UName);
                    Response.Write(recommand);

                    return View();
                }

            }







        }
        //清除小甜饼

        #endregion
        #region 
        public ActionResult Log_off()
        {

            HttpContext.Request.Cookies.Remove(FormsAuthentication.FormsCookieName);
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");

        }


        #endregion

        #region 注册 啦
        public ActionResult Register() { return View(); }




            [HttpPost]
        public ActionResult Register(User user)
        {//数值过滤
            user.UName = user.UName.Trim();
            user.UPassword = user.UPassword.Trim();

            if (user.UName == ""||user.UName==null)
            {
                string recommend = string.Format("<script>alert('用户名不能为空')</script>");
                Response.Write(recommend);
                return View(user);
            }
            else{//已存在

            if (userService.GetEntities(d => d.UName == user.UName).Count() != 0)
            {
                string recommend = string.Format("<script>alert('用户{0}已存在')</script>", user.UName);
                Response.Write(recommend);
                return View(user);
            }
            else
            {
                    
                    if (Request["com"] == "" ||Request["com"]==null|| user.UPassword == "" || user.UPassword==null)
                    {
                        string recommend = string.Format("<script>alert('密码和确认密码不能为空')</script>");
                        Response.Write(recommend);
                        return View(user);
                    }
                    else
                    {
                        if (Request["com"].ToString()!= user.UPassword)
                        {
                            string recommend = string.Format("<script>alert('密码不一致{0}{1}')</script>", user.UPassword, Request["com"].ToString());
                            Response.Write(recommend);
                            return View(user);
                        }
                       
                        else
                        {
                            string recommend = "<script> alert('注册成功 ！请完善信息')</script>";
                            Response.Write(recommend);
                            return View("Complete",user);
                        }


                    }
            }
            }
        
  }
        #endregion


        #region 万山
        [HttpGet]
        public ActionResult Complete()
        {
            return View("Register"); //去注册
        }

        [HttpPost]
        public ActionResult Complete(User user)
        {
            userService.Add(user);

            string recommend = string.Format("<script>alert('用户{0} 信息已更新, 请登录')</script>",user.UName);
            Response.Write(recommend);
            return View("Login",user);
        }


        #endregion






        [Authorize]
        public ActionResult Admin(string name)
        {
            if (userService.GetEntities(Y => Y.UName == name).Count() == 0)
            { //找不到
                AdminService adminService = new AdminService();

                if (adminService.GetEntities(admin => admin.AName == name).Count() == 0)//用户表也找到？
                {

                    Response.Write("<script>alert('非法用户');window.top.location.replace('../Home/Index');window.parent.location.href = '../Home/Index';</script>");return View();
                }
                else
                {
                    return RedirectToAction("Index","Admin");
                }

            }
            else //return
            {
                return View(userService.GetEntities(y => y.UName == name).First());
            }
            //return View();
        }

        //        public ActionResult Error404()
        //        {
        //            return View();
        //        }
        //        //注销
        //        public ActionResult Log_off()
        //        {

        //            HttpContext.Request.Cookies.Remove(FormsAuthentication.FormsCookieName);
        //            FormsAuthentication.SignOut();
        //            return RedirectToAction("Index", "Home");

        //        }
        [Authorize]
        public ActionResult Me(string name)
        {
            ////string t = string.Format("<script>console.log('{0}') </script>", unam);Response.Write(t);
            ///   




            return View(userService.GetEntities(z => z.UName == name).FirstOrDefault());
        }

        public ActionResult Welcome()
        {
            return View();
        }


        //具体修改

         #region  修改   
        [Authorize]
        public ActionResult Change(string UName) //参数
        {

            //首先获取对应    UID的姓名
            // string UName = userService.GetEntities(s => s.UId == UId).FirstOrDefault().UName;
            string UID = userService.GetEntities(us => us.UName == UName).First().UId.ToString();
            //
            if (UID == null || UID == "")
            {
                Response.Write("<script>alert('请重新登录');window.top.location.replace('../Home/Index');window.parent.location.href = '../Home/Index';</script>");
                return View();
            }
            else
            {
                HttpContext context = System.Web.HttpContext.Current;
                var cookie = context.Request.Cookies[FormsAuthentication.FormsCookieName];

                //Response.Write(dtoModel);

                //6.将上下文中的User数据实例化，通过MyFormsPrincipal的构造函数 ticket，userData
                var ticket = FormsAuthentication.Decrypt(cookie.Value);
                var dtoModel = ticket.UserData;
                User user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(dtoModel);



                //string shw = string.Format("<script>alert('{0}+{1}+{2}');console.log('{3}{4}')</script>", user.UName, context.User.Identity.Name,user.UName,user.UName,user.UId);
                //Response.Write(shw);
                //context.User = new MyFormsPrincipal<User>(ticket, dtoModel)


                //想要改变的不是当前账户(传递过来的参数) 直接说没有权限
                if (user.UName != UName)
                {
                    Response.Write("<script>alert('你没有权限');location='../Home/Index'</script>");
                    return View();
                }

                if (UID.ToString() == "" || UID.ToString() == null)
                {
                    if (UName == null || UName == "")
                    {
                        string te = string.Format("<script>alert('参数错误')</script>");
                        return View("welcome");
                    }
                    else
                    {
                        User fuser = userService.GetEntities(x => x.UName == UName).FirstOrDefault();

                        return View(fuser);

                    }
                }

                else

                {
                    User fuser = userService.GetEntities(u => u.UId.ToString()==UID).FirstOrDefault();
                    return View(fuser);
                }


            }
        }
        
        [Authorize]
        [HttpPost]public  ActionResult Change(User user)
        {

            HttpContext context = System.Web.HttpContext.Current;

            //如果修改名字
            if (user.UName!= context.User.Identity.Name)
            {
                if (userService.GetEntities(s => s.UName == user.UName).Count() != 0)
                {//先查找
                    string recommend = string.Format("<script>alert('用户名{0}已存在，换个试试吧')</script>", user.UName);
                    Response.Write(recommend); return View(user);
                }



                else
                {

                    HttpContext.Request.Cookies.Remove(FormsAuthentication.FormsCookieName);
                    FormsAuthentication.SignOut();

                    userService.Update(user);



                    //重新把数据 添加到cookie
                        #region 加密


                        JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();


                        string data = javaScriptSerializer.Serialize(user);
                        //对 用户信息加密   Json序列



                        FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2,
                             user.UName,
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


                    //int delay = 150;
                    //Thread.Sleep(delay);


                    #endregion  //返回主页
                    Response.Write("<script>alert('修改成功,重新载入用户信息');window.top.location.replace('../Home/Index');window.parent.location.href = '../Home/Index';</script>");

                    return View(user);

                }  //修改姓名  首先查找自己

                //if (userService.GetEntities(s => s.UName == user.UName||s.UId!=user.UId).Count() != 0)   //查找这个名字但是UId不是自己的
                //{    
            }     //    string recommend = string.Format("<script>alert('用户名{0}已存在')</script>", user.UName);
                  //    Response.Write(recommend); return View(user);
                  //}

            else
            {  /*&    */
                // /string S = string.Format("<script > alert('修改成功')</script>");



                HttpContext.Request.Cookies.Remove(FormsAuthentication.FormsCookieName); //注销
                FormsAuthentication.SignOut();

                //重新添加数据到cookies
                #region 加密


                JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();


                string data = javaScriptSerializer.Serialize(user);
                //对 用户信息加密   Json序列



                FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(2,
                     user.UName,
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

               #endregion
                userService.Update(user);

                Response.Write("<script>alert('修改成功')</script>");


               
                return View(user);
            }
        }
        //获取


        #endregion
       
        
        
        [Authorize]



        public Ip Get_Ip_Address() //返回参数是一个Ip类
        {

            #region
            /*
            String Html;
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create("https://ncov.dxy.cn/ncovh5/view/pneumonia");
            Request.Timeout = 20 * 1000;//请求超时。
            Request.AllowAutoRedirect = true; //网页自动跳转。
            Request.UserAgent = "Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)";//伪装成谷歌爬虫。
            Request.Method = "GET"; //获取数据的方法。GET
            //Request.Method = "POST";//POST
            //Request.ContentType = "application/json";上传的格式JSON
            Request.KeepAlive = true; //保持
            HttpWebResponse Response = (HttpWebResponse)Request.GetResponse();
            using (StreamReader sReader = new StreamReader(Response.GetResponseStream(), Encoding.UTF8))
            {
                 Html= sReader.ReadToEnd();
            }
            ViewBag.Html = Html;
            ViewBag.Response = Response;
            */
            #endregion

            #region
            /*
            HtmlWeb htmlweb = new HtmlWeb();

            //设置编码
            htmlweb.OverrideEncoding = Encoding.UTF8;

            //设置文档
            HtmlDocument htmlDoc = htmlweb.Load(@"https://ncov.dxy.cn/ncovh5/view/pneumonia");

            HtmlNodeCollection Tree_Nodes = htmlDoc.DocumentNode.SelectNodes("//div[@class ='areaBox___3jZkr themeA___2th0K']");
            if(Tree_Nodes!=null)
            {
                ViewBag.Html = "没找到";
                
            }
            else
            {
                ViewBag.Html = "找到了";   
            }
*/


            #endregion


            #region
            /*
            String url = "https://wp.m.163.com/163/page/news/virus_report/index.html?_nw_=1&_anw_=1";
            //下载网页源代码 

            //第一种方法
            WebClient web = new WebClient();//创建webclient对象
            web.Encoding = System.Text.Encoding.UTF8;//定义对象语言
            string html = web.DownloadString(url);//向一个连接请求资源
            /*第二种方法
             WebClient wc = new WebClient();
            Byte[] pageData = wc.DownloadData("http://m.weather.com.cn/data/101110101.html");
                string rr = Encoding.GetEncoding("utf-8").GetString(pageData);



            //加载源代码，获取文档对象
            //var doc = new HtmlDocument(); doc.LoadHtml(docText);
            //更加xpath获取总的对象，如果不为空，就继续选择dl标签
            //var res = doc.DocumentNode.SelectSingleNode(@"/html[1]/body[1]/div[1]/div[6]/div[1]/div[1]/div[3]");
            ViewBag.Html = html;
*/


            #endregion

            #region

            //获取ip所在的地址

            HttpRequest Rps = System.Web.HttpContext.Current.Request;
            //在新会话启动时候
            Session["REMOTE_ADDR"] = Rps.ServerVariables["REMOTE_ADDR"].ToString();  //获得请求机器的ip
            if (Session["REMOTE_ADDR"].ToString().Equals("::1"))
            {
                Session["REMOTE_ADDR"] = "119.27.27.174";
            }

            string Address = Session["REMOTE_ADDR"].ToString();
            //string url = String.Format("http://ip.taobao.com/service/getIpInfo.php?ip={0}", Address);

            //string url = String.Format("http://ip.ws.126.net/ipquery?ip=223.96.50.44", Address);
            string url = String.Format("http://whois.pconline.com.cn/ipJson.jsp?ip={0}&json=true", Address);

            string Html;
            HttpWebRequest Request = (HttpWebRequest)WebRequest.Create(url);
            Request.Timeout = 20 * 1000;//请求超时。
            Request.AllowAutoRedirect = true; //网页自动跳转。
            Request.UserAgent = "Mozilla/5.0 (compatible; Googlebot/2.1; +http://www.google.com/bot.html)";//伪装成谷歌爬虫。
            Request.Method = "GET"; //获取数据的方法。GET
            //Request.Method = "POST";//POST
            //Request.ContentType = "application/json";上传的格式JSON
            Request.KeepAlive = true; //保持
            try
            {
                HttpWebResponse Response = (HttpWebResponse)Request.GetResponse();
                using (StreamReader sReader = new StreamReader(Response.GetResponseStream(), Encoding.Default))
                {
                    Html = sReader.ReadToEnd();
                }



                //ViewBag.Html = url;
                //ViewBag.Html = Html;


                //注意要先编译一下
                Ip ip = JsonConvert.DeserializeObject<Ip>(Html);
                ip.Time = DateTime.Now;
                //ViewBag.All = ip.city + ip.pro + "   " + ip.ip;
                if (ip.city.ToString() == "" && ip.pro.ToString() == "")//如果地区查找不到
                {
                    ip.pro = " ";
                    ip.city = "未知地区";
                }

                return ip;


            }


            catch
            {
                //如果获取ip地址错误或者用的太平洋网站错误
                Ip ip = new Ip();
                ip.pro = " ";
                ip.city = "地区未知";
                ip.ip = Address;
                return ip;
            }




            // Response.Write("这是会话开始的IP地址" + Session["REMOTE_ADDR"]+"<br/>");
            //Response.Write("这是会话的唯一标识符"+Session["SessionID"]);

            #endregion






            //http://ip.taobao.com/service/getIpInfo.php?ip=223.96.50.44
            //http://ip.taobao.com/service/getIpInfo.php?ip=11.1.1.1
        }

        

        ////检测是否是当前登录用户
        //public int Check()
        //{
        //    HttpContext context = System.Web.HttpContext.Current;
        //    var cookie = context.Request.Cookies[FormsAuthentication.FormsCookieName];

        //    //Response.Write(dtoModel);

        //    //6.将上下文中的User数据实例化，通过MyFormsPrincipal的构造函数 ticket，userData
        //    var ticket = FormsAuthentication.Decrypt(cookie.Value);
        //    var dtoModel = ticket.UserData;
        //    User user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(dtoModel);
                    
        //    if()


        //}

        public ActionResult history(string name, int? page)
        {
            HttpContext context = System.Web.HttpContext.Current;
            var cookie = context.Request.Cookies[FormsAuthentication.FormsCookieName];

            //Response.Write(dtoModel);

            //6.将上下文中的User数据实例化，通过MyFormsPrincipal的构造函数 ticket，userData
            var ticket = FormsAuthentication.Decrypt(cookie.Value);
            var dtoModel = ticket.UserData;
            User user = Newtonsoft.Json.JsonConvert.DeserializeObject<User>(dtoModel);

           // Response.Write(user.UName);

            if (user.UName!=name&&context.User.Identity.Name!=user.UName)
            {
                Response.Write("<script>alert('你没有权限');window.top.location.replace('../Home/Index');window.parent.location.href = '../Home/Index';</script>");

                return View(user);
            }


            else
            {
                UserLoginService userLoginService = new UserLoginService();


                IQueryable<UserLogin> userLogins = userLoginService.GetEntities(x => x.User.UName==context.User.Identity.Name);

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
        }


        public ActionResult Delete()
        {
            return View();
        }





    }



        //public class Ip
        //{

        //    //ip属性
        //    /// </summary>var lo="山东省", lc="菏泽市"; var localAddress={city:"菏泽市", province:"山东省"}


        //    ///</summary> city 所在城市
        //    public string city { get; set; }

        //    /// <summary>
        //    /// regionNames  地区所在名字
        //    /// </summary>
        //    public string regionNames { set; get; }

        //    /// <summary>
        //    /// ip地区所在省份
        //    /// </summary>
        //    public string pro { get; set; }

        //    /// <summary>
        //    /// ip地址
        //    /// </summary>
        //    public string ip { set; get; }


        //    public DateTime Time { set; get; }
        //    public string addr { set; get; }
        //    /// <summary>
        //    /// 类的默认构造函数
        //    /// </summary>
        //    public Ip()
        //    {

        //    }

        //}
    }
