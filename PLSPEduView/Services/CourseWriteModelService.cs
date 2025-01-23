using System;
using PLSPEduView.Models.DataModels;
using PLSPEduView.Models.ViewModels;

namespace PLSPEduView.Services;

public class CourseWriteModelService
{
    public CourseWriteModel GetCourseWriteModel()
        => GenerateCourseWriteModel(null);
    public CourseWriteModel GetCourseWriteModel(CourseWriteModel model)
        => GenerateCourseWriteModel(model);
    public CourseWriteModel GetCourseWriteModel(Course course)
    {
        var newCourse = new CourseWriteModel()
        {
            Code = course.CourseCode,
            Description = course.CourseDescription,
            Units = course.Units
        };

        return GenerateCourseWriteModel(newCourse);
    }

    public Course GetCourse(CourseWriteModel model)
    {
        Course course = new()
        {
            CourseCode = model.Code,
            CourseDescription = model.Description,
            Units = model.Units
        };

        return course;

    }

    private CourseWriteModel GenerateCourseWriteModel(CourseWriteModel? model)
    {
        if (model == null)
            return new();

        return model;
    }
}
