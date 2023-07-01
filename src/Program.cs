using Raylib_cs;
using Particles.Utils;

namespace Particles;

public static class Program
{
    private static List<Particle> s_particles = new();

    /// <summary>
    /// Start the program.
    /// </summary>
    /// <param name="args">The command-line arguments passed in.</param>
    public static void Main(string[] args)
    {
        SetConfigFlags();
        InitWindow();
        CreateRandomParticles();
        StartMainLoop();
    }

    /// <summary>
    /// Start the main game loop.
    /// 
    /// This assumes that the window has already been initialized.
    /// </summary>
    private static void StartMainLoop()
    {
        while (!Raylib.WindowShouldClose())
        {
            if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_LEFT))
                CreateRandomParticle();
            else if (Raylib.IsMouseButtonDown(MouseButton.MOUSE_BUTTON_RIGHT) && s_particles.Count > 0)
                s_particles.RemoveAt(0);

            var deltaTime = Raylib.GetFrameTime();

            Raylib.BeginDrawing();
            Raylib.ClearBackground(Color.BLACK);
            foreach (var particle in s_particles)
            {
                particle.Update(deltaTime);
            }
            DrawInfo();
            Raylib.EndDrawing();
        }
    }

    /// <summary>
    /// Draw information on the screen, including the FPS and the number of particles.
    /// </summary>
    private static void DrawInfo()
    {
        Raylib.DrawRectangle(0, 0, 200, 60, new Color(0, 0, 0, 200));
        Raylib.DrawText($"{Raylib.GetFPS()} FPS", 10, 10, 20, Color.WHITE);
        Raylib.DrawText($"{s_particles.Count} particles", 10, 30, 20, Color.WHITE);
    }

    /// <summary>
    /// Create random particles.
    /// </summary>
    private static void CreateRandomParticles()
    {
        for (var i = 0; i < 20; i++)
        {
            CreateRandomParticle();
        }
    }

    /// <summary>
    /// Create a particle with a random position and velocity.
    /// </summary>
    private static void CreateRandomParticle()
    {
        s_particles.Add(new()
        {
            Position = Vector2Utils.RandomVec(0.0f, Raylib.GetScreenWidth(), 0.0f, Raylib.GetScreenHeight()),
            Velocity = Vector2Utils.RandomVec(-200.0f, 200.0f, -200.0f, 200.0f),
        });
    }

    /// <summary>
    /// Initialize the window.
    /// </summary>
    private static void InitWindow()
    {
        Raylib.InitWindow(1280, 720, "Particles");
        Raylib.SetExitKey(KeyboardKey.KEY_NULL);
    }

    /// <summary>
    /// Set the Raylib configuration flags.
    /// 
    /// This should be called before the window is initialized, otherwise most flags will not apply.
    /// </summary>
    private static void SetConfigFlags()
    {
        Raylib.SetConfigFlags(
            ConfigFlags.FLAG_VSYNC_HINT
            | ConfigFlags.FLAG_WINDOW_RESIZABLE
            | ConfigFlags.FLAG_MSAA_4X_HINT
        );
    }
}
