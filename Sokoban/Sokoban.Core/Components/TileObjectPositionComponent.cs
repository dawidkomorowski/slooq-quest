using System.Diagnostics;
using Geisha.Engine.Core;
using Geisha.Engine.Core.Components;
using Geisha.Engine.Core.SceneModel;
using Sokoban.Core.LevelModel;
using Sokoban.Core.Util;

namespace Sokoban.Core.Components
{
    public sealed class TileObjectPositionComponent : BehaviorComponent
    {
        private Transform2DComponent _transform2D = null!;

        public TileObjectPositionComponent(Entity entity) : base(entity)
        {
        }

        public TileObject? TileObject { get; set; }

        public override void OnStart()
        {
            _transform2D = Entity.GetComponent<Transform2DComponent>();
        }

        public override void OnUpdate(GameTime gameTime)
        {
            Debug.Assert(TileObject != null, nameof(TileObject) + " != null");

            _transform2D.Translation = TileObject.Tile.GetTranslation();
        }
    }

    public sealed class TileObjectPositionComponentFactory : ComponentFactory<TileObjectPositionComponent>
    {
        protected override TileObjectPositionComponent CreateComponent(Entity entity) => new TileObjectPositionComponent(entity);
    }
}