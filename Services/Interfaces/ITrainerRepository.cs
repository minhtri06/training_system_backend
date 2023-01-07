using backend.Dto.Trainer;

namespace backend.Services.Interfaces
{
    public interface ITrainerRepository
    {
        bool CheckUsernameExist(string username);
        bool CheckIdExist(int trainerId);
        ICollection<TrainerDto> GetAll();
        TrainerDto GetById(int trainerId);
        TrainerDto Create(NewTrainerDto newTrainerDto);
        TrainerDto DeleteById(int trainerId);
        TrainerDto Update(int trainerId, UpdateTrainerDto updateTrainerDto);
    }
}
