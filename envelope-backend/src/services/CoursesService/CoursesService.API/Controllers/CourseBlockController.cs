using CoursesService.Application.Requests.CoursBlock;
using CoursesService.Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoursesService.API.Controllers;

[ApiController]
[Route("/[controller]")]
public class CourseBlockController: ControllerBase
{
    private readonly ICourseBlockService _courseBlockService;

    public CourseBlockController(ICourseBlockService courseBlockService)
    {
        _courseBlockService = courseBlockService;
    }

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Guid>> AddCourseAsync(AddCourseBlockRequest request, CancellationToken cancellationToken)
    {
        var id = await _courseBlockService.AddAsync(request, cancellationToken);
        if(id.IsSuccess) return Ok(id.Value);
        return NotFound(id.Exception!.Message);
    }

    [HttpDelete("{id:guid}")]
    [Authorize]
    public async Task<ActionResult<bool>> RemoveCourseAsync(Guid id, CancellationToken cancellationToken)
    {
        var isDeleted = await _courseBlockService.RemoveAsync(id, cancellationToken);

        if (isDeleted.IsSuccess) return Ok();
        return NotFound(isDeleted.Exception!.Message);
    }

    [HttpPut]
    [Authorize]
    public async Task<ActionResult<bool>> UpdateCourseAsync(UpdateCourseBlockRequest request, CancellationToken cancellationToken)
    {
        var isUpdated = await _courseBlockService.UpdateAsync(request, cancellationToken);
        if (isUpdated.IsSuccess) return Ok(isUpdated.Value);
        return NotFound(isUpdated.Exception!.Message);
    }
}