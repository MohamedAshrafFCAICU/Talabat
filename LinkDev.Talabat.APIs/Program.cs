using LinkDev.Talabat.APIs.Controllers.Errors;
using LinkDev.Talabat.APIs.Extensions;
using LinkDev.Talabat.APIs.Middlewares;
using LinkDev.Talabat.APIs.Services;
using LinkDev.Talabat.Core.Application;
using LinkDev.Talabat.Core.Application.Abstraction;
using LinkDev.Talabat.Infrastructure;
using LinkDev.Talabat.Infrastructure.Persistance;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var webApplicationbuilder = WebApplication.CreateBuilder(args);

            #region Configure Services
            
            // Add services to the container.

            webApplicationbuilder.Services.AddControllers()
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = false;
                options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(P => P.Value!.Errors.Count > 0)
                                       .Select(P => new ApiValidationErrorResponse.ValidationError()
                                       {
                                           Field = P.Key,
                                           Errors = P.Value!.Errors.Select(E => E.ErrorMessage)
                                       });
                                  

                    return new BadRequestObjectResult(new ApiValidationErrorResponse()
                    {
                        Errors = errors
                    });
                };

            })
            .AddApplicationPart(typeof(Controllers.AssemblyInformation).Assembly);

            //webApplicationbuilder.Services.Configure<ApiBehaviorOptions>(options =>
            //{
            //    options.SuppressModelStateInvalidFilter = false;
            //    options.InvalidModelStateResponseFactory = (actionContext) =>
            //    {
            //        var errors = actionContext.ModelState.Where(P => P.Value!.Errors.Count > 0)
            //                           .SelectMany(P => P.Value!.Errors)
            //                           .Select(E => E.ErrorMessage);

            //        return new BadRequestObjectResult(new ApiValidationErrorResponse()
            //        {
            //            Errors = errors
            //        });
            //    };
            //});

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
           
            webApplicationbuilder.Services.AddEndpointsApiExplorer();
            webApplicationbuilder.Services.AddSwaggerGen();

            webApplicationbuilder.Services.AddHttpContextAccessor();    

            webApplicationbuilder.Services.AddScoped<ILoggedInUserService , LoggedInUserService>();    



            webApplicationbuilder.Services.AddPersistanceServices(webApplicationbuilder.Configuration);

            webApplicationbuilder.Services.AddApplicationServices();

            webApplicationbuilder.Services.AddInfrastructureServices(webApplicationbuilder.Configuration);
            #endregion

            var app = webApplicationbuilder.Build();

            #region Databases Initialization
          
            await app.InitializeStoreContextAsync();

            #endregion

            #region Configure Kestrel Middlewares

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();


                //app.UseDeveloperExceptionPage();    
            }

            app.UseHttpsRedirection();

            app.UseStatusCodePagesWithReExecute("/Errors/{0}");

            app.UseAuthentication();    
            app.UseAuthorization(); 

            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllers();

            #endregion
            
            app.Run();
        }
    }
}
