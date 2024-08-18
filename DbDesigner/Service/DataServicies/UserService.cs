using AutoMapper;
using Common.Domain;
using Common.Dtos.UserDtos;
using Microsoft.EntityFrameworkCore;
using Repository.Repositories.Interfaces;
using Service.Interfaces;
using Service.Interfaces.Infrastructure.Auth;

namespace Service.DataServicies;

public class UserService
{
    private readonly IRepository<User> _repository;
    private readonly IJwtProvider _jwtProvider;
    private readonly IPasswordHasher _hasher;
    private readonly IMapper _mapper;


    public UserService(
        IRepository<User> repository,
        IJwtProvider jwtProvider,
        IPasswordHasher hasher, 
        IMapper mapper)
    {
        _repository = repository;
        _jwtProvider = jwtProvider;
        _hasher = hasher;
        _mapper = mapper;
    }

    public async Task Register(UserRegisterDto dto)
    {
        var passwordHash = _hasher.Generate(dto.Password);
        var user = _mapper.Map<User>(dto);
        user.PasswordHash = passwordHash;
        await _repository.AddAsync(user);
    }
    
    public async Task<string> Login(UserLoginDto dto)
    {
        var user = await _repository.Query().FirstOrDefaultAsync(e => e.Email == dto.Email);

        if (user == null)
        {
            throw new Exception("This user does not exist");
        }

        var passwordIsCorrect = _hasher.Verify(dto.Password, user.PasswordHash);

        if (!passwordIsCorrect)
        {
            throw new Exception("Password is not correct");
        }

        var token = _jwtProvider.GenerateToken(user);

        return token;
    }
}