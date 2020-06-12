using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GUDB.IBLL
{
    public interface IBaseService<T> where T : class, new()
    {

        #region 查询 [返回实例]
        IQueryable<T> GetEntities(Expression<Func<T, bool>> whereLambda);

        IQueryable<T> GetPageEntities<S>(int pageSize, int pageIndex, out int total
                                            , Expression<Func<T, bool>> whereLambda
                                            , Expression<Func<T, S>> orderByLambda
                                            , bool isAsc);

        #endregion




        #region  增加[返回实例]  泛型

        T Add(T entity);

        #endregion



        #region [更 新 ]
        bool Update(T entity);


        #endregion


        #region
        bool Delete(T entity);
        #endregion
    }

}
