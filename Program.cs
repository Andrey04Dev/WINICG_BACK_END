
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using webapi.Data;
using webapi.Interfaces.Audits;
using webapi.Interfaces.Certification;
using webapi.Interfaces.Company_position;
using webapi.Interfaces.Complete_process_task;
using webapi.Interfaces.Files;
using webapi.Interfaces.Flags;
using webapi.Interfaces.Historial;
using webapi.Interfaces.Isorule;
using webapi.Interfaces.No_Accordance;
using webapi.Interfaces.Position;
using webapi.Interfaces.Process;
using webapi.Interfaces.Risks;
using webapi.Interfaces.Roles;
using webapi.Interfaces.Tasks;
using webapi.Interfaces.Users;
using webapi.Mapper;
using webapi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//adding cors
builder.Services.AddCors(o =>
{
    o.AddPolicy(name: "mycors", builder => {
        builder.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost").AllowAnyHeader().AllowAnyMethod();
    });
});

//adding the dbcontext
//Adding the connection string
builder.Services.AddSingleton<DbConnection>();
//builder.Services.AddDbContext<DbConnection>(options=> options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

//adding the claims permision
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidateAudience = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Token"]))
    };
});

//Mapper 
builder.Services.AddAutoMapper(typeof(RolesMapper).Assembly);
builder.Services.AddAutoMapper(typeof(UsersMapper).Assembly);
builder.Services.AddAutoMapper(typeof(IsoRuleMapper).Assembly);
builder.Services.AddAutoMapper(typeof(HistorialMapper).Assembly);
builder.Services.AddAutoMapper(typeof(RiskMapper).Assembly);

//Interfaces
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IAuditRepository, AuditRepository>();
builder.Services.AddScoped<ICertificationRepository, CertificationRepositiory>();
builder.Services.AddScoped<ICompanyPositionRepository, CompanyPositionRepository>();
builder.Services.AddScoped<ICompleteProcessTaskRepository, CompleteProcessTaskRepository>();
builder.Services.AddScoped<IFlagsRepository, FlagsRepository>();
builder.Services.AddScoped<IIsoRuleRepository, IsoRuleRepository>();
builder.Services.AddScoped<IProcessRepository, ProcessRepository>();
builder.Services.AddScoped<INoAccordanceRepository, NoAccordanceRepository>();
builder.Services.AddScoped<IRisksRepository, RisksRepository>();
builder.Services.AddScoped<ITasksRepository, TasksRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IPositionRepository, PositionRepository>();
builder.Services.AddScoped<ITokenServices, TokenServices>();
builder.Services.AddScoped<IFileRepository, FileRepository>();
builder.Services.AddScoped<IHistorialRepository, HistorialRepository>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.UseCors("mycors");

app.Run();
