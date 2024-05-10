using System;
using Microsoft.EntityFrameworkCore.tabela;

#nullable disable

namespace API.tabela
{
    /// <inheritdoc />
    public partial class Inicial : tabela
    {
        /// <inheritdoc />
        protected override void Up(tabelaBuilder tabelaBuilder)
        {
            tabelaBuilder.CreateTable(
                name: "calculo INSS",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false),
                    Nome = table.Column<string>(type: "TEXT", nullable: false),
                    Descricao = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    Valor = table.Column<double>(type: "REAL", nullable: false),
                    CriadoEm = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Quantidade = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tabela", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(tabelaBuilder tabelaBuilder)
        {
            tabelaBuilder.DropTable(
                name: "calculo INSS");
        }
    }
}
