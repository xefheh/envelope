using CoursesService.Application.Requests.CoursBlock;
using CoursesService.Application.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CoursesService.API.Controllers;

public class CourseBlockController: ControllerBase
{
    private readonly ICourseBlockService _courseBlockService;

    public CourseBlockController(ICourseBlockService courseBlockService)
    {
        _courseBlockService = courseBlockService;
    }

    public async Task<ActionResult<Guid>> AddCourseAsync(AddCourseBlockRequest request, CancellationToken cancellationToken)
    {
        var id = await _courseBlockService.AddAsync(request, cancellationToken);
        return Ok(id.Value);
    }

    public async Task<ActionResult<bool>> RemoveCourseAsync(Guid id, CancellationToken cancellationToken)
    {
        var isDeleted = await _courseBlockService.RemoveAsync(id, cancellationToken);

        if (isDeleted.IsSuccess) return Ok();
        return NotFound(isDeleted.Exception!.Message);
    }

    public async Task<ActionResult<bool>> UpdateCourseAsync(UpdateCourseBlockRequest request, CancellationToken cancellationToken)
    {
        var isUpdated = await _courseBlockService.UpdateAsync(request, cancellationToken);
        if (isUpdated.IsSuccess) return Ok(isUpdated.Value);
        return NotFound(isUpdated.Exception!.Message);
    }
}