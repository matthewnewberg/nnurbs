using System;
using System.Runtime.Serialization;

namespace NN.Geometry
{
  /// <summary>
  /// Represents a rectangular grid of 3D points.
  /// </summary>
  [Serializable]
  public class Point3dGrid : GeometryBase
  {
    /// <summary>
    /// Initializes a rectangular grid of points, with no points in it.
    /// </summary>
    public Point3dGrid()
    {
    }

    /// <summary>
    /// Initializes a rectangular grid of points with a given number of columns and rows.
    /// </summary>
    /// <param name="rows">An amount of rows.</param>
    /// <param name="columns">An amount of columns.</param>
    public Point3dGrid(int rows, int columns)
    {
    }
  }
}
