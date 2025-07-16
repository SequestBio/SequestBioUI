using RiskCalculator.Models.Cards;
using SequestBioAI.Data;

namespace RiskCalculator.Services.Cards;

/// <summary>
/// Service interface for analyzing tumor microenvironment (TME)
/// </summary>
public interface ITumorMicroenvironmentService
{
    /// <summary>
    /// Analyze tumor microenvironment from TSV data and clinical information
    /// </summary>
    /// <param name="tsvFileStream">RNAseq TSV file stream</param>
    /// <param name="clinicalData">Patient clinical data</param>
    /// <returns>Tumor microenvironment model</returns>
    Task<TumorMicroenvironmentModel> AnalyzeTumorMicroenvironmentAsync(Stream tsvFileStream, ClinicalData clinicalData);

    /// <summary>
    /// Calculate genomic instability score
    /// </summary>
    /// <param name="tsvFileStream">RNAseq TSV file stream</param>
    /// <returns>Genomic instability score (0-100)</returns>
    Task<int> CalculateGenomicInstabilityAsync(Stream tsvFileStream);

    /// <summary>
    /// Calculate tumor infiltrating lymphocytes (TIL) level
    /// </summary>
    /// <param name="tsvFileStream">RNAseq TSV file stream</param>
    /// <returns>TIL level (0-100)</returns>
    Task<int> CalculateTILLevelAsync(Stream tsvFileStream);

    /// <summary>
    /// Calculate mutation burden
    /// </summary>
    /// <param name="tsvFileStream">RNAseq TSV file stream</param>
    /// <returns>Mutation burden score</returns>
    Task<double> CalculateMutationBurdenAsync(Stream tsvFileStream);

    /// <summary>
    /// Calculate immune infiltration score
    /// </summary>
    /// <param name="tsvFileStream">RNAseq TSV file stream</param>
    /// <returns>Immune infiltration score</returns>
    Task<double> CalculateImmuneInfiltrationAsync(Stream tsvFileStream);
} 