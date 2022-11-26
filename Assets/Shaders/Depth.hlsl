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
    float distance = 0;
    payload.color = float4(distance, distance, distance, distance);
}
