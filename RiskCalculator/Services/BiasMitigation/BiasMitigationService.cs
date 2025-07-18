using System.Collections.Generic;
using SequestBioAI.BiasMitigation;

namespace RiskCalculator.Services.BiasMitigation
{
    public class BiasMitigationService
    {
        private readonly BiasMitigator _mitigator = new();

        public List<SampleDataWithDemographics> ApplyBiasMitigation(List<SampleDataWithDemographics> data)
        {
            return _mitigator.ApplyReweighting(data);
        }
    }
}