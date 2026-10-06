Console.Write("Informe o valor: R$ ");
decimal valor = Convert.ToDecimal(Console.ReadLine());

Console.Write("Informe a data de vencimento (dd/MM/yyyy): ");
DateTime dataVencimento = Convert.ToDateTime(Console.ReadLine());
DateTime dataHoje = DateTime.Today;

int diasAtraso = (dataHoje - dataVencimento).Days;
decimal juros = 0;

if (diasAtraso > 0)
{
    juros = valor * 0.025m * diasAtraso;
}
else
{
    diasAtraso = 0;
}

Console.WriteLine($"Dias em atraso: {diasAtraso}");
Console.WriteLine($"Valor dos juros: R$ {juros:F2}");
