namespace SequestBio.ScoreComponent.Patient.Data;

public class PatientData
{
    public string PatientId { get; set; } = string.Empty;
    public string TP53Status { get; set; } = string.Empty;
    public bool HasBoneMetastasis { get; set; }
    public double SII { get; set; }
    public double COL1A1 { get; set; }
    public double MMP9 { get; set; }
    public double HIESig { get; set; }
    public double OsteoMetSig { get; set; }
    public bool MYC_Amp { get; set; }
    public bool TP53_Mut { get; set; }
    public double Cholesterol { get; set; }
    public double AdipoSig { get; set; }
    public double TILs { get; set; }
    public string Stage { get; set; }
    public int Grade { get; set; }
    public double Ki67 { get; set; }
    public List<TumorFeature> TumorFeatures { get; set; } = new();

}
