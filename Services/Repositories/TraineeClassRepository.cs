using backend.Dto.TraineeClass;
using backend.Models;
using backend.Services.Interfaces;

namespace backend.Services.Repositories
{
    public class TraineeClassRepository : ITraineeClassRepository
    {
        private readonly AppDbContext _context;

        public TraineeClassRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool CheckIdExist(int traineeId, int classId)
        {
            return _context.TraineeClasses.Any(
                tc => tc.TraineeId == traineeId && tc.ClassId == classId
            );
        }

        public TraineeClassDto GetById(int traineeId, int classId)
        {
            var traineeClass = _context.TraineeClasses.Single(
                tc => tc.TraineeId == traineeId && tc.ClassId == classId
            );

            return Utils.DtoConversion.ConvertTraineeClass(traineeClass);
        }

        public ICollection<TraineeClassDto> GetAll()
        {
            var traineeClassDtos = _context.TraineeClasses
                .Select(tc => Utils.DtoConversion.ConvertTraineeClass(tc))
                .ToList();

            return traineeClassDtos;
        }

        public TraineeClassDto Create(NewTraineeClassDto newTraineeClassDto)
        {
            int courseId = (int)
                _context.Classes
                    .Single(c => c.Id == newTraineeClassDto.ClassId)
                    .CourseId;
            var traineeClass = new TraineeClass()
            {
                TraineeId = newTraineeClassDto.TraineeId,
                ClassId = newTraineeClassDto.ClassId,
                GPA = newTraineeClassDto.GPA,
                Status = Utils.DtoConversion.ConvertTraineeClassGPA(
                    newTraineeClassDto.GPA
                ),
                CourseId = courseId
            };

            _context.TraineeClasses.Add(traineeClass);
            _context.SaveChanges();

            return Utils.DtoConversion.ConvertTraineeClass(traineeClass);
        }

        public TraineeClassDto DeleteById(int traineeId, int classId)
        {
            var traineeClass = _context.TraineeClasses.Single(
                tc => tc.TraineeId == traineeId && tc.ClassId == classId
            );

            _context.TraineeClasses.Remove(traineeClass);
            _context.SaveChanges();

            return Utils.DtoConversion.ConvertTraineeClass(traineeClass);
        }

        public TraineeClassDto Update(
            int traineeId,
            int classId,
            UpdateTraineeClassDto updateTraineeClassDto
        )
        {
            var traineeClass = _context.TraineeClasses.Single(
                tc => tc.TraineeId == traineeId && tc.ClassId == classId
            );

            Utils.EntityMapping.MapTraineeClassFromUpdateDto(
                ref traineeClass,
                updateTraineeClassDto
            );

            _context.TraineeClasses.Update(traineeClass);
            _context.SaveChanges();

            return Utils.DtoConversion.ConvertTraineeClass(traineeClass);
        }
    }
}
