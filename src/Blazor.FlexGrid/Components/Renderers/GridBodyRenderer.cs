﻿using Blazor.FlexGrid.DataSet;
using Microsoft.Extensions.Logging;
using System;

namespace Blazor.FlexGrid.Components.Renderers
{
    public class GridBodyRenderer : GridCompositeRenderer
    {
        private readonly ILogger<GridBodyRenderer> logger;
        public GridBodyRenderer(ILogger<GridBodyRenderer> logger)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public override void Render(GridRendererContext rendererContext)
        {
            if (!rendererContext.TableDataSet.HasItems())
            {
                return;
            }

            rendererContext.RenderTreeBuilder.OpenElement(++rendererContext.Sequence, HtmlTagNames.TableBody);
            rendererContext.RenderTreeBuilder.AddAttribute(++rendererContext.Sequence, HtmlAttributes.Class, "table-body");

            try
            {
                foreach (var item in rendererContext.TableDataSet.Items)
                {
                    rendererContext.ActualItem = item;
                    foreach (var renderer in gridPartRenderers)
                        renderer.Render(rendererContext);
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"GridBodyRenderer ex: {ex}");
            }

            rendererContext.RenderTreeBuilder.CloseElement();
        }
    }
}
