using SequestBioAI.BiasMitigation;
using SequestBioAI.DataProcessing;
using SequestBioAI.ModelTraining;

namespace SequestBioAI.Utilities;

public static class SampleDataMapper
{
    public static SampleData MapFromRawSample(RawSample rawSample)
    {
        return new SampleData
        {
            Label = rawSample.Label,
            Feature1 = rawSample.Features.ContainsKey("Feature1") ? rawSample.Features["Feature1"] : 0f,
            Feature2 = rawSample.Features.ContainsKey("Feature2") ? rawSample.Features["Feature2"] : 0f
        };
    }

    public static SampleData MapFromDemographicSample(SampleDataWithDemographics demographicSample)
    {
        return new SampleData
        {
            Label = demographicSample.Label,
            Feature1 = demographicSample.Features.ContainsKey("Feature1") ? demographicSample.Features["Feature1"] : 0f,
            Feature2 = demographicSample.Features.ContainsKey("Feature2") ? demographicSample.Features["Feature2"] : 0f
        };
    }
}