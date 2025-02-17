using Skaar.MockDependencyInjection.Contracts;
using System.Diagnostics;

namespace Skaar.MockDependencyInjection.Moq
{
    [DebuggerDisplay("{Key}")]
    class MockArgumentResolver(ResolverSpecification key) : IArgumentResolver
    {
        private global::Moq.Mock?  _mock;
        public global::Moq.Mock Mock => _mock ??= CreateMock(Key.ArgumentType);
        public object Resolve() => Mock.Object;
        public ResolverSpecification Key { get; } = key;
        private global::Moq.Mock CreateMock(Type t)
        {
            var genericType = typeof(global::Moq.Mock<>).MakeGenericType(t);
            return (global::Moq.Mock)Activator.CreateInstance(genericType)!;
        }
    }
}