using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUDB.Model;


namespace GUDB.BLL
{
    public class TypeService:BaseService<GUDB.Model.Type>
    {
        #region 解释
        //初始程序员 直接
        //IUserDal userDal = new IUserDal();

        //稍微高级 静态工厂  返回值是一个接口
        //IUserDal userDal = StaticDalFactory.GetUserDal();

        //更高级程序员 IOC、DI依赖注入  spring.Net

        /* IUserDal userDal = new UserDal();//依赖接口编程*/



        //依赖静态工厂类
        //   IUserDal userDal = StaticDalFactory.GetUserDal();



        //依赖静态工厂类的Dbsession 直接建立联系
        //DbSession dbSession = new DbSession();

        ////调用用接口  去拿到dal  dal与dal 之间不相互影响
        //private IDbSession dbSession = DbSessionFactory.GetCurrentDbSession();

        ///*调用方法直接 dbSession.UserDal*/

        #endregion

        ///Summary
        ///对父类抽象函数进行重写
        ///Summary

        public override void SetCurrentDal()
        {
            CurrentDal = this.dbSession.TypeDal;
        }

    }
}
