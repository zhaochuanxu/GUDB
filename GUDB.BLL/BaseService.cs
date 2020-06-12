using GUDB.DALFactory;
using GUDB.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 最基本的增删查改
/// </summary>
//模块内高内聚  模块间低耦合
namespace GUDB.BLL
{
    #region
    /// <summary>
    /// 必须是可实例化的非静态   
    /// 
    /// 父类逼迫子类给父类一个属性赋值 ——抽象函数
    /// 赋值的操作在方法调用前完成 ——构造函数
    /// </summary>  只有这样才能传递过来Dal  dbsession就是来获得所有dal  关键是现在当前不知道dal是谁 但是子类知道 所以就让子类给父类赋值 
    /// <typeparam name="T">用于继传过来T承</typeparam>
    #endregion
    public abstract class BaseService<T> where T : class, new()
    {

        //获取当前实例化DAL
        public IDbSession dbSession {
            get {
                return DbSessionFactory.GetCurrentDbSession(); 
                } 
        }
        /// <summary>
        /// 接受各种DAL
        /// </summary>
        //不管是写UserDal 还是Itypeda 都是间接继承了IBaseDal 
        public IBaseDal<T> CurrentDal { get; set; }
        public abstract void SetCurrentDal();//抽象方法： 子类必须实现抽象方法

        /// <summary>
        /// 基类的构造方法 实现抽象方法 进行赋值
        /// </summary>
        public BaseService()
        {
            SetCurrentDal();
        }




        /// <summary>
        /// 注意这时候CurrentDal是当前的Dal
        /// </summary>
        /// <param name="whereLambda"></param>
        /// <returns></returns>

        //CRUB    直接使用DAL里的方法

        #region 查询 [返回实例]
        public IQueryable<T> GetEntities(Expression<Func<T, bool>> whereLambda)
        {
            return CurrentDal.GetEntities(whereLambda);

        }


        public IQueryable<T> GetPageEntities<S>(int pageSize, int pageIndex, out int total
                                            , Expression<Func<T, bool>> whereLambda
                                            , Expression<Func<T, S>> orderByLambda
                                            , bool isAsc)

        {
            return CurrentDal.GetPageEntities(pageSize, pageIndex, out total
                                             , whereLambda, orderByLambda
                                            , isAsc);

        }

        #endregion




        #region  增加[返回实例]  泛型
        public T Add(T entity)
        {
            CurrentDal.Add(entity);
            dbSession.SaveChanges();
            return entity;
        }
        #endregion



        #region [更 新 ]
        public bool Update(T entity)
        {
            CurrentDal.Update(entity);
            return dbSession.SaveChanges() > 0;

        }
        #endregion


        #region [删除]
        public bool Delete(T entity)
        {
            CurrentDal.Delete(entity);
            return dbSession.SaveChanges() > 0;
        }
        #endregion

    }
}
