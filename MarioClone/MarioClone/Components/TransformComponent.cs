using Microsoft.Xna.Framework;

namespace MarioClone.Components;

public class TransformComponent
{
    public Vector2 Position { get; set; }
    public float Rotation { get; set; }
    public Vector2 Scale { get; set; } = Vector2.One;
}
