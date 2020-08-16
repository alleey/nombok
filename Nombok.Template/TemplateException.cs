using System;
using System.Runtime.Serialization;
using Nombok.Shared;

namespace Nombok.Template
{
  public class TemplateException : NombokException
  {
    public TemplateException()
    {
    }

    public TemplateException(string message) : base(message)
    {
    }

    public TemplateException(string message, Exception innerException) : base(message, innerException)
    {
    }
  }
}
