using System;
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

    public CourseViewModel GetCourseViewModel() 
        => GenerateCourseViewModel(null);

    private CourseViewModel GenerateCourseViewModel(CourseViewModel? currentModel)
    {
        var model = currentModel ?? new();

        model.Courses = _courseRepository.GetAll();

        return model;
    }
}
