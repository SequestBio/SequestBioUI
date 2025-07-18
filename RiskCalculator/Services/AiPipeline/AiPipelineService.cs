using Microsoft.ML;
using RiskCalculator.Services.BiasMitigation;
using RiskCalculator.Services.FeatureSelection;
using SequestBioAI.BiasMitigation;
using SequestBioAI.DataProcessing;
using SequestBioAI.ModelTraining;
using SequestBioAI.Utilities;

namespace RiskCalculator.Services.AiPipeline;

public class AiPipelineService
{
    private readonly FeatureSelectionService _featureSelectionService;
    private readonly BiasMitigationService _biasMitigationService;
    private readonly DataPreprocessor _dataPreprocessor;
    private readonly ModelTrainer _modelTrainer;

    public AiPipelineService(
        FeatureSelectionService featureSelectionService,
        BiasMitigationService biasMitigationService,
        DataPreprocessor dataPreprocessor,
        ModelTrainer modelTrainer)
    {
        _featureSelectionService = featureSelectionService;
        _biasMitigationService = biasMitigationService;
        _dataPreprocessor = dataPreprocessor;
        _modelTrainer = modelTrainer;
    }

    public ITransformer TrainModel(List<RawSample> rawSamples, List<string> allFeatures, int topFeatures)
    {
        if (rawSamples == null || !rawSamples.Any())
            throw new ArgumentException("Raw samples cannot be null or empty");

        if (allFeatures == null || !allFeatures.Any())
            throw new ArgumentException("Feature list cannot be null or empty");

        var selectedFeatures = _featureSelectionService.SelectTopFeatures(allFeatures, topFeatures);

        var preprocessedSamples = _dataPreprocessor.PreprocessData(rawSamples);

        var demographicSamples = preprocessedSamples.Select(p => new SampleDataWithDemographics
        {
            Label = p.Label,
            Features = p.Features,
            Group = "Default"
        }).ToList();

        var balancedSamples = _biasMitigationService.ApplyBiasMitigation(demographicSamples);

        var mlSamples = balancedSamples.Select(SampleDataMapper.MapFromDemographicSample).ToList();

        return _modelTrainer.TrainModel(mlSamples, selectedFeatures);
    }
}