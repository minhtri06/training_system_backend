using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

using backend.Dto.Trainee;
using backend.Models;
using backend.Services.Interfaces;

namespace backend.Services.Repositories
{
    public class TraineeRepository : ITraineeRepository
    {
        private readonly AppDbContext _context;
        private readonly int _saltLength = 32;

        public TraineeRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool CheckUsernameExist(string username)
        {
            return _context.Trainees.Any(t => t.username == username);
        }

        private string GenerateSalt(int length)
        {
            var saltBytes = new byte[length];

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(saltBytes);
                return Convert.ToBase64String(saltBytes);
            }
        }

        private string HashPassword(string password, string salt)
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
            var saltBytes = Encoding.UTF8.GetBytes(salt);

            using (var hmac = new HMACSHA512(saltBytes))
            {
                return Encoding.UTF8.GetString(hmac.ComputeHash(passwordBytes));
            }
        }

        private TraineeDto ConvertTraineeToDto(Trainee trainee)
        {
            return new TraineeDto()
            {
                Id = trainee.Id,
                FirstName = trainee.FirstName,
                LastName = trainee.LastName,
                Level = trainee.Level,
                SystemRole = trainee.SystemRole,
                ImgLink = trainee.ImgLink,
                username = trainee.username,
                RoleId = trainee.Role != null ? trainee.Role.Id : null,
                DepartmentId =
                    trainee.Department != null ? trainee.Department.Id : null,
            };
        }

        public TraineeDto CreateTrainee(NewTraineeDto newTraineeDto)
        {
            var role = _context.Roles.SingleOrDefault(
                r => r.Id == newTraineeDto.RoleId
            );
            var department = _context.Departments.SingleOrDefault(
                d => d.Id == newTraineeDto.DepartmentId
            );

            var salt = GenerateSalt(_saltLength);

            var passwordHash = HashPassword(newTraineeDto.password, salt);

            var newTrainee = new Trainee()
            {
                FirstName = newTraineeDto.FirstName,
                LastName = newTraineeDto.LastName,
                Level = newTraineeDto.Level,
                SystemRole = newTraineeDto.SystemRole,
                ImgLink = newTraineeDto.ImgLink,
                username = newTraineeDto.username,
                passwordHash = passwordHash,
                PasswordSalt = salt,
                Role = role,
                Department = department,
            };

            _context.Trainees.Add(newTrainee);
            _context.SaveChanges();

            return ConvertTraineeToDto(newTrainee);
        }

        public ICollection<Trainee> GetAllTrainees()
        {
            return _context.Trainees
                .ToList();
        }
    }
}
