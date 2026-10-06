using System.IO;
using System.Text.Json;
using ds001.Model;


List<Vendedor> vendedores = new List<Vendedor>();
string caminho = Path.Combine(
    AppContext.BaseDirectory,
    "resource",
    "data.json"
);

string json = File.ReadAllText(caminho);
ListaVendas listaVendas = JsonSerializer.Deserialize<ListaVendas>(json);

foreach (var venda in listaVendas.vendas)
{
    if (vendedores.Exists(v => v.Nome == venda.vendedor))
    {
        Vendedor vendedorExistente = vendedores.Find(v => v.Nome == venda.vendedor);
        vendedorExistente.Vendas.Add(venda.valor);
    }
    else
    {
        Vendedor novoVendedor = new Vendedor(venda.vendedor);
        novoVendedor.Vendas.Add(venda.valor);
        vendedores.Add(novoVendedor);
    }
}

foreach (var vendedor in vendedores)
{
    decimal comissao = vendedor.CalcularComissao();
    Console.WriteLine($"Vendedor: {vendedor.Nome}, Comissão Total: {comissao:C}");
}