using RiskCalculator.Models.Cards;
using SequestBioAI.Data;

namespace RiskCalculator.Services.Cards;

/// <summary>
/// Interface for SHAP Waterfall analysis service
/// </summary>
public interface IShapWaterfallService
{
    /// <summary>
    /// Generate SHAP waterfall analysis for ranked additive contributions
    /// </summary>
    /// <param name="tsvFileStream">RNA-seq data stream</param>
    /// <param name="clinicalData">Patient clinical data</param>
    /// <returns>SHAP waterfall analysis results</returns>
    Task<ShapWaterfallModel> GenerateShapWaterfallAsync(Stream tsvFileStream, ClinicalData clinicalData);
} 