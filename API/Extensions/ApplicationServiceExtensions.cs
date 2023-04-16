using Application.Activities;
using Application.Core;
using Application.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Infrastructure.Security;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Extensions
{
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            // order isn't important when it comes to services, so we'll just add our entity here
            // BTW, we added Entity Framework to our Persistence project, but not the API project which we're in now.
            // the API project does have a transitive dependency via the Application project to the Persistence project, 
            // so we should be able to access AddDbContext from here. however, we should probably run dotnet restore at the solution level.  
            services.AddDbContext<DataContext>(opt => {
                // see appSettings.Development.json for DefaultConnection definition
                opt.UseSqlite(config.GetConnectionString("DefaultConnection"));
            });
            services.AddCors(opt => 
            {
                opt.AddPolicy("CorsPolicy", policy => 
                {
                    policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000");
                });
            });
            // on application start, this looks into our assembly where this class is located and it registers all our MediatR handlers
            // ... so it knows where to send the notifications/activites that we're getting MediatR to take care of 
            services.AddMediatR(typeof(List.Handler));
            services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssemblyContaining<Create>();
            services.AddHttpContextAccessor();
            services.AddScoped<IUserAccessor, UserAccessor>(); // enables injection inside our Application handlers

            return services;
        }
    }
}