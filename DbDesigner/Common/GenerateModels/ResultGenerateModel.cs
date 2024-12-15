namespace Common.GenerateModels;

public class ResultGenerateModel
{
    public int ProjectId { get; set; }

    public string CreateScript { get; set; } = string.Empty;
    
    public string? DataScript { get; set; }

    public IEnumerable<(string DomainName, string DomainContent)> Domains { get; set; } = [];

    public (string OrmName, string OrmContect) Orm { get; set; } = ("", "");
    
    public ArchitectureGenerateModel? Architecture { get; set; }
}