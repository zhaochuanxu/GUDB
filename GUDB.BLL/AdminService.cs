using GUDB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUDB.BLL
{
    public class AdminService:BaseService<Admin>
    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.dbSession.AdminDal;
        }
    }
}
