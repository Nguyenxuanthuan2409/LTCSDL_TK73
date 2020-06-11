using Newtonsoft.Json;

using LTCSDL.BLL;
using LTCSDL.Common.Rsp;
using LTCSDL.Common.BLL;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LTCSDL.BLL
{
    using DAL;
    using DAL.Models;
    using LTCSDL.Common.Req;

    public class ShippersSvc : GenericSvc<ShippersRep, Shippers>
    {
        public SingleRsp CreateShippers(ShippersReq req)
        {
            var res = new SingleRsp();
            try
            {
                Shippers c = new Shippers();
                //c.ShipperId = req.ShipperId;      // Vi ID tu tang nen khong go
                c.CompanyName = req.CompanyName;
                c.Phone = req.Phone;

                //
                res = base.Create(c);
                res.Data = c;
            }
            catch (Exception ex)
            {
                res.SetError(ex.StackTrace);
            }

            return res;
        }
    }
}
