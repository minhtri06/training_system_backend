using System.Security.Cryptography;
using System.Text;
using backend.Dto.AdminUser;
using backend.Dto.Class;
using backend.Dto.Course;
using backend.Dto.CourseCertificate;
using backend.Dto.Token;
using backend.Dto.Trainee;
using backend.Models;

namespace backend.Services.Repositories
{
    internal class Utils
    {
        public static readonly int SALT_LENGTH = 32;

        public class DtoConversion
        {
            public static TraineeDto ConvertTrainee(Trainee trainee)
            {
                return new TraineeDto()
                {
                    Id = trainee.Id,
                    FirstName = trainee.FirstName,
                    LastName = trainee.LastName,
                    Level = trainee.Level,
                    SystemRole = trainee.SystemRole,
                    ImgLink = trainee.ImgLink,
                    Username = trainee.Username,
                    RoleId = trainee.RoleId,
                    DepartmentId = trainee.DepartmentId,
                    RefreshTokenId = trainee.RefreshTokenId
                };
            }

            public static ClassDto ConvertClass(Class _class)
            {
                return new ClassDto()
                {
                    Id = _class.Id,
                    Name = _class.Name,
                    StartDate = _class.StartDate,
                    EndDate = _class.EndDate,
                    CourseId = _class.CourseId
                };
            }

            public static CourseDto ConvertCourse(Course course)
            {
                return new CourseDto()
                {
                    Id = course.Id, 
                    Name = course.Name, 
                    Online = course.Online,
                    Duration = course.Duration,
                    LearningObjective = course.LearningObjective,
                    ImgLink = course.ImgLink,
                    Description = course.Description,
                    TrainerId = course.TrainerId
                };
            }

            public static CourseCertificateDto ConvertCourseCertificate(CourseCertificate courseCertificate)
            {
                return new CourseCertificateDto()
                {
                    TraineeId = courseCertificate.TraineeId,
                    CourseId = courseCertificate.CourseId,
                    StartDate = courseCertificate.StartDate,
                    Duration = courseCertificate.Duration
                };
            }
            public static RefreshTokenDto ConvertRefreshToken(
                RefreshToken refreshToken
            )
            {
                return new RefreshTokenDto()
                {
                    Id = refreshToken.Id,
                    Token = refreshToken.Token,
                    ExpiryTime = refreshToken.ExpiryTime,
                    CreatedTime = refreshToken.CreatedTime,
                };
            }

            public static AdminUserDto ConvertAdminUser(AdminUser adminUser)
            {
                return new AdminUserDto()
                {
                    Id = adminUser.Id,
                    FirstName = adminUser.FirstName,
                    LastName = adminUser.LastName,
                    Username = adminUser.Username,
                    ImgLink = adminUser.ImgLink,
                    SystemRole = adminUser.SystemRole,
                    RefreshTokenId = adminUser.RefreshTokenId
                };
            }

            internal static ClassDto? ConvertClass(Course @class)
            {
                throw new NotImplementedException();
            }
        }

        public class Security
        {
            public static string GenerateSalt(int length)
            {
                var saltBytes = new byte[length];

                using (var rng = RandomNumberGenerator.Create())
                {
                    rng.GetBytes(saltBytes);
                    return Convert.ToBase64String(saltBytes);
                }
            }

            public static string HashPassword(string password, string salt)
            {
                var passwordBytes = Encoding.UTF8.GetBytes(password);
                var saltBytes = Encoding.UTF8.GetBytes(salt);

                using (var hmac = new HMACSHA512(saltBytes))
                {
                    return Encoding.UTF8.GetString(
                        hmac.ComputeHash(passwordBytes)
                    );
                }
            }
        }
    }
}
