using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.GenerationModel;
using DbDesigner.Application.Interfaces.DataServices;
using DbDesigner.Domain.Domain;
using Microsoft.AspNetCore.Authorization;

namespace DbDesigner.Web.Controllers;

[Authorize(Roles = "User")]
public class GenerationModelController(IBaseDataService<GenerationModel, GenerationModelDto, GenerationModelFilterDto, ComboboxDto> dataService)
    : BaseController<GenerationModel, GenerationModelDto, GenerationModelFilterDto, ComboboxDto>(dataService);