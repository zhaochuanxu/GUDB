using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GUDB.Model;
namespace GUDB.IDAL
{
 public interface IDbSession
    {


        #region 拥有所有实例属性
        IUserDal UserDal { get; }

        ITypeDal TypeDal { get; }

        IEventDal EventDal { get; }
        
        IDamageDal DamageDal { get; }

        IDamageBuildingDal DamageBuildingDal { get; }

        IDamagePeopleDal DamagePeopleDal { get; }

        IDamageResourceDal DamageResourceDal { get; }
        IAdminDal AdminDal { get; }
        IDamageOtherDal DamageOtherDal { get; }

        IInvestigatorDal InvestigatorDal { get; }

        IUnitDal UnitDal { get;  }
        


        IUserLoginDal UserLoginDal { get; }


        #endregion

        #region 批量保存 SaveChange
        int SaveChanges();
        #endregion
    }
}
