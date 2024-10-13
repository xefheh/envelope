namespace AuthService.Application.Exceptions;

public class EmailExistsException(string message) : Exception(message) { }