using backend.Dto.Department;
using backend.Dto.DepartmentLearningPath;
using backend.Dto.LearningPath;

namespace backend.Services.Interfaces
{
    public interface IDepartmentLearningPathRepository
    {
        bool CheckIdExist(int departmentId, int learningPathId);
        ICollection<DepartmentLearningPathDto> GetAll();
        ICollection<DepartmentDto> GetAllDepartmentsOfALearningPath(
            int learningPathId
        );
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
