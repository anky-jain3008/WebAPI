using System;

namespace WebAPI.Data.ViewModel
{
    public class CustomException : Exception
    {
        public CustomException(string message) : base(message)
        {
        }
    }
}