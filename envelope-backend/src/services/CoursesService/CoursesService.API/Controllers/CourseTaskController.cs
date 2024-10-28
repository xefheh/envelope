using CoursesService.Application.Requests.CoursTasks;
using CoursesService.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoursesService.API.Controllers;

[ApiController]
[Route("/[controller]")]
public class CourseTaskController: ControllerBase
{
    private readonly ICourseTaskService _courseTaskService;

    public CourseTaskController(ICourseTaskService courseTaskService)
    {
        _courseTaskService = courseTaskService;
    }

    [HttpPost]
    public async Task<ActionResult<Guid>> AddCourseAsync(AddCourseTaskRequest request, CancellationToken cancellationToken)
    {
        var id = await _courseTaskService.AddAsync(request, cancellationToken);
        if(id.IsSuccess) return Ok(id.Value);
        return NotFound(id.Exception!.Message);
    }

    [HttpDelete("{id:guid}")]
    public async Task<ActionResult<bool>> RemoveCourseAsync(Guid id, CancellationToken cancellationToken)
    {
        var isDeleted = await _courseTaskService.RemoveAsync(id, cancellationToken);

        if (isDeleted.IsSuccess) return Ok();
        return NotFound(isDeleted.Exception!.Message);
    }
}