namespace Common.GenerateModels;

public class ResultGenerateModel
{
    public int ProjectId { get; set; }

    public string CreateScript { get; set; } = string.Empty;

    public IEnumerable<(string DomainName, string DomainContent)> Domains { get; set; } = [];
}