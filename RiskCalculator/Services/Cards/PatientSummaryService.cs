using RiskCalculator.Models.Cards;
using SequestBioAI.Data;

namespace RiskCalculator.Services.Cards;

/// <summary>
/// Service for processing patient summary data
/// </summary>
public class PatientSummaryService : IPatientSummaryService
{
    public async Task<PatientSummaryModel> ProcessPatientDataAsync(ClinicalData clinicalData)
    {
        var riskFactors = await IdentifyRiskFactorsAsync(clinicalData);
        var clinicalSummary = await GenerateClinicalSummaryAsync(clinicalData);
        
        return new PatientSummaryModel
        {
            ClinicalInfo = clinicalData,
            HasClinicalData = !string.IsNullOrEmpty(clinicalData.FullName),
            ClinicalSummary = clinicalSummary,
            IdentifiedRiskFactors = riskFactors
        };
    }

    public async Task<List<string>> IdentifyRiskFactorsAsync(ClinicalData clinicalData)
    {
        // Enhance this method to identify more sophisticated risk factors
        var riskFactors = new List<string>();

        // Basic risk factor identification
        if (!string.IsNullOrEmpty(clinicalData.CancerType))
        {
            // Add cancer type-specific risk factors
            if (clinicalData.CancerType.Contains("Breast", StringComparison.OrdinalIgnoreCase))
            {
                riskFactors.Add("Breast cancer diagnosis");
                
                if (clinicalData.BiomarkerStatus?.Contains("HER2+") == true)
                {
                    riskFactors.Add("HER2-positive subtype");
                }
                
                if (clinicalData.BiomarkerStatus?.Contains("ER-") == true)
                {
                    riskFactors.Add("Estrogen receptor negative");
                }
                
                if (clinicalData.BiomarkerStatus?.Contains("PR-") == true)
                {
                    riskFactors.Add("Progesterone receptor negative");
                }
            }
        }

        // Stage-based risk factors
        if (clinicalData.CancerSubtypeStage?.Contains("III", StringComparison.OrdinalIgnoreCase) == true ||
            clinicalData.CancerSubtypeStage?.Contains("IV", StringComparison.OrdinalIgnoreCase) == true)
        {
            riskFactors.Add("Advanced stage disease");
        }

        // Grade-based risk factors
        if (clinicalData.TumorGrade >= 3)
        {
            riskFactors.Add("High-grade tumor");
        }

        // Age-based risk factors
        if (clinicalData.DateOfBirth.HasValue)
        {
            var age = DateTime.Now.Year - clinicalData.DateOfBirth.Value.Year;
            if (age > 65)
            {
                riskFactors.Add("Advanced age");
            }
            else if (age < 40)
            {
                riskFactors.Add("Young age at diagnosis");
            }
        }

        // Prior treatment history
        if (!string.IsNullOrEmpty(clinicalData.PriorTreatments))
        {
            if (clinicalData.PriorTreatments.Contains("Chemotherapy", StringComparison.OrdinalIgnoreCase))
            {
                riskFactors.Add("Prior chemotherapy treatment");
            }
            if (clinicalData.PriorTreatments.Contains("Radiation", StringComparison.OrdinalIgnoreCase))
            {
                riskFactors.Add("Prior radiation treatment");
            }
        }

        return await Task.FromResult(riskFactors);
    }

    public async Task<string> GenerateClinicalSummaryAsync(ClinicalData clinicalData)
    {
        // Enhance this method to generate more comprehensive clinical summaries
        if (string.IsNullOrEmpty(clinicalData.FullName))
        {
            return "No clinical data available.";
        }

        var summary = new List<string>();

        // Basic patient information
        summary.Add($"Patient: {clinicalData.FullName}");
        
        if (clinicalData.DateOfBirth.HasValue)
        {
            var age = DateTime.Now.Year - clinicalData.DateOfBirth.Value.Year;
            summary.Add($"Age: {age} years");
        }

        // Cancer information
        if (!string.IsNullOrEmpty(clinicalData.CancerType))
        {
            summary.Add($"Diagnosis: {clinicalData.CancerType}");
        }

        if (!string.IsNullOrEmpty(clinicalData.CancerSubtypeStage))
        {
            summary.Add($"Stage/Subtype: {clinicalData.CancerSubtypeStage}");
        }

        // Biomarker status
        if (!string.IsNullOrEmpty(clinicalData.BiomarkerStatus))
        {
            summary.Add($"Biomarkers: {clinicalData.BiomarkerStatus}");
        }

        // Treatment history
        if (!string.IsNullOrEmpty(clinicalData.PriorTreatments))
        {
            summary.Add($"Prior Treatments: {clinicalData.PriorTreatments}");
        }

        // Report metadata
        summary.Add($"Report Generated: {clinicalData.ReportGenerationDate:yyyy-MM-dd}");
        
        if (!string.IsNullOrEmpty(clinicalData.ReportingPhysician))
        {
            summary.Add($"Physician: {clinicalData.ReportingPhysician}");
        }

        return await Task.FromResult(string.Join("; ", summary));
    }
} 