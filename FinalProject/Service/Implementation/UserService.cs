using FinalProject.Data;
using FinalProject.Data.Entities;
using FinalProject.Data.Models.RequestModels;
using FinalProject.Data.ReturnedResponse;
using FinalProject.Service.Interface;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Linq;
using FinalProject.Data.Constants;
using AutoMapper;
using FinalProject.Data.Models.DTOs;

namespace FinalProject.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IMapper mapper;
        public UserService(ApplicationDbContext context, UserManager<User> userManager, IMapper mapper, SignInManager<User> signInManager)
        {
            this.context = context;
            _userManager = userManager;
            this.mapper = mapper;
            _signInManager = signInManager;
        }

        public async Task<ApiResponse> CreateUser(CreateUserRequestModel model)
        {
            var isExists = await _userManager.FindByEmailAsync(model.Email);
            if (isExists != null)
            {
                return ReturnedResponse.ErrorResponse("Email already exists", null, StatusCodes.RecordExists);
            }

            var user = new User();
            
            user.Email = model.Email;
            user.UserName = model.Email;
            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            
            var x =await _userManager.CreateAsync(user,model.Password);

            if(!x.Succeeded)
            {
                return ReturnedResponse.ErrorResponse("Couldn't create user", x.Errors.First().Description, StatusCodes.GeneralError);
            }

            var userDto = mapper.Map<UserDto>(user);
            return ReturnedResponse.SuccessResponse("User already created", userDto, StatusCodes.Successful);
        }


        public async Task<ApiResponse> Login(LoginRequestModel model)
        {
            if (string.IsNullOrEmpty(model.Password))
                return ReturnedResponse.ErrorResponse("User email can't be null or empty", null, StatusCodes.GeneralError);


            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user == null)
                return ReturnedResponse.ErrorResponse("User not found", null, StatusCodes.NoRecordFound);

            var isValid = await _signInManager.PasswordSignInAsync(user, model.Password, false, true);

            if(isValid.Succeeded)
            {
                //todo: implement jwt here
                return ReturnedResponse.SuccessResponse("User successfully logged in", null, StatusCodes.Successful);
            }

            return ReturnedResponse.ErrorResponse("User couldn't login", null, StatusCodes.GeneralError);

        }
    }
}
