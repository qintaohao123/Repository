using System.Collections.ObjectModel;
using System.Windows;

namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        #region field

        public static DependencyProperty TipsProperty = DependencyProperty.Register("Tips", typeof(ObservableCollection<string>), typeof(MainWindow));

        public ObservableCollection<string> Tips
        {
            get { return (ObservableCollection<string>)GetValue(TipsProperty); }
            set
            {
                SetValue(TipsProperty, value);
            }
        }
        #endregion
        public MainWindow()
        {
            InitializeComponent();
            Tips = new ObservableCollection<string>()
              {
                "我是外国人，但是我用Navigator2",
                "如何使用Navigator2发送命令",
                "奶粉俱乐部的网址是",
                "输入窗口的快捷键"
              };


        }
    }
}
