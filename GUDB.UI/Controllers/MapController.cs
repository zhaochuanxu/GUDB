using GUDB.BLL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using GUDB.Model;
namespace GUDB.UI.Controllers
{
    public class MapController : Controller
    {
        // GET: Map
        public ActionResult Index()
        {
            return View();
        }



        #region 不在详细展示
        //    //获取地震跳转页面         
        //public ActionResult EarthQuake(string location,string date )
        //    {

        //        //没数据可不行  直接跳转页面 

        //        if (location == "" || date == "" || location == null || date == null)
        //        {
        //            return Redirect("~/Home/Index");

        //        }

        //        else
        //        {
        //            //处理传过来的时间
        //            DateTime time = Convert.ToDateTime(date);
        //            //string s = String.Format("<script>alert('{0}')</script>",time+ location);
        //            //Response.Write(s);


        //            //处理已知的数据
        //            ViewData["Location"] = location;
        //            ViewData["Time"] = time;



        //            //查找地震ID
        //            TypeService typeService = new TypeService();
        //            IQueryable<Model.Type> types = typeService.GetEntities(u=>u.TName=="EarthQuake");
        //            Model.Type type = types.FirstOrDefault();


        //            //查找此处经纬度
        //            EventService eventService = new EventService();

        //            //Event event1
        //            //int event1 = eventService.GetEntities(u => u.TId == type.TId && u.ETime == time && u.ELocation == location).Count();

        //            Event event1 = eventService.GetEntities(u => u.TId == type.TId && u.ETime == time && u.ELocation == location).FirstOrDefault();
        //            //int event1  = eventService.GetEntities(u => u.ELocation == location).Count();

        //            //填充未知的数据

        //            ViewData["Lat"] = event1.ELat;

        //            ViewData["Long"] = event1.ELong;
        //            ViewData["Level"] = event1.Elevel;

        //            ViewData["Id"] = event1.EId;

        //            ViewData["Detail"] = "在时刻(UTC + 8)"+event1.ETime + "经度"+event1.ELat+"(⁰)"+ "纬 度"+event1.ELong +"(⁰)的"+event1.ELocation+ "处发生  震级(M)为" + event1.Elevel +"的地震，造成的损失为"+event1.EDamageDes+"该地的地质构造特点为"+event1.EEarthDes;
        //            #region
        //            ////查询用联通手机号的用户的姓名，性别，手机号，电话类型
        //            //var userdto = users.Join(phones, t => t.Id, p => p.UserId, (t, p) => new UserDto
        //            //{
        //            //    Name = t.Name,
        //            //    Gender = t.Gender,
        //            //    PhoneNumber = p.PhoneNumber,
        //            //    PhoneType = p.Type.ToString()
        //            //}).Where(t => t.PhoneType == "联通");
        //            #endregion


        //            //ViewBag.data = event1.ELocation + "     "+event1+ "  "+type.TName;


        //            //http://localhost:50140/Map/EarthQuake?location=%E4%B9%8C%E5%85%8B%E5%85%B0&&date=2020-04-07T10:16:12


        //            return View();

        //        }
        //    }

        #endregion









