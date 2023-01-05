using backend.Models;
using backend.Dto.Class;
using backend.Services.Interfaces;

namespace backend.Services.Repositories
{
    public class ClassRepository: IClassRepository
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
            var _class = _context.Courses.SingleOrDefault(c => c.Id == classId);

            if (_class != null)
            {
                return Utils.DtoConversion.ConvertClass(_class);
            }
            return null;
        }

        public ClassDto Create(NewClassDto newClassDto)
        {
            var course = _context.Courses.SingleOrDefault(c => c.Id == newClassDto.CourseId);
            
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
            var _class = _context.Classes.SingleOrDefault(c => c.Id == classId);

            if (_class == null)
            { 
                return null;
            }

            _context.Classes.Remove(_class);
            _context.SaveChanges();

            return Utils.DtoConversion.ConvertClass(_class);
        }

        public ClassDto? Update(int classId, UpdateClassDto updateClassDto)
        {
            var _class = _context.Classes.SingleOrDefault(c => c.Id == classId);

            if (_class == null)
            {
                return null;
            }

            Course? course = null;
            if (updateClassDto.CourseId != null)
            {
                course = _context.Courses.SingleOrDefault(c => c.Id == updateClassDto.CourseId);
                if (course == null)
                {
                    throw new Exception("CourseId not found!!!");
                }
            }

            _class.Name = updateClassDto.Name;
            _class.StartDate = updateClassDto.StartDate;
            _class.EndDate = updateClassDto.EndDate;
            _class.Course = course;

            _context.Classes.Update(_class);
            _context.SaveChanges();

            return Utils.DtoConversion.ConvertClass(_class);
        }
    }
}