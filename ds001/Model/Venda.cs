using System;
using System.Text.Json.Serialization;

namespace ds001.Model;

public class Venda(string vendedor, decimal valor)
{
    public string vendedor { get; } = vendedor;
    public decimal valor { get; } = valor;
}
