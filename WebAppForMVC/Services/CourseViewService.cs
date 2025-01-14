using System;
using WebAppForMVC.Models.ViewModels;
using WebAppForMVC.Repository;

namespace WebAppForMVC.Services;

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
