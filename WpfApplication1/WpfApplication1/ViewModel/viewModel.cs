using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows;
using System.Windows.Controls;

namespace WpfApplication1.ViewModel
{
    class ViewModel:DependencyObject
    {

        public static DependencyProperty IsPopupProperty = DependencyProperty.Register("IsPopup", typeof(bool), typeof(ViewModel));

        public bool IsPopup
        {
            get { return (bool)GetValue(IsPopupProperty); }
            set
            {
                SetValue(IsPopupProperty, value);
            }
        }
        
        public static DependencyProperty TextBlocksProperty = DependencyProperty.Register("TextBlocks", typeof(ObservableCollection<TextBlock>), typeof(ViewModel));

        public ObservableCollection<TextBlock> TextBlocks
        {
            get { return (ObservableCollection<TextBlock>)GetValue(TextBlocksProperty); }
            set
            {
                SetValue(TextBlocksProperty, value);
            }
        }


        public static DependencyProperty InputContentProperty = DependencyProperty.Register("InputContent", typeof(string), typeof(ViewModel));

        public string InputContent
        {
            get { return (string)GetValue(InputContentProperty); }
            set
            {
                SetValue(InputContentProperty, value);
            }
        }

        public static DependencyProperty MyTalkRecordProperty = DependencyProperty.Register("MyTalkRecord",
            typeof (TalkRecord), typeof (ViewModel));

        public TalkRecord MyTalkRecord
        {
            get { return (TalkRecord) GetValue(MyTalkRecordProperty); }
            set { SetValue(MyTalkRecordProperty, value); }
        }
        
        public delegate void SetPopupDelegate(bool isOpen);

        public SetPopupDelegate setPopupDel;

        public event SetPopupDelegate EventSetPopupDel;

        public void DoEventSetPopupDel(bool isOpen)
        {
            EventSetPopupDel(isOpen);

        } 

    }

    public class TalkRecord
    {
       public string Message { set; get; }
        public string Reply { set; get; }

    }
}
