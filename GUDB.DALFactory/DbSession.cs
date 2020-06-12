using GUDB.DAL;
using GUDB.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 是为逻辑层和业务层做一个桥梁
/// </summary>
namespace GUDB.DALFactory
{

    //负责创建数据操作类的实例，将业务层和数据层解耦 业务层调用DBSession创建数据操作类的实例

    //业务层调用数据操作层都是通过接口


    /// <summary>
    /// DBSession 工厂类（数据会话层），作用：创建数据操作类的实例
    /// 业务层通过DBSession调用相应的数据操作层的实例这样讲业务层和数据操作层解耦
    /// </summary>


    //拥有所有Dal实例



    ///summary
    ///职责 提供给业务逻辑层 进行实例化
    ///     整个数据库访问层和数据库的会话
    ///Character:拥有所有实例
    ///           依赖接口编程
    ///summary
    public class DbSession :IDbSession
    {

        #region 简单工厂或抽象工厂部分

        public IUserDal UserDal
        {
            get { return StaticDalFactory.GetUserDal(); }
        }

        public ITypeDal TypeDal
        {
            get { return StaticDalFactory.GetTypeDal(); }
        }


        public IEventDal EventDal
        {
            get { return StaticDalFactory.GetEventDal(); }
        }


        public IDamageDal DamageDal
        {
            get { return StaticDalFactory.GetDamageDal(); }
        }



        public IDamageBuildingDal DamageBuildingDal
        {
            get { return StaticDalFactory.GetDamageBuildingDal(); }

        }

        public IDamageOtherDal DamageOtherDal
        {
            get { return StaticDalFactory.GetDamageOtherDal(); }


        }



        public IDamagePeopleDal DamagePeopleDal
        {
            get { return StaticDalFactory.GetDamagePeopleDal(); } 
        }


        public IDamageResourceDal DamageResourceDal
        {
            get { return StaticDalFactory.GetDamageResourceDal(); }
        }

        public IAdminDal AdminDal
        {
            get { return StaticDalFactory.AdminDal(); }
        }
        public IUnitDal UnitDal {
            get { return StaticDalFactory.UnitDal(); }
        }


        public IUserLoginDal UserLoginDal { get { return StaticDalFactory.UserLoginDal(); } }



        public IInvestigatorDal InvestigatorDal
        {
            get { return StaticDalFactory.InvestigatorDal(); } 
        }

        #endregion



        ///Summary
        //拿到当前上下文的实体 然后把修改的实体进行整体提交 提交  不用每次提交 只需要整体提交上下文实体保存  
        ///职责：批量提交整个数据库访问层和数据库的会话 
        ///Summary

        public int SaveChanges()
        {
            return DbContextFactory.GetCurrentDbContext().SaveChanges();
        }

    }




}