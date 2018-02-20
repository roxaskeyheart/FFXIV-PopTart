using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace FFXIV_PopTart
{
    internal class Locator
    {
        static Locator()
        {
            // Set up IoC container
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            // Register our main applications entry point (Chromatics)
            SimpleIoc.Default.Register<PopTart>();

            // Register an interface used for writing to the Console/Log/Application
            SimpleIoc.Default.Register<ILocate>(() => SimpleIoc.Default.GetInstance<PopTart>());
        }


        /// <summary>
        ///     Static reference to our main entry point into Chromatics
        /// </summary>
        public static PopTart PopTartInstance => SimpleIoc.Default.GetInstance<PopTart>();
    }
}
