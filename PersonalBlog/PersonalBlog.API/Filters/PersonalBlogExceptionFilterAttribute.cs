﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace PersonalBlog.API.Filters;

public class PersonalBlogExceptionFilterAttribute : Attribute, IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        var ex = context.Exception;
        IActionResult actionResult = ex switch
        {
            KeyNotFoundException => new NotFoundObjectResult(new ErrorDTO
            {
                Message = ex.Message,
                StackTrace = ex.StackTrace,
                Source = ex.Source
            }),
            _ => new BadRequestObjectResult(new ErrorDTO()
            {
                Message = ex.Message,
                StackTrace = ex.StackTrace,
                Source = ex.Source
            })
            {
                StatusCode = 500
            }
        };

        context.ExceptionHandled = true;
        context.Result = actionResult;
    }
}
