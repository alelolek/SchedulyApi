using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json.Serialization;
using System.Text;
using Persistence;
using Application.Implementations;
using Application.Interfaces;
using Persistence.Repository.Implementations;
using Persistence.Repository.Interfaces;
using Persistence.Mapping;
using API.Servicio;

namespace API
{
    public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers().AddJsonOptions(x =>
			x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

			services.AddDbContext<ApplicationDbContext>(options =>
			options.UseSqlServer(Configuration.GetConnectionString("Connection")));

			services.AddEndpointsApiExplorer();

			services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)//autorizacion
				.AddJwtBearer(opciones => opciones.TokenValidationParameters = new TokenValidationParameters
				{
					ValidateIssuer = false,
					ValidateAudience = false,
					ValidateLifetime = true,
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(
						Encoding.UTF8.GetBytes(Configuration["keyJwt"])),
					ClockSkew = TimeSpan.Zero
				});

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Schceduly", Version = "v1" });

				c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header
				});
				c.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						new string[]{ }
					}
				});
			});

			services.AddAutoMapper(
				typeof(CategoryMapperProfile),
				typeof(EventMapperProfile)
				);

			services.AddScoped<ICategoryService, CategoryService>();
			services.AddScoped<ICategoryRepository, CategoryRepository>();
			services.AddScoped<IEventRepository, EventRepository>();
			services.AddScoped<IEventService, EventService>();

			services.AddIdentity<IdentityUser, IdentityRole>()
				.AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();//autorizacion

			services.AddAuthorization(opciones =>
			{
				opciones.AddPolicy("EsAdminPolicy", politica => politica.RequireClaim("EsAdmin", "1"));
			});

			services.AddCors(opciones =>
			{
				opciones.AddDefaultPolicy(builder =>
				{
					builder.WithOrigins("https://apirequest.io").AllowAnyMethod().AllowAnyHeader();
				});
			});

			services.AddDataProtection();

			services.AddTransient<HashService>();
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseCors();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}

}
