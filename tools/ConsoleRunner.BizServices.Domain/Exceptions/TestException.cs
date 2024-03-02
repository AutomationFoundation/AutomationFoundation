using System;
using System.Runtime.Serialization;

namespace ConsoleRunner.BizServices.Domain.Exceptions;

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