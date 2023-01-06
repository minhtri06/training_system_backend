using backend.Dto.LearningPathCertificate;
using backend.Models;
using backend.Services.Interfaces;

namespace backend.Services.Repositories
{
    public class LearningPathCertificateRepository : ILearningPathCertificateRepository
    {
        private readonly AppDbContext _context;

        public LearningPathCertificateRepository(AppDbContext context)
        {
            _context = context;
        }

        public bool CheckIdExist(int traineeId, int learningPathId)
        {
            return _context.LearningPathCertificates.Any(lpc => lpc.TraineeId == traineeId && lpc.LearningPathId == learningPathId);
        }

        public LearningPathCertificateDto GetById(int traineeId, int learningPathId)
        {
            var learningPathCertificate = _context.LearningPathCertificates.Single(
                lpc => lpc.TraineeId == traineeId && lpc.LearningPathId == learningPathId
            );

            return Utils.DtoConversion.ConvertLearningPathCertificate(learningPathCertificate);
        }

        public ICollection<LearningPathCertificateDto> GetAll()
        {
            var learningPathCertificateDtos = _context.LearningPathCertificates
                .Select(lpc => Utils.DtoConversion.ConvertLearningPathCertificate(lpc))
                .ToList();

            return learningPathCertificateDtos;
        }

        public LearningPathCertificateDto Create(NewLearningPathCertificateDto newLearningPathCertificateDto)
        {
            var learningPathCertificate = new LearningPathCertificate()
            {
                TraineeId = newLearningPathCertificateDto.TraineeId,
                LearningPathId = newLearningPathCertificateDto.LearningPathId,
                StartDate = newLearningPathCertificateDto.StartDate,
                Duration = newLearningPathCertificateDto.Duration
            };

            _context.LearningPathCertificates.Add(learningPathCertificate);
            _context.SaveChanges();

            return Utils.DtoConversion.ConvertLearningPathCertificate(learningPathCertificate);
        }

        public LearningPathCertificateDto DeleteById(int traineeId, int learningPathId)
        {
            var learningPathCertificate = _context.LearningPathCertificates.Single(
                lpc => lpc.TraineeId == traineeId && lpc.LearningPathId == learningPathId
            );

            _context.LearningPathCertificates.Remove(learningPathCertificate);
            _context.SaveChanges();

            return Utils.DtoConversion.ConvertLearningPathCertificate(learningPathCertificate);
        }

        public LearningPathCertificateDto Update(
            int traineeId, int learningPathId,
            UpdateLearningPathCertificateDto updateLearningPathCertificateDto
        )
        {
            var learningPathCertificate = _context.LearningPathCertificates.Single(
                lpc => lpc.TraineeId == traineeId && lpc.LearningPathId == learningPathId
            );

            Utils.EntityMapping.MapLearningPathCertificateFromDto(
                ref learningPathCertificate,
                updateLearningPathCertificateDto
            );

            _context.LearningPathCertificates.Update(learningPathCertificate);
            _context.SaveChanges();

            return Utils.DtoConversion.ConvertLearningPathCertificate(learningPathCertificate);
        }
    }
}
