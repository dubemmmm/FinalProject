using FinalProject.Data.Models.RequestModels;
using FinalProject.Data.ReturnedResponse;
using System.Threading.Tasks;

namespace FinalProject.Service.Interface
{
    public interface IUserService
    {
        Task<ApiResponse> Login(LoginRequestModel model);
        Task<ApiResponse> CreateUser(CreateUserRequestModel model);
    }
}
