using System;
using System.Collections.Generic;
using System.Text;
using LTCSDL.Common.DAL;
using System.Linq;

namespace LTCSDL.DAL
{
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
   
        public class ShippersRep:GenericRep<NorthwindContext, Shippers>
        {

        #region -- Overides --
        public override Shippers Read(int id)
        {
            var res = All.FirstOrDefault(p => p.ShipperId == id);
            return res;
        }
       
        public int Remove(int id)
        {
            var m = base.All.First(i => i.ShipperId == id);
            m = base.Delete(m); //TODO
            return m.ShipperId;
        }
        #endregion
    }

}
