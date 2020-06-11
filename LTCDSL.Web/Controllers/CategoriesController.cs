using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LTCSDL.Web.Controllers
{
    using BLL;
    using DAL.Models;
    using Common.Req;
    using System.Collections.Generic;
    //using BLL.Req;
    using Common.Rsp;

    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        public CategoriesController()
        {
            _svc = new CategoriesSvc();
        }

        [HttpPost("get-by-id")]
        public IActionResult getCategoryById([FromBody]SimpleReq req)
        {
            var res = new SingleRsp();
            res = _svc.Read(req.Id);
            return Ok(res);
        }

        [HttpPost("get-all")]
        public IActionResult getAllCategories()
        {
            var res = new SingleRsp();
            res.Data = _svc.All;
            return Ok(res);
        }

        [HttpPost("get-by-getCustOrderHist")]
        public IActionResult getCustOrderHist([FromBody]SimpleReq req)
        {
            var res = new SingleRsp();
            res.Data = _svc.getCustOrderHist(req.Keyword);
            return Ok(res);

        }

        [HttpPost("get-by-GetCustOrdersDetail")]
        public IActionResult GetCustOrdersDetail([FromBody]SimpleReq req)
        {
            var res = new SingleRsp();
            res.Data = _svc.GetCustOrdersDetail(req.Id);
            return Ok(res);
        }

        [HttpPost("get-by-GetCustOrdersOrders")]
        public IActionResult GetCustOrdersOrders([FromBody]SimpleReq req)
        {
            var res = new SingleRsp();
            res.Data = _svc.GetCustOrdersOrders(req.Keyword);
            return Ok(res);
        }

        [HttpPost("get-by-getCustOrderHist_LinQ")]
        public IActionResult getCustOrderHist_LinQ([FromBody]SimpleReq req)
        {
            var res = new SingleRsp();
            res.Data = _svc.getCustOrderHist_LinQ(req.Keyword);
            return Ok(res);
        }

        [HttpPost("get-by-getCustOrdersDetail_LinQ")]
        public IActionResult getCustOrdersDetail_LinQ([FromBody]SimpleReq req)
        {
            var res = new SingleRsp();
            res.Data = _svc.getCustOrdersDetail_LinQ(req.Id);
            return Ok(res);
        }

        [HttpPost("get-by-getEmployeeRevenue")]
        public IActionResult getEmployeeRevenue([FromBody]RevenueReq req)
        {
            var res = new SingleRsp();
            res.Data = _svc.getEmployeeRevenue(req.dataF);
            return Ok(res);
        }

        [HttpPost("get-by-getemployeeRevenue_linq")]
        public IActionResult getEmployeeRevenue_LinQ([FromBody]RevenueReq req)
        {
            var res = new SingleRsp();
            res.Data = _svc.getEmployeeRevenue(req.dataF);
            return Ok(res);
        }

        [HttpPost("get-by-getEmployeeRevenueStartEnd")]
        public IActionResult getEmployeeRevenueStartEnd([FromBody]RevenueReq req)
        {
            var res = new SingleRsp();
            res.Data = _svc.getEmployeeRevenueStartEnd(req.dataF,req.dataT);
            return Ok(res);
        }

        [HttpPost("get-by-getEmployeeRevenueStartEnd_LinQ")]
        public IActionResult getEmployeeRevenueStartEnd_LinQ([FromBody]RevenueReq req)
        {
            var res = new SingleRsp();
            res.Data = _svc.getEmployeeRevenueStartEnd_LinQ(req.dataF, req.dataT);
            return Ok(res);
        }

        [HttpPost("get-by-OrderFromToPagination")]
        public IActionResult OrderFromToPagination([FromBody]RevenueReq req)
        {
            var res = new SingleRsp();
            res.Data = _svc.OrderFromToPagination(req.dataF, req.dataT, req.size, req.page);
            return Ok(res);
        }

        [HttpPost("get-by-ChiTietDonHangTheoID")]
        public IActionResult ChiTietDonHangTheoID([FromBody]SimpleReq req)
        {
            var res = new SingleRsp();
            res.Data = _svc.ChiTietDonHangTheoID(req.Id);
            return Ok(res);
        }
        private readonly CategoriesSvc _svc;
    }
}