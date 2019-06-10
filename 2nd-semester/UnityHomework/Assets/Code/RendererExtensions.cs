using UnityEngine;

/// <summary>
/// Renderer extensions
/// </summary>
public static class RendererExtensions
{
    /// <summary>
    /// Checks whether object visible from camera
    /// </summary>
    /// <param name="renderer">object's renderer</param>
    /// <param name="camera">camera</param>
    /// <returns></returns>
    public static bool IsVisibleFrom(this Renderer renderer, Camera camera)
    {
        Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
        return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
    }
}