        /// <summary>
        /// 展示详细信息 对于每一个type具有严格区分
        /// </summary>
        /// <param name="location"></param>
        /// <param name="date"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public ActionResult Detail(string location, string date, string type)
        {



            if (location == "" || date == "" || location == null || date == null || type == null || type == "")
            {
                return Redirect("~/Home/Index");

            }

            else
            {
                //处理传过来的时间
                DateTime time = Convert.ToDateTime(date);
                //string s = String.Format("<script>alert('{0}')</script>",time+ location);
                //Response.Write(s);


                //处理已知的数据
                ViewData["Location"] = location;
                ViewData["Time"] = time;


                ////查找传过来的ID
                ////查找地震ID
                //TypeService typeService = new TypeService();
                //IQueryable<Model.Type> types = typeService.GetEntities(u => u.TName == "EarthQuake");
                //Model.Type type = types.FirstOrDefault();



                //完善信息

                //查找此处经纬度
                EventService eventService = new EventService();

                Event event1 = eventService.GetEntities(u => u.ETime == time && u.ELocation == location && u.TId.ToString() == type).FirstOrDefault();


                //填充未知的数据

                ViewData["Lat"] = event1.ELat;  

                ViewData["Long"] = event1.ELong;
                ViewData["Level"] = event1.Elevel; //假装处理 实际上获得的是
                ViewData["Range"] = event1.ERange;
                ViewData["Id"] = event1.EId;


                //获取事件类型的 等级述

                ViewData["DamageLevelDec"] = event1.Type.TDamageLevelDec;ViewBag.DamageNameEvents = event1.Type.TName;

                ViewData["Detail"] = "在时刻(UTC + 8)" + event1.ETime + "经度" + event1.ELong + "(⁰)" + "纬 度" + event1.ELat + "(⁰)的" + event1.ELocation + "处发生  震级(M)为" + event1.Elevel + "的地震，造成的损失为" + event1.EDamageDes + "该地的地质构造特点为" + event1.EEarthDes;
                #region
                ////查询用联通手机号的用户的姓名，性别，手机号，电话类型
                //var userdto = users.Join(phones, t => t.Id, p => p.UserId, (t, p) => new UserDto
                //{
                //    Name = t.Name,
                //    Gender = t.Gender,
                //    PhoneNumber = p.PhoneNumber,
                //    PhoneType = p.Type.ToString()
                //}).Where(t => t.PhoneType == "联通");
                #endregion


                //ViewBag.data = event1.ELocation + "     "+event1+ "  "+type.TName;


                //http://localhost:50140/Map/EarthQuake?location=%E4%B9%8C%E5%85%8B%E5%85%B0&&date=2020-04-07T10:16:12


                return View();



            } //return View();
        }
    






        ////获取泥石流跳转页面
        //public ActionResult MudSlides(string location, string date)
        //{


        //    //没数据可不行  直接跳转页面 

        //    if (location == "" || date == "" || location == null || date == null)
        //    {
        //        return Redirect("~/Home/Index");

        //    }

        //    else
        //    {
        //        //处理传过来的时间
        //        DateTime time = Convert.ToDateTime(date);
        //        //string s = String.Format("<script>alert('{0}')</script>",time+ location);
        //        //Response.Write(s);


        //        //处理已知的数据
        //        ViewData["Location"] = location;
        //        ViewData["Time"] = time;



        //        //查找地震ID
        //        TypeService typeService = new TypeService();
        //        IQueryable<Model.Type> types = typeService.GetEntities(u => u.TName == "EarthQuake");
        //        Model.Type type = types.FirstOrDefault();


        //        //查找此处经纬度
        //        EventService eventService = new EventService();

        //        //Event event1
        //        //int event1 = eventService.GetEntities(u => u.TId == type.TId && u.ETime == time && u.ELocation == location).Count();

        //        Event event1 = eventService.GetEntities(u => u.TId == type.TId && u.ETime == time && u.ELocation == location).FirstOrDefault();
        //        //int event1  = eventService.GetEntities(u => u.ELocation == location).Count();

        //        //填充未知的数据

        //        ViewData["Lat"] = event1.ELat;

        //        ViewData["Long"] = event1.ELong;
        //        ViewData["Level"] = event1.Elevel;

        //        ViewData["Id"] = event1.EId;

        //        ViewData["Detail"] = "在时刻(UTC + 8)" + event1.ETime + "经度" + event1.ELat + "(⁰)" + "纬 度" + event1.ELong + "(⁰)的" + event1.ELocation + "处发生  震级(M)为" + event1.Elevel + "的地震，造成的损失为" + event1.EDamageDes + "该地的地质构造特点为" + event1.EEarthDes;
        //        #region
        //        ////查询用联通手机号的用户的姓名，性别，手机号，电话类型
        //        //var userdto = users.Join(phones, t => t.Id, p => p.UserId, (t, p) => new UserDto
        //        //{
        //        //    Name = t.Name,
        //        //    Gender = t.Gender,
        //        //    PhoneNumber = p.PhoneNumber,
        //        //    PhoneType = p.Type.ToString()
        //        //}).Where(t => t.PhoneType == "联通");
        //        #endregion


        //        //ViewBag.data = event1.ELocation + "     "+event1+ "  "+type.TName;


        //        //http://localhost:50140/Map/EarthQuake?location=%E4%B9%8C%E5%85%8B%E5%85%B0&&date=2020-04-07T10:16:12


        //        return View();



        //    }


        //}


    }
}