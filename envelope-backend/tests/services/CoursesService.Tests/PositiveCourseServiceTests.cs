using CoursesService.Application.Requests.CoursBlock;
using CoursesService.Application.Requests.Course;
using CoursesService.Application.Requests.CoursTasks;
using CoursesService.Application.Services;
using CoursesService.Tests.Infrastructure;

namespace CoursesService.Tests;

public class PositiveCourseServiceTests
{
    [Fact]
    public async Task Valid_CourseService_AddCourse()
    {
        var request = new AddCourseRequest()
        {
            Name = "Основы программирования на примере C#",
            Description = " Курс разработан для студентов первого года обучения компьютерных специальностей УрФУ. " +
                          "Рассчитан на людей с минимальным опытом программирования и знакомит с основами " +
                          "синтаксиса C# и стандартными классами .NET, с основами ООП и базовыми алгоритмами."
        };

        var courseMockRepository = new CourseMockRepository();
        var courseService = new CourseService(courseMockRepository);
        var id = await courseService.AddAsync(request, CancellationToken.None);
        Assert.True(id.IsSuccess);
    }

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
        var courseService = new CourseService(courseMockRepository);
        var id = await courseService.AddAsync(request, CancellationToken.None);
        var isDeleted = await courseService.RemoveAsync(id.Value, CancellationToken.None);
        Assert.True(isDeleted.Value);
    }

    [Fact]
    public async Task Valid_CourseService_GetCourse()
    {
        var request = new AddCourseRequest()
        {
            Name = "Основы программирования на примере C#",
            Description = " Курс разработан для студентов первого года обучения компьютерных специальностей УрФУ. " +
                          "Рассчитан на людей с минимальным опытом программирования и знакомит с основами " +
                          "синтаксиса C# и стандартными классами .NET, с основами ООП и базовыми алгоритмами."
        };

        var courseMockRepository = new CourseMockRepository();
        var courseService = new CourseService(courseMockRepository);
        var id = await courseService.AddAsync(request, CancellationToken.None);
        var course = await courseService.GetAsync(id.Value, CancellationToken.None);
        Assert.True(course.IsSuccess);
    }

    [Fact]
    public async Task Valid_CourseService_GetCourses()
    {
        var requestPartOne = new AddCourseRequest()
        {
            Name = "Основы программирования на примере C#",
            Description = " Курс разработан для студентов первого года обучения компьютерных специальностей УрФУ. " +
                          "Рассчитан на людей с минимальным опытом программирования и знакомит с основами " +
                          "синтаксиса C# и стандартными классами .NET, с основами ООП и базовыми алгоритмами."
        };
        
        var requestPartTwo = new AddCourseRequest()
        {
            Name = "Основы программирования на примере C#. Часть 2",
            Description = " Вторая часть курса Основы программирования на C# знакомит с более сложными " +
                          "языковыми конструкциями: обобщёнными типами (generics), генераторами последовательностей, LINQ, " +
                          "а также с джентльменским набором алгоритмов и структур данных. " +
                          "Рассчитан на людей, знакомых с основами синтаксиса C#."
        };
        
        var courseMockRepository = new CourseMockRepository();
        var courseService = new CourseService(courseMockRepository);
        await courseService.AddAsync(requestPartOne, CancellationToken.None);
        await courseService.AddAsync(requestPartTwo, CancellationToken.None);
        var courses = await courseService.GetCoursesAsync(CancellationToken.None);
        Assert.True(courses.IsSuccess);
        Assert.True(courses.Value.Response.Count > 1);
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
        var courseService = new CourseService(courseMockRepository);
        var id = await courseService.AddAsync(requestPartOne, CancellationToken.None);
        
        var requestPartTwo = new UpdateCourseRequest()
        {
            Id = id.Value,
            Name = "Основы программирования на примере C#. Часть 2",
            Description = " Вторая часть курса Основы программирования на C# знакомит с более сложными " +
                          "языковыми конструкциями: обобщёнными типами (generics), генераторами последовательностей, LINQ, " +
                          "а также с джентльменским набором алгоритмов и структур данных. " +
                          "Рассчитан на людей, знакомых с основами синтаксиса C#."
        };

        var isCourseUpdated = await courseService.UpdateAsync(requestPartTwo, CancellationToken.None);
        Assert.True(isCourseUpdated.IsSuccess);
    }

    [Fact]
    public async Task Valid_CourseService_AddCourseBlock()
    {
        var requestCourse = new AddCourseRequest()
        {
            Name = "Основы программирования на примере C#",
            Description = " Курс разработан для студентов первого года обучения компьютерных специальностей УрФУ. " +
                          "Рассчитан на людей с минимальным опытом программирования и знакомит с основами " +
                          "синтаксиса C# и стандартными классами .NET, с основами ООП и базовыми алгоритмами."
        };

        var courseMockRepository = new CourseMockRepository();
        var courseService = new CourseService(courseMockRepository);
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
        var id = await courseBlockService.AddAsync(request, CancellationToken.None);
        Assert.True(id.IsSuccess);
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
        var courseService = new CourseService(courseMockRepository);
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
        var id = await courseBlockService.AddAsync(request, CancellationToken.None);
        var isCourseBlockDeleted = await courseBlockService.RemoveAsync(id.Value, CancellationToken.None);
        Assert.True(isCourseBlockDeleted.IsSuccess);
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
        var courseService = new CourseService(courseMockRepository);
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
        var id = await courseBlockService.AddAsync(request, CancellationToken.None);

        var updateRequest = new UpdateCourseBlockRequest()
        {
            Id = id.Value,
            Name = "Ошибки",
            Description = "Описание ошибок в языке C#"
        };

        var isCourseBlockUpdated = await courseBlockService.UpdateAsync(updateRequest, CancellationToken.None);
        Assert.True(isCourseBlockUpdated.Value);
    }

    [Fact]
    public async Task Valid_CourseService_AddCourseTask()
    {
        var requestCourse = new AddCourseRequest()
        {
            Name = "Основы программирования на примере C#",
            Description = " Курс разработан для студентов первого года обучения компьютерных специальностей УрФУ. " +
                          "Рассчитан на людей с минимальным опытом программирования и знакомит с основами " +
                          "синтаксиса C# и стандартными классами .NET, с основами ООП и базовыми алгоритмами."
        };

        var courseMockRepository = new CourseMockRepository();
        var courseService = new CourseService(courseMockRepository);
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
        var taskId = await courseTaskService.AddAsync(requestTask, CancellationToken.None);
        Assert.True(taskId.IsSuccess);
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
        var courseService = new CourseService(courseMockRepository);
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
        var taskId = await courseTaskService.AddAsync(requestTask, CancellationToken.None);
        var isCourseTaskDeleted = await courseTaskService.RemoveAsync(taskId.Value, CancellationToken.None);
        Assert.True(isCourseTaskDeleted.IsSuccess);
    }
}