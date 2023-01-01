using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;

using backend.Dto.Trainee;
using backend.Models;
using backend.Services.Interfaces;

namespace backend.Services.Repositories
{
    internal class Util
    {
        public static TraineeDto ConvertTraineeToDto(Trainee trainee)
        {
            return new TraineeDto()
            {
                Id = trainee.Id,
                FirstName = trainee.FirstName,
                LastName = trainee.LastName,
                Level = trainee.Level,
                SystemRole = trainee.SystemRole,
                ImgLink = trainee.ImgLink,
                username = trainee.Username,
                RoleId = trainee.RoleId,
                DepartmentId = trainee.DepartmentId
            };
        }

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
                return Encoding.UTF8.GetString(hmac.ComputeHash(passwordBytes));
            }
        }
    }

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
            return _context.Trainees.Any(t => t.Username == username);
        }

        public TraineeDto CreateTrainee(NewTraineeDto newTraineeDto)
        {
            var role = _context.Roles.SingleOrDefault(
                r => r.Id == newTraineeDto.RoleId
            );
            var department = _context.Departments.SingleOrDefault(
                d => d.Id == newTraineeDto.DepartmentId
            );

            var salt = Util.GenerateSalt(_saltLength);
            var passwordHash = Util.HashPassword(newTraineeDto.Password, salt);

            var newTrainee = new Trainee()
            {
                FirstName = newTraineeDto.FirstName,
                LastName = newTraineeDto.LastName,
                Level = newTraineeDto.Level,
                SystemRole = SystemRole.Trainee,
                ImgLink = newTraineeDto.ImgLink,
                Username = newTraineeDto.Username,
                PasswordHash = passwordHash,
                PasswordSalt = salt,
                Role = role,
                Department = department,
            };

            _context.Trainees.Add(newTrainee);
            _context.SaveChanges();

            return Util.ConvertTraineeToDto(newTrainee);
        }

        public IQueryable<TraineeDto> GetAllTrainees()
        {
            var traineeDtos =
                from trainee in _context.Trainees
                select Util.ConvertTraineeToDto(trainee);

            return traineeDtos;
        }

        public TraineeDto? GetTraineeById(int traineeId)
        {
            var trainee = _context.Trainees.SingleOrDefault(
                t => t.Id == traineeId
            );

            return trainee != null ? Util.ConvertTraineeToDto(trainee) : null;
        }
    }
}
