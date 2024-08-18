﻿using Common.Domain;

namespace Service.Interfaces.Infrastructure.Auth;

public interface IJwtProvider
{
    public string GenerateToken(User user);
}