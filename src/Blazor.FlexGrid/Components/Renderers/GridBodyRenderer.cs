﻿using Blazor.FlexGrid.DataSet;
using Blazor.FlexGrid.Permission;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Logging;
using Microsoft.JSInterop;
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

        

        protected override void BuildRenderTreeInternal(GridRendererContext rendererContext, PermissionContext permissionContext)
        {
            if (!rendererContext.TableDataSet.GroupingOptions.IsGroupingActive)
            {
                rendererContext.OpenElement(HtmlTagNames.TableBody, rendererContext.CssClasses.TableBody);
                try
                {
                    foreach (var item in rendererContext.TableDataSet.Items)
                    {
                        rendererContext.ActualItem = item;
                        foreach (var renderer in gridPartRenderers)
                            renderer.BuildRendererTree(rendererContext, permissionContext);
                    }

                }
                catch (Exception ex)
                {
                    logger.LogError($"Error occured during rendering grid view body. Ex: {ex}");
                }

                rendererContext.CloseElement();
            }
            else
            {
                if (rendererContext.TableDataSet?.GroupedItems != null)
                foreach (var item in rendererContext.TableDataSet.GroupedItems)
                {
                        rendererContext.ActualItem = item;
                        foreach (var renderer in gridPartRenderers)
                            renderer.BuildRendererTree(rendererContext, permissionContext);
                }
                
                
            }
        }

        public override bool CanRender(GridRendererContext rendererContext)
            => rendererContext.TableDataSet.HasItems();
    }
}
