using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.DataBase;
using DbDesigner.Application.Interfaces.DataServices;
using DbDesigner.Domain.Domain;
using Microsoft.AspNetCore.Authorization;

namespace DbDesigner.Web.Controllers;


[Authorize(Roles = "User")]
public class DataBaseController(IBaseDataService<DataBase, DataBaseDto, DataBaseFilterDto, ComboboxDto> dataService)
    : BaseController<DataBase, DataBaseDto, DataBaseFilterDto, ComboboxDto>(dataService);