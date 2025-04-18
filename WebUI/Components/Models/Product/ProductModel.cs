﻿namespace WebUI.Components.Models
{
    public class ProductModel
    {
        public Guid Id { get; set; }

        public List<Guid>? ProductGroupIds { get; set; } = [];

        public List<Guid>? ShopIds { get; set; } = [];

        public List<Guid>? RackIds { get; set; } = [];

        public List<Guid>? IncomingOrderIds { get; set; } = [];

        public List<Guid>? CustomerOrderDetailIds { get; set; } = [];

        public List<Guid>? RefundOrderIds { get; set; } = [];

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Tag { get; set; }

        public double Weight { get; set; }

        public double Height { get; set; }

        public double Length { get; set; }

        public double Width { get; set; }

        public string Sku { get; set; }

        public List<Guid> PhotoIds { get; set; }
    }
}
