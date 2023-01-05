using backend.Dto.DepartmentLearningPath;

namespace backend.Services.Interfaces
{
    public interface IDepartmentLearningPathRepository
    {
        bool CheckIdExist(int departmentId, int learningPathId);
        ICollection<DepartmentLearningPathDto> GetAll();
        DepartmentLearningPathDto GetById(int departmentId, int learningPathId);
        DepartmentLearningPathDto Create(
            NewDepartmentLearningPathDto newDepartmentLearningPathDto
        );
        DepartmentLearningPathDto DeleteById(
            int departmentId,
            int learningPathId
        );
    }
}
