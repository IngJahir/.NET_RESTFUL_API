using System;
using System.Collections.Generic;
using System.Text;

namespace SocialMedia.CORE.Exceptions
{
    public class BusinessException:Exception
    {
        public BusinessException() { }
        public BusinessException(string menssage): base(menssage) { }
    }
}
