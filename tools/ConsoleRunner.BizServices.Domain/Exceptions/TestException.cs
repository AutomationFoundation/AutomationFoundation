using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleRunner.BizServices.Domain.Exceptions
{
    [Serializable]
    public class TestException : ApplicationException
    {
        public TestException()
            : base("This is a test exception!")
        {
        }

        protected TestException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}