using System.Web.Mvc;
using ToDo.Services.Zadania;
using Unity;
using Unity.Mvc5;

namespace ToDo
{
    public static class UnityConfig
    {
        public static void RegisterComponents()
        {
			var container = new UnityContainer();

            container.RegisterType<IZadanieService, Services.Zadania.ZadanieService>();
            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();
            
            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }
    }
}