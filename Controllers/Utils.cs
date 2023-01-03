using backend.Dto.ApiResponse;
using backend.Dto.Token;
using backend.Models;

namespace backend.Controllers
{
    internal class Utils
    {
        public class CommonResponse
        {
            public static ApiResponseDto SOMETHING_WENT_WRONG { get; } =
                new ApiResponseDto()
                {
                    Success = false,
                    Message = "Something went wrong"
                };

            public static ApiResponseDto WRONG_USERNAME_OR_PASSWORD { get; } =
                new ApiResponseDto()
                {
                    Success = false,
                    Message = "Wrong username or password"
                };

            public static ApiResponseDto FORBIDDEN { get; } =
                new ApiResponseDto() { Success = false, Message = "Forbidden" };

            public static ApiResponseDto NOT_FOUND { get; } =
                new ApiResponseDto() { Success = false, Message = "Not found" };

            public static readonly ApiResponseDto USERNAME_ALREADY_EXISTS =
                new ApiResponseDto()
                {
                    Success = false,
                    Message = "Username already exists"
                };

            public static ApiResponseDto LoginSuccessfully(
                string accessToken,
                string refreshToken
            )
            {
                return new ApiResponseDto()
                {
                    Success = true,
                    Message = "Login successfully",
                    Data = new List<TokenResponseDto>()
                    {
                        new TokenResponseDto()
                        {
                            AccessToken = accessToken,
                            RefreshToken = refreshToken
                        }
                    }
                };
            }

            public static ApiResponseDto RefreshTokenSuccessfully(
                string accessToken
            )
            {
                return new ApiResponseDto()
                {
                    Success = true,
                    Message = "Refresh token successfully",
                    Data = new List<Object>()
                    {
                        new { accessToken = accessToken }
                    }
                };
            }

            public static ApiResponseDto CreateSuccessfully(
                string objectName,
                Object objectDto
            )
            {
                return new ApiResponseDto()
                {
                    Success = true,
                    Message = "Create " + objectName + " successfully",
                    Data = new List<Object>() { objectDto }
                };
            }
        }
    }
}
