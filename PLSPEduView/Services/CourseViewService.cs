using System;
using System.Threading.Tasks;
using PLSPEduView.Models.ViewModels;
using PLSPEduView.Repository;

namespace PLSPEduView.Services;

public class CourseViewService
{
    private readonly CourseRepository _courseRepository;
    public CourseViewService(CourseRepository repository)
    {
        _courseRepository = repository;
    }

    public async Task<CourseViewModel> GetCourseViewModelAsync() 
        => await GenerateCourseViewModel(null);

    private async Task<CourseViewModel> GenerateCourseViewModel(CourseViewModel? currentModel)
    {
        var model = currentModel ?? new();

        model.Courses = await _courseRepository.GetAllAsync();

        return model;
    }
}
