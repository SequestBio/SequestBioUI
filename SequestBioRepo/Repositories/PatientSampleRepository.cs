using Microsoft.EntityFrameworkCore;
using SequestBioDataModel.Entities;
using SequestBioRepo.DbContext;

namespace SequestBioRepo.Repositories;

public class PatientSampleRepository : IPatientSampleRepository
{
    private readonly SequestBioDbContext _dbContext;

    public PatientSampleRepository(SequestBioDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<PatientSampleEntity>> GetSamplesByPatientIdAsync(Guid patientId)
    {
        return await _dbContext.PatientSamples
            .Where(x => x.PatientId == patientId)
            .ToListAsync();
    }

    public async Task SavePredictionResultAsync(PredictionResultEntity predictionResult)
    {
        await _dbContext.PredictionResults.AddAsync(predictionResult);
        await _dbContext.SaveChangesAsync();
    }
}