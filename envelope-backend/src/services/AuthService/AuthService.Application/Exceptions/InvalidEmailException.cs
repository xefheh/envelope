namespace AuthService.Application.Exceptions;

public class InvalidEmailException(string message) : Exception(message){}