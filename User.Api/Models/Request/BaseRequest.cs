using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace User.Api.Models.Request
{
    public class BaseRequest<T> where T : class
    {
        public T Data { get; set; }
    }
}