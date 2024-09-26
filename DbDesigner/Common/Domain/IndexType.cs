﻿using Common.Domain.BaseDomain;

namespace Common.Domain;

public class IndexType : BaseModel, IHasId, IHasName, IHasDescription
{
    public int Id { get; set; }
    
    public string Name { get; set; } = string.Empty;
    
    public string? Description { get; set; }

    public ICollection<DataBase> DataBases { get; set; } = [];
}