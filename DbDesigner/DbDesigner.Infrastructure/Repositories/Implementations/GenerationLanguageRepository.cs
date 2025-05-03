using DbDesigner.Domain.Domain;

namespace DbDesigner.Infrastructure.Repositories.Implementations;

public class GenerationLanguageRepository(DbDesignerContext context)
    : Repository<GenerationLanguage>(context);