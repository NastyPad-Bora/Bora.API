using AutoMapper;
using Bora.API.Security.Authorization.Handlers.Interfaces;
using Bora.API.Security.Domain.Model;
using Bora.API.Security.Domain.Repository;
using Bora.API.Security.Domain.Service;
using Bora.API.Security.Domain.Service.Communication;
using Bora.API.Security.Resources;
using Bora.API.Shared.Domain.Repository;

namespace Bora.API.Security.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IJwtHandler _jwtHandler;

    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IMapper mapper, IJwtHandler jwtHandler)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _jwtHandler = jwtHandler;
    }

    public async Task<IEnumerable<User>> ListAll()
    {
        return await _userRepository.ListAll();
    }

    public async Task<UserResponse> FindByIdAsync(Guid id)
    {
        var existingUser = await _userRepository.FindById(id);
        if (existingUser == null)
            return new UserResponse("Sorry, but a user with that id does not exist.");
        await _userRepository.SetRolesFromUser(id);
        return new UserResponse(existingUser);
    }

    public async Task<UserResponse> Register(RegisterRequest registerRequest)
    {
        var existingUsername = registerRequest.Username != null &&
                               await _userRepository.ExistByUsername(registerRequest.Username);
        var existingEmail = registerRequest.Email != null && await _userRepository.ExistByEmail(registerRequest.Email);
        if (existingUsername)
            return new UserResponse($"Username '{registerRequest.Username}' is already taken.");
        if (existingEmail)
            return new UserResponse($"Email '{registerRequest.Email} is already taken.");
        // Map the 'authRequest'
        var newUser = _mapper.Map<RegisterRequest, User>(registerRequest);
        // Hash Password
        newUser.HashedPassword = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password);
        try
        {
            await _userRepository.AddAsync(newUser);
            await _unitOfWork.CompleteAsync();
            return new UserResponse(newUser);
        }
        catch (Exception exception)
        {
            return new UserResponse(exception.Message);
        }
    }

    public async Task<AuthResponse> Auth(AuthRequest authRequest)
    {
        var existingUsernameOrEmail = await _userRepository.FindByUsernameOrEmail(authRequest.UsernameOrEmail);
        Console.WriteLine($"xdxxddsadasd");
        Console.WriteLine($"Id {existingUsernameOrEmail.Id!.Value}");
        if (existingUsernameOrEmail == null ||
            !BCrypt.Net.BCrypt.Verify(authRequest.Password, existingUsernameOrEmail.HashedPassword))
        {
            return new AuthResponse("Authentication failed.");
        }
        await _userRepository.SetRolesFromUser(existingUsernameOrEmail.Id!.Value);
        var authResource = _mapper.Map<User, AuthResource>(existingUsernameOrEmail);
        authResource.Token = _jwtHandler.GenerateToken(existingUsernameOrEmail);
        return new AuthResponse(authResource);
    }

    public Task<UserResponse> Update(User updatedUser)
    {
        throw new NotImplementedException();
    }
}