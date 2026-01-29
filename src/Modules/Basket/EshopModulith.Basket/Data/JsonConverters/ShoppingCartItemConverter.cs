using System.Text.Json;
using System.Text.Json.Serialization;

namespace EshopModulith.Basket.Data.JsonConverters
{
    public class ShoppingCartItemConverter : JsonConverter<ShoppingCartItem>
    {
        public override ShoppingCartItem Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var doc = JsonDocument.ParseValue(ref reader);
            var root = doc.RootElement;

            return new ShoppingCartItem(
                root.GetProperty("Id").GetGuid(),
                root.GetProperty("ShoppingCartId").GetGuid(),
                root.GetProperty("ProductId").GetGuid(),
                root.GetProperty("Quantity").GetInt32(),
                root.GetProperty("Color").GetString()!,
                root.GetProperty("Price").GetDecimal(),
                root.GetProperty("ProductName").GetString()!
            );
        }

        public override void Write(Utf8JsonWriter writer, ShoppingCartItem value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();

            writer.WriteString("Id", value.Id);
            writer.WriteString("ShoppingCartId", value.ShoppingCartId);
            writer.WriteString("ProductId", value.ProductId);
            writer.WriteNumber("Quantity", value.Quantity);
            writer.WriteString("Color", value.Color);
            writer.WriteNumber("Price", value.Price);
            writer.WriteString("ProductName", value.ProductName);

            writer.WriteEndObject();
        }
    }

}
