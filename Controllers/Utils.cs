using backend.Dto.AdminUser;
using backend.Dto.ApiResponse;
using backend.Dto.Token;
using backend.Services.Interfaces;

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
                    Message = "something went wrong"
                };

            public static ApiResponseDto WRONG_USERNAME_OR_PASSWORD { get; } =
                new ApiResponseDto()
                {
                    Success = false,
                    Message = "wrong username or password"
                };

            public static ApiResponseDto FORBIDDEN { get; } =
                new ApiResponseDto() { Success = false, Message = "forbidden" };

            public static ApiResponseDto NOT_FOUND { get; } =
                new ApiResponseDto() { Success = false, Message = "not found" };

            public static readonly ApiResponseDto USERNAME_ALREADY_EXISTS =
                new ApiResponseDto()
                {
                    Success = false,
                    Message = "username already exists"
                };

            public static readonly ApiResponseDto WRONG_REFRESH_TOKEN =
                new ApiResponseDto()
                {
                    Success = false,
                    Message = "wrong refresh token"
                };

            public static ApiResponseDto LoginSuccessfully(
                Object userInfo,
                string accessToken,
                string refreshToken
            )
            {
                return new ApiResponseDto()
                {
                    Success = true,
                    Message = "login successfully",
                    Data = new List<TokenResponseDto>()
                    {
                        new TokenResponseDto()
                        {
                            userInfo = userInfo,
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
                    Message = "refresh token successfully",
                    Data = new List<Object>()
                    {
                        new { accessToken = accessToken }
                    }
                };
            }

            public static ApiResponseDto CreateObjectSuccessfully(
                string objectName,
                Object objectDto
            )
            {
                return new ApiResponseDto()
                {
                    Success = true,
                    Message = "create " + objectName + " successfully",
                    Data = new List<Object>() { objectDto }
                };
            }

            public static ApiResponseDto ObjectNotFound(string objectName)
            {
                return new ApiResponseDto()
                {
                    Success = false,
                    Message = objectName + " not found"
                };
            }

            public static ApiResponseDto GetObjectSuccessfully(
                string objectName,
                Object objectDto
            )
            {
                return new ApiResponseDto()
                {
                    Success = true,
                    Message = "get " + objectName + " successfully",
                    Data = new List<Object>() { objectDto }
                };
            }

            public static ApiResponseDto DeleteObjectSuccessfully(
                string objectName,
                Object objectDto
            )
            {
                return new ApiResponseDto()
                {
                    Success = true,
                    Message = "delete " + objectName + " successfully",
                    Data = new List<Object>() { objectDto }
                };
            }

            public static ApiResponseDto UpdateObjectSuccessfully(
                string objectName,
                Object objectDto
            )
            {
                return new ApiResponseDto()
                {
                    Success = true,
                    Message = "update " + objectName + " successfully",
                    Data = new List<Object>() { objectDto }
                };
            }

            public static ApiResponseDto UpdateObjectFailed(string objectName)
            {
                return new ApiResponseDto()
                {
                    Success = true,
                    Message = "update " + objectName + " failed",
                };
            }

            public static ApiResponseDto GetAllObjectsSuccessfully(
                string objectName,
                Object objectDtos
            )
            {
                return new ApiResponseDto()
                {
                    Success = true,
                    Message = "get all " + objectName + " successfully",
                    Data = objectDtos
                };
            }

            public static ApiResponseDto ResponseException(string exception)
            {
                return new ApiResponseDto()
                {
                    Success = false,
                    Message = exception,
                };
            }

            public static ApiResponseDto ObjectAlreadyExist(string objectName)
            {
                return new ApiResponseDto()
                {
                    Success = false,
                    Message = objectName + " already exist",
                };
            }
        }
    }
}
