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
        Console.WriteLine("Funcionário criado com sucesso.");
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

    public void Atualizar(int id, string novoNome)
    {
        using var conexao = Conexao.ObterConexao();
        conexao.Open();
        var cmd = new MySqlCommand("UPDATE funcionario SET nome = @nome WHERE id_funcionario = @id", conexao);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@nome", novoNome);
        cmd.ExecuteNonQuery();
        Console.WriteLine("Funcionário atualizado com sucesso.");
    }

    public void Deletar(int id)
    {
        using var conexao = Conexao.ObterConexao();
        conexao.Open();
        var cmd = new MySqlCommand("DELETE FROM funcionario WHERE id_funcionario = @id", conexao);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
        Console.WriteLine("Funcionário deletado com sucesso.");
    }
}