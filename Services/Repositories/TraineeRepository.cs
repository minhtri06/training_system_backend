using backend.Dto.Trainee;
using backend.Models;
using backend.Services.Interfaces;
using backend.Dto.Login;
using backend.Dto.Token;

namespace backend.Services.Repositories
{
    public class TraineeRepository : ITraineeRepository
    {
        private readonly AppDbContext _context;

        public TraineeRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool CheckUsernameExist(string username)
        {
            return _context.Trainees.Any(t => t.Username == username);
        }

        public TraineeDto? GetByLoginInfo(LoginDto loginDto)
        {
            var trainee = _context.Trainees.SingleOrDefault(
                t => t.Username == loginDto.Username
            );

            if (trainee == null)
            {
                return null;
            }

            var checkedPasswordHash = Utils.Security.HashPassword(
                loginDto.Password,
                trainee.PasswordSalt
            );

            if (checkedPasswordHash != trainee.PasswordHash)
            {
                return null;
            }

            return Utils.DtoConversion.ConvertTrainee(trainee);
        }

        public TraineeDto Create(NewTraineeDto newTraineeDto)
        {
            var role = _context.Roles.SingleOrDefault(
                r => r.Id == newTraineeDto.RoleId
            );
            var department = _context.Departments.SingleOrDefault(
                d => d.Id == newTraineeDto.DepartmentId
            );

            var salt = Utils.Security.GenerateSalt(Utils.SALT_LENGTH);
            var passwordHash = Utils.Security.HashPassword(
                newTraineeDto.Password,
                salt
            );

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

            return Utils.DtoConversion.ConvertTrainee(newTrainee);
        }

        public TraineeDto? GetById(int traineeId)
        {
            var trainee = _context.Trainees.SingleOrDefault(
                t => t.Id == traineeId
            );

            return trainee != null ? Utils.DtoConversion.ConvertTrainee(trainee) : null;
        }

        public ICollection<TraineeDto> GetAll()
        {
            return _context.Trainees
                .Select(t => Utils.DtoConversion.ConvertTrainee(t))
                .ToList();
        }

        public TraineeDto? GetByUsername(string username)
        {
            var trainee = _context.Trainees.SingleOrDefault(
                t => t.Username == username
            );

            return trainee != null ? Utils.DtoConversion.ConvertTrainee(trainee) : null;
        }

        public void AddRefreshToken(int traineeId, int TokenId)
        {
            var trainee = _context.Trainees.Single(t => t.Id == traineeId);

            if (trainee.RefreshTokenId != null)
            {
                throw new Exception(
                    "Admin user already have a token, we cannot add another token"
                );
            }

            var refreshToken = _context.RefreshTokens.Single(
                t => t.Id == TokenId
            );

            trainee.RefreshToken = refreshToken;

            _context.Trainees.Update(trainee);
            _context.SaveChanges();
        }
    }
}
