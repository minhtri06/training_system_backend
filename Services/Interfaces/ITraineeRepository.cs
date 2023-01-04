using backend.Dto.Trainee;
using backend.Dto.Login;
using backend.Dto.Token;

namespace backend.Services.Interfaces
{
    public interface ITraineeRepository
    {
        bool CheckUsernameExist(string username);
        TraineeDto? GetByLoginInfo(LoginDto loginDto);
        TraineeDto? GetById(int traineeId);
        ICollection<TraineeDto> GetAll();
        TraineeDto? GetByUsername(string username);
        TraineeDto Create(NewTraineeDto newTraineeDto);
        void AddRefreshToken(int traineeId, int TokenId);
    }
}
