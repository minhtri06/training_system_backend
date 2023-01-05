using backend.Dto.LearningPath;

namespace backend.Services.Interfaces
{
    public interface ILearningPathRepository
    {
        bool CheckIdExist(int learningPathId);
        LearningPathDto GetById(int learningPathId);
        ICollection<LearningPathDto> GetAll();
        LearningPathDto Create(NewLearningPathDto newLearningPathDto);
        LearningPathDto DeleteById(int learningPathId);
        LearningPathDto Update(
            int learningPathId,
            UpdateLearningPathDto updateLearningPathDto
        );
    }
}
