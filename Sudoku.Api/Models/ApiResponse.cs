using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Net.Security;

namespace Sudoku.Api.Models
{
    public class ApiResponse<T>
    {
        public ApiResponse()
        {
            Errors = new List<ApiError>();
        }
        public ApiResponse(T data) : this()
        {
            Data = data;
        }
        public ApiResponse(string message, int code, string details = null) : this()
        {
            Errors.Add(new ApiError{Code = code, Details = details, Message = message});
        }
        public T Data { get; set; }
        public string Type { get; set; }
        public List<ApiError> Errors { get; }
        public bool IsSucces => Errors.Any();
    }

    public class ApiError
    {
        public string Message { get; set; }
        public int Code { get; set; }
        public string Details { get; set; }
    }
}