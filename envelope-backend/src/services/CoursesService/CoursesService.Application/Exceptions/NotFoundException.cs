using System.Reflection;

namespace CoursesService.Application.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(MemberInfo type, object key) : base($"{type.Name} with key {key} not found!") { }
}