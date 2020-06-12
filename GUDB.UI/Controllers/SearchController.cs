using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using Newtonsoft.Json;

using GUDB.BLL;
using GUDB.UI.Models;
using GUDB.Model;
using System.Web.UI.WebControls.Expressions;

namespace GUDB.UI.Controllers
{
    public class SearchController : Controller
    {
        // GET: Search
        public ActionResult Index(string key)
        {
            #region 初始化位置定位

            ViewBag.NowLocOne = "数据查询";

            ViewBag.link = "Index";
            #endregion
           
            if (key == null || key == "") { return View(); }
            else
            {
                string type = key.Trim();/* = Request.Form["key"];*/
                switch (type)
                {

                    case "泥石流":
                        {

                            return View("mudslides");
                        }
                    case "岩溶塌陷":
                        {

                            return View("Karstcollapse");
                        }
                    case "地震":
                        {
                            return View("EarthQuake");
                        }
                    case "土壤侵蚀":
                        {
                            return View("SoilErosion");
                        }
                    case "胀缩土":
                        {
                            return View("SHRINKAGESOIL");
                        }
                    case "海岸侵蚀":
                        {
                            return View(" COASTEROSION");
                        }

                    default:
                        {
                            return View();
                        }


                }
            }




            
        }

        #region 处理具体

        /// <summary>
        /// 地震
        /// </summary>
        /// <returns></returns>
        public ActionResult EarthQuake()
        {
            #region 初始化位置定位
            ViewBag.NowLocOne = "数据查询";
            ViewBag.NowLocTwo = ">地震数据";
            ViewBag.linkTwo = "Index/EarthQuake";
            ViewBag.link = "Index"; //数据查询的首页
            #endregion





            return View();
        }



        /// <summary>
        /// 泥石流
        /// </summary>

        public ActionResult MudSlides()
        {
            #region 初始化位置定位

            ViewBag.NowLocOne = "数据查询";
            ViewBag.NowLocTwo = ">泥石流数据";

            ViewBag.link = "Index";
            #endregion
            return View();
        }



        /// <summary>
        /// 岩溶塌陷
        /// </summary>
        /// <returns></returns>
        public ActionResult KarstCollapse()
        {
            #region 初始化位置定位
            ViewBag.NowLocOne = "数据查询";
            ViewBag.NowLocTwo = ">岩溶塌陷数据";
            ViewBag.linkTwo = "Index/KarstCollapse";
            ViewBag.link = "Index"; //数据查询的首页
            #endregion
            return View();
        }

        /// <summary>
        /// 土壤侵蚀
        /// </summary>
        /// <returns></returns>
        public ActionResult SoilEroSion()
        {
            #region 初始化位置定位
            ViewBag.NowLocOne = "数据查询";
            ViewBag.NowLocTwo = ">土壤侵蚀数据";
            ViewBag.linkTwo = "Index/EarthQuake";
            ViewBag.link = "Index"; //数据查询的首页
            #endregion
            return View();
        }
       
        /// <summary>
        /// 胀缩土
        /// </summary>
        /// <returns></returns>
        public ActionResult ShrinkageSoil()
        {
            #region 初始化位置定位
            ViewBag.NowLocOne = "数据查询";
            ViewBag.NowLocTwo = ">胀缩土数据";
            ViewBag.linkTwo = "Index/ShrinkageSoil";
            ViewBag.link = "Index"; //数据查询的首页
            #endregion

            return View();
            

        }

