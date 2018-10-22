using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPIsTrainingProject.Data;

namespace WebAPIsTrainingProject.Controllers
{
    [Produces("application/json")]
    [Route("api/RegisterUser")]
    public class RegisterUserController : Controller
    {
        //private readonly APIsTrainingContext context;

        //public RegisterUserController(APIsTrainingContext APIcontext)
        //{
        //    context = APIcontext;
        //}

        //[HttpGet("{id}")]
        //public IActionResult NewUser([FromRoute] int id)
        //{
        //    var user = context.LoginTable.SingleOrDefault(m => m.UserID == id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }
        //    return Ok(user);
        //}

        //public JsonResult DoesUserNameExist(string Username)
        //{
        //    return Json(!context.LoginTable.Any(x => x.Username == Username));
        //}

        //[HttpPost]
        //public IActionResult RegisterUser(localLoginModel localModel)
        //{
        //    var selectedName = DoesUserNameExist(localModel.Username);
        //    if (selectedName.Value is true)
        //    {
        //        localLoginModel model = new localLoginModel()
        //        {
        //            FirstName = localModel.FirstName,
        //            LastName = localModel.LastName,
        //            Username = localModel.Username,
        //            Password = localModel.Password,
        //            ImagePath = localModel.ImagePath
        //        };
        //        context.LoginTable.Add(localModel);
        //        context.SaveChanges();
        //        ViewBag.created = "User created successfully.";
        //    }
        //    else
        //    {
        //        ViewBag.notCreated = "User not created.";
        //    }
        //    return CreatedAtAction("LoginUser", "localLoginModel", new { id = localModel.UserID }, localModel);
        //}
    }
}