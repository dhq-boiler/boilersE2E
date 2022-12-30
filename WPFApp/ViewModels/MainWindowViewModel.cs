using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reactive.Disposables;
using System.Windows.Controls;
using System;
using Windows.Graphics;
using System.Windows.Input;
using System.Windows;
using WPFApp.Views;
using System.ComponentModel.Design;

namespace WPFApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private CompositeDisposable disposables = new CompositeDisposable();
        public ReactivePropertySlim<string> Fomula { get; } = new ReactivePropertySlim<string>();
        public ReactivePropertySlim<string> Display { get; } = new ReactivePropertySlim<string>();

        public ReactiveCommand BackSpaceCommand { get; } = new ReactiveCommand();
        public ReactiveCommand ClearEntryCommand { get; } = new ReactiveCommand();
        public ReactiveCommand ClearCommand { get; } = new ReactiveCommand();
        public ReactiveCommand DivideCommand { get; } = new ReactiveCommand();
        public ReactiveCommand MultipleCommand { get; } = new ReactiveCommand();
        public ReactiveCommand MinusCommand { get; } = new ReactiveCommand();
        public ReactiveCommand PlusCommand { get; } = new ReactiveCommand();
        public ReactiveCommand PlusMinusCommand { get; } = new ReactiveCommand();
        public ReactiveCommand EqualCommand { get; } = new ReactiveCommand();
        public ReactiveCommand OneCommand { get; } = new ReactiveCommand();
        public ReactiveCommand TwoCommand { get; } = new ReactiveCommand();
        public ReactiveCommand ThreeCommand { get; } = new ReactiveCommand();
        public ReactiveCommand FourCommand { get; } = new ReactiveCommand();
        public ReactiveCommand FiveCommand { get; } = new ReactiveCommand();
        public ReactiveCommand SixCommand { get; } = new ReactiveCommand();
        public ReactiveCommand SevenCommand { get; } = new ReactiveCommand();
        public ReactiveCommand EightCommand { get; } = new ReactiveCommand();
        public ReactiveCommand NineCommand { get; } = new ReactiveCommand();
        public ReactiveCommand ZeroCommand { get; } = new ReactiveCommand();
        public ReactiveCommand DotCommand { get; } = new ReactiveCommand();
        public ReactiveCommand<TextChangedEventArgs> DisplayTextChangedCommand { get; } = new ReactiveCommand<TextChangedEventArgs>();
        public ReactiveCommand<KeyEventArgs> DisplayKeyDownCommand { get; } = new ReactiveCommand<KeyEventArgs>();
        public ReactiveCommand ContentRenderedCommand { get; } = new ReactiveCommand();

        private bool clearFlag = true;
        private bool equalFlag = false;
        private bool ctrlV = false;

        public MainWindowViewModel()
        {
            ContentRenderedCommand.Subscribe(() =>
            {
                var window = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w is MainWindow);
                var mw = (MainWindow)window;
                mw.display.Focus();
                mw.display.SelectAll();
            })
            .AddTo(disposables);
            Fomula.Value = string.Empty;
            Display.Value = "0";
            clearFlag = true;
            DisplayTextChangedCommand.Subscribe(e =>
            {
                if (equalFlag)
                    return;
                var textbox = e.Source as TextBox;
                if (ctrlV)
                {
                    Fomula.Value += textbox.Text;
                }
                else
                {
                    if (textbox.Text.Length - 1 >= 0)
                    {
                        Fomula.Value += Display.Value.Substring(Display.Value.IndexOf(textbox.Text) + textbox.Text.Length - 1);
                    }
                }
            })
            .AddTo(disposables);
            DisplayKeyDownCommand.Subscribe(e =>
            {
                ctrlV = ((Keyboard.Modifiers & ModifierKeys.Control) == ModifierKeys.Control) && e.Key == Key.V;
            })
            .AddTo(disposables);
            BackSpaceCommand.Subscribe(() =>
            {
                if (Display.Value == "0")
                    return;
                Display.Value = Display.Value.Substring(0, Display.Value.Length - 1);
                if (Display.Value.Length == 0)
                {
                    Display.Value = "0";
                    clearFlag = true;
                }
                Fomula.Value = Fomula.Value.Substring(0, Fomula.Value.Length - 1);
            })
            .AddTo(disposables);
            ClearEntryCommand.Subscribe(() =>
            {
                Fomula.Value = string.Empty;
                ctrlV = false;
                equalFlag = true;
                Display.Value = "0";
                clearFlag = true;
                equalFlag = false;
            })
            .AddTo(disposables);
            ClearCommand.Subscribe(() =>
            {
                if (Fomula.Value.Length == 0)
                    return;
                Fomula.Value = Fomula.Value.Substring(0, Fomula.Value.LastIndexOf(Display.Value));
                ctrlV = false;
                Display.Value = "0";
                clearFlag = true;
                equalFlag = false;
            })
            .AddTo(disposables);
            DivideCommand.Subscribe(() =>
            {
                RemovePreviousAddedOperatorIfPreviousCharIsOperator();
                Fomula.Value += "÷";
                ctrlV = false;
                clearFlag = true;
                equalFlag = false;
            })
            .AddTo(disposables);
            MultipleCommand.Subscribe(() =>
            {
                RemovePreviousAddedOperatorIfPreviousCharIsOperator();
                Fomula.Value += "×";
                ctrlV = false;
                clearFlag = true;
                equalFlag = false;
            })
            .AddTo(disposables);
            MinusCommand.Subscribe(() =>
            {
                RemovePreviousAddedOperatorIfPreviousCharIsOperator();
                Fomula.Value += "-";
                ctrlV = false;
                clearFlag = true;
                equalFlag = false;
            })
            .AddTo(disposables);
            PlusCommand.Subscribe(() =>
            {
                RemovePreviousAddedOperatorIfPreviousCharIsOperator();
                Fomula.Value += "+";
                ctrlV = false;
                clearFlag = true;
                equalFlag = false;
            })
            .AddTo(disposables);
            PlusMinusCommand.Subscribe(() =>
            {
                var temp = Display.Value;
                Fomula.Value = Fomula.Value.Substring(0, Fomula.Value.LastIndexOf(Display.Value));
                Display.Value = "0";
                if (temp.StartsWith("-"))
                {
                    Display.Value = temp.Substring(1);
                }
                else if (temp.StartsWith("(-"))
                {
                    Display.Value = temp.Substring(2, temp.Substring(2).Length - 1);
                }
                else
                {
                    if (IfPreviousCharIsOperator())
                    {
                        Display.Value = $"(-{temp})";
                    }
                    else
                    {
                        Display.Value = $"-{temp}";
                    }
                }
                Fomula.Value += Display.Value;
            })
            .AddTo(disposables);
            EqualCommand.Subscribe(() =>
            {
                ctrlV = false;
                equalFlag = true;
                Display.Value = StrCalc<double>(Fomula.Value.Replace("×", "*").Replace("÷", "/")).ToString();
                equalFlag = false;
            })
            .AddTo(disposables);
            OneCommand.Subscribe(() =>
            {
                ClearDisplayIfFlagIsTrue();
                ctrlV = false;
                equalFlag = false;
                Display.Value += "1";
            })
            .AddTo(disposables);
            TwoCommand.Subscribe(() =>
            {
                ClearDisplayIfFlagIsTrue();
                ctrlV = false;
                equalFlag = false;
                Display.Value += "2";
            })
            .AddTo(disposables);
            ThreeCommand.Subscribe(() =>
            {
                ClearDisplayIfFlagIsTrue();
                ctrlV = false;
                equalFlag = false;
                Display.Value += "3";
            })
            .AddTo(disposables);
            FourCommand.Subscribe(() =>
            {
                ClearDisplayIfFlagIsTrue();
                ctrlV = false;
                equalFlag = false;
                Display.Value += "4";
            })
            .AddTo(disposables);
            FiveCommand.Subscribe(() =>
            {
                ClearDisplayIfFlagIsTrue();
                ctrlV = false;
                equalFlag = false;
                Display.Value += "5";
            })
            .AddTo(disposables);
            SixCommand.Subscribe(() =>
            {
                ClearDisplayIfFlagIsTrue();
                ctrlV = false;
                equalFlag = false;
                Display.Value += "6";
            })
            .AddTo(disposables);
            SevenCommand.Subscribe(() =>
            {
                ClearDisplayIfFlagIsTrue();
                ctrlV = false;
                equalFlag = false;
                Display.Value += "7";
            })
            .AddTo(disposables);
            EightCommand.Subscribe(() =>
            {
                ClearDisplayIfFlagIsTrue();
                ctrlV = false;
                equalFlag = false;
                Display.Value += "8";
            })
            .AddTo(disposables);
            NineCommand.Subscribe(() =>
            {
                ClearDisplayIfFlagIsTrue();
                ctrlV = false;
                equalFlag = false;
                Display.Value += "9";
            })
            .AddTo(disposables);
            ZeroCommand.Subscribe(() =>
            {
                ClearDisplayIfFlagIsTrue();
                ctrlV = false;
                equalFlag = false;
                Display.Value += "0";
            })
            .AddTo(disposables);
            DotCommand.Subscribe(() =>
            {
                if (int.TryParse(Display.Value, out int i))
                {
                    Fomula.Value += ".";
                    Display.Value += ".";
                    ctrlV = false;
                    equalFlag = false;
                }
            })
            .AddTo(disposables);
        }
        
        /// <summary>
        /// 引用：@mizu-kazu [C#] eval風 文字列式を演算する
        /// https://qiita.com/mizu-kazu/items/e75e5cd8c91dbf34d44c
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="s"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private static T StrCalc<T>(string s, params object[] args)
        {
            using (DataTable dt = new DataTable())
            {
                s = string.Format(s, args);
                object result = dt.Compute(s, "");
                var converter = TypeDescriptor.GetConverter(typeof(T));
                return (T)converter.ConvertFromString(result.ToString());
            }
        }

        private void ClearDisplayIfFlagIsTrue()
        {
            if (clearFlag)
            {
                Display.Value = string.Empty;
                clearFlag = false;
            }
        }

        private void RemovePreviousAddedOperatorIfPreviousCharIsOperator()
        {
            if (IfPreviousCharIsOperator())
            {
                Fomula.Value = Fomula.Value.Substring(0, Fomula.Value.Length - 1);
            }
        }

        private bool IfPreviousCharIsOperator()
        {
            return Fomula.Value.Last() == '+'
                || Fomula.Value.Last() == '-'
                || Fomula.Value.Last() == '×'
                || Fomula.Value.Last() == '÷';
        }
    }
}
