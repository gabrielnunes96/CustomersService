global using CustomerIssuer.Models;
using CustomerIssuer.Data.Context;
using CustomerIssuer.Services.TransactionServices;
using Microsoft.OpenApi.Models;
using System.Reflection;

namespace CustomerIssuer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(swagger =>
            {
                swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "CustomerIssuerAPI",
                    Description = "API para manipular dados referentes a transações de cartões de crédito",
                    TermsOfService = new Uri("https://abcdxyz.com"),
                    Contact = new OpenApiContact()
                    {
                        Name = "Gabriel Nunes Campos",
                        Email = "gabriel.nunes@b2card.com.br",
                        Url = new Uri("https://gabrielcampos.com")
                    },
                    License = new OpenApiLicense()
                    {
                        Name = "Open Source",
                        Url = new Uri("https://opensource.com")
                    }
                });
                var arquivoSwaggerXML = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var diretorioArquivoXML = Path.Combine(AppContext.BaseDirectory, arquivoSwaggerXML);
                swagger.IncludeXmlComments(diretorioArquivoXML);
            });

            builder.Services.AddScoped<ITransactionService, TransactionService>();
            builder.Services.AddDbContext<DataContext>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI(ui =>
                {
                    ui.SwaggerEndpoint("./v1/swagger.json", "CustomerIssuerAPI");
                });
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();

        }
    }
}