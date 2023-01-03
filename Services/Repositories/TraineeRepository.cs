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
        private readonly int _saltLength = 32;

        public TraineeRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool CheckUsernameExist(string username)
        {
            return _context.Trainees.Any(t => t.Username == username);
        }

        public TraineeDto? GetTraineeByLoginInfo(LoginDto loginDto)
        {
            var trainee = _context.Trainees.SingleOrDefault(
                t => t.Username == loginDto.Username
            );

            if (trainee == null)
            {
                return null;
            }

            var checkedPasswordHash = Utils.HashPassword(
                loginDto.Password,
                trainee.PasswordSalt
            );

            if (checkedPasswordHash != trainee.PasswordHash)
            {
                return null;
            }

            return Utils.ConvertTraineeToDto(trainee);
        }

        public TraineeDto CreateTrainee(NewTraineeDto newTraineeDto)
        {
            var role = _context.Roles.SingleOrDefault(
                r => r.Id == newTraineeDto.RoleId
            );
            var department = _context.Departments.SingleOrDefault(
                d => d.Id == newTraineeDto.DepartmentId
            );

            var salt = Utils.GenerateSalt(_saltLength);
            var passwordHash = Utils.HashPassword(newTraineeDto.Password, salt);

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

            return Utils.ConvertTraineeToDto(newTrainee);
        }

        public IQueryable<TraineeDto> GetAllTrainees()
        {
            var traineeDtos =
                from trainee in _context.Trainees
                select Utils.ConvertTraineeToDto(trainee);

            return traineeDtos;
        }

        public TraineeDto? GetTraineeById(int traineeId)
        {
            var trainee = _context.Trainees.SingleOrDefault(
                t => t.Id == traineeId
            );

            return trainee != null ? Utils.ConvertTraineeToDto(trainee) : null;
        }

        public TraineeDto? GetTraineeByUsername(string username)
        {
            var trainee = _context.Trainees.SingleOrDefault(
                t => t.Username == username
            );

            return trainee != null ? Utils.ConvertTraineeToDto(trainee) : null;
        }

        public int AddRefreshToken(int traineeId, int TokenId)
        {
            var trainee = _context.Trainees.SingleOrDefault(
                t => t.Id == traineeId
            );

            if (trainee == null)
            {
                return 1;
            }

            var refreshToken = _context.RefreshTokens.SingleOrDefault(
                t => t.Id == TokenId
            );

            if (refreshToken == null)
            {
                return 2;
            }

            trainee.RefreshToken = refreshToken;

            _context.Trainees.Update(trainee);
            _context.SaveChanges();

            return 0;
        }

        public RefreshTokenDto? GetRefreshTokenByTraineeId(int traineeId)
        {
            var trainee = _context.Trainees.SingleOrDefault(
                t => t.Id == traineeId
            );

            if (trainee == null || trainee.RefreshToken == null)
            {
                return null;
            }

            return Utils.ConvertRefreshTokenToDto(trainee.RefreshToken);
        }
    }
}
