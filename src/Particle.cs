using System.Numerics;
using Raylib_cs;
using Particles.Extensions;

namespace Particles;

/// <summary>
/// A single particle that bounces around the screen.
/// </summary>
public class Particle
{
    /// <summary>
    /// The position of this particle.
    /// </summary>
    /// <value>A 2D vector representing the position of this particle.</value>
    public Vector2 Position { get; set; }
    /// <summary>
    /// The velocity of this particle.
    /// </summary>
    /// <value>A 2D vector representing the velocity of this particle.</value>
    public Vector2 Velocity { get; set; }

    /// <summary>
    /// This particle's path.
    /// 
    /// The Z dimension is the lifetime of this point, used for decaying.
    /// </summary>
    /// <value>A list of the previous positions.</value>
    private List<Vector3> _path = new();

    /// <summary>
    /// Update this particle.
    /// </summary>
    /// <param name="deltaTime">
    /// The time between the last frame and this one. This is used to make operations independent of the framerate.
    /// </param>
    public void Update(float deltaTime)
    {
        var borderCollisionNormal = CalculateBorderCollisionNormal();
        if (borderCollisionNormal is not null)
        {
            Velocity = Vector2.Reflect(Velocity, borderCollisionNormal.Value);
        }

        Velocity += Vector2.UnitY * 150.0f * deltaTime;

        if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_MIDDLE))
        {
            var mousePos = Raylib.GetMousePosition();

            var xDiff = mousePos.X - Position.X;
            var yDiff = mousePos.Y - Position.Y;

            Velocity += new Vector2(xDiff * 5.0f, yDiff * 5.0f) * deltaTime;
        }

        Velocity = Vector2.Clamp(Velocity, new Vector2(-500.0f), new Vector2(500.0f));

        UpdateAndDrawPath(deltaTime);

        Position += Velocity * deltaTime;
    }

    /// <summary>
    /// Calculate the collision normal between the border and this particle.
    /// </summary>
    /// <returns>The collision normal, or null if the particle didn't collide.</returns>
    private Vector2? CalculateBorderCollisionNormal()
    {
        int screenWidth = Raylib.GetScreenWidth();
        int screenHeight = Raylib.GetScreenHeight();

        // Collision with the left/right borders
        if (Position.X < 0 || Position.X > screenWidth)
        {
            return new Vector2(-1, 0);
        }

        // Collision with the top/bottom borders
        if (Position.Y < 0 || Position.Y > screenHeight)
        {
            return new Vector2(0, -1);
        }

        // Particle didn't collide
        return null;
    }

    /// <summary>
    /// Update and draw the path.
    /// </summary>
    /// <param name="deltaTime">
    /// The time between the last frame and this one. This is used to make operations independent of the framerate.
    /// </param>
    private void UpdateAndDrawPath(float deltaTime)
    {
        _path.Add(new Vector3(Position, 1.0f));

        Vector3? lastPathPoint = null;
        for (var i = 0; i < _path.Count; i++)
        {
            var pathPoint = _path[i];

            var lifetime = pathPoint.Z;
            if (lifetime <= 0.0f)
            {
                _path.RemoveAt(i);
                continue;
            }

            _path[i] -= new Vector3(0.0f, 0.0f, 3.0f * deltaTime);

            var color = new Color(255, 255, 255, (int)Math.Clamp(lifetime * 255.0f, 0.0f, 255.0f));
            if (lastPathPoint is not null)
                Raylib.DrawLineV(lastPathPoint.Value.As2d(), pathPoint.As2d(), color);
            else
                Raylib.DrawPixelV(pathPoint.As2d(), color);

            lastPathPoint = pathPoint;
        }
    }
}
