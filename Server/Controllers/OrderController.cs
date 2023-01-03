using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrdersSystem.DBContext;
using OrdersSystem.Models;
using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using OrdersSystem.DTOS;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using System.Runtime.CompilerServices;

namespace OrdersSystem.Controllers
{
    //[Route("[controller]")]
    //[ApiController]
    public class OrderController : Controller
    {
        /// <summary>
        /// DBContext, use for DB actions
        /// </summary>
        private readonly OrderContext _context;
        public OrderController(OrderContext ctx)
        {
            _context= ctx;
        }


        [HttpGet("GetOrdersByDate/{id}/{StartDate}/{EndDate}")]
        public object GetOrdersByDate(string id, string StartDate, string EndDate)
        {

            DateTime startDT = Convert.ToDateTime(StartDate);
            DateTime endDT = Convert.ToDateTime(EndDate);
            var orders = (from x in this._context.Orders
                          join y in _context.Users on x.User.Id equals y.Id
                          where x.User.Id == Convert.ToInt32(id) &&
                            x.Date <= endDT && x.Date >= startDT
                          select new
                          {
                              id = x.Id,
                              agent = x.Agent,
                              status = x.Status,
                              customer = x.Customer,
                              customersite = x.CustomerSite,
                              details = x.Details,
                              user = y.UserName,
                              date = x.Date.ToShortDateString()

                          });
            return orders.ToList();
        }

            // GET: OrderController/Details/5
            [HttpGet]
        [Route("orders")]
        public object GetOrders(string id)
        {
            var orders = (from x in this._context.Orders
                          join y in _context.Users on x.User.Id equals y.Id
                          where x.User.Id == Convert.ToInt32(id)
                          select new
                          {
                              id = x.Id,
                              agent = x.Agent,
                              status = x.Status,
                              customer = x.Customer,
                              customersite = x.CustomerSite,
                              details = x.Details,
                              user = y.UserName
                                
                          });
            return orders.ToList();
            //return Ok(new
            //{
            //    orders = orders,
            //});
        }

        // GET: OrderController/Details/5
        public ActionResult GetOrdersSum(int id, string from, string to)
        {
            DateTime fromDate = Convert.ToDateTime(from);
            DateTime toDate = Convert.ToDateTime(to);
            var orders = (from x in this._context.Orders
                          where x.User.Id == id &&
                          x.Date <= toDate && x.Date >= fromDate
                          select x);
            return Ok(new
            {
                orders = orders,
            });
        }

    }
}
