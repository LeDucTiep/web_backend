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
                context.Response.StatusCode = exception1.StatusCode;
                exception1.TraceId = context.TraceIdentifier;

                // Ghi nội dung lỗi 
                await context.Response.WriteAsync(
                    text: exception1.ToString()
                    );
            }
            else if (exception is InternalException exception2)
            {
                // Ghi mã lỗi
                context.Response.StatusCode = exception2.StatusCode;
                exception2.TraceId = context.TraceIdentifier;

                // Ghi nội dung lỗi
                await context.Response.WriteAsync(
                    text: exception2.ToString()
                    );
            }

            else if (exception is BadRequestException exception3)
            {
                // Ghi mã lỗi
                context.Response.StatusCode = exception3.StatusCode;
                exception3.TraceId = context.TraceIdentifier;

                // Ghi nội dung lỗi 
                await context.Response.WriteAsync(
                    text: exception3.ToString()
                    );
            }
            else
            {
                await Console.Out.WriteLineAsync(exception.Message);
            }
        }
        #endregion
    }
}
