using backend.Dto.Trainer;
using backend.Models;
using backend.Services.Interfaces;

namespace backend.Services.Repositories
{
    public class TrainerRepository : ITrainerRepository
    {
        private readonly AppDbContext _context;

        public TrainerRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool CheckIdExist(int trainerId)
        {
            return _context.Trainers.Any(t => t.Id == trainerId);
        }

        public ICollection<TrainerDto> GetAll()
        {
            return _context.Trainers
                .Select(t => Utils.DtoConversion.ConvertTrainer(t))
                .ToList();
        }

        public TrainerDto GetById(int trainerId)
        {
            var trainer = _context.Trainers.Single(t => t.Id == trainerId);

            return Utils.DtoConversion.ConvertTrainer(trainer);
        }

        public TrainerDto Create(NewTrainerDto newTrainerDto)
        {
            var salt = Utils.Security.GenerateSalt(Utils.SALT_LENGTH);
            var passwordHash = Utils.Security.HashPassword(
                newTrainerDto.Password,
                salt
            );

            var newTrainer = new Trainer()
            {
                FirstName = newTrainerDto.FirstName,
                LastName = newTrainerDto.LastName,
                SystemRole = SystemRole.Trainer,
                ImgLink = newTrainerDto.ImgLink,
                Username = newTrainerDto.Username,
                PasswordHash = passwordHash,
                PasswordSalt = salt,
            };

            _context.Trainers.Add(newTrainer);
            _context.SaveChanges();

            return Utils.DtoConversion.ConvertTrainer(newTrainer);
        }
    }
}
