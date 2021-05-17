using System;

namespace Gravitas.Infrastructure.Common.Attribute
{
    [AttributeUsage(AttributeTargets.Field)]
    public class OpActionNameAttribute : System.Attribute
    {
        public OpActionNameAttribute(string actionName)
        {
            ActionName = actionName;
        }
        public string ActionName { get; }
    }
}