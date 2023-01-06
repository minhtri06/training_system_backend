using backend.Dto.LearningPathCourse;
using backend.Models;
using backend.Services.Interfaces;

namespace backend.Services.Repositories
{
    public class LearningPathCourseRepository : ILearningPathCourseRepository
    {
        private readonly AppDbContext _context;

        public LearningPathCourseRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool CheckIdExist(int courseId, int learningPathId)
        {
            return _context.LearningPathCourses.Any(lpco => lpco.CourseId == courseId && lpco.LearningPathId == learningPathId);
        }

        public LearningPathCourseDto GetById(int courseId, int learningPathId)
        {
            var learningPathCourse = _context.LearningPathCourses.Single(
                lpc => lpc.CourseId == courseId && lpc.LearningPathId == learningPathId
            );

            return Utils.DtoConversion.ConvertLearningPathCourse(learningPathCourse);
        }

        public ICollection<LearningPathCourseDto> GetAll()
        {
            var learningPathCourseDtos = _context.LearningPathCourses
                .Select(lpco => Utils.DtoConversion.ConvertLearningPathCourse(lpco))
                .ToList();

            return learningPathCourseDtos;
        }

        public LearningPathCourseDto Create(NewLearningPathCourseDto newLearningPathCourseDto)
        {
            var learningPathCourse = new LearningPathCourse()
            {
                CourseId = newLearningPathCourseDto.CourseId,
                LearningPathId = newLearningPathCourseDto.LearningPathId,
                CourseOrder = newLearningPathCourseDto.CourseOrder
            };

            _context.LearningPathCourses.Add(learningPathCourse);
            _context.SaveChanges();

            return Utils.DtoConversion.ConvertLearningPathCourse(learningPathCourse);
        }

        public LearningPathCourseDto DeleteById(int courseId, int learningPathId)
        {
            var learningPathCourse = _context.LearningPathCourses.Single(
                lpco => lpco.CourseId == courseId && lpco.LearningPathId == learningPathId
            );

            _context.LearningPathCourses.Remove(learningPathCourse);
            _context.SaveChanges();

            return Utils.DtoConversion.ConvertLearningPathCourse(learningPathCourse);
        }

        public LearningPathCourseDto Update(
            int courseId, int learningPathId,
            UpdateLearningPathCourseDto updateLearningPathCourseDto
        )
        {
            var learningPathCourse = _context.LearningPathCourses.Single(
                lpc => lpc.CourseId == courseId && lpc.LearningPathId == learningPathId
            );

            Utils.EntityMapping.MapLearningPathCourseFromDto(
                ref learningPathCourse,
                updateLearningPathCourseDto
            );

            _context.LearningPathCourses.Update(learningPathCourse);
            _context.SaveChanges();

            return Utils.DtoConversion.ConvertLearningPathCourse(learningPathCourse);
        }
    }
}