        /// <summary>
        /// 海岸侵蚀
        /// </summary>
        /// <returns></returns>
        public ActionResult CoastErosion()
        {
            #region 初始化位置定位
            ViewBag.NowLocOne = "数据查询";
            ViewBag.NowLocTwo = ">海岸数据";
            ViewBag.linkTwo = "Index/CoastErosion";
            ViewBag.link = "Index"; //数据查询的首页
            #endregion
            return View();
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>

        public ActionResult Other()
        {
            return View();
        }


        public ActionResult W() { return View(); }


        #endregion



        /// <summary>
        /// 展示在CHARTS
        /// </summary>
        /// <returns></returns>
        public ActionResult Chart(string id)
        {
            if (id == "" || id == null) {Response.Write("<script>alert('请输入参数')</script>");return View("index"); }

            //处理 参数
            switch (id) {
                case "1": ViewBag.ChartType ="地震"; break;//地震
                default: ViewBag.ChartType = "地震"; break;

            }

            return View();
        }

        
        public JsonResult dataChart()
        {
            return Json("",JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 返回前段Json数据
        /// </summary>
        /// <returns></returns>
        public JsonResult GetData()
        {

            EventService EventService = new EventService();

           

            IQueryable<Event> events = EventService.GetEntities(U => true);
            #region 测试
            //List<Event> cities = new List<Event>()
            //{
            //    new Event{EId ="0",ETime=DateTime.Now,ELocation="长清" },
            //    new Event{EId="5",ETime=DateTime.Now,ELocation="福岛" } ,
            //    new Event{EId="1",ETime=DateTime.Now,ELocation="福岛"},
            //    new Event{EId="2",ETime=DateTime.Now,ELocation="福岛"},

            //};
            //string data = JsonConvert.SerializeObject(cities);

            #endregion

            string data = JsonConvert.SerializeObject(events);

            return Json(data, JsonRequestBehavior.AllowGet);
        }





        [HttpPost]
        public JsonResult Data()
        {

            //初始化事件类
            SearchEvent events = new SearchEvent();

            if (Request.Form["start"] != "") { events.start = Convert.ToDateTime(Request.Form["start"]); }
            else { events.start = DateTime.Now.AddYears(-50); }

            if (Request.Form["end"] != "") { events.end = Convert.ToDateTime(Request.Form["end"]); }
            else { events.end = DateTime.Now; }

            //经度
            if (Request.Form["minlong"] != "") { events.minlong = Convert.ToDouble(Request.Form["minlong"]); }
            else { events.minlong = -180.0; }

            if (Request.Form["maxlong"] != "") { events.maxlong = Convert.ToDouble(Request.Form["maxlong"]); }
            else { events.maxlong = 180.0; }

            
            //纬度
            if (Request.Form["minlat"] != "") { events.minlat= Convert.ToDouble(Request.Form["minlat"]);}
            else { events.minlat = -90.0; }

            if (Request.Form["maxlat"] != "") { events.maxlat= Convert.ToDouble(Request.Form["maxlat"]); }
            else { events.maxlat= 90.0; }

            if (Request.Form["minlevel"] != "") { events.minlevel = Convert.ToDouble(Request.Form["minlevel"]); }
            else { events.minlevel=1.0; }

            if (Request.Form["maxlevel"] != "") { events.maxlevel = Convert.ToDouble(Request.Form["maxlevel"]); }
            else { events.maxlevel = 14; }
            //events.start = Convert.ToDateTime(Request.Form["start"]);
            //events.end = Convert.ToDateTime(Request.Form["end"]);
            //经度为空

            if (Request.Form["minlat"] != "") { }

               //纬度为空
            if(Request.Form["end"] != "") { }
            
            //events.minlong = Request.Form["minlong"];
            //events.maxlong = Request.Form["maxlong"];

            //events.minlat = Request.Form["minlat"];
            //events.maxlat = Request.Form["maxlat"];

            //events.minlevel = Request.Form["minlevel"];
            //events.maxlevel = Request.Form["maxlevel"];


            var settings = new JsonSerializerSettings();
            string data="";

            events.tid = Request.Form["type"];

            //按照事件进入数据库查找
            EventService eventService = new EventService();

            IQueryable<Event> events1 = eventService.GetEntities(u => u.ETime >= events.start && u.ETime <= events.end
                                && u.Elevel>=events.minlevel && u.Elevel<=events.maxlevel
                                && u.ELong <=events.maxlong && u.ELong>=events.minlong
                                && u.ELat<=events.maxlat && u.ELat>=events.minlat
               &&u.TId.ToString()==events.tid
            );

            //eventService.GetPageEntities(10,);
            data = JsonConvert.SerializeObject(events1);
            

            return Json(data,JsonRequestBehavior.AllowGet);
        }

    }
}