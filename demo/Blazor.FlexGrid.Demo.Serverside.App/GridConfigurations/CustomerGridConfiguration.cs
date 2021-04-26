﻿using Blazor.FlexGrid.Components.Configuration;
using Blazor.FlexGrid.Components.Configuration.Builders;
using Blazor.FlexGrid.Demo.Shared;

namespace Blazor.FlexGrid.Demo.Client.GridConfigurations
{
    public class CustomerGridConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.IsMasterTable();
            builder.HasDetailRelationship<Order>(c => c.Id, o => o.CustomerId)
               .HasCaption("Orders")
               .HasPageSize(10);

            builder.HasDetailRelationship<CustomerAddress>(c => c.Id, o => o.CustomerId)
                .HasCaption("Customer addresses");

            builder.Property(c => c.Id)
                .IsSortable();

            builder.Property(c => c.Email)
                .HasReadPermissionRestriction(perm => perm.IsInRole("Read"))
                .HasWritePermissionRestriction(perm => perm.IsInRole("Write"));
        }
    }
}
