using System.Windows.Media;
using CodeConcussion.KVL.Utilities.Messages;

namespace CodeConcussion.KVL.ViewModels
{
    public sealed class SettingsViewModel : BaseViewModel
    {
        public Color BackgroundColor
        {
            get => GameManager.BackgroundColor;
            set
            {
                GameManager.BackgroundColor = value;
                NotifyOfPropertyChange(() => BackgroundColor);
            }
        }

        public bool PlayErrorSound
        {
            get => GameManager.PlayErrorSound;
            set
            {
                GameManager.PlayErrorSound = value;
                NotifyOfPropertyChange(() => PlayErrorSound);
            }
        }

        public void Close()
        {
            PublishMessage(MessageType.CloseSettings);
        }
    }
}