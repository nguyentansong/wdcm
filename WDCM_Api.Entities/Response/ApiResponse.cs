using System;
using System.Collections.Generic;
using System.Text;

namespace WDCM_Api.Entities.Response
{
    public class ApiResponse
    {
        public bool IsSuccess { get; set; }
        public object Content { get; set; }
        public string Message { get; set; }
        public int? StatusCode { get; set; }

        public ApiResponse()
        {

        }
        public static ApiResponse SuccessResponse(object content = null)
        {
            return new ApiResponse(true, content, null, 1);
        }

        public ApiResponse(bool isSuccess, object content = null, string message = null, int? statusCode = null)
        {
            IsSuccess = isSuccess;
            Content = content;
            Message = message;
            StatusCode = statusCode;
        }
    }
}
