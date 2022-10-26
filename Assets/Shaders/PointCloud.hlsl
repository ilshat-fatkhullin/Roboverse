#pragma raytracing test
 
struct RayPayload
{
    float4 color;
};

struct RayAttributes
{
    float2 barycentrics;
};
 
[shader("closesthit")]
void ClosestHit(inout RayPayload payload : SV_RayPayload, RayAttributes attributes : SV_IntersectionAttributes)
{
    float3 worldRayOrigin = WorldRayOrigin() + WorldRayDirection() * RayTCurrent();
    payload.color = float4(worldRayOrigin, 1);
}
