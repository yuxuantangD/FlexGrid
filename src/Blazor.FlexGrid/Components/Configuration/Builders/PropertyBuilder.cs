﻿using Blazor.FlexGrid.Permission;
using System;
using System.Linq.Expressions;

namespace Blazor.FlexGrid.Components.Configuration.Builders
{
    public class PropertyBuilder<TProperty, TEntity>
    {
        private InternalPropertyBuilder Builder { get; }

        public PropertyBuilder(InternalPropertyBuilder internalPropertyBuilder)
        {
            Builder = internalPropertyBuilder ?? throw new ArgumentNullException(nameof(internalPropertyBuilder));
        }

        public PropertyBuilder<TProperty, TEntity> HasCaption(string caption)
        {
            Builder.HasCaption(caption);

            return this;
        }

        public PropertyBuilder<TProperty, TEntity> IsVisible(bool isVisible)
        {
            Builder.IsVisible(isVisible);

            return this;
        }

        public PropertyBuilder<TProperty, TEntity> IsSortable()
        {
            Builder.IsSortable(true);

            return this;
        }

        public PropertyBuilder<TProperty, TEntity> HasOrder(int order)
        {
            Builder.HasOrder(order);

            return this;
        }

        public PropertyBuilder<TProperty, TEntity> HasValueFormatter(Expression<Func<TProperty, string>> valueFormatterExpression)
        {
            Builder.HasValueFormatter(valueFormatterExpression);

            return this;
        }

        public PropertyBuilder<TProperty, TEntity> HasCompositeValueFormatter(Expression<Func<TEntity, string>> valueFormatterExpression)
        {
            Builder.HasCompositeValueFormatter(valueFormatterExpression);

            return this;
        }

        public PropertyBuilder<TProperty, TEntity> HasReadPermissionRestriction(Func<ICurrentUserPermission, bool> permissionRestrictionFunc)
        {
            Builder.HasReadPermissionRestriction(permissionRestrictionFunc);

            return this;
        }

        public PropertyBuilder<TProperty, TEntity> HasWritePermissionRestriction(Func<ICurrentUserPermission, bool> permissionRestrictionFunc)
        {
            Builder.HasWritePermissionRestriction(permissionRestrictionFunc);

            return this;
        }
    }
}
