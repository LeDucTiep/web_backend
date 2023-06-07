using MISA.WebFresher2023.Demo.Common.MyException;
using MISA.WebFresher2023.Demo.Common.Resource;
using System.Net;

namespace MISA.WebFresher2023.Demo.Middleware
{
    /// <summary>
    /// Class xử lý exception bằng middleware 
    /// </summary>
    /// Author: LeDucTiep (23/05/2023)
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
        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
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
            else if (exception is InternalException exception2)
            {
                // Ghi mã lỗi
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                // Ghi nội dung lỗi
                await context.Response.WriteAsync(
                    text: new BaseException()
                    {
                        ErrorCode = exception2.ErrorCode,
                        UserMessage = UserMessage.InternalError,
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        MoreInfo = exception.HelpLink,
                    }.ToString()
                    );
            }
            else if (
                exception is PagingArgumentException exception3)
            {
                // Ghi mã lỗi
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                // Ghi nội dung lỗi 
                await context.Response.WriteAsync(
                    text: new BaseException()
                    {
                        ErrorCode = exception3.ErrorCode,
                        UserMessage = UserMessage.PagingArgumentError,
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        MoreInfo = exception.HelpLink,
                    }.ToString()
                    );
            }
            else if (exception is ExsistedException exception4)
            {
                // Ghi mã lỗi
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                // Ghi nội dung lỗi 
                await context.Response.WriteAsync(
                    text: new BaseException()
                    {
                        ErrorCode = exception4.ErrorCode,
                        UserMessage = UserMessage.ExistedEmployeeCode,
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
