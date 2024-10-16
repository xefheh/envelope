namespace AuthService.Application.Exceptions;

public class UsernameNotExistsException(string message) : Exception(message) { }