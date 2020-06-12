using GUDB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GUDB.IDAL;
using System.Threading.Tasks;

namespace GUDB.DAL
{

    //类实现接口(如果继承的不是抽象类就必须要实现所有方
    public class UserDal :BaseDal<User>,IUserDal
    {

        /*

        #region 上下文实体
        GUDBContext dbContext = new GUDBContext();

        #endregion
        #region  查询
        public User GetUserById(int id)
        {
           
            return dbContext.Users.Find(id);

        }

        public List<User> GetAllUser()
        {

            return dbContext.Users.ToList();
        }

        //单元测试
        
        /// <summary>
        /// 随意按照某个条件过滤 动态调用方法  
        /// 用这个方法做任何调查
        /// </summary>
        public IQueryable<User> GetUsers(Expression<Func<User,bool>> whereLamda)
        {
            return dbContext.Users.Where(whereLamda).AsQueryable();
        }


        #endregion



        #region 增加
        public User Add(User user)
        {

            dbContext.Users.Add(user);
            dbContext.SaveChanges();
            return user;
        }
        #endregion


        #region 改
        public bool Update(User user)
        {
            dbContext.Entry(user).State = EntityState.Modified;
             //dbContext.Entry(user).State = EntityState.Unchange;
             //   db.User.Attach(user);
             
            return dbContext.SaveChanges() > 0;
        }

        #endregion



        #region 删除
        public bool delete(User user)
        {
            dbContext.Entry(user).State=EntityState.Detached;
            return dbContext.SaveChanges()>0;
        }
        #endregion
      */


    }
}