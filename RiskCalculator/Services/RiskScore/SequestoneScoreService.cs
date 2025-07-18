using System.IO;
using System.Threading.Tasks;
using RiskCalculator.Result;
using RiskCalculator.Services.AiPipeline;
using RiskCalculator.Services.FeatureSelection;
using RiskCalculator.Services.Tumor;
using SequestBioAI.Data;
using SequestBioAI.DataProcessing;
using SequestBioAI.ModelTraining;
using SequestBioAI.RiskScore;
using SequestBioAI.Utilities;

namespace RiskCalculator.Services.RiskScore;

/// <summary>
/// Service that calculates a Sequestone Score for a given patient.
/// This can be extended later to incorporate real AI or quantum model results.
/// </summary>
public class SequestoneScoreService
{
    private readonly TumorFeatureService _tumorFeatureService;
    private readonly FeatureSelectionService _featureSelectionService;
    private readonly AiPipelineService _aiPipelineService;

    /// <summary>
    /// Computes a mock Sequestone risk score for the specified patient.
    /// This is currently randomized and should be replaced with real predictive logic.
    /// </summary>
    public SequestoneScoreService(TumorFeatureService tumorFeatureService, 
        FeatureSelectionService featureSelectionService,
        AiPipelineService aiPipelineService)
    {
        _tumorFeatureService = tumorFeatureService;
        _featureSelectionService = featureSelectionService;
        _aiPipelineService = aiPipelineService;

    }
    
    public async Task<PatientScoreResult> GetScoreAsync(Stream tsvFileStream)
    {
        int score = await RiskScoreCalculator.CalculateRiskCategory(tsvFileStream);

        string category = score switch
        {
            >= 10 => "High Risk",
            >= 6 => "Moderate Risk",
            _ => "Low Risk"
        };

        string recommendation = category switch
        {
            "High Risk" => "Initiate aggressive therapy and monitor closely.",
            "Moderate Risk" => "Consider standard of care with added monitoring.",
            _ => "Low risk. Continue with standard protocols."
        };

        return new PatientScoreResult
        {
            Score = score,
            RiskCategory = category,
            Recommendation = recommendation
        };
    }
    
    public PatientScoreResult GetAiPredictedScore(List<RawSample> rawSamples)
    {
        var allFeatures = _tumorFeatureService.GetAllFeatures().Select(f => f.Name).ToList();

        var trainedModel = _aiPipelineService.TrainModel(rawSamples, allFeatures, 100); //Ashkan Arvin top 100 features?

        var modelTrainer = new ModelTrainer();
        
        var firstSample = rawSamples.First();
        var sampleData = SampleDataMapper.MapFromRawSample(firstSample);

        var aiScore = modelTrainer.Predict(trainedModel, sampleData);

        return new PatientScoreResult
        {
            Score = (int)(aiScore * 100), // Scale to 0–100
            RiskCategory = GetRiskCategory(aiScore * 100),
            Recommendation = GetRecommendation(aiScore * 100)
        };
    }

    private string GetRiskCategory(double score) =>
        score switch
        {
            >= 10 => "High Risk",
            >= 6 => "Moderate Risk",
            _ => "Low Risk"
        };

    private string GetRecommendation(double score) =>
        GetRiskCategory(score) switch
        {
            "High Risk" => "Initiate aggressive therapy and monitor closely.",
            "Moderate Risk" => "Consider standard of care with added monitoring.",
            _ => "Low risk. Continue with standard protocols."
        };

}