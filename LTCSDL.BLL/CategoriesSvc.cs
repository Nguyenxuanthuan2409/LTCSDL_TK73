﻿using Newtonsoft.Json;

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

    public class CategoriesSvc : GenericSvc<CategoriesRep, Categories>
    {
        #region -- Overrides --
        
        /// <summary>
        /// Read single object
        /// </summary>
        /// <param name="id">Primary key</param>
        /// <returns>Return the object</returns>
        public override SingleRsp Read(int id)
        {
            var res = new SingleRsp();

            var m = _rep.Read(id);
            res.Data = m;

            return res;
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="m">The model</param>
        /// <returns>Return the result</returns>
        public override SingleRsp Update(Categories m)
        {
            var res = new SingleRsp();

            var m1 = m.CategoryId > 0 ? _rep.Read(m.CategoryId) : _rep.Read(m.Description);
            if (m1 == null)
            {
                res.SetError("EZ103", "No data.");
            }
            else
            {
                res = base.Update(m);
                res.Data = m;               
            }

            return res;
        }
        #endregion

        #region -- Methods --

        /// <summary>
        /// Initialize
        /// </summary>
        public CategoriesSvc() { }

        public object getCustOrderHist(string cusId)
        {
            return _rep.getCustOrderHist(cusId);

        }

        public object GetCustOrdersDetail(int ordId)
        {
            return _rep.GetCustOrdersDetail(ordId);
        }

        public object GetCustOrdersOrders(string ordId)
        {
            return _rep.GetCustOrdersOrders(ordId);
        }

        public object getCustOrderHist_LinQ(string ordId)
        {
            return _rep.getCustOrderHist_LinQ(ordId);
        }

        public object getCustOrdersDetail_LinQ(int ordId)
        {
            return _rep.getCustOrdersDetail_LinQ(ordId);
        }

        public object getEmployeeRevenue(DateTime date)
        {
            return _rep.getEmployeeRevenue(date);
        }

        public object getEmployeeRevenueStartEnd(DateTime begindate, DateTime enddate)
        {
            return _rep.getEmployeeRevenueStartEnd(begindate, enddate);
        }

        public object getEmployeeRevenueStartEnd_LinQ(DateTime begindate, DateTime enddate)
        {
            return _rep.getEmployeeRevenueStartEnd(begindate, enddate);
        }

        public object getEmployeeRevenue_LinQ(DateTime date)       // ADO.DOT NET
        {
            return _rep.getEmployeeRevenue_LinQ(date);
        }


        public object OrderFromToPagination(DateTime dateF, DateTime dateT, int size, int page)
        {
            return _rep.OrderFromToPagination(dateF, dateT, size, page);
        }

        public object ChiTietDonHangTheoID(int id)
        {
            return _rep.ChiTietDonHangTheoID(id);
        }
        #endregion
    }
}
