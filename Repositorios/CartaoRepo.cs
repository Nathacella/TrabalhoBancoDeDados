using MySql.Data.MySqlClient;
using System;

public class CartaoRepo
{
    public void Criar(int idCliente, decimal saldo, string status)
    {
        using var conexao = Conexao.ObterConexao();
        conexao.Open();
        var cmd = new MySqlCommand("INSERT INTO cartao (id_cliente, saldo, status) VALUES (@id_cliente, @saldo, @status)", conexao);
        cmd.Parameters.AddWithValue("@id_cliente", idCliente);
        cmd.Parameters.AddWithValue("@saldo", saldo);
        cmd.Parameters.AddWithValue("@status", status);
        cmd.ExecuteNonQuery();
    }

    public void Listar()
    {
        using var conexao = Conexao.ObterConexao();
        conexao.Open();
        var cmd = new MySqlCommand("SELECT * FROM cartao", conexao);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine($"ID: {reader["id_cartao"]} | Cliente: {reader["id_cliente"]} | Saldo: {reader["saldo"]} | Status: {reader["status"]}");
        }
    }

    public void Atualizar(int id, decimal saldo, string status)
    {
        using var conexao = Conexao.ObterConexao();
        conexao.Open();
        var cmd = new MySqlCommand("UPDATE cartao SET saldo=@saldo, status=@status WHERE id_cartao=@id", conexao);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@saldo", saldo);
        cmd.Parameters.AddWithValue("@status", status);
        cmd.ExecuteNonQuery();
    }

    public void Deletar(int id)
    {
        using var conexao = Conexao.ObterConexao();
        conexao.Open();
        var cmd = new MySqlCommand("DELETE FROM cartao WHERE id_cartao=@id", conexao);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }
}