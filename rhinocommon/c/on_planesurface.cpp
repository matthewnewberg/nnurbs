#include "StdAfx.h"

RH_C_FUNCTION ON_PlaneSurface* ON_PlaneSurface_New(const ON_PLANE_STRUCT* plane, ON_INTERVAL_STRUCT xExtents, ON_INTERVAL_STRUCT yExtents)
{
  ON_PlaneSurface* rc = NULL;
  if( plane )
  {
    const ON_Interval* _x = (const ON_Interval*)&xExtents;
    const ON_Interval* _y = (const ON_Interval*)&yExtents;

    ON_Plane temp = FromPlaneStruct(*plane);
    rc = new ON_PlaneSurface(temp);

    if (rc)
    {
      rc->SetExtents(0, *_x, true);
      rc->SetExtents(1, *_y, true);
    }
  }
  return rc;
}

////////////////////////////////////////////////////////////////

RH_C_FUNCTION void ON_ClippingPlaneSurface_GetPlane(const ON_ClippingPlaneSurface* pConstClippingPlaneSurface, ON_PLANE_STRUCT* plane)
{
  if( pConstClippingPlaneSurface && plane )
  {
    CopyToPlaneStruct(*plane, pConstClippingPlaneSurface->m_clipping_plane.m_plane);
  }
}

RH_C_FUNCTION void ON_ClippingPlaneSurface_SetPlane(ON_ClippingPlaneSurface* pClippingPlaneSurface, const ON_PLANE_STRUCT* plane)
{
  if( pClippingPlaneSurface && plane )
  {
    ON_Plane temp = FromPlaneStruct(*plane);
    pClippingPlaneSurface->m_plane = temp;
  }
}

RH_C_FUNCTION int ON_ClippingPlaneSurface_ViewportIdCount(const ON_ClippingPlaneSurface* pConstClippingPlaneSurface)
{
  int rc = 0;
  if( pConstClippingPlaneSurface )
    rc = pConstClippingPlaneSurface->m_clipping_plane.m_viewport_ids.Count();
  return rc;
}

RH_C_FUNCTION ON_UUID ON_ClippingPlaneSurface_ViewportId(const ON_ClippingPlaneSurface* pConstClippingPlaneSurface, int i)
{
  if( pConstClippingPlaneSurface && i>=0 && i<pConstClippingPlaneSurface->m_clipping_plane.m_viewport_ids.Count() )
  {
    const ON_UUID* ids = pConstClippingPlaneSurface->m_clipping_plane.m_viewport_ids.Array();
    if( ids )
      return ids[i];
  }
  return ::ON_nil_uuid;
}