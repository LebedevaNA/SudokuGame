using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Sudoku.Api.Models;
using Sudoku.Application.Exceptions;

namespace Sudoku.Api.Middleware
{
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            if (context.Exception is AlreadyExistsException)
            {
                context.Result = new OkObjectResult(new ApiResponse<object>(context.Exception.Message, 500,
                    context.Exception.InnerException?.Message));
                context.ExceptionHandled = true;
            }

            if (context.Exception is ConcurentException)
            {
                context.Result = new OkObjectResult(new ApiResponse<object>(context.Exception.Message, 505,
                    context.Exception.InnerException?.Message));
                context.ExceptionHandled = true;
            }

            if (context.Exception is GameException)
            {
                context.Result = new OkObjectResult(new ApiResponse<object>(context.Exception.Message, 205,
                    context.Exception.InnerException?.Message));
                context.ExceptionHandled = true;
            }

            if (context.Exception is NotFoundException)
            {
                context.Result = new OkObjectResult(new ApiResponse<object>(context.Exception.Message, 500,
                    context.Exception.InnerException?.Message));
                context.ExceptionHandled = true;
            }
        }
    }
}