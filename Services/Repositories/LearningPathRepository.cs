using backend.Dto.LearningPath;
using backend.Models;
using backend.Services.Interfaces;

namespace backend.Services.Repositories
{
    public class LearningPathRepository : ILearningPathRepository
    {
        private readonly AppDbContext _context;

        public LearningPathRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool CheckIdExist(int learningPathId)
        {
            return _context.LearningPaths.Any(lp => lp.Id == learningPathId);
        }

        public LearningPathDto GetById(int learningPathId)
        {
            var learningPath = _context.LearningPaths.Single(
                lp => lp.Id == learningPathId
            );

            return Utils.DtoConversion.ConvertLearningPath(learningPath);
        }

        public ICollection<LearningPathDto> GetAll()
        {
            var learningPathDtos = _context.LearningPaths
                .Select(lp => Utils.DtoConversion.ConvertLearningPath(lp))
                .ToList();

            return learningPathDtos;
        }

        public LearningPathDto Create(NewLearningPathDto newLearningPathDto)
        {
            var learningPath = new LearningPath()
            {
                Name = newLearningPathDto.Name,
                Description = newLearningPathDto.Description,
                ImgLink = newLearningPathDto.ImgLink,
                ForRoleId = newLearningPathDto.ForRoleId
            };

            _context.LearningPaths.Add(learningPath);
            _context.SaveChanges();

            return Utils.DtoConversion.ConvertLearningPath(learningPath);
        }

        public LearningPathDto DeleteById(int learningPathId)
        {
            var learningPath = _context.LearningPaths.Single(
                lp => lp.Id == learningPathId
            );

            _context.LearningPaths.Remove(learningPath);
            _context.SaveChanges();

            return Utils.DtoConversion.ConvertLearningPath(learningPath);
        }

        public LearningPathDto Update(
            int learningPathId,
            UpdateLearningPathDto updateLearningPathDto
        )
        {
            var learningPath = _context.LearningPaths.Single(
                lp => lp.Id == learningPathId
            );

            Utils.EntityMapping.MapLearningPathFromDto(
                ref learningPath,
                updateLearningPathDto
            );

            _context.LearningPaths.Update(learningPath);
            _context.SaveChanges();

            return Utils.DtoConversion.ConvertLearningPath(learningPath);
        }
    }
}
