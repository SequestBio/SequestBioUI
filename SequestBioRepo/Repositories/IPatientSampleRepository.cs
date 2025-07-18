using SequestBioDataModel.Entities;

namespace SequestBioRepo.Repositories;

public interface IPatientSampleRepository
{
    Task<List<PatientSampleEntity>> GetSamplesByPatientIdAsync(Guid patientId);
    Task SavePredictionResultAsync(PredictionResultEntity predictionResult);
}