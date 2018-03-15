using System;
namespace AppFom.Models
{
    public class OperResult<T>
    {
        public OperResult() { }

        public int code { get; set; }

        public string message { get; set; }

        public T data { get; set; }

    }
}