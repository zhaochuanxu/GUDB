using GUDB.BLL;
using GUDB.Model;
using System.Linq;
using System.Web.Mvc;

using PagedList.Mvc;
using PagedList;
namespace GUDB.UI.Controllers
{

    /// <summary>
    /// 此处用 Id查询  是因为在EventID是唯一
    //
    /// </summary>
    public class DamageController : Controller
    {
        // GET: Damage
        public ActionResult Index()
        {
            return View();
        }

        //Get
        /// <summary>
        /// 损失概况
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult EarthQuakeDamage(string Id)
        {
            #region 初始化位置定位
            ViewBag.NowLocOne = "数据查询";
            ViewBag.NowLocTwo = ">地震数据";

            ViewBag.NowLocThree = ">损失概况";
            #endregion



            //没数据可不行  直接跳转页面 

            if (Id==null||Id==" ")
            {
                return Redirect("~/Home/Index");

            }

            else
            {
                DamageService damageService = new DamageService();
                Damage damage = damageService.GetEntities(u => u.EId == Id).FirstOrDefault();
                //int num = damageService.GetEntities(u => u.EId == Id).Count();
                //string s  = string.Format("<script>alert('{0}')</script>",num);
                //Response.Write(s);
                return View(damage);
            }
        }


            



        /// <summary>
        /// 建筑损失详细
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EarthQuakeDamageBuilding(string Id,int? page)
        {
            if (Id == "" || Id == null)
            {
                return Redirect("~/Search/EarthQuake");
            }

            else
            {

                DamagaeBuildingService damagaeBuildingService = new DamagaeBuildingService();

                //多 个值
                IQueryable<DamageBuilding> damageBuildings = damagaeBuildingService.GetEntities(u => u.EId == Id);


                //string s  = string.Format("<script>alert('{0}')</script>",num);
                //Response.Write(s);

                //第几页  
                //int pageNumber = 1;
                int pageNumber = page ?? 1; //默认为

                //每页显示多少条  
                //int pageSize = 5;
                int pageSize = 10;


                //根据ID升序排序  
                damageBuildings = damageBuildings.OrderBy(x => x.DBId);

                //通过ToPagedList扩展方法进行分页  
                IPagedList<DamageBuilding> DamageBuildingListPagedList = damageBuildings.ToPagedList(pageNumber, pageSize);

                //将分页处理后的列表传给View 
                return View(DamageBuildingListPagedList);

                //return View(damageBuildings);
            }

        }




        /// <summary>
        /// 人员伤亡名单
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public ActionResult EarthQuakeDamagePeople(string Id,int? page)
        {

            if (Id == "" || Id == null)
            {
                return Redirect("~/Search/EarthQuake");
            }
            else {


                DamagePeopleService damagePeopleService = new DamagePeopleService();
                //获取人
                IQueryable<DamagePeople> damagePeoples = damagePeopleService.GetEntities(u => u.EId == Id);

                //int pageNumber = 1;
                int pageNumber = page ?? 1; //默认为

                //每页显示多少条  
                //int pageSize = 5;
                int pageSize = 10;
                
                damagePeoples = damagePeoples.OrderBy(x => x.DPId);
                IPagedList<DamagePeople> DamageBuildingListPagedList =  damagePeoples.ToPagedList(pageNumber, pageSize);
                return View(DamageBuildingListPagedList);
                //return View(damagePeoples);
            }
        }





        /// <summary>
        /// 现场图片照片 资源
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult EarthQuakeResource(string Id,int? page)
        {

            if (Id == "" || Id == null)
            {
                return Redirect("~/Search/EarthQuake");
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

            //DamageResourceService damageResourceService= new DamageResourceService();

            //DamageResource damageResource = damageResourceService.GetEntities(u => u.EId == Id).FirstOrDefault();

            ////int num = damageService.GetEntities(u => u.EId == Id).Count();
            ////string s  = string.Format("<script>alert('{0}')</script>",num);
            ////Response.Write(s);
            //return View(damageResource);
            //return View();
        }







        public ActionResult EarthQuakeOther(string Id,int? page)
        {

            if (Id == "" || Id == null)
            {
                return Redirect("~/Search/EarthQuake");
            }
            else
            {


                DamageOtherService damageOtherService = new DamageOtherService();
                //获取人
                IQueryable<DamageOther> damageOthers =  damageOtherService.GetEntities(u => u.EId == Id);

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

    }
}