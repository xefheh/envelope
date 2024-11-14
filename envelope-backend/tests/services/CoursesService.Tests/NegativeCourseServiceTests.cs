using Envelope.Common.Exceptions;
using CoursesService.Application.Requests.CoursBlock;
using CoursesService.Application.Requests.Course;
using CoursesService.Application.Requests.CoursTasks;
using CoursesService.Application.Services;
using CoursesService.Tests.Infrastructure;

namespace CoursesService.Tests;

public class NegativeCourseServiceTests
{
    private readonly MockMessageBus _mockMessageBus = new MockMessageBus();
    
    [Fact]
    public async Task Valid_CourseService_RemoveCourse()
    {
        var request = new AddCourseRequest()
        {
            Name = "Основы программирования на примере C#",
            Description = " Курс разработан для студентов первого года обучения компьютерных специальностей УрФУ. " +
                          "Рассчитан на людей с минимальным опытом программирования и знакомит с основами " +
                          "синтаксиса C# и стандартными классами .NET, с основами ООП и базовыми алгоритмами."
        };

        var courseMockRepository = new CourseMockRepository();
        var courseService = new CourseService(courseMockRepository, _mockMessageBus);
        var id = new Guid();
        await courseService.AddAsync(request, CancellationToken.None);
        var isCourseDeleted = await courseService.RemoveAsync(id, CancellationToken.None);
        Assert.False(isCourseDeleted.IsSuccess);
        Assert.IsType<NotFoundException>(isCourseDeleted.Exception);
    }

    [Fact]
    public async Task Valid_CourseService_UpdateCourse()
    {
        var requestPartOne = new AddCourseRequest()
        {
            Name = "Основы программирования на примере C#",
            Description = " Курс разработан для студентов первого года обучения компьютерных специальностей УрФУ. " +
                          "Рассчитан на людей с минимальным опытом программирования и знакомит с основами " +
                          "синтаксиса C# и стандартными классами .NET, с основами ООП и базовыми алгоритмами."
        };
                
        var courseMockRepository = new CourseMockRepository();
        var courseService = new CourseService(courseMockRepository, _mockMessageBus);
        await courseService.AddAsync(requestPartOne, CancellationToken.None);

        var id = new Guid();
                
        var requestPartTwo = new UpdateCourseRequest()
        {
            Id = id,
            Name = "Основы программирования на примере C#. Часть 2",
            Description = " Вторая часть курса Основы программирования на C# знакомит с более сложными " +
                              "языковыми конструкциями: обобщёнными типами (generics), генераторами последовательностей, LINQ, " +
                              "а также с джентльменским набором алгоритмов и структур данных. " +
                              "Рассчитан на людей, знакомых с основами синтаксиса C#."
        };
        
       var isCourseUpdated = await courseService.UpdateAsync(requestPartTwo, CancellationToken.None);
       Assert.False(isCourseUpdated.IsSuccess);
       Assert.IsType<NotFoundException>(isCourseUpdated.Exception);
    }
    
    [Fact]
    public async Task Valid_CourseService_RemoveCourseBlock()
    {
        var requestCourse = new AddCourseRequest()
        {
            Name = "Основы программирования на примере C#",
            Description = " Курс разработан для студентов первого года обучения компьютерных специальностей УрФУ. " +
                          "Рассчитан на людей с минимальным опытом программирования и знакомит с основами " +
                          "синтаксиса C# и стандартными классами .NET, с основами ООП и базовыми алгоритмами."
        };

        var courseMockRepository = new CourseMockRepository();
        var courseService = new CourseService(courseMockRepository, _mockMessageBus);
        var idCourse = await courseService.AddAsync(requestCourse, CancellationToken.None);
        var request = new AddCourseBlockRequest()
        {
            CourseId = idCourse.Value,
            NameOfBlock = "Первое знакомство с C#",
            Description = "Часто после видео будут одна или несколько задач или тестов на закрепление материала. " +
                          "Решать эти задачи можно прямо в браузере, а специальная проверяющая система тут же проверит ваше решение. " +
                          "Лекционный материал лучше усваивается после применения на практике, поэтому не пренебрегайте решением задач."
        };
        var courseBlockService = new CourseBlockService(courseMockRepository);
        var id = new Guid();
        var isCourseBlockDeleted = await courseBlockService.RemoveAsync(id, CancellationToken.None);
        Assert.False(isCourseBlockDeleted.IsSuccess);
        Assert.IsType<NotFoundException>(isCourseBlockDeleted.Exception);
    }
    
