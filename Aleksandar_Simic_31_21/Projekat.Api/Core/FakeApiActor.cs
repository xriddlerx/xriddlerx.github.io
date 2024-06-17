using ProjekatApplication;

namespace Projekat.Api.Core
{
    public class FakeApiActor : IApplicationActor
    {
        public int Id => 1;

        public string Email => "test@gmail.com";

        public string FirstName => "Pera";

        public string LastName => "Peric";

        public IEnumerable<int> AllowedUseCases => new List<int> { 500 };
    }

    public class AdminFakeApiActor : IApplicationActor
    {
        public int Id => 2;

        public string Email => "admin@gmail.com";

        public string FirstName => "Aleksandar";

        public string LastName => "Simic";

        public IEnumerable<int> AllowedUseCases => Enumerable.Range(1, 1000);
    }
}
