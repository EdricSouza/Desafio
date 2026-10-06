using System.Text.Json;

var caminhoJson = Path.Combine(Directory.GetCurrentDirectory(), "resource", "data.json");

if (!File.Exists(caminhoJson))
{
    caminhoJson = Path.Combine(AppContext.BaseDirectory, "resource", "data.json");
}

if (!File.Exists(caminhoJson))
{
    Console.WriteLine("Arquivo resource/data.json não encontrado.");
    return;
}

var opcoesJson = new JsonSerializerOptions
{
    WriteIndented = true,
    PropertyNameCaseInsensitive = true
};

EstoqueDados? dados;

try
{
    dados = JsonSerializer.Deserialize<EstoqueDados>(File.ReadAllText(caminhoJson), opcoesJson);
}
catch (JsonException)
{
    Console.WriteLine("O arquivo data.json possui um formato inválido.");
    return;
}

if (dados is null)
{
    Console.WriteLine("Não foi possível carregar o estoque.");
    return;
}

long ultimoIdentificador = 0;

while (true)
{
    Console.WriteLine("\n=== CONTROLE DE ESTOQUE ===");
    Console.WriteLine("1 - Dar entrada");
    Console.WriteLine("2 - Dar saída");
    Console.WriteLine("3 - Visualizar estoque");
    Console.WriteLine("0 - Encerrar");
    Console.Write("Opção: ");
    var opcao = Console.ReadLine();

    if (opcao == "0")
    {
        break;
    }

    if (opcao == "3")
    {
        Console.WriteLine("\n ESTOQUE ATUAL ");
        Console.WriteLine($"{"Código",-10} {"Produto",-35} {"Quantidade",10}");
        Console.WriteLine(new string('-', 57));

        foreach (var item in dados.Estoque)
        {
            Console.WriteLine($"{item.CodigoProduto,-10} {item.DescricaoProduto,-35} {item.QuantidadeEstoque,10}");
        }

        continue;
    }

    if (opcao is not ("1" or "2"))
    {
        Console.WriteLine("Opção inválida.");
        continue;
    }

    Console.Write("Código do produto: ");
    if (!int.TryParse(Console.ReadLine(), out var codigoProduto))
    {
        Console.WriteLine("Código inválido.");
        continue;
    }

    var produto = dados.Estoque.FirstOrDefault(p => p.CodigoProduto == codigoProduto);
    if (produto is null)
    {
        Console.WriteLine("Produto não encontrado.");
        continue;
    }

    Console.Write("Quantidade: ");
    if (!int.TryParse(Console.ReadLine(), out var quantidade) || quantidade <= 0)
    {
        Console.WriteLine("A quantidade deve ser um número maior que zero.");
        continue;
    }

    if (opcao == "2" && quantidade > produto.QuantidadeEstoque)
    {
        Console.WriteLine($"Estoque insuficiente. Quantidade disponível: {produto.QuantidadeEstoque}.");
        continue;
    }

    Console.Write("Descrição da movimentação: ");
    var descricao = Console.ReadLine()?.Trim();
    if (string.IsNullOrWhiteSpace(descricao))
    {
        Console.WriteLine("A descrição é obrigatória.");
        continue;
    }

    produto.QuantidadeEstoque += opcao == "1" ? quantidade : -quantidade;

    var movimentacao = new Movimentacao
    {
        Identificador = Math.Max(DateTime.UtcNow.Ticks, ultimoIdentificador + 1),
        Descricao = descricao
    };
    ultimoIdentificador = movimentacao.Identificador;

    File.WriteAllText(caminhoJson, JsonSerializer.Serialize(dados, opcoesJson));

    Console.WriteLine("\nMovimentação realizada com sucesso!");
    Console.WriteLine($"Identificador: {movimentacao.Identificador}");
    Console.WriteLine($"Descrição: {movimentacao.Descricao}");
    Console.WriteLine($"Produto: {produto.DescricaoProduto}");
    Console.WriteLine($"Estoque final: {produto.QuantidadeEstoque}");
}
