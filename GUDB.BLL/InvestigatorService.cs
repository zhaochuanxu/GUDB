using GUDB.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GUDB.BLL
{
   public class InvestigatorService:BaseService<Investigator>
    {

        public override void SetCurrentDal()
        {
            CurrentDal = this.dbSession.InvestigatorDal;
        }
    }
}
