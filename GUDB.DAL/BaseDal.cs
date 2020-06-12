using GUDB.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GUDB.DAL
{
    #region
    /// <summary>
    /// 职责： 封装所有的 DAL的公共crub方法
    /// 类的职责一定要单一
    /// </summary>
    /// <typeparam name="T"></typeparam>


    #endregion

    public class BaseDal<T> where T : class, new()
    {

        //依赖抽象编程 好处：可以做到变化时候改变最小  后边是子 类 返回
        public DbContext dbContext
        {
            get { return DbContextFactory.GetCurrentDbContext(); }
        }


        //CRUB

        #region 查询 [返回实例]
        public IQueryable<T> GetEntities(Expression<Func<T, bool>> whereLambda)
        {
            return dbContext.Set<T>().Where(whereLambda).AsQueryable();
        }


        public IQueryable<T> GetPageEntities<S>(int pageSize, int pageIndex, out int total
                                            , Expression<Func<T, bool>> whereLambda
                                            , Expression<Func<T, S>> orderByLambda
                                            , bool isAsc)
        {

            total = dbContext.Set<T>().Where(whereLambda).Count();
            if (isAsc)
            {
                var temp = dbContext.Set<T>().Where(whereLambda)
                     .OrderBy<T, S>(orderByLambda)
                     .Skip(pageSize * (pageIndex - 1))
                     .Take(pageSize).AsQueryable();

                return temp;
            }

            else //降序
            {
                var temp = dbContext.Set<T>().Where(whereLambda)
                       .OrderByDescending<T, S>(orderByLambda)
                     .Skip(pageSize * (pageIndex - 1))
                     .Take(pageSize).AsQueryable();

                return temp;
            }
        }

        #endregion




        #region  增加[返回实例]  泛型

        public T Add(T entity)
        {
            dbContext.Set<T>().Add(entity);

            //dbContext.SaveChanges();
            return entity;
        }

        #endregion



        #region [更 新 ]
        public bool Update(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            //return dbContext.SaveChanges() > 0;

            return true;
        }


        #endregion


        #region
        public bool Delete(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Deleted;

            //return dbContext.SaveChanges() > 0;
            return true;
        }
        #endregion

    }

}