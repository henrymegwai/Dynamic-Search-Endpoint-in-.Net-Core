using System;
using System.Collections.Generic;
using System.Text;

namespace TimeChimp.Core.Utlilities
{
    public class ExecutionResponse<T> where T : class
    {
        public bool Status { get; set; }
        public string Message { get; set; }
        public int PageNumber { get;  set; }
        public int ItemCount { get;  set; }
        public T Data { get; set; }
    }
}
