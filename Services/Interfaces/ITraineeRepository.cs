using backend.Dto.Trainee;
using backend.Models;

namespace backend.Services.Interfaces
{
    public interface ITraineeRepository
    {
        bool CheckUsernameExist(string username);
        TraineeDto? GetTraineeById(int traineeId);

        TraineeDto CreateTrainee(NewTraineeDto newTraineeDto);

        IQueryable<TraineeDto> GetAllTrainees();
    }
}
