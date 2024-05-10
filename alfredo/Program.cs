using System.ComponentModel.DataAnnotations;
using API.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

//Registrar o serviço de banco de dados na aplicação
builder.Services.AddDbContext<AppDataContext>();

var app = builder.Build();

//EndPoints - Funcionalidades
//GET: http://localhost:5225/
app.MapGet("/", () => "");

//POST: http://localhost:5225/api/funcionario/cadastrar
app.MapPost("/api/funcionario/cadastrar",
    ([FromBody] funcionario funcionario,
    [FromServices] AppDataContext ctx) =>
    {
        "nome": "seu nome";
        "cpf": "12345678912";
    });

//GET: http://localhost:5225/api/funcionario/listar
app.MapGet("/api/funcionario/listar",
    ([FromServices] AppDataContext ctx) =>
    [
        {
            "funcionarioId": "1",
            "nome": "joao da silva",
            "cpf":"123.456.789-09"
        },

        {
            "funcionarioId": "2",
            "nome": "maria oliveira",
            "cpf":"987.654.321-00"
        },

        {
            "funcionarioId": "3",
            "nome": "pedro santos",
            "cpf":"456.789.123-45"
        },
    ]
{
    if (ctx.folha.Any())
    {
        return Results.Ok(ctx.folha.ToList());
    }
    return Results.NotFound("funcionario nao encontrado!");


});

//POST: http://localhost:5225/api/folha/cadastrar
app.MapPost("/api/folha/cadastrar",
    ([FromBody] folha folha,
    [FromServices] AppDataContext ctx) =>

    {
      "valor": "50",
      "quantidade": "1000",
      "mes": "10",
      "ano": "2023",
      "funcionarioId": "1"

    }
{
    //Validação dos atributos do folha
    List<ValidationResult> erros = new
        List<ValidationResult>();
    if(!Validator.TryValidateObject(
        folha, new ValidationContext(folha),
        erros, true))
    {
        return Results.BadRequest(erros);
    }

    //RN: Não permitir folhas iguais
    folha? folhaBuscada = ctx.folha.
        FirstOrDefault(x => x.Nome == folha.Nome);
    if (folhaBuscada is not null)
    {
        return Results.
            BadRequest("Já existe uma folha com o mesmo nome!");
    }

    //Adicionar o folha dentro do banco de dados
    ctx.folha.Add(folha);
    ctx.SaveChanges();
    return Results.Created("", folha);
});

//GET: http://localhost:5225/api/folha/listar
app.MapGet("/api/folha/listar",
    ([FromServices] AppDataContext ctx) =>
    [
        {
            "folhaId": "1",
            "valor": "20.0",
            "quantidade": "160",
            "mes": "1",
            "ano": "2023",
            "salarioBruto": "3000.0",
            "impostoIrrf": "375.0",
            "impostoInss":"150.0",
            "impostoFgts":"240.0",
            "salarioLiquido":"2475.0",
            "funcionario": {
                "funcionarioId": "1",
                "nome": "joao da silva",
                "cpf":"123.456.789-09"
            },
            "funcionarioId": "1"
        },

        {
            "folhaId": "2",
            "valor": "18.5",
            "quantidade": "150",
            "mes": "2",
            "ano": "2023",
            "salarioBruto": "3000.0",
            "impostoIrrf": "375.0",
            "impostoInss":"150.0",
            "impostoFgts":"240.0",
            "salarioLiquido":"2475.0",
            "funcionario": {
                "funcionarioId": "2",
                "nome": "maria oliveira",
                "cpf":"987.654.321-00"
            }, 
            "funcionarioId": "2"
        },
    ]
{
    if (ctx.folha.Any())
    {
        return Results.Ok(ctx.folha.ToList());
    }
    return Results.NotFound("funcionario nao encontrado!");


});

//POST: http://localhost:5225/api/folha/buscar/{cpf}/{mes}/{ano}
app.MapPost("/api/folha/buscar/{cpf}/{mes}/{ano}",
    ([FromBody] folha folha,
    [FromServices] AppDataContext ctx) =>

    {
         "folhaId": "1",
            "valor": "20.0",
            "quantidade": "160",
            "mes": "1",
            "ano": "2023",
            "salarioBruto": "3000.0",
            "impostoIrrf": "375.0",
            "impostoInss":"150.0",
            "impostoFgts":"240.0",
            "salarioLiquido":"2475.0",
            "funcionario": {
                "funcionarioId": "1",
                "nome": "joao da silva",
                "cpf":"123.456.789-09"
    },
    "funcionarioId": "1"
 }

{
    
    folha? folha =
        ctx.folha.FirstOrDefault(x => x.Id == id);
    if (folha is null)
    {
        return Results.NotFound("folha não encontrada!");
    }
    return Results.Ok(folha);
});


app.Run();
