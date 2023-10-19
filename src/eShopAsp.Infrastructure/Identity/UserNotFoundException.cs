namespace eShopAsp.Infrastructure.Identity;

public class UserNotFoundException : Exception 
{
    public UserNotFoundException(string username) 
        : base($"No use found with username: {username}") {}
}