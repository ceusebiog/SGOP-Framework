using System;
using SGOP.Base;

namespace SGOP.Model.Response
{
  [Serializable]
  public class MessageResponse : BaseResponse
  {
    public string message;
  }
}