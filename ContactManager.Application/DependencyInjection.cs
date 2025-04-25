using ContactManager.Core.Interfaces;
using ContactManager.Core.Services;
using ContactManager.Infrastructure.Repositories;
using Ninject;

namespace ContactManager.Application
{
    public static class DependencyInjection
    {
        public static void AddApplicationServices(IKernel kernel, string filePath)
        {
            kernel.Bind<IContactRepository>().To<FileContactRepository>().InSingletonScope().WithConstructorArgument("filePath", filePath);
            kernel.Bind<ContactService>().ToSelf().InSingletonScope();
        }
    }
}