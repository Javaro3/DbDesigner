using DbDesigner.Domain.Domain;

namespace DbDesigner.Infrastructure.Repositories.Implementations;

public class LanguageRepository(DbDesignerContext context)
    : Repository<Language>(context);