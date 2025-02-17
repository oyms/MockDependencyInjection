using Moq;
using Skaar.MockDependencyInjection.Contracts;
using System.Reflection;

namespace Skaar.MockDependencyInjection.Moq
{
    public class Fixture<T> : Skaar.MockDependencyInjection.Fixture<T> where T : class
    {
        public Mock<TA> Arg<TA>() where TA : class
        {
            var key = ResolverSpecification.New<TA>();
            if (Resolvers[key] is not MockArgumentResolver resolver)
            {
                AssertNotResolved();
                resolver = new MockArgumentResolver(key);
                Resolvers.Add(resolver);
            }
            return (Mock<TA>)resolver.Mock;
        }
        
        protected override IArgumentResolver CreateArgumentResolver(ParameterInfo parameter)
        {
            var key = new ResolverSpecification(parameter);
            return new MockArgumentResolver(key);
        }
    }
}