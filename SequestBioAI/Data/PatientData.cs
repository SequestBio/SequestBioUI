namespace SequestBioAI.Data;

public class PatientData
{
    /// <summary>
    /// Clinical information for the patient (demographics, diagnosis, treatment history)
    /// </summary>
    public ClinicalData ClinicalInfo { get; set; } = new();

    public string PatientId { get; set; } = string.Empty;

    // Key clinical and risk markers
    public string TP53Status { get; set; } = string.Empty;
    public bool HasBoneMetastasis { get; set; }
    public bool TP53_Mut { get; set; }
    public bool MYC_Amp { get; set; }

    // Quantitative markers
    public double SII { get; set; }
    public double Ki67 { get; set; }
    public double TILs { get; set; }
    public double Cholesterol { get; set; }
    public double AdipoSig { get; set; }
    public double HIESig { get; set; }
    public double OsteoMetSig { get; set; }
    public double COL1A1 { get; set; }

    // Cancer staging
    public string Stage { get; set; } = string.Empty;
    public int Grade { get; set; }

    // Gene-specific expression values
    public double ANKIB1 { get; set; }
    public double TSPAN6 { get; set; }
    public double TNMD { get; set; }
    public double DPM1 { get; set; }
    public double SCYL3 { get; set; }
    public double C1ORF112 { get; set; }
    public double FGR { get; set; }
    public double CFH { get; set; }
    public double FUCA2 { get; set; }
    public double GCLC { get; set; }
    public double NFYA { get; set; }
    public double STPG1 { get; set; }
    public double NIPAL3 { get; set; }
    public double LAS1L { get; set; }
    public double ENPP4 { get; set; }
    public double SEMA3F { get; set; }
    public double CFTR { get; set; }
    public double AL353572_4 { get; set; }
    public double AC015818_10 { get; set; }
    public double AL389889_2 { get; set; }
    public double AC130307_1 { get; set; }
    public double AL109837_3 { get; set; }
    public double AC107918_5 { get; set; }
    public double AL031293_1 { get; set; }
    public double AC244636_3 { get; set; }
    public double AC091027_3 { get; set; }
    public double AL353151_2 { get; set; }
    public double AC099521_3 { get; set; }
    public double AL162718_3 { get; set; }
    public double AC108050_1 { get; set; }
    public double AC068279_2 { get; set; }
    public double AL136295_22 { get; set; }
    public double AC006130_3 { get; set; }
    public double AC233976_2 { get; set; }
    public double BX469938_1 { get; set; }
    public double AL353748_3 { get; set; }
    public double AC107385_2 { get; set; }
    public double AC007271_1 { get; set; }
    public double AL137786_3 { get; set; }
    public double Z80897_2 { get; set; }
    public double AC025614_2 { get; set; }
    public double AC096747_1 { get; set; }
    public double AL513487_1 { get; set; }
    public double AL161851_2 { get; set; }
    public double AC006230_1 { get; set; }
    public double AL162742_2 { get; set; }
    public double AC104850_2 { get; set; }
    public double AC018714_2 { get; set; }
    public double AL359632_1 { get; set; }
    public double AC020930_1 { get; set; }
    public double AC112482_2 { get; set; }
    public double AC103833_2 { get; set; }
    public double AC113615_2 { get; set; }
    public double AC092275_1 { get; set; }
    public double AC013701_2 { get; set; }
    public double AL158136_1 { get; set; }
    public double AC025756_1 { get; set; }
    public double AC008273_1 { get; set; }
    public double AC140118_2 { get; set; }
    public double AC068535_2 { get; set; }
    public double AC119150_2 { get; set; }
    public double AC026341_2 { get; set; }
    public double AC016925_3 { get; set; }
    public double AC093577_1 { get; set; }
    public double AC107918_6 { get; set; }
    public double AC005014_4 { get; set; }
    public double ABBA01045074_2 { get; set; }
    public double AP000477_3 { get; set; }
    public double AP003173_1 { get; set; }
    public double AC117569_2 { get; set; }
    public double Z98745_2 { get; set; }
    public double AC128687_3 { get; set; }
    public double AL135784_1 { get; set; }
    public double AL390242_1 { get; set; }
    public double AL355999_1 { get; set; }
    public double AC005632_6 { get; set; }
    public double AL034380_2 { get; set; }
    public double AL391840_3 { get; set; }
    public double AC105046_2 { get; set; }
    public double AC016705_3 { get; set; }
    public double AC011407_1 { get; set; }
    public double AC005081_1 { get; set; }
    public double AL133475_1 { get; set; }
    public double AL096701_4 { get; set; }

    // Generic tumor features (optional for ML or exploratory analysis)
    public List<TumorFeature> TumorFeatures { get; set; } = new();
}