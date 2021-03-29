﻿using Microsoft.AspNetCore.Blazor;
using Microsoft.AspNetCore.Blazor.Components;
using Microsoft.AspNetCore.Blazor.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.FlexGrid.Tests.Mocks
{
    public class TestRenderer : Renderer
    {
        public TestRenderer() : this(new TestServiceProvider())
        {
        }

        public TestRenderer(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public Action<RenderBatch> OnUpdateDisplay { get; set; }

        public List<CapturedBatch> Batches { get; }
            = new List<CapturedBatch>();

        public new void DispatchEvent(int componentId, int eventHandlerId, UIEventArgs args)
            => base.DispatchEvent(componentId, eventHandlerId, args);

        public T InstantiateComponent<T>() where T : IComponent
            => (T)InstantiateComponent(typeof(T));

        protected override Task UpdateDisplayAsync(in RenderBatch renderBatch)
        {
            OnUpdateDisplay?.Invoke(renderBatch);

            var capturedBatch = new CapturedBatch();
            Batches.Add(capturedBatch);

            for (var i = 0; i < renderBatch.UpdatedComponents.Count; i++)
            {
                ref var renderTreeDiff = ref renderBatch.UpdatedComponents.Array[i];
                capturedBatch.AddDiff(renderTreeDiff);
            }

            // Clone other data, as underlying storage will get reused by later batches
            capturedBatch.ReferenceFrames = renderBatch.ReferenceFrames.ToArray();
            capturedBatch.DisposedComponentIDs = renderBatch.DisposedComponentIDs.ToList();

            return Task.FromResult(0);
        }
    }
}
