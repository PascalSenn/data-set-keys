using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using test;


namespace test
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddGraphQLServer()
                .AddPagingArguments()
                .AddQueryType<Query>();
            services.AddDbContext<ApplicationDbContext>(o => o.UseNpgsql("Host=localhost;Database=data-set-keys;Username=postgres;Password=postgres"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            var context = app.ApplicationServices.GetRequiredService<ApplicationDbContext>();
            if (context.Database.EnsureCreated())
            {
                context.Persons.Add(new Person(Guid.NewGuid(), "Luke Skywalker", DateTimeOffset.UtcNow.AddDays(-1)));
                context.Persons.Add(new Person(Guid.NewGuid(), "Anakin Skywalker", DateTimeOffset.UtcNow.AddDays(-2)));
                context.Persons.Add(new Person(Guid.NewGuid(), "Leia Organa", DateTimeOffset.UtcNow.AddDays(-3)));
                context.Persons.Add(new Person(Guid.NewGuid(), "Han Solo", DateTimeOffset.UtcNow.AddDays(-4)));
                context.Persons.Add(new Person(Guid.NewGuid(), "Yoda", DateTimeOffset.UtcNow.AddDays(-5)));
                context.Persons.Add(new Person(Guid.NewGuid(), "Obi-Wan Kenobi", DateTimeOffset.UtcNow.AddDays(-6)));
                context.Persons.Add(new Person(Guid.NewGuid(), "Darth Vader", DateTimeOffset.UtcNow.AddDays(-7)));
                context.Persons.Add(new Person(Guid.NewGuid(), "Boba Fett", DateTimeOffset.UtcNow.AddDays(-8)));
                context.Persons.Add(new Person(Guid.NewGuid(), "Lando Calrissian", DateTimeOffset.UtcNow.AddDays(-9)));
                context.Persons.Add(new Person(Guid.NewGuid(), "Padmé Amidala", DateTimeOffset.UtcNow.AddDays(-10)));
                context.Persons.Add(new Person(Guid.NewGuid(), "Mace Windu", DateTimeOffset.UtcNow.AddDays(-11)));
                context.Persons.Add(new Person(Guid.NewGuid(), "Qui-Gon Jinn", DateTimeOffset.UtcNow.AddDays(-12)));
                context.Persons.Add(new Person(Guid.NewGuid(), "Count Dooku", DateTimeOffset.UtcNow.AddDays(-13)));
                context.Persons.Add(new Person(Guid.NewGuid(), "Sheev Palpatine", DateTimeOffset.UtcNow.AddDays(-14)));
                context.Persons.Add(new Person(Guid.NewGuid(), "Jabba the Hutt", DateTimeOffset.UtcNow.AddDays(-15)));
                context.Persons.Add(new Person(Guid.NewGuid(), "R2-D2", DateTimeOffset.UtcNow.AddDays(-16)));
                context.Persons.Add(new Person(Guid.NewGuid(), "C-3PO", DateTimeOffset.UtcNow.AddDays(-17)));
                context.Persons.Add(new Person(Guid.NewGuid(), "Chewbacca", DateTimeOffset.UtcNow.AddDays(-18)));
                context.Persons.Add(new Person(Guid.NewGuid(), "Poe Dameron", DateTimeOffset.UtcNow.AddDays(-19)));
                context.Persons.Add(new Person(Guid.NewGuid(), "Finn", DateTimeOffset.UtcNow.AddDays(-20)));
                context.Persons.Add(new Person(Guid.NewGuid(), "Rey", DateTimeOffset.UtcNow.AddDays(-21)));
                context.Persons.Add(new Person(Guid.NewGuid(), "Kylo Ren", DateTimeOffset.UtcNow.AddDays(-22)));
                context.Persons.Add(new Person(Guid.NewGuid(), "Snoke", DateTimeOffset.UtcNow.AddDays(-23)));
                context.Persons.Add(new Person(Guid.NewGuid(), "BB-8", DateTimeOffset.UtcNow.AddDays(-24)));
                context.Persons.Add(new Person(Guid.NewGuid(), "R2-Q5", DateTimeOffset.UtcNow.AddDays(-25)));
                context.Persons.Add(new Person(Guid.NewGuid(), "R2-KT", DateTimeOffset.UtcNow.AddDays(-26)));
                context.Persons.Add(new Person(Guid.NewGuid(), "R2-C4", DateTimeOffset.UtcNow.AddDays(-27)));
                context.Persons.Add(new Person(Guid.NewGuid(), "R2-A6", DateTimeOffset.UtcNow.AddDays(-28)));
                context.Persons.Add(new Person(Guid.NewGuid(), "R2-R9", DateTimeOffset.UtcNow.AddDays(-29)));
                context.Persons.Add(new Person(Guid.NewGuid(), "R2-B1", DateTimeOffset.UtcNow.AddDays(-30)));
                context.Persons.Add(new Person(Guid.NewGuid(), "R2-X2", DateTimeOffset.UtcNow.AddDays(-31)));
                context.SaveChanges();
            }
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGraphQL();
            });
        }
    }
}

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Person> Persons => Set<Person>();

    override protected void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}