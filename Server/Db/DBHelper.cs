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
            /*
             
                         // Look for any students.
            if (context.Students.Any())
            {
                return;   // DB has been seeded
            }

            var students = new Student[]
            {
            new Student{FirstMidName="Carson",LastName="Alexander",EnrollmentDate=DateTime.Parse("2005-09-01")},
            new Student{FirstMidName="Meredith",LastName="Alonso",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Student{FirstMidName="Arturo",LastName="Anand",EnrollmentDate=DateTime.Parse("2003-09-01")},
            new Student{FirstMidName="Gytis",LastName="Barzdukas",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Student{FirstMidName="Yan",LastName="Li",EnrollmentDate=DateTime.Parse("2002-09-01")},
            new Student{FirstMidName="Peggy",LastName="Justice",EnrollmentDate=DateTime.Parse("2001-09-01")},
            new Student{FirstMidName="Laura",LastName="Norman",EnrollmentDate=DateTime.Parse("2003-09-01")},
            new Student{FirstMidName="Nino",LastName="Olivetto",EnrollmentDate=DateTime.Parse("2005-09-01")}
            };
            foreach (Student s in students)
            {
                context.Students.Add(s);
            }
            context.SaveChanges();

            var courses = new Course[]
            {
            new Course{CourseID=1050,Title="Chemistry",Credits=3},
            new Course{CourseID=4022,Title="Microeconomics",Credits=3},
            new Course{CourseID=4041,Title="Macroeconomics",Credits=3},
            new Course{CourseID=1045,Title="Calculus",Credits=4},
            new Course{CourseID=3141,Title="Trigonometry",Credits=4},
            new Course{CourseID=2021,Title="Composition",Credits=3},
            new Course{CourseID=2042,Title="Literature",Credits=4}
            };
            foreach (Course c in courses)
            {
                context.Courses.Add(c);
            }
            context.SaveChanges();

            var enrollments = new Enrollment[]
            {
            new Enrollment{StudentID=1,CourseID=1050,Grade=Grade.A},
            new Enrollment{StudentID=1,CourseID=4022,Grade=Grade.C},
            new Enrollment{StudentID=1,CourseID=4041,Grade=Grade.B},
            new Enrollment{StudentID=2,CourseID=1045,Grade=Grade.B},
            new Enrollment{StudentID=2,CourseID=3141,Grade=Grade.F},
            new Enrollment{StudentID=2,CourseID=2021,Grade=Grade.F},
            new Enrollment{StudentID=3,CourseID=1050},
            new Enrollment{StudentID=4,CourseID=1050},
            new Enrollment{StudentID=4,CourseID=4022,Grade=Grade.F},
            new Enrollment{StudentID=5,CourseID=4041,Grade=Grade.C},
            new Enrollment{StudentID=6,CourseID=1045},
            new Enrollment{StudentID=7,CourseID=3141,Grade=Grade.A},
            };
            foreach (Enrollment e in enrollments)
            {
                context.Enrollments.Add(e);
            }
            context.SaveChanges();
             
             */
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
