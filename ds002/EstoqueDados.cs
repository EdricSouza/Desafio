using System.Text.Json.Serialization;

public class EstoqueDados
{
    [JsonPropertyName("estoque")]
    public List<Produto> Estoque { get; set; } = [];
}
