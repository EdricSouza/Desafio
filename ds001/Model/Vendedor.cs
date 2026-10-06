using System;

namespace ds001.Model;

public class Vendedor
{
    public Vendedor(string nome)
    {
        Nome = nome;
        Vendas = new List<decimal>();
    }

    public string Nome { get; }
    public List<decimal> Vendas { get; }
    public decimal ComissaoTotal { get; set; }
    
    public decimal CalcularComissao()
    {
        foreach (var venda in Vendas)
        {
            if (venda < 100)
            {
                ComissaoTotal += 0;
            }
            else if (venda < 500)
            {
                ComissaoTotal += venda * 0.01m;
            }
            else
            {
                ComissaoTotal += venda * 0.05m;
            }
        }
        
        return ComissaoTotal;
    }
}
