using Microsoft.AspNetCore.Mvc;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPIsTrainingProject.Models
{
    [ModelMetadataType(typeof(UserMetaData))]
    public class localLoginModel : LoginModel
    {
    }

    public class UserMetaData
    {
        [Remote("DoesUserNameExist", "RegisterUser", ErrorMessage = "Username already exists.")]
        public string Username { get; set; }
    }
}
