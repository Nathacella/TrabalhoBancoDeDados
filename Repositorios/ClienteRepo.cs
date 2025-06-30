using MySql.Data.MySqlClient;
using System;

public class ClienteRepo
{
    public void Criar(string nome, string cpf, string email)
    {
        using var conexao = Conexao.ObterConexao();
        conexao.Open();
        var cmd = new MySqlCommand("INSERT INTO cliente (nome, cpf, email) VALUES (@nome, @cpf, @email)", conexao);
        cmd.Parameters.AddWithValue("@nome", nome);
        cmd.Parameters.AddWithValue("@cpf", cpf);
        cmd.Parameters.AddWithValue("@email", email);
        cmd.ExecuteNonQuery();
    }

    public void Listar()
    {
        using var conexao = Conexao.ObterConexao();
        conexao.Open();

        var cmd = new MySqlCommand(@"
        SELECT c.id_cliente, c.nome, c.cpf, c.email, ct.id_cartao
        FROM cliente c
        LEFT JOIN cartao ct ON c.id_cliente = ct.id_cliente", conexao);

        using var reader = cmd.ExecuteReader();
        while (reader.Read())
        {
            string cartaoInfo = reader["id_cartao"] != DBNull.Value ? reader["id_cartao"].ToString() : "Nenhum cartão";
            Console.WriteLine($"ID: {reader["id_cliente"]} | Nome: {reader["nome"]} | CPF: {reader["cpf"]} | Email: {reader["email"]} | Cartão: {cartaoInfo}");
        }
    }


    public void Atualizar(int id, string nome, string cpf, string email)
    {
        using var conexao = Conexao.ObterConexao();
        conexao.Open();
        var cmd = new MySqlCommand("UPDATE cliente SET nome=@nome, cpf=@cpf, email=@email WHERE id_cliente=@id", conexao);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.Parameters.AddWithValue("@nome", nome);
        cmd.Parameters.AddWithValue("@cpf", cpf);
        cmd.Parameters.AddWithValue("@email", email);
        cmd.ExecuteNonQuery();
    }

    public void Deletar(int id)
    {
        using var conexao = Conexao.ObterConexao();
        conexao.Open();
        var cmd = new MySqlCommand("DELETE FROM cliente WHERE id_cliente=@id", conexao);
        cmd.Parameters.AddWithValue("@id", id);
        cmd.ExecuteNonQuery();
    }
}