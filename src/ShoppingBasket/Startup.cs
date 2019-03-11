using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShoppingBasket.Services;

namespace ShoppingBasket
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            var inMemoryBasketStoreOptions =  new InMemoryBasketStoreOptions();
            services.Configure<InMemoryBasketStoreOptions>(_configuration.GetSection("InMemoryBasketStore"));
            services.Configure<InMemoryItemStoreOptions>(_configuration.GetSection("InMemoryItemStore"));
            services.AddShoppingBasketCore().AddInMemoryStore();

            // Laying out a potential authorization policy for how we would authenticate and authorize the various parties
            services.AddAuthorization(options =>
            {
                options.AddPolicy("ManageItems", policy =>
                {
                    policy.AddAuthenticationSchemes("AzureAD");
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("Team", "Operations");
                });
                options.AddPolicy("ViewBaskets", policy =>
                {
                    policy.AddAuthenticationSchemes("AzureAD");
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("Team", "Sales", "Marketing");
                });
                options.AddPolicy("ManageBasket", policy =>
                {
                    // We could use a client certificate or a shared secret to authenticate the POS
                    policy.AddAuthenticationSchemes("ClientCertificate");
                    policy.RequireAuthenticatedUser();
                });
            });
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
