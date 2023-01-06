using backend.Dto.LearningPathCourse;

namespace backend.Services.Interfaces
{
    public interface ILearningPathCourseRepository
    {
        bool CheckIdExist(int courseId, int learningPathId);
        LearningPathCourseDto GetById(int courseId, int learningPathId);
        ICollection<LearningPathCourseDto> GetAll();
        LearningPathCourseDto Create(NewLearningPathCourseDto newLearningPathCourseDto);
        LearningPathCourseDto DeleteById(int courseId, int learningPathId);
        LearningPathCourseDto Update(
            int courseId, int learningPathId,
            UpdateLearningPathCourseDto updateLearningPathCourseDto
        );
    }
}
