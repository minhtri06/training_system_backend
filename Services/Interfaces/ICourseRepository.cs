using backend.Dto.Course;

namespace backend.Services.Interfaces
{
    public interface ICourseRepository
    {
        ICollection<CourseDto> GetAll();
        CourseDto? GetById(int courseId);
        CourseDto Create(NewCourseDto newCourseDto);
        CourseDto? DeleteById(int courseId);
        CourseDto? Update(int courseId, UpdateCourseDto updateCourseDto);
    }
}
