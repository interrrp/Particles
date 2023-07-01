using System.Numerics;

namespace Particles.Extensions;

/// <summary>
/// Extension methods for the <see cref="System.Numerics.Vector3" /> class.
/// </summary>
public static class Vector3Extensions
{
    /// <summary>
    /// Cut off the Z dimension, only returning X and Y as a 2D vector (<see cref="System.Numerics.Vector2" />).
    /// </summary>
    /// <param name="vec3">The 3D vector.</param>
    /// <returns>The 3D vector with the Z dimension removed.</returns>
    public static Vector2 As2d(this Vector3 vec3)
    {
        return new()
        {
            X = vec3.X,
            Y = vec3.Y,
        };
    }
}
