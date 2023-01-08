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
            if (newTraineeClassDto.GPA) { }
            var traineeClass = new TraineeClass()
            {
                TraineeId = newTraineeClassDto.TraineeId,
                ClassId = newTraineeClassDto.ClassId,
                GPA = newTraineeClassDto.GPA
            };

            _context.TraineeClasses.Add(traineeClass);
            _context.SaveChanges();

            return Utils.DtoConversion.ConvertTraineeClass(traineeClass);
        }

        public TraineeClassDto DeleteById(int traineeId, int classId)
        {
            var traineeClass = _context.TraineeClasses.Single(
                tc =>
                    tc.CourseId == courseId
                    && tc.LearningPathId == learningPathId
            );

            _context.TraineeClasss.Remove(traineeClass);
            _context.SaveChanges();

            return Utils.DtoConversion.ConvertTraineeClass(traineeClass);
        }

        public TraineeClassDto Update(
            int traineeId,
            int classId,
            UpdateTraineeClassDto updateTraineeClassDto
        )
        {
            var traineeClass = _context.TraineeClasss.Single(
                lpc =>
                    lpc.CourseId == courseId
                    && lpc.LearningPathId == learningPathId
            );

            Utils.EntityMapping.MapTraineeClassFromDto(
                ref traineeClass,
                updateTraineeClassDto
            );

            _context.TraineeClasss.Update(traineeClass);
            _context.SaveChanges();

            return Utils.DtoConversion.ConvertTraineeClass(traineeClass);
        }
    }
}
