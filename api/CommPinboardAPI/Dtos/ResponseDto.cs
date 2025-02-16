using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommPinboardAPI.Dtos
{
    public class ResponseDto
    {
        public bool IsSuccess {get;set;} = true;

        public string Message {get;set;}

        public object Data {get;set;}

        public ResponseDto(bool IsSuccess = true, string Message = null, object Data = null)
        {
            IsSuccess = IsSuccess;
            Message = Message;
            Data = Data;
        }
    }
}