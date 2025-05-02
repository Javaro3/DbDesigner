namespace DbDesigner.Application.Dtos;

public class TransportDto<T>
{
    public List<T> Data { get; set; } = [];

    public int TotalCount { get; set; }
}