namespace AuthService.Persistence.Exceptions;

public class NotFoundConnectionStringException(string message) : Exception(message) { }