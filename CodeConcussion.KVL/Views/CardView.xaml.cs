using System.Windows;

namespace CodeConcussion.KVL.Views
{
    public partial class CardView
    {
        public CardView()
        {
            InitializeComponent();
        }
        
        #region IsWrong

        public static readonly DependencyProperty IsWrongProperty = DependencyProperty.Register(
            "IsWrong", typeof(bool), typeof(CardView), new PropertyMetadata(default(bool)));

        public bool IsWrong
        {
            get { return (bool)GetValue(IsWrongProperty); }
            set { SetValue(IsWrongProperty, value); }
        }

        #endregion
    }
}