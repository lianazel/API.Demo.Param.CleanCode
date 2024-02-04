
// > Pour "IParamRepository" & "DefaultParamRepository" <
using API.Demo.Param.CleanCode.Infrastructures.Repositories;
using API.Demo.Param.CleanCode.Domain;
using API.Demo.Param.CleanCode.Infrastructures;

using Microsoft.EntityFrameworkCore;
using System.Reflection;

// > Ajout Du MediatR ( Pour CQRS ) <
using MediatR;

// =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
// #####                     ---  Program.cs  ---                       #####
// #####                  **** Programme de Démarrage   ****            #####
// #####                           (.Net 6)                             #####
// =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=--=-=-=-=-
var builder = WebApplication.CreateBuilder(args);


// =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
// #####         **** Entity Framework Core .Net6  ****                     #####
// #####    Connxion du context ef Core à la chaine de connexion            #####
// =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
// > Voir : 
// ==> https://stackoverflow.com/questions/68980778/config-connection-string-in-net-core-6
builder.Services.AddDbContext<DataContext>(options =>
{
    // > Pour la prod <
     options.UseSqlServer(builder.Configuration.GetConnectionString("PROD_BDD"));

    // > Pour Tests : on écrit dans une Base Sqlite <
    //options.UseSqlite(builder.Configuration.GetConnectionString("DEV_BDD"));

});

// =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
// #####                            **** Entity Framework Core .Net6  ****                     #####
// ##### On met en place le lien entre l'interface et la classe qui respecte ce contrat ici.   #####
// ##### "builder.Services.AddScoped<IParamRepository, DefaultParamRepository>();"             #####
// #####                                                                                       #####
// ##### Le Framework pourra alors effectuer l'injection d'une instance de la classe...        #####
// #####  "DefaultParamRepository" qui respecte le contrat de l'interface...                   #####
// ##### ... "IParamRepository".                                                               #####
// =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-
builder.Services.AddScoped<IParamRepository, DefaultParamRepository>();



// =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-==-=-=-=-=-=-==
// ###                            **** MediatR CQRS .Net6  ****                                #####
// ###  Mise en place du MeDiatR                                                               #####
// ###  Ajouter au debut de ce code "using MediatR;"                                           ##### 
// ###  voir lien :                                                                           #####                                                                          
// ###   https://stackoverflow.com/questions/75635995/adding-mediatr-service                   ##### 
// =-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=-=
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// > On se prépare à créer la BDD  <
using (var scope = app.Services.CreateScope())
{
    // -- Création des Tables si elles sont inexistantes -  
    var dbContext = scope.ServiceProvider.GetRequiredService<DataContext>();

    // > Création des tables -
    dbContext.Database.EnsureCreated();

}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
