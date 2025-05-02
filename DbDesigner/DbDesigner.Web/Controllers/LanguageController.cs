using DbDesigner.Application.Dtos;
using DbDesigner.Application.Dtos.Language;
using DbDesigner.Application.Interfaces.DataServices;
using DbDesigner.Domain.Domain;
using Microsoft.AspNetCore.Authorization;

namespace DbDesigner.Web.Controllers;

[Authorize(Roles = "User")]
public class LanguageController(IBaseDataService<Language, LanguageDto, LanguageFilterDto, ComboboxDto> dataService)
    : BaseController<Language, LanguageDto, LanguageFilterDto, ComboboxDto>(dataService);