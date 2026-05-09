using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using AutuShopping.API.Models;

namespace AutuShopping.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly IConfiguration _config;

        public ProdutosController(IConfiguration config)
        {
            _config = config;
        }

        private SqlConnection GetConnection()
        {
            return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
        }

        // ========================
        // LISTAR
        // ========================
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var lista = new List<Produto>();

            using var conn = GetConnection();
            await conn.OpenAsync();

            var cmd = new SqlCommand("SELECT * FROM Produtos", conn);
            var reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                lista.Add(new Produto
                {
                    ProdutoID = (int)reader["ProdutoID"],
                    Codigo = reader["Codigo"].ToString(),
                    Nome = reader["Nome"].ToString(),
                    PrecoCompra = (decimal)reader["PrecoCompra"],
                    PrecoVenda = (decimal)reader["PrecoVenda"],
                    Estoque = (int)reader["Estoque"],
                    EstoqueMinimo = (int)reader["EstoqueMinimo"]
                });
            }

            return Ok(lista);
        }

        // ========================
        // CRIAR
        // ========================
        [HttpPost]
        public async Task<IActionResult> Post(Produto produto)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();

            var cmd = new SqlCommand(@"
                INSERT INTO Produtos (Codigo, Nome, PrecoCompra, PrecoVenda, Estoque, EstoqueMinimo)
                VALUES (@Codigo, @Nome, @PrecoCompra, @PrecoVenda, @Estoque, @EstoqueMinimo)", conn);

            cmd.Parameters.AddWithValue("@Codigo", produto.Codigo);
            cmd.Parameters.AddWithValue("@Nome", produto.Nome);
            cmd.Parameters.AddWithValue("@PrecoCompra", produto.PrecoCompra);
            cmd.Parameters.AddWithValue("@PrecoVenda", produto.PrecoVenda);
            cmd.Parameters.AddWithValue("@Estoque", produto.Estoque);
            cmd.Parameters.AddWithValue("@EstoqueMinimo", produto.EstoqueMinimo);

            await cmd.ExecuteNonQueryAsync();

            return Ok("Produto criado com sucesso!");
        }

        // ========================
        // ATUALIZAR
        // ========================
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, Produto produto)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();

            var cmd = new SqlCommand(@"
                UPDATE Produtos
                SET Codigo=@Codigo,
                    Nome=@Nome,
                    PrecoCompra=@PrecoCompra,
                    PrecoVenda=@PrecoVenda,
                    Estoque=@Estoque,
                    EstoqueMinimo=@EstoqueMinimo
                WHERE ProdutoID=@Id", conn);

            cmd.Parameters.AddWithValue("@Id", id);
            cmd.Parameters.AddWithValue("@Codigo", produto.Codigo);
            cmd.Parameters.AddWithValue("@Nome", produto.Nome);
            cmd.Parameters.AddWithValue("@PrecoCompra", produto.PrecoCompra);
            cmd.Parameters.AddWithValue("@PrecoVenda", produto.PrecoVenda);
            cmd.Parameters.AddWithValue("@Estoque", produto.Estoque);
            cmd.Parameters.AddWithValue("@EstoqueMinimo", produto.EstoqueMinimo);

            await cmd.ExecuteNonQueryAsync();

            return Ok("Produto atualizado com sucesso!");
        }

        // ========================
        // ELIMINAR
        // ========================
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            using var conn = GetConnection();
            await conn.OpenAsync();

            var cmd = new SqlCommand("DELETE FROM Produtos WHERE ProdutoID=@Id", conn);
            cmd.Parameters.AddWithValue("@Id", id);

            await cmd.ExecuteNonQueryAsync();

            return Ok("Produto eliminado com sucesso!");
        }
    }
}