    [Fact]
    public async Task Valid_CourseService_UpdateCourseBlock()
    {
        var requestCourse = new AddCourseRequest()
        {
            Name = "Основы программирования на примере C#",
            Description = " Курс разработан для студентов первого года обучения компьютерных специальностей УрФУ. " +
                          "Рассчитан на людей с минимальным опытом программирования и знакомит с основами " +
                          "синтаксиса C# и стандартными классами .NET, с основами ООП и базовыми алгоритмами."
        };

        var courseMockRepository = new CourseMockRepository();
        var courseService = new CourseService(courseMockRepository, _mockMessageBus);
        var idCourse = await courseService.AddAsync(requestCourse, CancellationToken.None);
        
        var request = new AddCourseBlockRequest()
        {
            CourseId = idCourse.Value,
            NameOfBlock = "Первое знакомство с C#",
            Description = "Часто после видео будут одна или несколько задач или тестов на закрепление материала. " +
                          "Решать эти задачи можно прямо в браузере, а специальная проверяющая система тут же проверит ваше решение. " +
                          "Лекционный материал лучше усваивается после применения на практике, поэтому не пренебрегайте решением задач."
        };
        
        var courseBlockService = new CourseBlockService(courseMockRepository);
        await courseBlockService.AddAsync(request, CancellationToken.None);
        var id = new Guid();
        
        var updateRequest = new UpdateCourseBlockRequest()
        {
            Id = id,
            Name = "Ошибки",
            Description = "Описание ошибок в языке C#"
        };

        var isCourseBlockUpdated = await courseBlockService.UpdateAsync(updateRequest, CancellationToken.None);
        Assert.False(isCourseBlockUpdated.IsSuccess);
        Assert.IsType<NotFoundException>(isCourseBlockUpdated.Exception);
    }
    
     [Fact]
    public async Task Valid_CourseService_RemoveTask()
    {
        var requestCourse = new AddCourseRequest()
        {
            Name = "Основы программирования на примере C#",
            Description = " Курс разработан для студентов первого года обучения компьютерных специальностей УрФУ. " +
                          "Рассчитан на людей с минимальным опытом программирования и знакомит с основами " +
                          "синтаксиса C# и стандартными классами .NET, с основами ООП и базовыми алгоритмами."
        };

        var courseMockRepository = new CourseMockRepository();
        var courseService = new CourseService(courseMockRepository, _mockMessageBus);
        var idCourse = await courseService.AddAsync(requestCourse, CancellationToken.None);
        
        var requestBlock = new AddCourseBlockRequest()
        {
            CourseId = idCourse.Value,
            NameOfBlock = "Первое знакомство с C#",
            Description = "Часто после видео будут одна или несколько задач или тестов на закрепление материала. " +
                          "Решать эти задачи можно прямо в браузере, а специальная проверяющая система тут же проверит ваше решение. " +
                          "Лекционный материал лучше усваивается после применения на практике, поэтому не пренебрегайте решением задач."
        };
        
        var courseBlockService = new CourseBlockService(courseMockRepository);
        var coursId = await courseBlockService.AddAsync(requestBlock, CancellationToken.None);

        var requestTask = new AddCourseTaskRequest()
        {
            BlockId = coursId.Value,
            Task = new Guid()
        };
        
        var courseTaskService = new CourseTaskService(courseMockRepository);
        await courseTaskService.AddAsync(requestTask, CancellationToken.None);
        var taskId = new Guid();
        var isCourseTaskDeleted = await courseTaskService.RemoveAsync(taskId, CancellationToken.None);
        Assert.False(isCourseTaskDeleted.IsSuccess);
        Assert.IsType<NotFoundException>(isCourseTaskDeleted.Exception);
    }
}