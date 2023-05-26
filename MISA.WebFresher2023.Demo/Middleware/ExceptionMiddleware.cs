using Microsoft.AspNetCore.Http;
using MISA.WebFresher2023.Demo.Common.MyException;
using MISA.WebFresher2023.Demo.Common.Resource;
using System;
using System.Net;
using System.Text.Json;

namespace MISA.WebFresher2023.Demo.Middleware
{
    public class ExceptionMiddleware
    {
        #region Field
        /// <summary>
        /// Request tiếp theo
        /// </summary>
        /// Author: LeDucTiep (23/05/2023)
        private readonly RequestDelegate _next;
        #endregion

        #region Contructor
        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        #endregion

        #region Method
        /// <summary>
        /// Hàm gọi của middleware
        /// </summary>
        /// <param name="context">Nội dung</param>
        /// <returns>Task</returns>
        /// Author: LeDucTiep (23/05/2023)
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);

            }
            catch (Exception exception)
            {
                await HandleExceptionAsync(context, exception);
            }
        }

        /// <summary>
        /// Hàm xử lý lỗi 
        /// </summary>
        /// <param name="context">Nội dung</param>
        /// <param name="exception">Lỗi</param>
        /// <returns>Task</returns>
        /// Author: LeDucTiep (23/05/2023)
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            if (exception is NotFoundException exception1)
            {
                // Ghi mã lỗi
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                // Ghi nội dung lỗi 
                await context.Response.WriteAsync(
                    text: new BaseException()
                    {
                        ErrorCode = exception1.ErrorCode,
                        UserMessage = UserMessage.NotFoundError,
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        MoreInfo = exception.HelpLink,
                    }.ToString()
                    );
            }
            else if (exception is InternalException)
            {
                // Ghi mã lỗi
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                // Ghi nội dung lỗi
                await context.Response.WriteAsync(
                    text: new BaseException()
                    {
                        ErrorCode = ((NotFoundException)exception).ErrorCode,
                        UserMessage = UserMessage.InternalError,
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        MoreInfo = exception.HelpLink,
                    }.ToString()
                    );
            }
            else if (
                exception is PagingArgumentException)
            {
                // Ghi mã lỗi
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                // Ghi nội dung lỗi 
                await context.Response.WriteAsync(
                    text: new BaseException()
                    {
                        ErrorCode = ((NotFoundException)exception).ErrorCode,
                        UserMessage = UserMessage.PagingArgumentError,
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        MoreInfo = exception.HelpLink,
                    }.ToString()
                    );
            }
        }
        #endregion
    }
}
