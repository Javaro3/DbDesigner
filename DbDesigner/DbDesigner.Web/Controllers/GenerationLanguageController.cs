using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.GenerationLanguage;
using DbDesigner.Application.Interfaces.DataServices;
using DbDesigner.Domain.Domain;
using Microsoft.AspNetCore.Authorization;

namespace DbDesigner.Web.Controllers;

[Authorize(Roles = "User")]
public class GenerationLanguageController(IBaseDataService<GenerationLanguage, GenerationLanguageDto, GenerationLanguageFilterDto, ComboboxDto> dataService)
    : BaseController<GenerationLanguage, GenerationLanguageDto, GenerationLanguageFilterDto, ComboboxDto>(dataService);