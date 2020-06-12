using GUDB.IDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace GUDB.DALFactory
{
    //确保使用一个唯一的上下文

    public class DbSessionFactory
    {
        public static IDbSession GetCurrentDbSession()
        {


            ///保证对象线程内唯一

            IDbSession dbContext = CallContext.GetData("DbSession") as IDbSession;
            if (dbContext == null)
            {
                dbContext = new DbSession();
                CallContext.SetData("DbSession", dbContext);
            }

            return dbContext;
        }
    }
}
