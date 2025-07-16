namespace SequestBioAI.Data;

/// <summary>
/// Represents clinical information for a patient that will be displayed 
/// in the Patient Summary card and used throughout the application.
/// </summary>
public class ClinicalData
{
    /// <summary>
    /// Patient's full name (e.g., "Jane Doe")
    /// </summary>
    public string FullName { get; set; } = string.Empty;

    /// <summary>
    /// Medical Record Number or Patient ID (e.g., "12345")
    /// </summary>
    public string PatientId { get; set; } = string.Empty;

    /// <summary>
    /// Patient's date of birth
    /// </summary>
    public DateTime? DateOfBirth { get; set; }

    /// <summary>
    /// Date when this report was generated
    /// </summary>
    public DateTime ReportGenerationDate { get; set; } = DateTime.Now;

    // Diagnosis Summary Information
    /// <summary>
    /// Primary cancer type (e.g., "Breast Cancer", "Lung Cancer")
    /// </summary>
    public string CancerType { get; set; } = string.Empty;

    /// <summary>
    /// Specific cancer subtype and staging (e.g., "Breast Cancer Stage II", "Non-Small Cell Lung Cancer Stage IIIA")
    /// </summary>
    public string CancerSubtypeStage { get; set; } = string.Empty;

    /// <summary>
    /// Key biomarker status (e.g., "ER+, PR+, HER2-", "EGFR+, ALK-")
    /// </summary>
    public string BiomarkerStatus { get; set; } = string.Empty;

    /// <summary>
    /// Brief summary of prior treatments (e.g., "Lumpectomy, Chemotherapy (6 cycles of TAC), Radiation")
    /// </summary>
    public string PriorTreatments { get; set; } = string.Empty;

    // Additional clinical details that might be useful
    /// <summary>
    /// Tumor grade (1-3) if applicable
    /// </summary>
    public int? TumorGrade { get; set; }

    /// <summary>
    /// TNM staging if available (e.g., "T2N1M0")
    /// </summary>
    public string TNMStaging { get; set; } = string.Empty;

    /// <summary>
    /// Primary site of the tumor
    /// </summary>
    public string PrimarySite { get; set; } = string.Empty;

    /// <summary>
    /// Any additional clinical notes or comments
    /// </summary>
    public string ClinicalNotes { get; set; } = string.Empty;

    /// <summary>
    /// Physician or institution generating the report
    /// </summary>
    public string ReportingPhysician { get; set; } = string.Empty;

    /// <summary>
    /// Institution or laboratory name
    /// </summary>
    public string Institution { get; set; } = string.Empty;
} 