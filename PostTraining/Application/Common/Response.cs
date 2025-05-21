using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PostTraining.Application.Common
{
    public class Response<T>
    {
        public Boolean Success { get; set; }
        public String Message { get; set; }
        public T Payload { get; set; }
    }
}