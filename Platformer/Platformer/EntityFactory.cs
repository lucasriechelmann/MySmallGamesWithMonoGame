using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.ECS;
using MonoGame.Extended.Graphics;
using Platformer.Components;
using Platformer.Enums;

namespace Platformer;

public class EntityFactory
{
    readonly World _world;
    readonly ContentManager _contentManager;
    public EntityFactory(World world, ContentManager contentManager)
    {
        _world = world;
        _contentManager = contentManager;
    }
    public Entity CreatePlayer(Vector2 position)
    {
        Texture2D texture = _contentManager.Load<Texture2D>("Sprites/Square");
        //Texture2DAtlas atlas = Texture2DAtlas.Create("Sprites//square", texture, 32, 32);
        //SpriteSheet sprite = new SpriteSheet("Sprites//square", atlas);
        Sprite sprite = new Sprite(texture);

        Entity player = _world.CreateEntity();
        player.Attach(sprite);
        player.Attach(BodyComponent.CreateDynamicBody(position, new(32, 32)));
        player.Attach(new PlayerComponent());

        return player;
    }
    public void CreateTile(int x, int y, int width, int height)
    {
        var entity = _world.CreateEntity();
        entity.Attach(new BodyComponent
        {
            Position = new(x * width, y * height),
            Size = new(width, height),
            BodyType = BodyType.Static,
            Velocity = Vector2.Zero
        });
    }
}
