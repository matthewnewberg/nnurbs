using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace NN.Geometry
{
    /// <summary>
    /// Represents a single item in a pointcloud. A PointCloud item 
    /// always has a location, but it has an optional normal vector and color.
    /// </summary>
    public class PointCloudItem
    {
        #region fields
        int m_index = -1;
        #endregion

        #region constructors

        public PointCloudItem()
        {
        }

        internal PointCloudItem(int index)
        {
            m_index = index;
        }
        #endregion

        #region properties
        /// <summary>
        /// Gets or sets the location of this point cloud item.
        /// </summary>
        public Point3d Location { get; set; }
        
        /// <summary>
        /// Gets or sets the X component of this point cloud item location.
        /// </summary>
        public double X
        {
            get
            {
                return Location.X;
            }
            set
            {
                Point3d pt = Location;
                pt.X = value;
                Location = pt;
            }
        }
        /// <summary>
        /// Gets or sets the Y component of this point cloud item location.
        /// </summary>
        public double Y
        {
            get
            {
                return Location.Y;
            }
            set
            {
                Point3d pt = Location;
                pt.Y = value;
                Location = pt;
            }
        }
        /// <summary>
        /// Gets or sets the Z component of this point cloud item location.
        /// </summary>
        public double Z
        {
            get
            {
                return Location.Z;
            }
            set
            {
                Point3d pt = Location;
                pt.Z = value;
                Location = pt;
            }
        }

        /// <summary>
        /// Gets or sets the normal vector for this point cloud item.
        /// </summary>
        public Vector3d Normal { get; set; }
        
        /// <summary>
        /// Gets or sets the color of this point cloud item.
        /// </summary>
        public Color Color { get; set; }
        
        /// <summary>
        /// Gets or sets the hidden flag of this point cloud item.
        /// </summary>
        public bool Hidden { get; set; }
        
        /// <summary>
        /// Gets the index of this point cloud item.
        /// </summary>
        public int Index
        {
            get { return m_index; }
            set { m_index =value; }
        }
        #endregion
    }

    /// <summary>
    /// Represents a collection of coordinates with optional normal vectors and colors.
    /// </summary>
    [Serializable]
    public class PointCloud : GeometryBase, IEnumerable<PointCloudItem>
    {

        List<PointCloudItem> pointCloud = new List<PointCloudItem>();
        /// <summary>
        /// Initializes a new instance of the <see cref="PointCloud"/> class,
        /// copying (Merge) the content of another pointcloud.
        /// </summary>
        public PointCloud()
        {

        }

        /// <summary>
        /// Gets the number of points in this pointcloud.
        /// </summary>
        public int Count
        {
            get
            {
                return pointCloud.Count;
            }
        }
        /// <summary>
        /// Gets the item at the given index.
        /// </summary>
        /// <param name="index">Index of item to retrieve.</param>
        /// <returns>The item at the given index.</returns>
        public PointCloudItem this[int index]
        {
            get
            {
                return pointCloud[index];
            }
        }

        /// <summary>
        /// Gets an enumerator that allows to modify each pointcloud point.
        /// </summary>
        /// <returns>A instance of <see cref="IEnumerator{PointCloudItem}"/>.</returns>
        public IEnumerator<PointCloudItem> GetEnumerator()
        {
            return pointCloud.GetEnumerator();
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return pointCloud.GetEnumerator();
        }
        
    }
}