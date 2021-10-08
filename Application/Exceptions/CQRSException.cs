using System;

namespace Application.Exceptions
{
    public class CQRSException : Exception
    {
        public int StatusCode { get; set; }


        public CQRSException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}