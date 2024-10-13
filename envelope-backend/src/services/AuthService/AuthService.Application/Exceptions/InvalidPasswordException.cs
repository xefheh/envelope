namespace AuthService.Application.Exceptions;

public class InvalidPasswordException(string message) : Exception(message) { }