namespace Skaar.MockDependencyInjection.Moq
{
    public class Fixture<T> : Skaar.MockDependencyInjection.Fixture<T> where T : class
    {
        protected override T CreateInstance()
        {
            throw new NotImplementedException();
        }
    }
}