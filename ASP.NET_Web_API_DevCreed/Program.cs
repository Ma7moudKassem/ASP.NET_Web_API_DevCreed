using ASP.NET_Web_API_DevCreed.Services.MovieServices;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
         options.UseSqlServer(connectionString));

builder.Services.AddControllers();
builder.Services.AddTransient<IGenreServices, GenreServices>();
builder.Services.AddTransient<IMovieServices, MovieServices>();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Kassem Api",
        Description = "This is my first api",
        TermsOfService = new Uri("https://www.googlle.com"),
        Contact = new OpenApiContact
        {
            Name = "Mahmoud Kassem",
            Email = "www.modykassem123@gmail.com",
            Url = new Uri("https://www.googlle.com"),
        },
        License = new OpenApiLicense
        {
            Name = "OpenGl",
            Url = new Uri("https://www.googlle.com"),
        }
    }); 
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter you JWT key",
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer",
                },
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
    });
builder.Services.AddCors();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(c => c.AllowAnyHeader()
                  .AllowAnyMethod().AllowAnyOrigin());

app.UseAuthorization();

app.MapControllers();

app.Run();
