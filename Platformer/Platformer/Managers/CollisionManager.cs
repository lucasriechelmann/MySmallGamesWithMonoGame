using Microsoft.Xna.Framework;
using MonoGame.Extended;
using Platformer.Components;
using Platformer.Enums;
using System;
using System.Collections.Generic;

namespace Platformer.Managers;

public class CollisionManager
{
    public Vector2 Gravity { get; set; }
    readonly List<BodyComponent> _dynamicBodies = new();
    readonly List<BodyComponent> _staticBodies = new();
    public CollisionManager(Vector2 gravity)
    {
        Gravity = gravity;
    }
    public void AddBody(BodyComponent body)
    {
        if (body is null)
            return;

        if (body.BodyType == BodyType.Dynamic)
        {
            _dynamicBodies.Add(body);
            return;
        }

        _staticBodies.Add(body);
    }

    public void RemoveBody(BodyComponent body)
    {
        if (body is null)
            return;

        if (body.BodyType == BodyType.Dynamic)
        {
            _dynamicBodies.Remove(body);
            return;
        }

        _staticBodies.Remove(body);
    }
    public void Update(float deltaTime)
    {        
        foreach(BodyComponent dynamicBody in _dynamicBodies)
        {
            dynamicBody.Velocity += Gravity;

            dynamicBody.X += dynamicBody.Velocity.X * deltaTime;
            ResolveCollisiont(dynamicBody, deltaTime);

            dynamicBody.Y += dynamicBody.Velocity.Y * deltaTime;
            ResolveCollisiont(dynamicBody, deltaTime);
        }
    }
    void ResolveCollisiont(BodyComponent dynamicBody, float deltaTime)
    {
        foreach (var staticBody in _staticBodies)
        {
            if(AabbAabb(dynamicBody.Bounding, staticBody.Bounding, out RectangleF overlap))
            {
                Vector2 displacement = Vector2.Zero;

                if (overlap.Width < overlap.Height)
                {
                    // Horizontal overlap is smaller, adjust X position
                    if (dynamicBody.Center.X < staticBody.Center.X)
                    {
                        displacement.X = -overlap.Width; // Move left
                    }
                    else
                    {
                        displacement.X = overlap.Width; // Move right
                    }
                }
                else
                {
                    // Vertical overlap is smaller, adjust Y position
                    if (dynamicBody.Center.Y < staticBody.Center.Y)
                    {
                        displacement.Y = -overlap.Height; // Move up
                    }
                    else
                    {
                        displacement.Y = overlap.Height; // Move down
                    }
                }

                // Apply displacement to dynamic body position
                dynamicBody.X += displacement.X;
                dynamicBody.Y += displacement.Y;
            }
        }

    }
    #region Collision Detection
    bool AabbAabb(RectangleF rectangle1, RectangleF rectangle2, out RectangleF overlap)
    {
        overlap = RectangleF.Empty;

        if (rectangle1.Intersects(rectangle2))
        {
            float xOverlap = Math.Min(rectangle1.Right, rectangle2.Right) - Math.Max(rectangle1.Left, rectangle2.Left);
            float yOverlap = Math.Min(rectangle1.Bottom, rectangle2.Bottom) - Math.Max(rectangle1.Top, rectangle2.Top);

            overlap = new RectangleF(
                Math.Max(rectangle1.Left, rectangle2.Left),
                Math.Max(rectangle1.Top, rectangle2.Top),
                xOverlap,
                yOverlap
            );
            return true;
        }
        return false;
    }

    bool CircleCircle(CircleF a, CircleF b)
    {
        var distance = a.Radius + b.Radius;
        var distanceSquared = distance * distance;
        var x = a.Center.X + b.Center.X;
        var y = a.Center.Y + b.Center.Y;
        return distanceSquared < x * x + y * y;
    }

    bool CircleCircle(CircleF a, CircleF b, out CircleF overlap)
    {
        overlap = new CircleF(Vector2.Zero, 0);

        if (CircleCircle(a, b))
        {
            var distance = a.Radius + b.Radius;
            var distanceSquared = distance * distance;
            var x = a.Center.X + b.Center.X;
            var y = a.Center.Y + b.Center.Y;
            var distanceToCenterSquared = x * x + y * y;

            var overlapRadius = (distanceSquared - distanceToCenterSquared) / 2;
            var overlapCenter = new Vector2(x, y) * overlapRadius / distanceToCenterSquared;

            overlap = new CircleF(overlapCenter, overlapRadius);
            return true;
        }
        return false;
        
    }
    #endregion
}
