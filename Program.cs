
using Microsoft.EntityFrameworkCore;
using webapi.Data;
using webapi.Interfaces.Audits;
using webapi.Interfaces.Certification;
using webapi.Interfaces.Company_position;
using webapi.Interfaces.Complete_process_task;
using webapi.Interfaces.Flags;
using webapi.Interfaces.Isorule;
using webapi.Interfaces.No_Accordance;
using webapi.Interfaces.Process;
using webapi.Interfaces.Risks;
using webapi.Interfaces.Roles;
using webapi.Interfaces.Tasks;
using webapi.Interfaces.Users;
using webapi.Mapper;

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

//Mapper 
builder.Services.AddAutoMapper(typeof(RolesMapper).Assembly);

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
