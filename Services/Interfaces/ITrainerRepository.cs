using backend.Dto.Trainer;

namespace backend.Services.Interfaces
{
    public interface ITrainerRepository
    {
        bool CheckIdExist(int trainerId);
        ICollection<TrainerDto> GetAll();
        TrainerDto GetById(int trainerId);
    }
}
