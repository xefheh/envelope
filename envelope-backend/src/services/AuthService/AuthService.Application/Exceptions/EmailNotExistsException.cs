namespace AuthService.Application.Exceptions;

public class EmailNotExistsException(string message) : Exception(message) { }