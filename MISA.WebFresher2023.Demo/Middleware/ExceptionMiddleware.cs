﻿using Microsoft.AspNetCore.Http;
using MISA.WebFresher2023.Demo.Common.MyException;
using System;
using System.Net;
using System.Text.Json;

namespace MISA.WebFresher2023.Demo.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        

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
        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            if(exception is NotFoundException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.NotFound;

                await context.Response.WriteAsync(
                    text: new BaseException()
                    {
                        ErrorCode = ((NotFoundException)exception).ErrorCode,
                        UserMessage = "Không tìm thấy tài nguyên",
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        MoreInfo = exception.HelpLink,
                    }.ToString()
                    );
            }else if (exception is InternalException)
            {
                context.Response.StatusCode= (int)HttpStatusCode.InternalServerError;
                await context.Response.WriteAsync(
                    text: new BaseException()
                    {
                        ErrorCode = ((NotFoundException)exception).ErrorCode,
                        UserMessage = "Lỗi hệ thống",
                        DevMessage = exception.Message,
                        TraceId = context.TraceIdentifier,
                        MoreInfo = exception.HelpLink,
                    }.ToString()
                    );
            }
        }
    }
}
