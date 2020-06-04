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
        private readonly CategoriesSvc _svc;
    }
}