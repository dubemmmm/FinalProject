using FinalProject.Data.Constants;
using FinalProject.Data.Enum;
using FinalProject.Data.Models.RequestModels;
using FinalProject.Data.ReturnedResponse;
using FinalProject.Service.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace FinalProject.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthenticationController : Controller
    {
        public readonly IUserService userService;
        public static IWebHostEnvironment _environment;
        private readonly ILogger<AuthenticationController> logger;

        public AuthenticationController(IUserService userService, IWebHostEnvironment environment, ILogger<AuthenticationController> logger)
        {
            this.userService = userService;
            this.logger = logger;
            _environment = environment;
        }

        [HttpPost]
        [Route("api/v1/SignUp")]

        public async Task<IActionResult> SignUp(CreateUserRequestModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var errMessage = string.Join(" | ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                }

                var response = await userService.CreateUser(model);
                if (response.Status ==  Status.Successful.ToString())
                {
                    return Ok(response);
                }
                else
                {
                    return BadRequest(response);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ReturnedResponse.ErrorResponse($"An error ocurred: {ex.Message}", null, StatusCodes.GeneralError));
            }
        }
    }
}
