namespace NetworkSchedulesGraph;

public record ProformaSchedule(
    int ProformaId,
    string ScenarioName,
    string ScenarioAssumption,
    string ServiceCode,
    string RotationName,
    string ServiceName,
    string? ProformaStatus,
    int? PreviousProformaId)
{
    public int? ParentProformaId { get; } = PreviousProformaId;

    public int ChildProformaId { get; } = ProformaId;
}
