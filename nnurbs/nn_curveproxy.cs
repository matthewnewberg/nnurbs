using System;

namespace NN.Geometry
{
  /// <summary>
  /// Provides strongly-typed access to Brep edges.
  /// </summary>
  public class CurveProxy : Curve
  {
    /// <summary>
    /// Protected constructor for internal use.
    /// </summary>
    protected CurveProxy()
    {
    }

    /// <summary>
    /// True if "this" is a curve is reversed from the "real" curve geometry
    /// </summary>
    public bool ProxyCurveIsReversed { get; set; }

  }
}
