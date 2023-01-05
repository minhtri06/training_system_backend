using backend.Dto.Trainee;
using backend.Dto.Login;
using backend.Dto.Token;

namespace backend.Services.Interfaces
{
    public interface ITraineeRepository
    {
        bool CheckUsernameExist(string username);
        ICollection<TraineeDto> GetAll();
        TraineeDto? GetByLoginInfo(LoginDto loginDto);
        TraineeDto? GetById(int traineeId);
        TraineeDto? GetByUsername(string username);
        TraineeDto Create(NewTraineeDto newTraineeDto);
        TraineeDto? Update(int traineeId, UpdateTraineeDto updateTraineeDto);
        void AddRefreshToken(int traineeId, int TokenId);
    }
}
