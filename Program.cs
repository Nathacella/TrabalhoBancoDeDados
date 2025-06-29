class Program
{
    static void Main(string[] args)
    {
        var clienteRepo = new ClienteRepo();
        var cartaoRepo = new CartaoRepo();
        var produtoRepo = new ProdutoRepo();
        var funcionarioRepo = new FuncionarioRepo();
        var pedidoRepo = new PedidoRepo();

        while (true)
        {
            Console.WriteLine("\n--- MENU ---");
            Console.WriteLine("1. Criar Cliente\n2. Listar Clientes\n3. Atualizar Cliente\n4. Deletar Cliente");
            Console.WriteLine("5. Criar Cartão\n6. Listar Cartões\n7. Atualizar Cartão\n8. Deletar Cartão");
            Console.WriteLine("9. Criar Produto\n10. Listar Produtos\n11. Atualizar Produto\n12. Deletar Produto");
            Console.WriteLine("13. Criar Pedido\n14. Listar Pedidos\n15. Deletar Pedido");
            Console.WriteLine("0. Sair");

            var opcao = Console.ReadLine() ?? "";

            switch (opcao)
            {
                case "1":
                    Console.Write("Nome: ");
                    var nome = Console.ReadLine() ?? "";
                    Console.Write("CPF: ");
                    var cpf = Console.ReadLine() ?? "";
                    Console.Write("Email: ");
                    var email = Console.ReadLine() ?? "";
                    clienteRepo.Criar(nome, cpf, email);
                    break;

                case "2":
                    clienteRepo.Listar();
                    break;

                case "3":
                    Console.Write("ID Cliente: ");
                    int idUpCliente = int.TryParse(Console.ReadLine(), out var icu) ? icu : 0;
                    Console.Write("Novo nome: ");
                    var novoNome = Console.ReadLine() ?? "";
                    Console.Write("Novo CPF: ");
                    var novoCpf = Console.ReadLine() ?? "";
                    Console.Write("Novo Email: ");
                    var novoEmail = Console.ReadLine() ?? "";
                    clienteRepo.Atualizar(idUpCliente, novoNome, novoCpf, novoEmail);
                    break;

                case "4":
                    Console.Write("ID Cliente para deletar: ");
                    int idDelCliente = int.TryParse(Console.ReadLine(), out var icd) ? icd : 0;
                    clienteRepo.Deletar(idDelCliente);
                    break;

                case "5":
                    Console.Write("ID Cliente: ");
                    int idCliente = int.TryParse(Console.ReadLine(), out var id1) ? id1 : 0;
                    Console.Write("Saldo: ");
                    decimal saldo = decimal.TryParse(Console.ReadLine(), out var s1) ? s1 : 0;
                    cartaoRepo.Criar(idCliente, saldo, "ativo");
                    break;

                case "6":
                    cartaoRepo.Listar();
                    break;

                case "7":
                    Console.Write("ID Cartão: ");
                    int idCartao = int.TryParse(Console.ReadLine(), out var icu2) ? icu2 : 0;
                    Console.Write("Novo saldo: ");
                    decimal novoSaldo = decimal.TryParse(Console.ReadLine(), out var ns) ? ns : 0;
                    Console.Write("Novo status: ");
                    var novoStatus = Console.ReadLine() ?? "";
                    cartaoRepo.Atualizar(idCartao, novoSaldo, novoStatus);
                    break;

                case "8":
                    Console.Write("ID Cartão para deletar: ");
                    int idDelCartao = int.TryParse(Console.ReadLine(), out var icd2) ? icd2 : 0;
                    cartaoRepo.Deletar(idDelCartao);
                    break;

                case "9":
                    Console.Write("Nome Produto: ");
                    var nomeProd = Console.ReadLine() ?? "";
                    Console.Write("Preço: ");
                    decimal preco = decimal.TryParse(Console.ReadLine(), out var p1) ? p1 : 0;
                    produtoRepo.Criar(nomeProd, preco);
                    break;

                case "10":
                    produtoRepo.Listar();
                    break;

                case "11":
                    Console.Write("ID Produto: ");
                    int idUpProd = int.TryParse(Console.ReadLine(), out var ipu) ? ipu : 0;
                    Console.Write("Novo nome: ");
                    var novoNomeProd = Console.ReadLine() ?? "";
                    Console.Write("Novo preço: ");
                    decimal novoPreco = decimal.TryParse(Console.ReadLine(), out var np) ? np : 0;
                    produtoRepo.Atualizar(idUpProd, novoNomeProd, novoPreco);
                    break;

                case "12":
                    Console.Write("ID Produto para deletar: ");
                    int idDelProd = int.TryParse(Console.ReadLine(), out var ipd) ? ipd : 0;
                    produtoRepo.Deletar(idDelProd);
                    break;

                case "13":
                    Console.Write("ID Cartão: ");
                    int idC = int.TryParse(Console.ReadLine(), out var icp) ? icp : 0;
                    Console.Write("ID Funcionário: ");
                    int idF = int.TryParse(Console.ReadLine(), out var ifp) ? ifp : 0;
                    var idPedido = pedidoRepo.Criar(idC, idF);
                    Console.Write("ID Produto: ");
                    int idProd = int.TryParse(Console.ReadLine(), out var idp) ? idp : 0;
                    Console.Write("Quantidade: ");
                    int qtd = int.TryParse(Console.ReadLine(), out var q1) ? q1 : 1;
                    pedidoRepo.AdicionarProduto(idPedido, idProd, qtd);
                    break;

                case "14":
                    pedidoRepo.Listar();
                    break;

                case "15":
                    Console.Write("ID Pedido para deletar: ");
                    int idDelPedido = int.TryParse(Console.ReadLine(), out var idpd) ? idpd : 0;
                    pedidoRepo.Deletar(idDelPedido);
                    break;

                case "0":
                    return;
            }
        }
    }
}