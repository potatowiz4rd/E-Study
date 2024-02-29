using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Study.ViewModel.Responses
{
    public class ResponseResult<T> where T : class
    {
        public ResponseResult() { }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public bool IsSuccessed { get; set; }
        public T Data { get; set; }
        public List<T> DataList { get; set; }
    }
}
