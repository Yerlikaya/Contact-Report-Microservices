using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Shared.Dtos
{
    public class Response<T>
    {
        public T Data { get; set; }
        [JsonIgnore]
        public int StatusCode { get; private set; }
        [JsonIgnore]
        public bool IsSuccessful { get; private set; }
        public List<string> Errors { get; set; }
        public static Response<T> Success(T Data, int statusCode)
        {
            return new Response<T> { Data = Data, StatusCode = statusCode, IsSuccessful = true };
        }
        public static Response<T> Success(int statusCode)
        {
            return new Response<T> { Data = default(T), StatusCode = statusCode, IsSuccessful = true };
        }
        public static Response<T> Fail(List<string> errors, int statusCode)
        {

            return new Response<T> { Errors = errors, StatusCode = statusCode, IsSuccessful = false };
        }
        public static Response<T> Fail(string errors, int statusCode)
        {
            return new Response<T> { Errors = new List<string> { errors }, StatusCode = statusCode, IsSuccessful = false };
        }
    }
}
