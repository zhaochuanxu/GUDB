using GUDB.Model;
using GUDB.UI.Models;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using GUDB.BLL;
namespace GUDB.UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {


            #region 处理Ip
            Ip ip = new Ip();
            ip = Get_Ip_Address();

            string FileName = System.Web.HttpContext.Current.Server.MapPath("~/www/log.log");
            string log_Content = ip.Time + "  " + ip.ip + "   " + ip.pro + "  " + ip.city + " " + ip.addr + "\n" + "\n";
            // 访问日志
            System.IO.File.AppendAllText(FileName, log_Content);

            #endregion

            #region 处理指示显示位置
            ViewBag.NowLocOne = "";
            ViewBag.NowLocTwo = "";
            #endregion

            //处理传输数据

            return View();

        }


        /// <summary>
        /// 利用业务层控制逻辑层
        /// </summary>
        UserService UserService = new UserService();
        public ActionResult About()
        {
         
               ViewData.Model = UserService.GetEntities(u => true);
            



            return View();
        }

        //ip地址
        public Ip Get_Ip_Address() //返回参数是一个Ip类
        {
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
                using (StreamReader sReader = new StreamReader(Response.GetResponseStream(),Encoding.Default))
                {
                    Html = sReader.ReadToEnd();
                }



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
        }


        [HttpPost]
        public ActionResult Contact(User user)
        {

            using (GUDBContext dbContext = new GUDBContext())
            {

                dbContext.Users.Add(new User()
                {
                    UName = user.UName,
                    UId = user.UId,
                    UIdName=user.UIdName,
                    UIdNumber=user.UIdNumber,
                    UMail=user.UMail,
                    UOtherConnect=user.UOtherConnect


                });

                dbContext.SaveChanges();
            }

            return View(user);

        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult LoveYourself()
        {

            return View();
        }
   
    }
}