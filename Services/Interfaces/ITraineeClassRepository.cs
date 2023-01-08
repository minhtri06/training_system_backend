using backend.Dto.TraineeClass;

namespace backend.Services.Interfaces
{
    public interface ITraineeClassRepository
    {
        bool CheckIdExist(int traineeId, int classId);
        TraineeClassDto GetById(int traineeId, int classId);
        ICollection<TraineeClassDto> GetAll();
        TraineeClassDto Create(NewTraineeClassDto newTraineeClassDto);
        TraineeClassDto DeleteById(int traineeId, int classId);
        TraineeClassDto Update(
            int traineeId, int classId,
            UpdateTraineeClassDto updateTraineeClassDto
        );
    }
}
