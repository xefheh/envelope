using CoursesService.Application.Requests.Course;
using CoursesService.Application.Responses.Courses.GetCourseResponse;
using CoursesService.Application.Responses.Courses.GetCoursesResponse;
using CoursesService.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoursesService.API.Controllers;

[ApiController]
[Route("/[controller]")]
public class CourseController: ControllerBase
{
    private readonly ICourseService _courseService;

    public CourseController(ICourseService courseService)
    {
        _courseService = courseService;
    }

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<CourseResponse>> GetCourseAsync(Guid id, CancellationToken cancellationToken)
    {
        var course = await _courseService.GetAsync(id, cancellationToken);
        if(course.IsSuccess) return Ok(course.Value);
        return NotFound(course.Exception!.Message);
    }

    [HttpGet]
    public async Task<ActionResult<CourseSearchResponse>> GetCoursesAsync(CancellationToken cancellationToken)
    {
        var courses = await _courseService.GetCoursesAsync(cancellationToken);
        return Ok(courses.Value);
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Guid>> AddCourseAsync(AddCourseRequest request, CancellationToken cancellationToken)
    {
        var id = await _courseService.AddAsync(request, cancellationToken);
        return Ok(id.Value);
    }

    [HttpDelete("{id:guid}")]
    [Authorize]
    public async Task<ActionResult<bool>> RemoveCourseAsync(Guid id, CancellationToken cancellationToken)
    {
        var isDeleted = await _courseService.RemoveAsync(id, cancellationToken);

        if (isDeleted.IsSuccess) return Ok();
        return NotFound(isDeleted.Exception!.Message);
    }

    [HttpPut]
    [Authorize]
    public async Task<ActionResult<bool>> UpdateCourseAsync(UpdateCourseRequest request, CancellationToken cancellationToken)
    {
        var isUpdated = await _courseService.UpdateAsync(request, cancellationToken);
        if (isUpdated.IsSuccess) return Ok(isUpdated.Value);
        return NotFound(isUpdated.Exception!.Message);
    }
}
