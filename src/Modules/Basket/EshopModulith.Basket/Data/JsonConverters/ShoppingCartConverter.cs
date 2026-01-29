using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EshopModulith.Basket.Data.JsonConverters
{
    internal class ShoppingCartConverter : JsonConverter<ShoppingCart>
    {
        public override ShoppingCart Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            using var doc = JsonDocument.ParseValue(ref reader);
            var root = doc.RootElement;

            var id = root.GetProperty("Id").GetGuid();
            var userName = root.GetProperty("UserName").GetString()!;
            var itemsElement = root.GetProperty("Items");

            var cart = ShoppingCart.Create(id, userName);

            var items = itemsElement.Deserialize<List<ShoppingCartItem>>(options);
            if (items != null)
            {
                var itemsField = typeof(ShoppingCart)
                    .GetField("_items", BindingFlags.Instance | BindingFlags.NonPublic);
                itemsField!.SetValue(cart, items);
            }

            return cart;
        }

        public override void Write(Utf8JsonWriter writer, ShoppingCart value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteString("Id", value.Id);
            writer.WriteString("UserName", value.UserName);

            writer.WritePropertyName("Items");
            JsonSerializer.Serialize(writer, value.Items, options);

            writer.WriteEndObject();
        }
    }

}
