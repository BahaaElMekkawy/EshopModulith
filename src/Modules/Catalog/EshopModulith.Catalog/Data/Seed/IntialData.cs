namespace EshopModulith.Catalog.Data.Seed
{
    public static class InitialData
    {
        public static IEnumerable<Product> Products => new List<Product>
        {
            Product.Create(
                new Guid("9f1b6a4e-3c2d-4b8a-9c7f-1e5a2d6f8b90"),
                "iPhone X",
                new List<string> { "Smartphones", "Electronics" },
                "Apple iPhone X with OLED display",
                "iphone-x.jpg",
                500
            ),

            Product.Create(
                new Guid("a1c3d5e7-1111-4a2b-8c9d-1234567890ab"),
                "Samsung Galaxy S21",
                new List<string> { "Smartphones", "Electronics" },
                "Samsung flagship Android smartphone",
                "galaxy-s21.jpg",
                650
            ),

            Product.Create(
                new Guid("b2d4f6a8-2222-4c3d-9e0f-abcdefabcdef"),
                "MacBook Pro 14",
                new List<string> { "Laptops", "Electronics" },
                "Apple MacBook Pro with M-series chip",
                "macbook-pro-14.jpg",
                2200
            ),

            Product.Create(
                new Guid("c3e5a7b9-3333-4d4e-a1b2-fedcba987654"),
                "Sony WH-1000XM5",
                new List<string> { "Audio", "Accessories" },
                "Noise cancelling wireless headphones",
                "sony-xm5.jpg",
                400
            ),

            Product.Create(
                new Guid("d4f6b8c0-4444-4e5f-b3c4-0123456789cd"),
                "Apple Watch Series 9",
                new List<string> { "Wearables", "Electronics" },
                "Latest Apple smartwatch with health tracking",
                "apple-watch-9.jpg",
                450
            )
        };
    }
}
