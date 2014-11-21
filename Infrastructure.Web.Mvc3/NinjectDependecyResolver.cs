using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using Ninject.Syntax;

namespace Infrastructure.Web.Mvc3
{
    /// <summary>
    /// Classe de controle para a injeção de dependência do Ninject 
    /// herdando da interface System.Web.Mvc.IDependencyResolver 
    /// </summary>
    public class NinjectDependecyResolver : IDependencyResolver
    {
        private readonly IResolutionRoot _resolutionRoot;

        /// <summary>
        /// Construtor que realiza a inicialização do kernel do Ninject.
        /// </summary>
        /// <param name="kernel">Uma instância do kernel de D.I.</param>
        public NinjectDependecyResolver(IResolutionRoot kernel)
        {
            _resolutionRoot = kernel;
        }
        /// <summary>
        /// Obtém o serviço passado e realiza a injeção de dependência
        /// </summary>
        /// <param name="serviceType">O serviço a ser obtido.</param>
        /// <returns>Um objeto com o serviço para injetar.</returns>
        public object GetService(Type serviceType)
        {
            return _resolutionRoot.TryGet(serviceType);
        }
        /// <summary>
        /// Obtém uma coleção de objetos represenando instancias do serviço desejado.
        /// </summary>
        /// <param name="serviceType">O serviço desejado.</param>
        /// <returns>Uma coleçao de objetos com o tipo do serviço.</returns>
        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _resolutionRoot.GetAll(serviceType);
        }
    }
}
