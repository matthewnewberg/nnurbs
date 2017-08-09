using System;
using NN.Runtime.InteropWrappers;

//don't make serializable yet.

namespace NN.Geometry
{
  /// <summary>
  /// Represents the geometry in a block definition.
  /// </summary>
  public class InstanceDefinitionGeometry : GeometryBase
  {
    public InstanceDefinitionGeometry() { }

    /// <summary>
    /// Gets or sets the name of the definition.
    /// </summary>
    public string Name { get; set; }
    
    /// <summary>
    /// Gets or sets the description of the definition.
    /// </summary>
    public string Description { get; set; }
    
    /// <summary>
    /// unique id for this instance definition
    /// </summary>
    public Guid Id { get; set; }
    
    public System.Collections.Generic.List<Guid> ObjectIds = new System.Collections.Generic.List<Guid>();
  }

  /// <summary>
  /// Represents a reference to the geometry in a block definition.
  /// </summary>
  public class InstanceReferenceGeometry : GeometryBase
  {
    /// <summary>
    /// Constructor used when creating nested instance references.
    /// </summary>
    /// <param name="instanceDefinitionId"></param>
    /// <param name="transform"></param>
    /// <example>
    /// <code source='examples\cs\ex_nestedblock.cs' lang='cs'/>
    /// </example>
    public InstanceReferenceGeometry(Guid instanceDefinitionId, Transform transform)
    {
      
    }

    public InstanceReferenceGeometry()
    {

    }
        
    /// <summary>
    /// The unique id for the parent instance definition of this instance reference.
    /// </summary>
    public Guid ParentIdefId { get; set; }
    
    /// <summary>Transformation for this reference.</summary>
    public Transform Xform { get; set; }
  }
}
