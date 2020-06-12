using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using GUDB.Model;
using System.Threading.Tasks;

namespace GUDB.BLL
{
    public class DamageResourceService:BaseService<DamageResource>


    {
        public override void SetCurrentDal()
        {
            CurrentDal = this.dbSession.DamageResourceDal;
        }
    }
}
