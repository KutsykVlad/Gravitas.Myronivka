using System;
using System.Configuration;
using System.Configuration.Install;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace Gravitas.Infrastucture.Service
{
	class BaseInstaller : Installer{

		public BaseInstaller() {
			Assembly assembly = Assembly.GetCallingAssembly();
			Configuration config = ConfigurationManager.OpenExeConfiguration(assembly.Location);
			
		}
	}
}
