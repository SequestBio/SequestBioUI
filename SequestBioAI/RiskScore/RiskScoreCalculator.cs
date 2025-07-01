using System.Globalization;
using System.Text;
using SequestBioAI.Data;

namespace SequestBioAI.RiskScore;

public static class RiskScoreCalculator
{
    /* ------------------------------------------------------------------
       CONFIGURABLE WEIGHTS  – genes with published association to
       breast‑cancer aggressiveness / metastasis.  Positive value ⇒
       raises risk; negative value ⇒ protective.  Magnitudes tuned to
       keep raw scores in ~0‑0.1 range so 0‑100 normalisation is stable.
       ------------------------------------------------------------------*/

    private static readonly Dictionary<string, double> GeneWeights = new(StringComparer.OrdinalIgnoreCase)
    {
        // Matrix‑remodelling & invasion
        {"MMP9",    0.0030},
        {"MMP2",    0.0020},
        {"MMP14",   0.0020},
        {"MMP11",   0.0018},
        {"LOX",     0.0025},

        // EMT transcription factors & markers
        {"TWIST1",  0.0025},
        {"SNAI1",   0.0020},
        {"SNAI2",   0.0020}, // SLUG
        {"VIM",     0.0020}, // Vimentin
        {"CDH2",    0.0020}, // N‑cadherin
        {"FSCN1",   0.0020},

        // Oncogenic signalling / proliferation
        {"MYC",     0.0030},
        {"TP53",    0.0040},
        {"ERBB2",   0.0030},
        {"PIK3CA",  0.0025},
        {"CCND1",   0.0020},
        {"AURKA",   0.0020},
        {"KRAS",    0.0025},

        // Angiogenesis & hypoxia response
        {"VEGFA",   0.0020},
        {"ANGPTL4", 0.0020},
        {"POSTN",   0.0020},
        {"CXCL12",  0.0018},
        {"CXCR4",   0.0020},

        // Immune‑evasion / checkpoints
        {"PDL1",    0.0015},

        // Drug resistance / stemness
        {"ABCB1",   0.0020},
        {"ALDH1A1", 0.0018},
        {"BIRC5",   0.0018}, // Survivin

        // Extracellular matrix rigidity – collagen I high expression
        {"COL1A1",  0.0020},

        // Protective / favourable‑prognosis genes
        {"CXCL9",  -0.0010},
        {"CXCL10", -0.0010},
        {"CD8",    -0.0015},
        {"ESR1",   -0.0015},
        {"PGR",    -0.0010},
        {"GATA3",  -0.0010},
        {"FOXA1",  -0.0010},
        {"CDH1",   -0.0020}, // E‑cadherin
        {"ABCA3",  -0.0010},
        {"CCL22",  -0.0010},
        {"FOXJ1",  -0.0010}
    };

    /* ------------------------------------------------------------------
       Clinical / functional factor weights
       ------------------------------------------------------------------*/
    private const double W_SII_High  = 0.0020;  // SequestOne Invasion Index >0.8
    private const double W_KI67_High = 0.0025;  // Ki‑67 >60 %
    private const double W_TP53_Mut  = 0.0030;  // TP53 mutation (if not captured by expression)

    private const double ExpressionPosThreshold = 1.0; // TPM cut‑off

    /* ------------------------------------------------------------------
       PUBLIC ENTRY POINT
       ------------------------------------------------------------------*/
    public static async Task<int> CalculateRiskCategory(Stream tsvFileStream)
    {
        using var reader = new StreamReader(tsvFileStream, Encoding.UTF8, leaveOpen: true);
        var patient = await ParsePatientDataFromTsv(reader);
        return CalculateRiskCategory(patient);
    }

    /* ------------------------------------------------------------------
       CORE CALCULATION
       ------------------------------------------------------------------*/
    private static int CalculateRiskCategory(PatientData data)
    {
        double risk   = 0.0;
        double denom  = 0.0;

        // Gene‑level loops
        foreach (var f in data.TumorFeatures)
        {
            if (!f.IsPositiveMarker) continue;
            double w = GeneWeights.TryGetValue(f.Name, out var wt) ? wt : 0.0015; // default small risk
            risk  += w;
            denom += Math.Abs(w);
        }

        // Clinical contributions
        if (data.SII > 0.8)                      { risk += W_SII_High;  denom += W_SII_High;  }
        if (data.Ki67 > 60)                      { risk += W_KI67_High; denom += W_KI67_High; }
        if (string.Equals(data.TP53Status, "mut", StringComparison.OrdinalIgnoreCase))
                                                 { risk += W_TP53_Mut;  denom += W_TP53_Mut;  }

        if (denom == 0) return 0;

        var normalised = (risk / denom) * 100.0;
        normalised = Math.Clamp(normalised, 0, 100);
        return (int)Math.Round(normalised);
    }

    /* ------------------------------------------------------------------
       TSV PARSER – expects 2nd column = gene symbol, 7th column = TPM
       ------------------------------------------------------------------*/
    private static async Task<PatientData> ParsePatientDataFromTsv(StreamReader reader)
    {
        var p = new PatientData { TumorFeatures = new List<TumorFeature>() };
        bool headerSeen = false;
        string? line;
        while ((line = await reader.ReadLineAsync()) != null)
        {
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith('#')) continue;
            if (!headerSeen) { headerSeen = true; continue; }
            var c = line.Split('\t');
            if (c.Length < 7) continue;
            if (!double.TryParse(c[6], NumberStyles.Any, CultureInfo.InvariantCulture, out var tpm)) continue;
            p.TumorFeatures.Add(new TumorFeature
            {
                Name             = c[1].Trim(),
                ExpressionLevel  = tpm >= ExpressionPosThreshold ? "Positive" : "Negative",
                IsPositiveMarker = tpm >= ExpressionPosThreshold
            });
        }
        return p;
    }
}
