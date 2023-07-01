using System;
using System.Numerics;

namespace Particles.Utils;

/// <summary>
/// Utilities for 2D vectors (<see cref="System.Numerics.Vector2" />).
/// </summary>
public static class Vector2Utils
{
    /// <summary>
    /// The global random number generator for this class.
    /// </summary>
    /// <returns>A <see cref="System.Random" /> instance.</returns>
    private static Random s_random = new();

    /// <summary>
    /// Generate a random 2D vector.
    /// </summary>
    /// <param name="minX">The minimum value for X.</param>
    /// <param name="maxX">The maximum value for X.</param>
    /// <param name="minY">The minimum value for Y.</param>
    /// <param name="maxY">The maximum value for Y.</param>
    /// <returns>A randomly generated 2D vector.</returns>
    public static Vector2 RandomVec(float minX, float maxX, float minY, float maxY)
    {
        return new()
        {
            X = (float)(s_random.NextDouble() * (maxX - minX) + minX),
            Y = (float)(s_random.NextDouble() * (maxY - minY) + minY),
        };
    }
}
