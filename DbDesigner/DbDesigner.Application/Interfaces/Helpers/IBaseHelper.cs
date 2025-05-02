namespace DbDesigner.Application.Interfaces.Helpers;

public interface IBaseHelper<T, TFilter>
{
    IQueryable<T> ApplySort(IQueryable<T> query, TFilter filter);
    
    IQueryable<T> ApplyFilter(IQueryable<T> query, TFilter filter);
}