using backend.Models;
using backend.Dto.Class;
using backend.Services.Interfaces;

namespace backend.Services.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly AppDbContext _context;

        public ClassRepository(AppDbContext context)
        {
            _context = context;
        }

        public ICollection<ClassDto> GetAll()
        {
            return _context.Classes
                .Select(c => Utils.DtoConversion.ConvertClass(c))
                .ToList();
        }

        public ClassDto? GetById(int classId)
        {
            var class_ = _context.Classes.SingleOrDefault(c => c.Id == classId);

            if (class_ != null)
            {
                return Utils.DtoConversion.ConvertClass(class_);
            }
            return null;
        }

        public ClassDto Create(NewClassDto newClassDto)
        {
            Course? course = null;
            if (newClassDto.CourseId != null)
            {
                course = _context.Courses.SingleOrDefault(
                    c => c.Id == newClassDto.CourseId
                );
                if (course == null)
                {
                    throw new Exception("CourseId not found!!!");
                }
            }

            var newClass = new Class()
            {
                Name = newClassDto.Name,
                StartDate = newClassDto.StartDate,
                EndDate = newClassDto.EndDate,
                Course = course
            };

            _context.Classes.Add(newClass);
            _context.SaveChanges();

            return Utils.DtoConversion.ConvertClass(newClass);
        }

        public ClassDto? DeleteById(int classId)
        {
            var class_ = _context.Classes.SingleOrDefault(c => c.Id == classId);

            if (class_ == null)
            {
                return null;
            }

            _context.Classes.Remove(class_);
            _context.SaveChanges();

            return Utils.DtoConversion.ConvertClass(class_);
        }

        public ClassDto? Update(int classId, UpdateClassDto updateClassDto)
        {
            var class_ = _context.Classes.SingleOrDefault(c => c.Id == classId);

            if (class_ == null)
            {
                return null;
            }

            Course? course = null;
            if (updateClassDto.CourseId != null)
            {
                course = _context.Courses.SingleOrDefault(
                    c => c.Id == updateClassDto.CourseId
                );
                if (course == null)
                {
                    throw new Exception("CourseId not found!!!");
                }
            }

            class_.Name = updateClassDto.Name;
            class_.StartDate = updateClassDto.StartDate;
            class_.EndDate = updateClassDto.EndDate;
            class_.Course = course;

            _context.Classes.Update(class_);
            _context.SaveChanges();

            return Utils.DtoConversion.ConvertClass(class_);
        }
    }
}
