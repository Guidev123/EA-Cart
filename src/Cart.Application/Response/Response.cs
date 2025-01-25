﻿using System.Text.Json.Serialization;

namespace Cart.Application.Response
{
    public class Response<TData>(
        TData? data,
        int code = Response<TData>.DEFAULT_STATUS_CODE,
        string? message = null,
        string[]? errors = null)
    {
        public const int DEFAULT_STATUS_CODE = 200;

        public int Code { get; } = code;
        public TData? Data { get; set; } = data;
        public string? Message { get; } = message;
        public string[]? Errors { get; } = errors;
        public bool IsSuccess
            => Code is >= DEFAULT_STATUS_CODE and <= 299;
    }
}
