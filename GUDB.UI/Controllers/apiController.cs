using GUDB.BLL;
using GUDB.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GUDB.UI.Controllers
{
    public class apiController : Controller
    {
        // GET: api
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult Data(string Name, string type)

        {
            EventService eventService = new EventService();
            string data;
            //
            if (Name == "" | Name == null)
            {
                //设置error 代码
                Errorcode error = new Errorcode();
                error.Code = "201";    //没有参数
                data = JsonConvert.SerializeObject(error);
            }
            else
            {
                UserService userService = new UserService();
                if (userService.GetEntities(user => user.UName == Name).Count() != 0) //用户存在
                {
                    if (type == null || type == "") {
                        Errorcode error = new Errorcode();
                        error.Code = "202";    //没有参数
                        data = JsonConvert.SerializeObject(error);
                    }
                    else
                    {
                        switch (type.ToUpper())
                        {

                            case "EARTHQUAKE":
                                {
                                    IQueryable<Event> events = eventService.GetEntities(u =>u.Type.TName== "EARTHQUAKE");
                                    List<SearchData> searchDatas = new List<SearchData>();
                                    foreach(var it in events)
                                    {
                                        searchDatas.Add(new SearchData { Time = it.ETime, Location = it.ELocation, Long = it.ELong ,Lat=it.ELat}) ;
                                    }
                                    data = JsonConvert.SerializeObject(searchDatas);
                                    break;
                                }
                            case "MUDSLIDE":
                                {
                                    IQueryable<Event> events = eventService.GetEntities(u => u.Type.TName == "MudSlide");
                                    List<SearchData> searchDatas = new List<SearchData>();
                                    foreach (var it in events)
                                    {
                                        searchDatas.Add(new SearchData { Time = it.ETime, Location = it.ELocation, Long = it.ELong, Lat = it.ELat });
                                    }
                                    data = JsonConvert.SerializeObject(searchDatas);
                                    break;
                                }
                            case "KARSTCOLLAPSE":
                                {
                                    IQueryable<Event> events = eventService.GetEntities(u => u.Type.TName == "KarstCollapse");
                                    List<SearchData> searchDatas = new List<SearchData>();
                                    foreach (var it in events)
                                    {
                                        searchDatas.Add(new SearchData { Time = it.ETime, Location = it.ELocation, Long = it.ELong, Lat = it.ELat });
                                    }
                                    data = JsonConvert.SerializeObject(searchDatas);
                                    break;
                                }
                            case "SOILEROSION":
                                {
                                    IQueryable<Event> events = eventService.GetEntities(u => u.Type.TName == "SoilErosion");
                                    List<SearchData> searchDatas = new List<SearchData>();
                                    foreach (var it in events)
                                    {
                                        searchDatas.Add(new SearchData { Time = it.ETime, Location = it.ELocation, Long = it.ELong, Lat = it.ELat });
                                    }
                                    data = JsonConvert.SerializeObject(searchDatas);
                                    break;
                                }
                            case "SHRINKAGESOIL":
                                {
                                    IQueryable<Event> events = eventService.GetEntities(u => u.Type.TName == "ShrinkageSoil");
                                    List<SearchData> searchDatas = new List<SearchData>();
                                    foreach (var it in events)
                                    {
                                        searchDatas.Add(new SearchData { Time = it.ETime, Location = it.ELocation, Long = it.ELong, Lat = it.ELat });
                                    }
                                    data = JsonConvert.SerializeObject(searchDatas);
                                    break;
                                }
                            case "COASTEROSION":
                                {
                                    IQueryable<Event> events = eventService.GetEntities(u => u.Type.TName == "CoastErosion");
                                    List<SearchData> searchDatas = new List<SearchData>();
                                    foreach (var it in events)
                                    {
                                        searchDatas.Add(new SearchData { Time = it.ETime, Location = it.ELocation, Long = it.ELong, Lat = it.ELat });
                                    }
                                    data = JsonConvert.SerializeObject(searchDatas);
                                    break;
                                }

                            default:
                                {
                                    Errorcode error = new Errorcode();
                                    error.Code = "202";    //没有参数
                                    data = JsonConvert.SerializeObject(error);
                                    break;
                                }


                        }
                    }
                }

                else
                {   //设置error 代码

                    Errorcode error = new Errorcode(); //user
                    error.Code = "203";    //没有参数 不存 在 
                    data = JsonConvert.SerializeObject(error);
               }
            }
            return Json(data, JsonRequestBehavior.AllowGet);
        }
    }




    //返回数据格式
    public class SearchData
  {
        /// <summary>
        /// 地质灾害发生事件
        /// </summary>
        public DateTime Time { get; set; }
        /// <summary>
        /// 地质灾害发生地点
        /// </summary>
        public string Location { get; set; }
        /// <summary>
        ///  纬度
        /// </summary>
        public double Lat { get; set; }

       /// <summary>
       /// 经度
       /// </summary>
        public double Long { get; set; }

        }

    //Error json
    public class Errorcode
    { 
        public string Code { get; set; }
        public Errorcode()
        {

        }
    
    }

}