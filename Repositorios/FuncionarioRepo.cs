using MySql.Data.MySqlClient;
using System;

public class FuncionarioRepo
{
    public void Criar(string nome)
    {
        using var conexao = Conexao.ObterConexao();
        conexao.Open();
        var cmd = new MySqlCommand("INSERT INTO funcionario (nome) VALUES (@nome)", conexao);
        cmd.Parameters.AddWithValue("@nome", nome);
        cmd.ExecuteNonQuery();
    }

    public void Listar()
    {
        using var conexao = Conexao.ObterConexao();
        conexao.Open();
        var cmd = new MySqlCommand("SELECT * FROM funcionario", conexao);
        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            Console.WriteLine($"ID: {reader["id_funcionario"]} | Nome: {reader["nome"]}");
        }
    }
}