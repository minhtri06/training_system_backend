using backend.Dto.Trainee;
using backend.Models;

namespace backend.Services.Interfaces
{
    public interface ITraineeRepository
    {
        TraineeDto CreateTrainee(NewTraineeDto newTrainee);
        ICollection<Trainee> GetAllTrainees();
        bool CheckUsernameExist(string username);
    }
}
