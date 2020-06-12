using GUDB.IDAL;
using GUDB.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace GUDB.DAL
{

    /// <summary>
    /// 上下文工厂 上下文共用一个上下文
    /// </summary>
    public  static class DbContextFactory
    {
        public static DbContext GetCurrentDbContext()
        {
            //return new GUDBContext();

            ///保证对象线程内唯一
            DbContext dbContext = CallContext.GetData("DbContext") as DbContext;
            if (dbContext == null) 
            {
                dbContext = new GUDBContext();
                CallContext.SetData("DbContext", dbContext);
            }

            return dbContext;


        }

        
    }
}
