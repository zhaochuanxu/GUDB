using GUDB.BLL;
using GUDB.Model;
using Microsoft.Ajax.Utilities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GUDB.UI.Controllers
{
    [Authorize]
    public class DownLoadController : Controller
    {
        // GET: DownLoad
        public ActionResult Index()
        {
            #region 初始化位置定位

            ViewBag.NowLocOne = "数据下载";

            ViewBag.link = "/DownLoad/Index";
            #endregion
            return View();
        }


        public JsonResult Data(string id)
        {
            var settings = new JsonSerializerSettings();
            
            string data = "";

            EventService eventService = new EventService();
           if (id == "0")
            {
                IQueryable<Event> events = eventService.GetEntities(u => true);
                data = JsonConvert.SerializeObject(events);
            }

           else
            {
                IQueryable<Event> events = eventService.GetEntities(e=>e.Type.TId.ToString()==id); data = JsonConvert.SerializeObject(events);
            }
            //eventService.GetPageEntities(10,);
          

            return Json(data,JsonRequestBehavior.AllowGet);
        }



        //下载图表
        public ActionResult ChaertDownLoad(string id)
        {
            if (id == "" || id == null) { Response.Write("<script>alert('请输入参数')</script>"); return View("index"); }

            //处理 参数
            switch (id)
            {

                case "1": ViewBag.ChartType = "地震"; ViewData["EventName"] = "地震"; break;//地震
                case "2": ViewBag.ChartType = "泥石流"; ViewData["EventName"] = "泥石流"; break;//地震
                case "3": ViewBag.ChartType ="岩溶侵蚀"; ViewData["EventName"] = "岩溶侵蚀"; break;//地震
                case "0": ViewBag.ChartType = ""; ViewData["EventName"] = ""; break;//地震

                case "4": ViewBag.ChartType = "土壤侵蚀"; ViewData["EventName"] = "土壤侵蚀"; break;//地震
                case "5": ViewBag.ChartType = "胀缩土"; ViewData["EventName"] = "地震"; break;//地震

                case "6": ViewBag.ChartType = "海岸侵蚀"; ViewData["EventName"] = "海岸侵蚀"; break;//地震

                default: ViewBag.ChartType = "地震"; ViewData["EventName"] = "地震"; break;

            }
            #region 初始化位置定位

            ViewBag.NowLocOne = "数据下载";

            ViewBag.link = "/DownLoad/Index";
            ViewBag.NowLocTwo= ">图表下载";
            #endregion
            ViewData["ChartTypeId"] = id;  //请求携带据
            return View();
        }




        public JsonResult DataChart()
        {

           string s=" ["+
  "{'name': '南宁市', 'value': [10,20,30,40,80,12]" +
    "},"+
  "{'name': '柳州市','value': [90,40,23,60,23,43]" +
"},"+
  "{'name': '梧州市','value': [90,40,23,60,23,43]"+
   " }'+  ]";

            return Json(s,JsonRequestBehavior.AllowGet);
        }

    }  
}