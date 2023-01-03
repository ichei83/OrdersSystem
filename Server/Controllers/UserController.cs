using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using OrdersSystem.DBContext;
using OrdersSystem.DTOS;
using OrdersSystem.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Threading.Tasks;

namespace OrdersSystem.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        /// <summary>
        /// DBContext, use for DB actions
        /// </summary>
        private readonly OrderContext _context;
        public UserController(OrderContext ctx)
        {
            _context = ctx;
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = (from x in _context.Users
             where x.UserName == model.Username
             select x).FirstOrDefault();

            if (user != null)
            {
                var result = (from x in _context.Users
                              join y in _context.UsersRole on x.Role.Id equals y.Id
                            where x.UserName == user.UserName && x.Password == model.Password
                            select new
                            {
                                username = x.UserName,
                                role = y.Name,
                                id = x.Id,
                            });
                if (result.Count() > 0)
                {
                    return Ok(new
                    {
                        user= result.FirstOrDefault(),
                    });
                }

            }
            return Unauthorized();
        }


        // GET: UserController/Details/5
        [HttpGet]
        [Route("users")]
        public object GetAllUsers(string role)
        {
            var user = this.HttpContext.Request.Headers.
                Where(x => x.Key == "Authorization").FirstOrDefault().Value.ToString();


            var currentUserData = (from x in this._context.Users
                         where x.UserName == user
                         select new
                         {
                             username = x.UserName,
                             role = x.Role.Name,
                             firstName = x.UserName,
                             lastName = x.UserName,
                             id = x.Id
                         }).FirstOrDefault();
            if(currentUserData.role == "Admin")
            {
                var users = (from x in this._context.Users
                             where x.Role.Name == role
                             select new
                             {
                                 username = x.UserName,
                                 role = x.Role.Name,
                                 firstName = x.UserName,
                                 lastName = x.UserName,
                                 id = x.Id
                             });
                return users.ToList();
            }
            else
            {
                return Unauthorized();
            }
            //return Ok(new
            //{
            //    users = users.ToList(),
            //});
        }

    }
}
