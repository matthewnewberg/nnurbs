using System;
using System.Runtime.Serialization;

namespace NN.Geometry
{
  /// <summary>
  /// Represents a view of the model placed on a page layout.
  /// </summary>
  public class DetailView : GeometryBase
  {
    public DetailView() {  }

    /// <summary>
    /// Gets or sets whether the view is parallel.
    /// </summary>
    public bool IsParallelProjection { get; set; }
    
    /// <summary>
    /// Gets or sets whether the view is perspective.
    /// </summary>
    public bool IsPerspectiveProjection { get; set; }
    

    /// <summary>
    /// Gets or sets whether the view projection is locked.
    /// </summary>
    /// <example>
    /// <code source='examples\vbnet\ex_addlayout.vb' lang='vbnet'/>
    /// <code source='examples\cs\ex_addlayout.cs' lang='cs'/>
    /// <code source='examples\py\ex_addlayout.py' lang='py'/>
    /// </example>
    public bool IsProjectionLocked { get; set; }
    

    /// <summary>
    /// Gets the page units/model units quotient.
    /// </summary>
    public double PageToModelRatio { get; set; }
   
  }
}
