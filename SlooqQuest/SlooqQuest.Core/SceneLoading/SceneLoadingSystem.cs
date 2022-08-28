using System;
using Geisha.Engine.Core;
using Geisha.Engine.Core.SceneModel;
using Geisha.Engine.Core.Systems;

namespace Sokoban.Core.SceneLoading
{
    public sealed class SceneLoadingSystem : ICustomSystem
    {
        private readonly ISceneManager _sceneManager;
        private LoadSceneComponent? _loadSceneComponent;

        public SceneLoadingSystem(ISceneManager sceneManager)
        {
            _sceneManager = sceneManager;
        }

        public string Name => "SceneLoadingSystem";

        public void ProcessFixedUpdate()
        {
        }

        public void ProcessUpdate(GameTime gameTime)
        {
            if (_loadSceneComponent != null)
            {
                _sceneManager.LoadEmptyScene(_loadSceneComponent.SceneBehaviorName);
            }
        }

        public void OnEntityCreated(Entity entity)
        {
        }

        public void OnEntityRemoved(Entity entity)
        {
        }

        public void OnEntityParentChanged(Entity entity, Entity? oldParent, Entity? newParent)
        {
        }

        public void OnComponentCreated(Component component)
        {
            if (component is LoadSceneComponent loadSceneComponent)
            {
                if (_loadSceneComponent != null)
                {
                    throw new InvalidOperationException($"Multiple {nameof(LoadSceneComponent)} components are not supported.");
                }

                _loadSceneComponent = loadSceneComponent;
            }
        }

        public void OnComponentRemoved(Component component)
        {
            if (component is LoadSceneComponent loadSceneComponent)
            {
                if (_loadSceneComponent != loadSceneComponent)
                {
                    throw new InvalidOperationException($"Unhandled {nameof(LoadSceneComponent)} instance is being removed.");
                }

                _loadSceneComponent = null;
            }
        }
    }
}