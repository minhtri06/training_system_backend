using backend.Dto.Class;

namespace backend.Services.Interfaces
{
    public interface IClassRepository
    {
        ICollection<ClassDto> GetAll();
        ClassDto? GetById(int classId);
        ClassDto Create(NewClassDto newClassDto);
        ClassDto? DeleteById(int classId);
        ClassDto? Update(int classId, UpdateClassDto updateClassDto);
    }
}
