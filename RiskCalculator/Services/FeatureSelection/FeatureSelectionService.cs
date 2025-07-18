using System.Collections.Generic;
using SequestBioQuantum.FeatureSelection;

namespace RiskCalculator.Services.FeatureSelection
{
    public class FeatureSelectionService
    {
        private readonly QuantumFeatureSelector _selector = new();

        public List<string> SelectTopFeatures(List<string> allFeatures, int maxFeatures)
        {
            return _selector.SelectFeatures(allFeatures, maxFeatures);
        }
    }
}