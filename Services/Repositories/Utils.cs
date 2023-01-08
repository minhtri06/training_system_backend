using System.Security.Cryptography;
using System.Text;
using backend.Dto.AdminUser;
using backend.Dto.Class;
using backend.Dto.Course;
using backend.Dto.CourseCertificate;
using backend.Dto.LearningPath;
using backend.Dto.Department;
using backend.Dto.DepartmentLearningPath;
using backend.Dto.Token;
using backend.Dto.Trainee;
using backend.Models;
using backend.Dto.LearningPathCertificate;
using backend.Dto.LearningPathCourse;
using backend.Dto.Trainer;
using backend.Dto.TraineeClass;

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

            public static TrainerDto ConvertTrainer(Trainer trainer)
            {
                return new TrainerDto()
                {
                    Id = trainer.Id,
                    FirstName = trainer.FirstName,
                    LastName = trainer.LastName,
                    ImgLink = trainer.ImgLink,
                    SystemRole = trainer.SystemRole,
                    Username = trainer.Username,
                    RefreshTokenId = trainer.RefreshTokenId,
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

            public static LearningPathDto ConvertLearningPath(
                LearningPath learningPath
            )
            {
                return new LearningPathDto()
                {
                    Id = learningPath.Id,
                    Name = learningPath.Name,
                    Description = learningPath.Description,
                    ImgLink = learningPath.ImgLink,
                    ForRoleId = learningPath.ForRoleId
                };
            }

            public static CourseCertificateDto ConvertCourseCertificate(
                CourseCertificate courseCertificate
            )
            {
                return new CourseCertificateDto()
                {
                    TraineeId = courseCertificate.TraineeId,
                    CourseId = courseCertificate.CourseId,
                    StartDate = courseCertificate.StartDate,
                    Duration = courseCertificate.Duration
                };
            }

            public static DepartmentDto ConvertDepartment(Department department)
            {
                return new DepartmentDto()
                {
                    Id = department.Id,
                    Name = department.Name
                };
            }

            public static DepartmentLearningPathDto ConvertDepartmentLearningPath(
                DepartmentLearningPath departmentLearningPath
            )
            {
                return new DepartmentLearningPathDto()
                {
                    DepartmentId = departmentLearningPath.DepartmentId,
                    LearningPathId = departmentLearningPath.LearningPathId
                };
            }

            public static LearningPathCertificateDto ConvertLearningPathCertificate(
                LearningPathCertificate learningPathCertificate
            )
            {
                return new LearningPathCertificateDto()
                {
                    TraineeId = learningPathCertificate.TraineeId,
                    LearningPathId = learningPathCertificate.LearningPathId,
                    StartDate = learningPathCertificate.StartDate,
                    Duration = learningPathCertificate.Duration
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

            public static LearningPathCourseDto ConvertLearningPathCourse(
                LearningPathCourse learningPathCourse
            )
            {
                return new LearningPathCourseDto()
                {
                    CourseId = learningPathCourse.CourseId,
                    LearningPathId = learningPathCourse.LearningPathId,
                    CourseOrder = learningPathCourse.CourseOrder
                };
            }

            public static TraineeClassDto ConvertTraineeClass(
                TraineeClass traineeClass
            )
            {
                return new TraineeClassDto()
                {
                    TraineeId = traineeClass.TraineeId,
                    ClassId = traineeClass.ClassId,
                    GPA = traineeClass.GPA,
                    Status = Utils.DtoConversion.ConvertTraineeClassGPA(
                        traineeClass.GPA
                    ),
                    CourseId = traineeClass.CourseId
                };
            }

            public static CertificatedTraineeDto ConvertLPathCertToCertTrainee(
                LearningPathCertificate lPathCert
            )
            {
                return new CertificatedTraineeDto()
                {
                    Id = lPathCert.Trainee.Id,
                    FirstName = lPathCert.Trainee.FirstName,
                    LastName = lPathCert.Trainee.LastName,
                    SystemRole = lPathCert.Trainee.SystemRole,
                    Level = lPathCert.Trainee.Level,
                    RoleId = lPathCert.Trainee.RoleId,
                    DepartmentId = lPathCert.Trainee.DepartmentId,
                    ImgLink = lPathCert.Trainee.ImgLink,
                    StartDate = lPathCert.StartDate,
                    Duration = lPathCert.Duration
                };
            }

            public static TraineeLearningState ConvertTraineeClassGPA(
                float? GPA
            )
            {
                if (GPA == null)
                {
                    return TraineeLearningState.InProgress;
                }

                if (GPA >= 7)
                {
                    return TraineeLearningState.Pass;
                }
                else
                {
                    return TraineeLearningState.Fail;
                }
            }
        }

        public class EntityMapping
        {
            public static void MapTraineeFromDto(
                ref Trainee trainee,
                UpdateTraineeDto updateTraineeDto,
                AppDbContext context
            )
            {
                Role? role = null;
                if (updateTraineeDto.RoleId != null)
                {
                    role = context.Roles.SingleOrDefault(
                        r => r.Id == updateTraineeDto.RoleId
                    );
                    if (role == null)
                    {
                        throw new Exception(
                            "Role id not found!!, cannot update trainee"
                        );
                    }
                }

                Department? department = null;
                if (updateTraineeDto.DepartmentId != null)
                {
                    department = context.Departments.SingleOrDefault(
                        d => d.Id == updateTraineeDto.DepartmentId
                    );
                    if (department == null)
                    {
                        throw new Exception(
                            "Department id not found!!, cannot update trainee"
                        );
                    }
                }

                trainee.FirstName = updateTraineeDto.FirstName;
                trainee.LastName = updateTraineeDto.LastName;
                trainee.Level = updateTraineeDto.Level;
                trainee.ImgLink = updateTraineeDto.ImgLink;
                trainee.Role = role;
                trainee.Department = department;
            }

            public static void MapLearningPathFromDto(
                ref LearningPath learningPath,
                UpdateLearningPathDto updateLearningPathDto
            )
            {
                learningPath.Name = updateLearningPathDto.Name;
                learningPath.Description = updateLearningPathDto.Description;
                learningPath.ImgLink = updateLearningPathDto.ImgLink;
                learningPath.ForRoleId = updateLearningPathDto.ForRoleId;
            }

            public static void MapLearningPathCertificateFromDto(
                ref LearningPathCertificate learningPathCertificate,
                UpdateLearningPathCertificateDto updateLearningPathCertificateDto
            )
            {
                learningPathCertificate.StartDate =
                    updateLearningPathCertificateDto.StartDate;
                learningPathCertificate.Duration =
                    updateLearningPathCertificateDto.Duration;
            }

            public static void MapLearningPathCourseFromDto(
                ref LearningPathCourse learningPathCourse,
                UpdateLearningPathCourseDto updateLearningPathCourseDto
            )
            {
                learningPathCourse.CourseOrder =
                    updateLearningPathCourseDto.CourseOrder;
            }

            public static void MapTrainerFromUpdateDto(
                ref Trainer trainer,
                UpdateTrainerDto updateTrainerDto
            )
            {
                trainer.FirstName = updateTrainerDto.FirstName;
                trainer.LastName = updateTrainerDto.LastName;
                trainer.ImgLink = updateTrainerDto.ImgLink;
            }

            public static void MapTraineeClassFromUpdateDto(
                ref TraineeClass traineeClass,
                UpdateTraineeClassDto updateTraineeClassDto
            )
            {
                traineeClass.GPA = updateTraineeClassDto.GPA;
                traineeClass.Status =
                    Utils.DtoConversion.ConvertTraineeClassGPA(
                        updateTraineeClassDto.GPA
                    );
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
