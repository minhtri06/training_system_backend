using backend.Models;
using backend.Dto.Course;
using backend.Services.Interfaces;

namespace backend.Services.Repositories
{
    public class CourseRepository : ICourseRepository
    {
        private readonly AppDbContext _context;

        public CourseRepository(AppDbContext context)
        {
            _context = context;
        }

        public ICollection<CourseDto> GetAll()
        {
            return _context.Courses
                .Select(c => Utils.DtoConversion.ConvertCourse(c))
                .ToList();
        }

        public CourseDto? GetById(int courseId)
        {
            var course = _context.Courses.SingleOrDefault(
                c => c.Id == courseId
            );

            if (course != null)
            {
                return Utils.DtoConversion.ConvertCourse(course);
            }
            return null;
        }

        public CourseDto Create(NewCourseDto newCourseDto)
        {
            Trainer? trainer = null;
            if (newCourseDto.TrainerId != null)
            {
                trainer = _context.Trainers.SingleOrDefault(
                    t => t.Id == newCourseDto.TrainerId
                );
                if (trainer == null)
                {
                    throw new Exception("TrainerId not found!!!");
                }
            }

            var newCourse = new Course()
            {
                Name = newCourseDto.Name,
                Online = newCourseDto.Online,
                Duration = newCourseDto.Duration,
                LearningObjective = newCourseDto.LearningObjective,
                ImgLink = newCourseDto.ImgLink,
                Description = newCourseDto.Description,
                Trainer = trainer
            };

            _context.Courses.Add(newCourse);
            _context.SaveChanges();

            return Utils.DtoConversion.ConvertCourse(newCourse);
        }

        public CourseDto? DeleteById(int courseId)
        {
            var course = _context.Courses.SingleOrDefault(
                c => c.Id == courseId
            );

            if (course == null)
            {
                return null;
            }

            _context.Courses.Remove(course);
            _context.SaveChanges();

            return Utils.DtoConversion.ConvertCourse(course);
        }

        public CourseDto? Update(int courseId, UpdateCourseDto updateCourseDto)
        {
            var course = _context.Courses.SingleOrDefault(
                c => c.Id == courseId
            );

            if (course == null)
            {
                return null;
            }

            Trainer? trainer = null;
            if (updateCourseDto.TrainerId != null)
            {
                trainer = _context.Trainers.SingleOrDefault(
                    t => t.Id == updateCourseDto.TrainerId
                );
                if (trainer == null)
                {
                    throw new Exception("TrainerId not found!!!");
                }
            }

            course.Name = updateCourseDto.Name;
            course.Online = updateCourseDto.Online;
            course.Duration = updateCourseDto.Duration;
            course.LearningObjective = updateCourseDto.LearningObjective;
            course.ImgLink = updateCourseDto.ImgLink;
            course.Description = updateCourseDto.Description;
            course.Trainer = trainer;

            _context.Courses.Update(course);
            _context.SaveChanges();

            return Utils.DtoConversion.ConvertCourse(course);
        }
    }
}
