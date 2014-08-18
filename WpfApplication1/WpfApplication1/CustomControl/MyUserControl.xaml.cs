using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WpfApplication1.ViewModel;

namespace WpfApplication1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MyUserControl
    {

        #region
        private List<string> tipsList;
        private string inputStr;
        private ViewModel.ViewModel _viewModelDate;
        private bool _isSelectedByKey = false;

        #endregion

        #region

        public static DependencyProperty TipsListProperty = DependencyProperty.Register("TipsList", typeof(ObservableCollection<string>), typeof(MyUserControl));

        public ObservableCollection<string> TipsList
        {
            get { return (ObservableCollection<string>)GetValue(TipsListProperty); }
            set
            {
                SetValue(TipsListProperty, value);
            }
        }

        public static DependencyProperty TextBoxHeightProperty = DependencyProperty.Register("TextBoxHeight", typeof (int), typeof (MyUserControl));

        public int TextBoxHeight
        {
            get { return (int)GetValue(TextBoxHeightProperty); }
            set { SetValue(TextBoxHeightProperty,value); }
        }
        #endregion

        #region delegate

        public delegate bool EnterDelegate(string str);

        public event EnterDelegate EnterEvent;
        #endregion

        public MyUserControl()
        {
            InitializeComponent();
            _viewModelDate = new ViewModel.ViewModel();

            //           TextBox1.DataContext = inputStr;
            _viewModelDate.TextBlocks = new ObservableCollection<TextBlock>();
            _viewModelDate.EventSetPopupDel += new ViewModel.ViewModel.SetPopupDelegate(SetPopupEventMethond);
            DataContext = _viewModelDate;

        }

        void SetPopupEventMethond(bool isOpen)
        {
            _viewModelDate.IsPopup = isOpen;
        }


        #region event function

        private void TextBox1_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (_isSelectedByKey)
            {
                _isSelectedByKey = false;
                _viewModelDate.DoEventSetPopupDel(true);
                return;
            }

            var subList = TipsList.ToList().FindAll(Search);
            _viewModelDate.MyTalkRecord = new TalkRecord() { Message = "dafe", Reply = "dafer" };

            var tmp = TextBox1.Tag.ToString();

            var height = TextBoxHeight;

            _viewModelDate.TextBlocks.Clear();

            if (subList.Count > 0)
            {
                for (int i = 0; i < subList.Count; i++)
                {

                    var textblock = new TextBlock();
                    string[] keywords = _viewModelDate.InputContent.Trim().Split(' ');
                    textblock.Tag = subList[i];
                    textblock.Text = subList[i];
                    foreach (var keyword in keywords)
                    {
                        int index = textblock.Text.IndexOf(keyword, System.StringComparison.Ordinal);
                        textblock.Text = textblock.Text.Replace(keyword, string.Empty);
                        Run run = new Run(keyword, textblock.ContentStart.GetPositionAtOffset(index + 1));
                        run.Foreground = new SolidColorBrush(Colors.Red);
                    }

                    _viewModelDate.TextBlocks.Add(textblock);


                }

                //                _viewModelDate.IsPopup = true;
                _viewModelDate.DoEventSetPopupDel(true);
            }
            else
            {
                //                _viewModelDate.IsPopup = false;
                _viewModelDate.DoEventSetPopupDel(false);
            }
        }

        #endregion

        #region

        private bool Search(string str)
        {
            if (string.IsNullOrEmpty(_viewModelDate.InputContent.Trim()))
            {
                return false;
            }

            string[] keywords = _viewModelDate.InputContent.Trim().Split(' ');

            return keywords.All(str.Contains);
        }

        #endregion

        private void Selector_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (_viewModelDate.TextBlocks.Count < 1)
            {
                return;
            }
            var selectedItem = e.AddedItems[0] as TextBlock;
            if (selectedItem != null)
            {
                _viewModelDate.InputContent = selectedItem.Tag.ToString();
            }
        }

        private void TextBox1_OnPreviewKeyDown(object sender, KeyEventArgs e)
        {

            if (e.Key == Key.Up)
            {
                _isSelectedByKey = true;
                if (tipsListBox.SelectedIndex == -1)
                {
                    tipsListBox.SelectedIndex = 0;
                }
                else
                {
                    if (tipsListBox.SelectedIndex == 0)
                    {
                        tipsListBox.SelectedIndex = tipsListBox.Items.Count - 1;
                    }
                    else
                    {
                        tipsListBox.SelectedIndex--;
                    }
                }

            }

            if (e.Key == Key.Down)
            {
                _isSelectedByKey = true;
                if (tipsListBox.SelectedIndex == -1)
                {
                    tipsListBox.SelectedIndex = 0;
                }
                else
                {
                    if (tipsListBox.SelectedIndex == tipsListBox.Items.Count - 1)
                    {
                        tipsListBox.SelectedIndex = 0;
                    }
                    else
                    {
                        tipsListBox.SelectedIndex++;
                    }
                }
            }

            if (e.Key == Key.Enter)
            {
                if (EnterEvent != null)
                {
                    EnterEvent(_viewModelDate.InputContent);
                }
            }
        }
    }
}
