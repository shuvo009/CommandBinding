using System;
using System.Collections.Generic;
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

namespace CommandBinding
{
	/// <summary>
	/// Interaction logic for UserControl1.xaml
	/// </summary>
	public partial class UserControl1 : UserControl ,ICommandSource
	{
		public UserControl1()
		{
			this.InitializeComponent();
		}

        public static readonly DependencyProperty commandProperty = DependencyProperty.Register("Command", typeof(ICommand), typeof(UserControl1), new PropertyMetadata(null));

        public static readonly DependencyProperty commpandParameterProperty = DependencyProperty.Register("CommandParameter", typeof(object), typeof(UserControl1), new PropertyMetadata(null));

       // public static readonly DependencyProperty commandTargetProperty = DependencyProperty.Register("CommandTarget", typeof(IInputElement), typeof(UserControl1), new PropertyMetadata(null));

        public ICommand Command
        {
            get { return (ICommand)GetValue(commandProperty); }
            set { SetValue(commandProperty,value); }
        }

        public object CommandParameter
        {
            get { return GetValue(commpandParameterProperty); }
            set { SetValue(commpandParameterProperty, value); }
        }

        public IInputElement CommandTarget
        {
            //get { return (IInputElement)GetValue(commandTargetProperty); }
            //set { SetValue(commandTargetProperty, value); }
            get { return default(IInputElement);}
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);

            var command = Command;
            var parameter = CommandParameter;
            var target = CommandTarget;

            var routedCmd = command as RoutedCommand;
            if (routedCmd != null && routedCmd.CanExecute(parameter, target))
            {
                routedCmd.Execute(parameter, target);
            }
            else if (command != null && command.CanExecute(parameter))
            {
                command.Execute(parameter);
            }

        }
    }
}