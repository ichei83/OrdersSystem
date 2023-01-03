using OrdersSystem.DBContext;
using OrdersSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OrdersSystem.Db
{
    public class DBHelper
    {
        public static void Initialize(OrderContext context)
        {
            context.Database.EnsureCreated();
       
        }
        public static void FillUsers(OrderContext context)
        {
            if(context.UsersRole.Count() == 0)
            {
                var role = new UserRole
                {
                    Name= "Admin"
                };
                context.UsersRole.Add(role);
                role = new UserRole
                {
                    Name = "User"
                };
                context.UsersRole.Add(role);

                context.SaveChanges();

            }
            if (context.Users.Count() == 0 && context.UsersRole.Count() > 0)
            {
                Random rnd = new Random();

                int number = rnd.Next(10, 15);
                var userRole = context.UsersRole.Where(x => x.Name == "User").FirstOrDefault();//user
                CreateAdminData(context);
                CreatUsersData(context, number, userRole);
            }


            context.Database.EnsureCreated();
        }

        private static void CreatUsersData(OrderContext context, int number, UserRole userRole)
        {
            for (int i = 1; i < number; i++)
            {
                var user = new User
                {
                    UserName = "user" + i,
                    Password = i.ToString(),
                    Role = userRole

                };
                var collection = DBHelper.FillOrders(context, user);
                foreach (var item in collection)
                {
                    user.Orders.Add(item);
                }
                context.Users.Add(user);
            }
            context.SaveChanges();
        }

        private static void CreateAdminData(OrderContext context)
        {
            var adminRole = context.UsersRole.Where(x => x.Name == "Admin").FirstOrDefault();//admin

            var user = new User
            {
                UserName = "AdminUser",
                Password = 0.ToString(),
                Role = adminRole

            };
            //var collection = DBHelper.FillOrders(context, user);
            //foreach (var item in collection)
            //{
            //    user.Orders.Add(item);
            //}
            context.Users.Add(user);
            context.SaveChanges();
        }

        private static List<Order> FillOrders(OrderContext context, User user)
        {
            Random rnd = new Random();
            int number = rnd.Next(5, 40);
            List<Order> orders = new List<Order>();
            for (int i = 0; i < number; i++)
            {

                var o = new Order
                {
                    Customer = "order " + i + " for " + user.UserName,
                    Agent = "Agent " + i,
                    CustomerSite = "Site " + i,Date = DateTime.Now,
                    Details = "Details " + i,
                    Status = "Status" + i,
                    User = user
                };
                orders.Add(o);
                context.Orders.Add(o);
            }

            return orders;
        }
    }
}
