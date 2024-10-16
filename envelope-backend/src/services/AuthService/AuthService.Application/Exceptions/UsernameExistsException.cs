namespace AuthService.Application.Exceptions;

public class UsernameExistsException(string message) : Exception(message) { }