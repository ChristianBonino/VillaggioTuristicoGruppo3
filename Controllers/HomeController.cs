using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MVC_TDPC13.Common;
using MVC_TDPC13.DB;
using MVC_TDPC13.DB.Entities;
using MVC_TDPC13.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace MVC_TDPC13.Controllers
{ 
    public class HomeController : Controller
    {
        private SignInManager<User> signInManager;
        private UserManager<User> userManager;
        private UserDBContext dbContext;

        private readonly ILogger<HomeController> logger;
        public HomeController(SignInManager<User> signInManager,
            UserManager<User> userManager,
            UserDBContext dbContext,
            ILogger<HomeController> logger)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.dbContext = dbContext;
            this.logger = logger;

        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult AdminPage()
        {
            return View();
        }

        //[Authorize(Roles = "User")]
        public IActionResult UserPage()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Explor()
        {
            return View();
        }


        public IActionResult SignIn([FromServices] UserDBContext dBContext, string userName, string email)
        {
            UsersAndRolesViewModel model = new UsersAndRolesViewModel();
            model.Users = dBContext.Users.Where(u =>
            (string.IsNullOrEmpty(userName) ? u.UserName != null : u.UserName.Contains(userName))
            &&
            (string.IsNullOrEmpty(email) ? true : u.Email != null && u.Email.Contains(email))
            ).Select(u => new User()
            {
                Id = u.Id,
                Email = u.Email,
                UserName = u.UserName,
                UserRoles = (from r in dBContext.Roles
                             join ur in dBContext.UserRoles.Where(ur => ur.UserId == u.Id) on r.Id equals ur.RoleId
                             select new Role()
                             {
                                 IdentityRole = r,
                                 RoleClaims = dBContext.RoleClaims.Where(rc => rc.RoleId == r.Id).ToList()
                             }).ToList(),
                UserClaims = dBContext.UserClaims.Where(uc => uc.UserId == u.Id).ToList()
            }).ToList();
            return View(model);
        }

        #region users
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromServices] UserManager<User> userManager, UsersAndRolesViewModel usersViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User user = await userManager.FindByEmailAsync(usersViewModel.Email);
                    if (user == null)
                    {
                        user = new User
                        {
                            UserName = usersViewModel.UserName,
                            Email = usersViewModel.Email
                        };
                        IdentityResult result = await userManager.CreateAsync(user, usersViewModel.Password);
                        if (result.Succeeded)
                            return Json("OK");

                        string errors = string.Empty;
                        foreach (IdentityError error in result.Errors)
                            errors += error.Code + ": " + error.Description + "\n";
                        return Json(errors);
                    }
                    else
                        return Json("Email is already taken");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
            }
            return Json("Invalid request");
        }
        //#region users
        //[HttpPost]
        //public async Task<IActionResult> CreateUser([FromServices] UserManager<User> userManager, UsersAndRolesViewModel usersViewModel)
        //{
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            User user = await userManager.FindByEmailAsync(usersViewModel.Email);
        //            if (user == null)
        //            {
        //                user = new User
        //                {
        //                    UserName = usersViewModel.UserName,
        //                    Email = usersViewModel.Email
        //                };
        //                IdentityResult result = await userManager.CreateAsync(user, usersViewModel.Password);
        //                if (result.Succeeded)
        //                    return Json("OK");

        //                string errors = string.Empty;
        //                foreach (IdentityError error in result.Errors)
        //                    errors += error.Code + ": " + error.Description + "\n";
        //                return Json(errors);
        //            }
        //            else
        //                return Json("Email is already taken");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        logger.LogError(ex, ex.Message);
        //    }
        //    return Json("Invalid request");
        //}
        [HttpPost]
        public async Task<IActionResult> DeleteUser([FromServices] UserDBContext dBContext, [FromServices] UserManager<User> userManager, string userId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        /*
                        List<IdentityUserClaim<string>> userClaims = dBContext.UserClaims.Where(uc => uc.UserId == user.Id).ToList();
                        dBContext.UserClaims.RemoveRange(userClaims);
                        */

                        List<IdentityUserRole<string>> userRoles = dBContext.UserRoles.Where(ur => ur.UserId == user.Id).ToList();
                        dBContext.UserRoles.RemoveRange(userRoles);

                        await dBContext.SaveChangesAsync();

                        IdentityResult result = await userManager.DeleteAsync(user);

                        if (result.Succeeded)
                            return Json("OK");

                        string errors = string.Empty;
                        foreach (IdentityError error in result.Errors)
                            errors += error.Code + ": " + error.Description;
                        return Json(errors);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
            }
            return Json("Invalid request");
        }

        [HttpPost]
        public async Task<IActionResult> AddRoleToUser([FromServices] UserManager<User> userManager, string userID, string roleName)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User user = await userManager.FindByIdAsync(userID);
                    await userManager.AddToRoleAsync(user, roleName);
                    await userManager.UpdateSecurityStampAsync(user);
                    return Json(ConstantValues.OK);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
            }
            return Json("Invalid request");
        }
        [HttpPost]
        public async Task<IActionResult> RemoveRoleFromUser([FromServices] UserManager<User> userManager, string userID, string roleName)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    User user = await userManager.FindByIdAsync(userID);
                    await userManager.RemoveFromRoleAsync(user, roleName);
                    await userManager.UpdateSecurityStampAsync(user);
                    return Json(ConstantValues.OK);
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
            }
            return Json("Invalid request");
        }
        [HttpPost]
        public async Task<IActionResult> AddClaimToUser([FromServices] UserDBContext dBContext, [FromServices] UserManager<User> userManager, string userID, string claimType, string claimValue)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (!string.IsNullOrEmpty(claimType) && !string.IsNullOrEmpty(claimValue))
                    {
                        dBContext.UserClaims.Add(new IdentityUserClaim<string>()
                        {
                            UserId = userID,
                            ClaimType = claimType,
                            ClaimValue = claimValue
                        });
                        await dBContext.SaveChangesAsync();

                        User user = await userManager.FindByIdAsync(userID);
                        await userManager.UpdateSecurityStampAsync(user);

                        return Json("OK");
                    }
                    else
                        return Json("Values cant be null");
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
            }
            return Json("Invalid request");
        }
        /*
        [HttpPost]
        public async Task<IActionResult> RemoveClaimFromUser([FromServices] UserDBContext dBContext, [FromServices] UserManager<User> userManager, string userID, string claimType, string claimValue)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    List<IdentityUserClaim<string>> claims = dBContext.UserClaims.Where(uc => uc.UserId == userID && uc.ClaimType == claimType && uc.ClaimValue == claimValue).ToList();
                    if (claims.Count > 0)
                    {
                        foreach (IdentityUserClaim<string> claim in claims)
                            dBContext.UserClaims.Remove(claim);
                        await dBContext.SaveChangesAsync();

                        User user = await userManager.FindByIdAsync(userID);
                        await userManager.UpdateSecurityStampAsync(user);

                        return Json("OK");
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
            }
            return Json("Invalid request");
        }
        */
        #endregion
        [HttpPost]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            try
            {
                User user = await userManager.FindByNameAsync(loginModel.UserName);
                if (user != null)
                {
                    var result = await signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, false, lockoutOnFailure: true);
                    if (result.Succeeded)
                    {

                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Redirect("Index");
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            try
            {
                if (signInManager.IsSignedIn(User))
                {
                    await signInManager.SignOutAsync();
                }
            }
            catch (Exception ex)
            {
            }
            return Redirect("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}

