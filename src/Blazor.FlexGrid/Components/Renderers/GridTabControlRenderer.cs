﻿using Blazor.FlexGrid.Components.Configuration;
using Blazor.FlexGrid.DataAdapters;
using Blazor.FlexGrid.DataSet;
using Blazor.FlexGrid.DataSet.Options;
using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;

namespace Blazor.FlexGrid.Components.Renderers
{
    public class GridTabControlRenderer : GridPartRenderer
    {
        public override void Render(GridRendererContext rendererContext)
        {
            if (!rendererContext.TableDataSet.ItemIsSelected(rendererContext.ActualItem) ||
                !rendererContext.GridConfiguration.IsMasterTable ||
                !(rendererContext.TableDataSet is IMasterTableDataSet masterTableDataSet))
            {
                return;
            }

            var selectedDataAdapter = masterTableDataSet.GetSelectedDataAdapter(rendererContext.ActualItem);

            rendererContext.OpenElement(HtmlTagNames.TableRow, rendererContext.CssClasses.TableRow);
            rendererContext.OpenElement(HtmlTagNames.TableColumn, rendererContext.CssClasses.TableCell);
            rendererContext.AddColspan();


            rendererContext.OpenElement(HtmlTagNames.Div, "tabs-header");
            rendererContext.OpenElement(HtmlTagNames.Div, "tabs-header-buttons");

            RenderTabs(rendererContext, masterTableDataSet, selectedDataAdapter);

            rendererContext.CloseElement();
            rendererContext.CloseElement();

            rendererContext.OpenElement(HtmlTagNames.Div, "tabs-content");
            rendererContext.AddDetailGridViewComponent(selectedDataAdapter);
            rendererContext.CloseElement();
            rendererContext.CloseElement();
            rendererContext.CloseElement();
        }

        private void RenderTabs(GridRendererContext rendererContext, IMasterTableDataSet masterTableDataSet, ITableDataAdapter selectedDataAdapter)
        {
            foreach (var dataAdapter in masterTableDataSet.DetailDataAdapters)
            {
                var masterDetailRelationship = rendererContext
                    .GridConfiguration
                    .FindRelationshipConfiguration(dataAdapter.UnderlyingTypeOfItem);

                var actualItem = rendererContext.ActualItem;
                rendererContext.OpenElement(HtmlTagNames.Button,
                    selectedDataAdapter.IsForSameUnderlyingType(dataAdapter) ? "tabs-button tabs-button-active" : "tabs-button");

                rendererContext.AddOnClickEvent(() =>
                    BindMethods.GetEventHandlerValue((UIMouseEventArgs e) =>
                        masterTableDataSet.SelectDataAdapter(new MasterDetailRowArguments(dataAdapter, actualItem)))
                );

                rendererContext.OpenElement(HtmlTagNames.Span, "tabs-button-text");
                rendererContext.AddContent(masterDetailRelationship.DetailGridViewPageCaption(dataAdapter));
                rendererContext.CloseElement();
                rendererContext.CloseElement();
            }
        }
    }
}
