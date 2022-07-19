using Sokoban.Core;

namespace Sokoban.Editor
{
    internal sealed class ModeInfo : IModeInfo
    {
        private readonly EditorState _editorState;

        public ModeInfo(EditorState editorState)
        {
            _editorState = editorState;
        }

        public Mode Mode => _editorState.Mode;
    }
}