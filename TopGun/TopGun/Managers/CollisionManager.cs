using AnasStudio.Engine.Collisions;
using Microsoft.Xna.Framework;
using TopGun.GameObjects;

namespace TopGun.Managers;
public class CollisionManager
{
    PlayerManager _playerManager;
    EnemyManager _enemyManager;
    public CollisionManager(PlayerManager playerManager, EnemyManager enemyManager)
    {
        _playerManager = playerManager;
        _enemyManager = enemyManager;
    }
    public void HandleCollisions()
    {
        HandlePlayerShootsCollisions();
        HandleEnemyShootsCollisions();
    }
    void HandlePlayerShootsCollisions()
    {
        foreach (Shoot shoot in _playerManager.Shoots.Objects)
        {
            foreach (Helicopter helicopter in _enemyManager.Helicopters.Objects)
            {
                if(CollisionDetection.ShapesIntersect(shoot.BoundingBox, helicopter.BoundingBox))
                {
                    shoot.Remove();
                    helicopter.Remove();
                }
            }
        }
        _enemyManager.Helicopters.RemoveRemoved();
        _playerManager.Shoots.RemoveRemoved();
    }
    void HandleEnemyShootsCollisions()
    {
        Rectangle playerBoundingBox = _playerManager.Player.BoundingBox;
        foreach (Shoot shoot in _enemyManager.Shoots.Objects)
        {
            if(CollisionDetection.ShapesIntersect(shoot.BoundingBox, playerBoundingBox))
            {
                shoot.Remove();
                _playerManager.Player.Remove();
            }
        }        

        foreach(Helicopter helicopter in _enemyManager.Helicopters.Objects)
        {
            if(CollisionDetection.ShapesIntersect(helicopter.BoundingBox, playerBoundingBox))
            {
                helicopter.Remove();
                _playerManager.Player.Remove();
            }
        }

        _enemyManager.Helicopters.RemoveRemoved();
        _enemyManager.Shoots.RemoveRemoved();
    }
}
