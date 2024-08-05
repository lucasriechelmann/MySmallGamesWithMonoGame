using Microsoft.Xna.Framework;
using MonoGame.Extended;
using Platformer.Enums;
using System;

namespace Platformer.Components;
public class BodyComponent
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public BodyType BodyType { get; set; }    
    public float X { get; set; }
    public float Y { get; set; }
    public float Width { get; set; }
    public float Height { get; set; }
    public Vector2 Position
    {
        get => new Vector2(X, Y);
        set 
        {
            X = value.X;
            Y = value.Y;
        }
    }
    public Vector2 Center => Bounding.Center;
    public Vector2 Velocity { get; set; }
    public Vector2 Size 
    { 
        get => new(Width, Height);
        set
        {
            Width = value.X;
            Height = value.Y;
        }
    }    
    public RectangleF Bounding => new(X, Y, Width, Height);    
    public static BodyComponent CreateDynamicBody(Vector2 position, Vector2 size) => new()
    {
        BodyType = BodyType.Dynamic,
        Position = position,
        Velocity = Vector2.Zero,
        Size = size
    };
    public static BodyComponent CreateStaticBody(Vector2 position, Vector2 size) => new()
    {
        BodyType = BodyType.Static,
        Position = position,
        Velocity = Vector2.Zero,
        Size = size
    };
}
