using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Quantum.Canon;
using SequestBioAI.Data;

namespace RiskCalculator.Services.Tumor;

public class TumorFeatureService
{
    private readonly IHttpClientFactory _httpClientFactory;

    public TumorFeatureService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    // Optional JSON loading (e.g., from wwwroot/data/features.json)
    public async Task<List<TumorFeature>> LoadFeaturesAsync()
    {
        var client = _httpClientFactory.CreateClient();
        var json = await client.GetStringAsync("data/features.json");
        return JsonSerializer.Deserialize<List<TumorFeature>>(json) ?? new List<TumorFeature>();
    }

    public List<TumorFeature> GetAllFeatures()
    {
        return new List<TumorFeature>
        {
            new TumorFeature
            {
                Name = "Cyclin B1",
                Category = "Cell Cycle/Mitosis",
                Pathway = "",
                Direction = "High ",
                ExpressionLevel = "Positive",
                Role =
                    "Regulates cell cycle progression, particularly G2/M transition. High expression often associated with aggressive tumors.",
                IsPositiveMarker = true
            },            new TumorFeature
            {
                Name = "Cyclin D1",
                Category = "Cell Cycle/Mitosis",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Positive",
                Role = "Promotes G1/S transition. Overexpression linked to poor prognosis.",
                IsPositiveMarker = true
            },            new TumorFeature
            {
                Name = "CDK1",
                Category = "Cell Cycle/Mitosis",
                Pathway = "",
                Direction = "High ",
                ExpressionLevel = "Positive",
                Role = "Key regulator of cell cycle, particularly mitosis.",
                IsPositiveMarker = true
            },            new TumorFeature
            {
                Name = "CDK2",
                Category = "Cell Cycle/Mitosis",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Positive",
                Role = "Involved in G1/S and S phase progression.",
                IsPositiveMarker = true
            },            new TumorFeature
            {
                Name = "Aurora Kinases",
                Category = "Cell Cycle/Mitosis",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Positive",
                Role =
                    "Regulate chromosome segregation and cytokinesis during mitosis. Overexpression contributes to genomic instability and tumor progression.",
                IsPositiveMarker = true
            },            new TumorFeature
            {
                Name = "HER2",
                Category = "Cell Cycle/Mitosis",
                Pathway = "",
                Direction = "High ",
                ExpressionLevel = "Positive",
                Role =
                    "HER2 acts as a networking receptor that mediates signaling to cancer cells, causing them to proliferate. Plays a key role in regulating cell growth and division",
                IsPositiveMarker = true
            },            new TumorFeature
            {
                Name = "Polo-like Kinases (PLK)",
                Category = "Cell Cycle/Mitosis",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Positive",
                Role = "Involved in various stages of mitosis, including centrosome maturation and spindle assembly.",
                IsPositiveMarker = true
            },            new TumorFeature
            {
                Name = "E-cadherin",
                Category = "Epithelial Structure",
                Pathway = "",
                Direction = "High ",
                ExpressionLevel = "Negative",
                Role = "Cell adhesion molecule. Loss of expression promotes EMT and invasion.",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "Keratins",
                Category = "Epithelial Structure",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Variable",
                Role =
                    "Intermediate filament proteins providing structural support to epithelial cells. Changes in keratin expression can be associated with EMT and tumor progression.",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "Laminins",
                Category = "Epithelial Structure",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Variable",
                Role =
                    "Components of the basement membrane. Alterations in laminin expression and organization can influence cell adhesion, migration, and invasion.",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "Collagen IV",
                Category = "Epithelial Structure",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Variable",
                Role =
                    "Major component of the basement membrane. Changes in collagen IV deposition and degradation are linked to tumor invasion and metastasis.",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "VEGF-A",
                Category = "Angiogenesis",
                Pathway = "",
                Direction = "High ",
                ExpressionLevel = "Positive",
                Role =
                    "Promotes blood vessel formation, increasing nutrient and oxygen supply to tumors, facilitating metastasis.",
                IsPositiveMarker = true
            },            new TumorFeature
            {
                Name = "VEGFRs",
                Category = "Angiogenesis",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Positive",
                Role = "Receptors for VEGF. Activation triggers signaling pathways that promote angiogenesis.",
                IsPositiveMarker = true
            },            new TumorFeature
            {
                Name = "Angiopoietins",
                Category = "Angiogenesis",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Positive",
                Role = "Regulate blood vessel maturation and stability.",
                IsPositiveMarker = true
            },            new TumorFeature
            {
                Name = "TWIST1",
                Category = "EMT (Epithelial-Mesenchymal Transition)",
                Pathway = "",
                Direction = "High ",
                ExpressionLevel = "Positive",
                Role = "Transcription factor that induces EMT, promoting cell motility and invasion.",
                IsPositiveMarker = true
            },            new TumorFeature
            {
                Name = "SNAIL",
                Category = "EMT (Epithelial-Mesenchymal Transition)",
                Pathway = "",
                Direction = "High ",
                ExpressionLevel = "Positive",
                Role = "Transcription factor that represses E-cadherin expression, promoting EMT.",
                IsPositiveMarker = true
            },            new TumorFeature
            {
                Name = "SLUG",
                Category = "EMT (Epithelial-Mesenchymal Transition)",
                Pathway = "",
                Direction = "High ",
                ExpressionLevel = "Positive",
                Role = "Transcription factor involved in EMT, contributing to cell migration and invasion.",
                IsPositiveMarker = true
            },            new TumorFeature
            {
                Name = "Vimentin",
                Category = "EMT (Epithelial-Mesenchymal Transition)",
                Pathway = "",
                Direction = "High ",
                ExpressionLevel = "Positive",
                Role =
                    "Intermediate filament protein expressed in mesenchymal cells. Upregulated during EMT, a marker of mesenchymal phenotype.",
                IsPositiveMarker = true
            },            new TumorFeature
            {
                Name = "N-cadherin",
                Category = "EMT (Epithelial-Mesenchymal Transition)",
                Pathway = "",
                Direction = "High ",
                ExpressionLevel = "Positive",
                Role = "Cell adhesion molecule expressed in mesenchymal cells. Promotes cell migration and invasion.",
                IsPositiveMarker = true
            },            new TumorFeature
            {
                Name = "PD-1",
                Category = "Immune Response",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Variable",
                Role =
                    "Immune checkpoint receptor expressed on T cells. Interaction with PD-L1 inhibits T cell activation. Can be targeted for immunotherapy.",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "PD-L1",
                Category = "Immune Response",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Variable",
                Role = "Ligand for PD-1, expressed on tumor cells and immune cells. Inhibits T cell activity.",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "CTLA-4",
                Category = "Immune Response",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Variable",
                Role = "Immune checkpoint receptor expressed on T cells. Inhibits T cell activation.",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "CD3",
                Category = "Immune Response",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Variable",
                Role = "T cell co-receptor involved in T cell activation.",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "CD4",
                Category = "Immune Response",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Variable",
                Role = "Marker of helper T cells.",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "CD8",
                Category = "Immune Response",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Variable",
                Role = "Marker of cytotoxic T cells.",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "IFN-γ",
                Category = "Immune Response",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Variable",
                Role =
                    "Cytokine produced by activated T cells and NK cells. Can have anti-tumor or pro-tumor effects depending on the context.",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "Granzyme B",
                Category = "Immune Response",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Variable",
                Role =
                    "Serine protease released by cytotoxic T cells and NK cells to induce apoptosis in target cells.",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "CXCL9",
                Category = "Immune Response",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Variable",
                Role = "Chemokine that attracts T cells to the tumor microenvironment.",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "CXCL10",
                Category = "Immune Response",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Variable",
                Role = "Chemokine that attracts T cells to the tumor microenvironment.",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "CXCR3",
                Category = "Immune Response",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Variable",
                Role = "Receptor for CXCL9 and CXCL10, expressed on T cells.",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "Matrix Metalloproteinases (MMPs)",
                Category = "Other TME Factors",
                Pathway = "",
                Direction = "High ",
                ExpressionLevel = "Positive",
                Role =
                    "Enzymes that degrade extracellular matrix components, facilitating invasion and metastasis. Examples include MMP2, MMP9, MMP14.",
                IsPositiveMarker = true
            },            new TumorFeature
            {
                Name = "FAP (Fibroblast Activation Protein)",
                Category = "Other TME Factors",
                Pathway = "",
                Direction = "High ",
                ExpressionLevel = "Positive",
                Role =
                    "Serine protease expressed by cancer-associated fibroblasts (CAFs). Promotes tumor growth, invasion, and angiogenesis.",
                IsPositiveMarker = true
            },            new TumorFeature
            {
                Name = "αSMA (alpha-Smooth Muscle Actin)",
                Category = "Other TME Factors",
                Pathway = "",
                Direction = "High ",
                ExpressionLevel = "Positive",
                Role = "Marker of activated fibroblasts, including CAFs.",
                IsPositiveMarker = true
            },            new TumorFeature
            {
                Name = "TGF-β (Transforming Growth Factor-beta)",
                Category = "Other TME Factors",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Variable",
                Role =
                    "Cytokine with complex roles in cancer. Can promote EMT, angiogenesis, and immune suppression, but can also have tumor-suppressive effects in early stages.",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "ABCB1 (MDR1, P-gp)",
                Category = "Drug Efflux Pumps",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Variable",
                Role =
                    "ATP-dependent efflux pump; transports various drugs (e.g., anthracyclines, taxanes) out of cells, reducing intracellular drug concentration. High expression strongly associated with multi-drug resistance and increased risk of relapse.",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "ABCC1 (MRP1)",
                Category = "Drug Efflux Pumps",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Variable",
                Role =
                    "ATP-dependent efflux pump; transports various drugs (e.g., anthracyclines, vinca alkaloids, methotrexate) and conjugated drugs out of cells. Overexpression linked to multi-drug resistance and poor prognosis.",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "ABCG2 (BCRP)",
                Category = "Drug Efflux Pumps",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Variable",
                Role =
                    "ATP-dependent efflux pump; transports a wide range of drugs (e.g., mitoxantrone, topotecan, some TKIs) out of cells. High expression associated with resistance, particularly in cancer stem-like cells.",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "BRCA1/2 (functional)",
                Category = "DNA Repair Pathways",
                Pathway = "",
                Direction = "High ",
                ExpressionLevel = "Variable",
                Role =
                    "Involved in homologous recombination (HR), a major DNA double-strand break repair pathway. Functional BRCA1/2 can lead to resistance to some therapies while loss-of-function mutations in these genes are associated with increased sensitivity to certain therapies.",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "ATM",
                Category = "DNA Repair Pathways",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Variable",
                Role =
                    "Key kinase in DNA damage response; activates cell cycle arrest and DNA repair. Alterations can lead to genomic instability but may also confer resistance to radiation and some chemotherapies. Role in RD is complex.",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "ATR",
                Category = "DNA Repair Pathways",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Variable",
                Role =
                    "Important kinase in DNA damage response, activated by replication stress and single-stranded DNA breaks. Inhibition sensitizes cells to therapy; high activity may contribute to RD.",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "CHK1/2",
                Category = "DNA Repair Pathways",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Variable",
                Role =
                    "Downstream effectors of ATM/ATR; mediate cell cycle arrest for DNA repair. Overexpression/hyperactivation may promote survival of damaged cells, contributing to resistance and RD.",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "ERCC1",
                Category = "DNA Repair Pathways",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Variable",
                Role =
                    "Involved in nucleotide excision repair (NER) pathway. High expression correlates with resistance to platinum-based chemotherapy.",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "MGMT",
                Category = "DNA Repair Pathways",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Variable",
                Role =
                    "Repairs alkylated guanine. High expression correlates with poorer response to alkylating agents.",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "CYP3A4",
                Category = "Altered Drug Metabolism",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Variable",
                Role =
                    "Metabolizes drugs into inactive forms. High expression associated with resistance to many types of chemotherapy.",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "BCL2",
                Category = "Apoptosis Evasion",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Variable",
                Role =
                    "Anti-apoptotic protein; inhibits programmed cell death. Overexpression associated with resistance to chemotherapy, particularly in hormone receptor-positive breast cancers.",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "TP53",
                Category = "Apoptosis Evasion",
                Pathway = "",
                Direction = "High ",
                ExpressionLevel = "Variable",
                Role =
                    "Tumor suppressor gene; regulates cell cycle arrest, DNA repair, and apoptosis. Mutations common in many cancers; associated with aggressive phenotype, resistance to therapy, and increased risk of RD.",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "ALDH1A1",
                Category = "Cancer Stem Cell Markers",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Variable",
                Role =
                    "Marker of cancer stem cells; linked to increased resistance to chemotherapy and higher risk of relapse, suggesting a role in RD.",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "CD44",
                Category = "Cancer Stem Cell Markers",
                Pathway = "",
                Direction = "High ",
                ExpressionLevel = "Variable",
                Role =
                    "Marker of cancer stem cells (especially CD44+/CD24-/low phenotype); associated with increased resistance to therapy, tumor recurrence, and metastasis, potentially contributing to RD.",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "CD133 (PROM1)",
                Category = "Cancer Stem Cell Markers",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Variable",
                Role =
                    "Marker for cancer stem cells; role in breast cancer still debated, but some studies link its expression to therapy resistance and poor prognosis, suggesting possible involvement in RD.",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "ESR1",
                Category = "Hormone Receptor Pathways",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Variable",
                Role =
                    "Encodes ERα; drives growth of ER-positive breast cancers. Mutations or alterations can lead to resistance to endocrine therapies, contributing to RD in ER-positive cases.",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "PGR",
                Category = "Hormone Receptor Pathways",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Variable",
                Role = "Encodes PR; alterations may contribute to endocrine therapy resistance and RD.",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "nan",
                Category = "Hormone Receptor Pathways",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Variable",
                Role = "nan",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "GATA1",
                Category = "Other Transcription Factors",
                Pathway = "",
                Direction = "High ",
                ExpressionLevel = "Postive",
                Role =
                    "These transcription factors can influence stromal remodeling and invasion, with altered expression often correlating with increased Met and potentially contributing to a microenvironment that supports RD",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "GATA2",
                Category = "Other Transcription Factors",
                Pathway = "",
                Direction = "High ",
                ExpressionLevel = "Postive",
                Role = "nan",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "GATA5",
                Category = "Other Transcription Factors",
                Pathway = "",
                Direction = "High ",
                ExpressionLevel = "Postive",
                Role = "nan",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "GATA6",
                Category = "Other Transcription Factors",
                Pathway = "",
                Direction = "High ",
                ExpressionLevel = "Postive",
                Role = "nan",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "MGA",
                Category = "Other Transcription Factors",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Variable",
                Role = "nan",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "SIX2",
                Category = "Other Transcription Factors",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Variable",
                Role = "nan",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "KRAS",
                Category = "Oncogenes/Signaling:",
                Pathway = "",
                Direction = "High ",
                ExpressionLevel = "Postive",
                Role =
                    "Activation of this oncogene drives signaling pathways that promote cell proliferation, survival, and migration, strongly associated with increased Met.",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "MYC",
                Category = "Oncogenes/Signaling:",
                Pathway = "",
                Direction = "High ",
                ExpressionLevel = "Postive",
                Role =
                    "Overexpression of this oncogene increases cell proliferation and metabolism, often linked to aggressive tumors with higher Met potential and increased risk of RD.",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "PIK3CA",
                Category = "Oncogenes/Signaling:",
                Pathway = "",
                Direction = "High ",
                ExpressionLevel = "Postive",
                Role =
                    "Activating mutations in this gene are common in cancer and can promote cell survival and invasion, contributing to Met and RD.",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "PCNA (Proliferating Cell Nuclear Antigen)",
                Category = "Cellular Processes:",
                Pathway = "",
                Direction = "High ",
                ExpressionLevel = "Positive",
                Role =
                    "High expression of this protein, involved in DNA replication, indicates increased proliferation and is often associated with aggressive tumors and higher risk of Met and RD.",
                IsPositiveMarker = true
            },            new TumorFeature
            {
                Name = "nan",
                Category = "Cellular Processes:",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Variable",
                Role = "nan",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "ABHD14A",
                Category = "Other Factors:",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Variable",
                Role =
                    "ABHD14A: While its role is less clear, altered expression may be involved in tumor progression and could indirectly influence Met.",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "ACS",
                Category = "Other Factors:",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Variable",
                Role =
                    "ACS: Involved in lipid metabolism, altered levels might contribute to the energy demands of metastatic cells.",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "AZGP1",
                Category = "Other Factors:",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Variable",
                Role =
                    "AZGP1: Reduced expression can be associated with increased tumor aggressiveness and Met in some cancers.",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "BEND3",
                Category = "Other Factors:",
                Pathway = "",
                Direction = "High ",
                ExpressionLevel = "Positive",
                Role =
                    "BEND3: Its precise role varies, but altered expression may contribute to changes in cell adhesion and invasion, affecting Met.",
                IsPositiveMarker = true
            },            new TumorFeature
            {
                Name = "CHAD",
                Category = "Other Factors:",
                Pathway = "",
                Direction = "High ",
                ExpressionLevel = "Positive",
                Role = "CHAD: May play a role in cell-ECM interactions, influencing invasion and Met.",
                IsPositiveMarker = true
            },            new TumorFeature
            {
                Name = "DHCR7",
                Category = "Other Factors:",
                Pathway = "",
                Direction = "High ",
                ExpressionLevel = "Positive",
                Role =
                    "DHCR7: Involved in cholesterol synthesis, which can be altered in cancer cells to support growth and spread.",
                IsPositiveMarker = true
            },            new TumorFeature
            {
                Name = "IL6ST",
                Category = "Other Factors:",
                Pathway = "",
                Direction = "High ",
                ExpressionLevel = "Positive",
                Role =
                    "IL6ST: This cytokine receptor mediates signaling that can promote inflammation and TME changes, indirectly affecting Met and RD.",
                IsPositiveMarker = true
            },            new TumorFeature
            {
                Name = "LRCH3",
                Category = "Other Factors:",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Variable",
                Role =
                    "LRCH3: Its function is not fully understood, but altered expression might be linked to cell migration.",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "MYL5",
                Category = "Other Factors:",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Variable",
                Role = "nan",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "NCOA3",
                Category = "Other Factors:",
                Pathway = "",
                Direction = "High ",
                ExpressionLevel = "Variable",
                Role =
                    "NCOA3: A nuclear receptor coactivator; overexpression can enhance hormone signaling, affecting tumor growth and potentially influencing recurrence.",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "OXTR",
                Category = "Other Factors:",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Variable",
                Role =
                    "OXTR: Oxytocin receptor; its role in cancer is complex, but it can influence cell signaling and behavior.",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "RSEM",
                Category = "Other Factors:",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Variable",
                Role =
                    "RSEM/STAR/TPM: These are not genes but terms related to RNA-seq data processing; they are not directly correlated with Met/RD but are important for analyzing gene expression.",
                IsPositiveMarker = false
            },            new TumorFeature
            {
                Name = "STAR",
                Category = "Other Factors:",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Variable",
                Role = "nan",
                IsPositiveMarker = false
            },
            new TumorFeature
            {
                Name = "nan",
                Category = "Other Factors:",
                Pathway = "",
                Direction = "Variable",
                ExpressionLevel = "Variable",
                Role = "nan",
                IsPositiveMarker = false
            },
            new TumorFeature
            {
                Name = "UACA",
                Category = "Other Factors:",
                Pathway = "",
                Direction = "High ",
                ExpressionLevel = "Variable",
                Role = "UACA: May play a role in cell adhesion and motility, influencing Met.",
                IsPositiveMarker = false
            },
        };
    }
}
