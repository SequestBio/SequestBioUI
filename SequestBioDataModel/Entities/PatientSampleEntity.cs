namespace SequestBioDataModel.Entities;

public class PatientSampleEntity
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public bool Label { get; set; }
    public Dictionary<string, float> Features { get; set; }
    public DateTime CreatedAt { get; set; }
}