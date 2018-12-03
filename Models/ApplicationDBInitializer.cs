using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PizzaTorium_complete.Models
{
    public class ApplicationDBInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == "Admin"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var role = new IdentityRole { Name = "Admin" };

                manager.Create(role);
            }

            if (!context.Users.Any(u => u.Email == "mrAdmin@founder.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser { UserName = "mrAdmin@founder.com", Email = "mrAdmin@founder.com" };

                manager.Create(user, "1234Admin");//Change Password ASAP!!
                manager.AddToRole(user.Id, "Admin");
            }

            var tempUser = new Users { Name = "Mr. Admin", Username = "mrAdmin@founder.com", Phone = "0794324213", Address = "220 14th Avenue", FavPizza = "Anything" };
            context.TblUsers.Add(tempUser);

            var ingredients = new List<Ingredients>
            {
                new Ingredients {Name = "Cheese", Cost= 2.00m, Checked =false },
                new Ingredients {Name = "Capers", Cost= 3.00m,Checked =false  },
                new Ingredients {Name = "Banana", Cost= 2.00m ,Checked =false },
                new Ingredients {Name = "Avacado", Cost= 4.00m ,Checked =false },
                new Ingredients {Name = "Chicken", Cost= 5.00m ,Checked =false },
                new Ingredients {Name = "Anchovies", Cost= 5.00m ,Checked =false },
                new Ingredients {Name = "Sausage", Cost= 5.00m ,Checked =false },
                new Ingredients {Name = "Mince", Cost= 6.00m ,Checked =false }
            };

            foreach (var item in ingredients)
            {
                context.Ingredients.Add(item);
            }

            var areas = new List<Areas>
            {
                new Areas {Area = "Hatfield" },
                new Areas {Area = "Centurion" },
                new Areas {Area = "Pretoria" },
                new Areas {Area = "Soshanguve" }
            };

            foreach (var item in areas)
            {
                context.Areas.Add(item);
            }

            var deliveries = new List<Delivery>
            {
                new Delivery { Name ="Micheal Steyn", Area = "Pretoria"},
                new Delivery { Name ="Jonothan Pretorius",Area = "Centurion"},
                new Delivery { Name ="Tristan Potgieter",Area = "Soshanguve"},
                new Delivery { Name ="Richard Basson",Area = "Hatfield" }
            };

            foreach (var item in deliveries)
            {
                context.Delivery.Add(item);
            }

            context.SaveChanges();
        }
    }
}