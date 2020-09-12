/*

   Copyright 2020 Esri

   Licensed under the Apache License, Version 2.0 (the "License");
   you may not use this file except in compliance with the License.
   You may obtain a copy of the License at

       http://www.apache.org/licenses/LICENSE-2.0

   Unless required by applicable law or agreed to in writing, software
   distributed under the License is distributed on an "AS IS" BASIS,
   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.

   See the License for the specific language governing permissions and
   limitations under the License.

*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcGIS.Core.CIM;
using ArcGIS.Core.Data;
using ArcGIS.Core.Geometry;
using ArcGIS.Desktop.Catalog;
using ArcGIS.Desktop.Core;
using ArcGIS.Desktop.Editing;
using ArcGIS.Desktop.Extensions;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Framework.Dialogs;
using ArcGIS.Desktop.Framework.Threading.Tasks;
using ArcGIS.Desktop.Mapping;

namespace GraphicsLayers.Selections
{
  internal class SelectByLasso : MapTool
  {
    public SelectByLasso()
    {
      IsSketchTool = true;
      SketchType = SketchGeometryType.Lasso;
      SketchOutputMode = SketchOutputMode.Map;
    }

    protected override Task OnToolActivateAsync(bool active)
    {
      return base.OnToolActivateAsync(active);
    }

    protected async override Task<bool> OnSketchCompleteAsync(Geometry geometry)
    {
      return await QueuedTask.Run(() =>
      {        
        MapView.Active.SelectElements(geometry, SelectionCombinationMethod.Add);
        return true;
      });
    }
    protected override void OnToolMouseUp(MapViewMouseButtonEventArgs e)
    {
      switch (e.ChangedButton)
      {
        case System.Windows.Input.MouseButton.Right:
          e.Handled = true;
          var menu = FrameworkApplication.CreateContextMenu(
                 "esri_layouts_mapGraphicContextMenu");
          menu.DataContext = this;
          menu.IsOpen = true;
          break;
      }
    }
  }
}