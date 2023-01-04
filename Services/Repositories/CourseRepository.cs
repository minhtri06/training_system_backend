using backend.Models;
using backend.Dto.Course;
using backend.Services.Interfaces;

namespace backend.Services.Repositories
{
    public class CourseRepository: ICourseRepository
    {
        private readonly AppDbContext _context;

        public CourseRepository(AppDbContext context)
        {
            _context = context;
        }

        public ICollection<CourseDto> GetAll()
        {
            return _context.Courses
                .Select(c => new CourseDto() 
                { 
                    Id = c.Id, 
                    Name = c.Name, 
                    Online = c.Online,
                    Duration = c.Duration,
                    LearningObjective = c.LearningObjective,
                    ImgLink = c.ImgLink,
                    Description = c.Description,
                    TrainerId = c.TrainerId
                })
                .ToList();
        }

        public CourseDto? GetById(int courseId)
        {
            var course = _context.Courses.SingleOrDefault(c => c.Id == courseId);

            if (course != null)
                return new CourseDto() 
                { 
                    Id = course.Id, 
                    Name = course.Name, 
                    Online = course.Online,
                    Duration = course.Duration,
                    LearningObjective = course.LearningObjective,
                    ImgLink = course.ImgLink,
                    Description = course.Description,
                    TrainerId = course.TrainerId
                };
            return null;
        }

        public CourseDto Create(NewCourseDto newCourseDto)
        {
            var newCourse = new Course() 
            { 
                Name = newCourseDto.Name, 
                Online = newCourseDto.Online,
                Duration = newCourseDto.Duration,
                LearningObjective = newCourseDto.LearningObjective,
                ImgLink = newCourseDto.ImgLink,
                Description = newCourseDto.Description,
                TrainerId = newCourseDto.TrainerId
            };

            _context.Courses.Add(newCourse);
            _context.SaveChanges();

            return new CourseDto() 
            { 
                Id = newCourse.Id,
                Name = newCourse.Name, 
                Online = newCourse.Online,
                Duration = newCourse.Duration,
                LearningObjective = newCourse.LearningObjective,
                ImgLink = newCourse.ImgLink,
                Description = newCourse.Description,
                TrainerId = newCourse.TrainerId
            };
        }

        public int DeleteById(int courseId)
        {
            var course = _context.Courses.SingleOrDefault(r => r.Id == courseId);

            if (course == null)
                return -1;

            _context.Courses.Remove(course);
            _context.SaveChanges();

            return courseId;
        }

        public int Update(int courseId, UpdateCourseDto updateCourseDto)
        {
            var course = _context.Courses.SingleOrDefault(r => r.Id == courseId);

            if (course == null)
            {
                return -1;
            }

            course.Name = updateCourseDto.Name;
            course.Name = updateCourseDto.Name;
            course.Online = updateCourseDto.Online;
            course.Duration = updateCourseDto.Duration;
            course.LearningObjective = updateCourseDto.LearningObjective;
            course.ImgLink = updateCourseDto.ImgLink;
            course.Description = updateCourseDto.Description;
            course.TrainerId = updateCourseDto.TrainerId;

            _context.Courses.Update(course);
            _context.SaveChanges();

            return courseId;
        }
    }
}