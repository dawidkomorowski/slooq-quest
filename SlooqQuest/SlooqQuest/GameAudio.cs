using Geisha.Engine.Audio;
using Geisha.Engine.Audio.Backend;
using Geisha.Engine.Core.Assets;
using SlooqQuest.Assets;

namespace SlooqQuest
{
    internal sealed class GameAudio
    {
        private readonly IAudioPlayer _audioPlayer;
        private readonly IAssetStore _assetStore;

        private AssetId _currentMusic;
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
            if (_currentMusic == assetId)
            {
                return;
            }

            _currentMusic = assetId;

            _musicPlayback?.Dispose();

            _musicPlayback = _audioPlayer.Play(_assetStore.GetAsset<ISound>(assetId));
            _musicPlayback.Stopped += (sender, args) => _musicPlayback.Play();
        }
    }
}