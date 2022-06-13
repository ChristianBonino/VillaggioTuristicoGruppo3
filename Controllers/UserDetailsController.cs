using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC_TDPC13.Contexts;
using MVC_TDPC13.DB.Entities;
using MVC_TDPC13.Models;
using Microsoft.AspNetCore.Mvc;

namespace MVC_TDPC13.Controllers
{
    public class UserDetailsController : Controller
    {
        public IActionResult SignIn([FromServices]UserDBContext dBContext, string userID)
        {
            User user = dBContext.Users.Where(u => u.Id == userID).Select(us => new User()
            {
                Id = us.Id,
                Email = us.Email,
                UserName = us.UserName,
                UserRoles = (from r in dBContext.Roles
                             join ur in dBContext.UserRoles.Where(ur => ur.UserId == userID) on r.Id equals ur.RoleId
                             select new Role()
                             {
                                 IdentityRole = r,
                                 RoleClaims = dBContext.RoleClaims.Where(rc => rc.RoleId == r.Id).ToList()
                             }).ToList(),
                UserClaims = dBContext.UserClaims.Where(uc => uc.UserId == userID).ToList()
            }).FirstOrDefault();
            return View(new UsersAndRolesViewModel()
            {
                Users = new List<User>() { user }
            });
            return View(user);
        }
    }
}