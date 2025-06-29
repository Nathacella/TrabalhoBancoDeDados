using MySql.Data.MySqlClient;
using System;

public class ProdutoRepo
{
    public void Criar(string nome, decimal preco)
    {
        using var conn = Conexao.ObterConexao();
        conn.Open();
        var cmd = new MySqlCommand("INSERT INTO produto (nome, preco) VALUES (@nome, @preco)", conn);
        cmd.Parameters.AddWithValue("@nome", nome);
        cmd.Parameters.AddWithValue("@preco", preco);
        cmd.ExecuteNonQuery();
        Console.WriteLine("Produto criado com sucesso.");
    }

    public void Listar()
    {
        using var conn = Conexao.ObterConexao();
        conn.Open();
        var cmd = new MySqlCommand("SELECT * FROM produto", conn);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine($"ID: {reader["id_produto"]}, Nome: {reader["nome"]}, Preço: {reader["preco"]}");
        }
    }

    public void Atualizar(int id, string nome, decimal preco)
    {
        using var conn = Conexao.ObterConexao();
        conn.Open();
        var cmd = new MySqlCommand("UPDATE produto SET nome=@nome, preco=@preco WHERE id_produto=@id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@nome", nome);
        cmd.Parameters.AddWithValue("@preco", preco);
        cmd.ExecuteNonQuery();
        Console.WriteLine("Produto atualizado com sucesso.");
    }

    public void Deletar(int id)
    {
        using var conn = Conexao.ObterConexao();
        conn.Open();
        var cmd = new MySqlCommand("DELETE FROM produto WHERE id_produto=@id", conn);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
        Console.WriteLine("Produto deletado com sucesso.");
    }
}