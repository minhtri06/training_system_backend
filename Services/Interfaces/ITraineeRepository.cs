using backend.Dto.Trainee;
using backend.Dto.Login;
using backend.Dto.Token;

namespace backend.Services.Interfaces
{
    public interface ITraineeRepository
    {
        bool CheckUsernameExist(string username);
        TraineeDto? GetTraineeByLoginInfo(LoginDto loginDto);
        TraineeDto? GetTraineeById(int traineeId);
        TraineeDto? GetTraineeByUsername(string username);
        TraineeDto CreateTrainee(NewTraineeDto newTraineeDto);
        IQueryable<TraineeDto> GetAllTrainees();
        int AddRefreshToken(int traineeId, int TokenId);
        RefreshTokenDto? GetRefreshTokenByTraineeId(int traineeId);
    }
}
