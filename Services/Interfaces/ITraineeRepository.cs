using backend.Dto.Trainee;
using backend.Dto.Login;
using backend.Dto.Token;

namespace backend.Services.Interfaces
{
    public interface ITraineeRepository
    {
        bool CheckIdExist(int traineeId);
        bool CheckUsernameExist(string username);
        ICollection<TraineeDto> GetAll();
        TraineeDto? GetByLoginInfo(LoginDto loginDto);
        TraineeDto? GetById(int traineeId);
        TraineeDto? GetByUsername(string username);
        TraineeDto Create(NewTraineeDto newTraineeDto);
        TraineeDto? Update(int traineeId, UpdateTraineeDto updateTraineeDto);
        TraineeDto DeleteById(int traineeId);
        void AddRefreshToken(int traineeId, int TokenId);
    }
}
