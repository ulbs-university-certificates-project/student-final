using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using student_final.Data;
using student_final.Documents.Services;
using student_final.Documents.Services.Interfaces;
using student_final.Emails.Services;
using student_final.Emails.Services.Interfaces;
using student_final.QR.Services;
using student_final.QR.Services.Interfaces;
using student_final.Registers.Models;
using student_final.Registers.Services;
using student_final.Registers.Services.Interfaces;
using student_final.Students.Repository;
using student_final.Students.Repository.Interfaces;
using student_final.Students.Services;
using student_final.Students.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("student-crud", domain => domain.WithOrigins("http://localhost:4200")
        .AllowAnyHeader()
        .AllowAnyMethod()
    );
}); 

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region BASE

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("Default")!,
        new MySqlServerVersion(new Version(8, 0, 21))));

builder.Services.AddFluentMigratorCore()
    .ConfigureRunner(rb => rb
        .AddMySql5()
        .WithGlobalConnectionString(builder.Configuration.GetConnectionString("Default"))
        .ScanIn(typeof(Program).Assembly).For.Migrations())
    .AddLogging(lb => lb.AddFluentMigratorConsole());

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

#endregion

#region REPOSITORIES

builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<Register>();

#endregion

#region SERVICES

builder.Services.AddScoped<IStudentCommandService, StudentCommandService>();
builder.Services.AddScoped<IStudentQueryService, StudentQueryService>();
builder.Services.AddScoped<IRegisterQueryService, RegisterQueryService>();
builder.Services.AddScoped<IRegisterCommandService, RegisterCommandService>();
builder.Services.AddScoped<IQRCommandService, QRCommandService>();
builder.Services.AddScoped<IDocumentsCommandService, DocumentsCommandService>();
builder.Services.AddScoped<IEmailSenderCommandService, EmailSenderCommandService>();

#endregion

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseExceptionHandler("/Home/Error");
app.UseDeveloperExceptionPage();

using (var scope = app.Services.CreateScope())
{
    var runner = scope.ServiceProvider.GetRequiredService<IMigrationRunner>();
    runner.MigrateUp();
}

app.UseCors("student-crud");
app.Run();