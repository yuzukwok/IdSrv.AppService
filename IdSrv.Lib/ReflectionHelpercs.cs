using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IdSrv.Lib
{
  
        /// <summary>
        /// This class is used to get all assemblies in bin folder of a web application.
        /// </summary>
        public class AssemblyFinder 
        {
            /// <summary>
            /// The search option used to find assemblies in bin folder. 
            /// </summary>
            public static SearchOption FindAssembliesSearchOption = SearchOption.TopDirectoryOnly; //TODO: Make this non static and rename to SearchOption

            private List<Assembly> _assemblies;

            private readonly object _syncLock = new object();

            /// <summary>
            /// This return all assemblies in bin folder of the web application.
            /// </summary>
            /// <returns>List of assemblies</returns>
            public List<Assembly> GetAllAssemblies()
            {
                if (_assemblies == null)
                {
                    lock (_syncLock)
                    {
                        if (_assemblies == null)
                        {
                            _assemblies = GetAllAssembliesInternal();
                        }
                    }
                }

                return _assemblies;
            }

            private List<Assembly> GetAllAssembliesInternal()
            {
                var assembliesInBinFolder = new List<Assembly>();
                
                var dllFiles = Directory.GetFiles(System.Environment.CurrentDirectory, "*.dll", FindAssembliesSearchOption).ToList();

                foreach (string dllFile in dllFiles)
                {
                    var locatedAssembly=Assembly.LoadFile(dllFile);
                    if (locatedAssembly != null)
                    { 

                        assembliesInBinFolder.Add(locatedAssembly);
                    }
                }

                return assembliesInBinFolder;
            }
        }
    
}
