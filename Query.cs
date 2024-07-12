using System;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate.Pagination;
using HotChocolate.Types;
using HotChocolate.Types.Pagination;

namespace test
{
    public class Query
    {
        [UsePaging]
        public async Task<Connection<Person>> Works(ApplicationDbContext context, PagingArguments arguments)
        {
            return await context.Persons.OrderBy(x => x.Name).ToPageAsync(arguments).ToConnectionAsync();
        }
        [UsePaging]
        public async Task<Connection<Person>> Fails(ApplicationDbContext context, PagingArguments arguments)
        {
            // The issue is that there is no serializer for DateTimeOffset etc.
            return await context.Persons.OrderBy(x => x.CreatedAt).ToPageAsync(arguments).ToConnectionAsync();
        }
    }

    public class Person
    {
        public Person(
            Guid id,
            string name,
            DateTimeOffset createdAt)
        {
            Id = id;
            Name = name;
            CreatedAt = createdAt;
        }

        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public DateTimeOffset CreatedAt { get; private set; }
    }
}
