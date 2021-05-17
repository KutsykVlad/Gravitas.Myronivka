using System;

namespace Gravitas.Infrastructure.Common.Attribute
{
	[AttributeUsage(AttributeTargets.Class)]
	public class OpControllerNameAttribute : System.Attribute
	{
		public string ControllerName { get; }

		public OpControllerNameAttribute(string actionName)
		{
			ControllerName = actionName;
		}
	}
}
