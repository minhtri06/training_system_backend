using backend.Models;
using backend.Dto.DepartmentLearningPath;
using backend.Services.Interfaces;
using backend.Dto.LearningPath;
using backend.Dto.Department;

namespace backend.Services.Repositories
{
    public class DepartmentLearningPathRepository
        : IDepartmentLearningPathRepository
    {
        private readonly AppDbContext _context;

        public DepartmentLearningPathRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool CheckIdExist(int departmentId, int learningPathId)
        {
            return _context.DepartmentLearningPaths.Any(
                dlp =>
                    dlp.DepartmentId == departmentId
                    && dlp.LearningPathId == learningPathId
            );
        }

        public ICollection<DepartmentLearningPathDto> GetAll()
        {
            return _context.DepartmentLearningPaths
                .Select(
                    dlp =>
                        Utils.DtoConversion.ConvertDepartmentLearningPath(dlp)
                )
                .ToList();
        }

        public ICollection<DepartmentDto> GetAllDepartmentsOfALearningPath(
            int learningPathId
        )
        {
            return _context.DepartmentLearningPaths
                .Where(dlp => dlp.LearningPathId == learningPathId)
                .Select(dlp => dlp.Department)
                .Select(d => Utils.DtoConversion.ConvertDepartment(d))
                .ToList();
        }

        public DepartmentLearningPathDto GetById(
            int departmentId,
            int learningPathId
        )
        {
            var departmentLearningPath =
                _context.DepartmentLearningPaths.Single(
                    c =>
                        c.DepartmentId == departmentId
                        && c.LearningPathId == learningPathId
                );

            return Utils.DtoConversion.ConvertDepartmentLearningPath(
                departmentLearningPath
            );
        }

        public DepartmentLearningPathDto Create(
            NewDepartmentLearningPathDto newDepartmentLearningPathDto
        )
        {
            var newDepartmentLearningPath = new DepartmentLearningPath()
            {
                DepartmentId = newDepartmentLearningPathDto.DepartmentId,
                LearningPathId = newDepartmentLearningPathDto.LearningPathId
            };

            _context.DepartmentLearningPaths.Add(newDepartmentLearningPath);
            _context.SaveChanges();

            return Utils.DtoConversion.ConvertDepartmentLearningPath(
                newDepartmentLearningPath
            );
        }

        public DepartmentLearningPathDto DeleteById(
            int departmentId,
            int learningPathId
        )
        {
            var departmentLearningPath =
                _context.DepartmentLearningPaths.Single(
                    c =>
                        c.DepartmentId == departmentId
                        && c.LearningPathId == learningPathId
                );

            _context.DepartmentLearningPaths.Remove(departmentLearningPath);
            _context.SaveChanges();

            return Utils.DtoConversion.ConvertDepartmentLearningPath(
                departmentLearningPath
            );
        }
    }
}
