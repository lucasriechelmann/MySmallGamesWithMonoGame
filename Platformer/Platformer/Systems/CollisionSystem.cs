﻿using Microsoft.Xna.Framework;
using MonoGame.Extended;
using MonoGame.Extended.ECS;
using MonoGame.Extended.ECS.Systems;
using Platformer.Components;
using Platformer.Managers;

namespace Platformer.Systems;

public class CollisionSystem : EntityProcessingSystem
{
    ComponentMapper<BodyComponent> _bodyMapper;
    ComponentMapper<Transform2> _transformMapper;
    CollisionManager _collisionManager;
    public CollisionSystem() : base(Aspect.All(typeof(BodyComponent), typeof(Transform2)))
    {
        _collisionManager = new CollisionManager(new Vector2(0, 60));
    }

    public override void Initialize(IComponentMapperService mapperService)
    {
        _bodyMapper = mapperService.GetMapper<BodyComponent>();
        _transformMapper = mapperService.GetMapper<Transform2>();
    }
    protected override void OnEntityAdded(int entityId) =>
        _collisionManager.AddBody(_bodyMapper.Get(entityId));
    protected override void OnEntityRemoved(int entityId) =>
        _collisionManager.RemoveBody(_bodyMapper.Get(entityId));
    public override void Update(GameTime gameTime)
    {
        _collisionManager.Update(gameTime.GetElapsedSeconds());
        base.Update(gameTime);
    }

    public override void Process(GameTime gameTime, int entityId)
    {
        var transform = _transformMapper.Get(entityId);
        var body = _bodyMapper.Get(entityId);
        transform.Position = body.Position;
    }
}
