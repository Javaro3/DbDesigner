﻿using Common.Domain.BaseDomain;

namespace Common.Domain;

public class UserRole : BaseModel
{
    public int UserId { get; set; }
    
    public int RoleId { get; set; }
}