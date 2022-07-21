using Geisha.Engine.Audio;
using Geisha.Engine.Audio.Backend;
using Geisha.Engine.Core.Assets;
using Sokoban.Assets;

namespace Sokoban
{
    internal sealed class GameAudio
    {
        private readonly IAudioPlayer _audioPlayer;
        private readonly IAssetStore _assetStore;

        private IPlayback? _musicPlayback;

        public GameAudio(IAudioBackend audioBackend, IAssetStore assetStore)
        {
            _audioPlayer = audioBackend.AudioPlayer;
            _assetStore = assetStore;
        }

        public void PlayMainMenuMusic()
        {
            PlayMusicLoop(SokobanAssetId.Music.MainMenu);
        }

        private void PlayMusicLoop(AssetId assetId)
        {
            _musicPlayback?.Dispose();

            _musicPlayback = _audioPlayer.Play(_assetStore.GetAsset<ISound>(assetId));
            _musicPlayback.Stopped += (sender, args) => _musicPlayback.Play();
        }
    }
}