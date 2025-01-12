﻿namespace WebUI.Components.Models
{
    public class ProductGroupModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public List<Guid> ProductIds { get; set; } = [];
    }
}
