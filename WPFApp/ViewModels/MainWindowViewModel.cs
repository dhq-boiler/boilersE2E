using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using WPFApp.Views;

namespace WPFApp.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private CompositeDisposable disposables = new CompositeDisposable();
        public ReadOnlyReactivePropertySlim<string> Fomula { get; }

        public class FomulaList : ReactiveCollection<string>
        {
            internal void RemoveLastChar()
            {
                var last = this.Last();
                RemoveLastString();
                var newItem = last.Substring(0, last.Length - 1);
                Add(newItem);
            }

            internal void Initialize()
            {
                Clear();
                Add(string.Empty);
            }

            internal void RemoveLastString()
            {
                if (this.Count() < 1)
                    return;
                if (IfPreviousCharIsOperator())
                    return;
                RemoveAt(this.Count() - 1);
            }

            internal void AppendChar(char v)
            {
                var last = this.Last();
                RemoveLastString();
                var newItem = last + v;
                Add(newItem);
            }

            internal void ReplaceLastString(string value)
            {
                if (!IfPreviousCharIsOperator())
                RemoveLastString();
                Add(value);
            }

            private bool IfPreviousCharIsOperator()
            {
                if (this.Count() == 0)
                {
                    return false;
                }
                return this.Last().LastOrDefault() == '+'
                    || this.Last().LastOrDefault() == '-'
                    || this.Last().LastOrDefault() == '×'
                    || this.Last().LastOrDefault() == '÷';
            }
        }

        public FomulaList FomulaListObj { get; } = new FomulaList();
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
            Fomula = FomulaListObj.ObserveProperty(x => x.Count).Select(_ => string.Join(string.Empty, FomulaListObj)).ToReadOnlyReactivePropertySlim();
            ContentRenderedCommand.Subscribe(() =>
            {
                var window = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w is MainWindow);
                var mw = (MainWindow)window;
                mw.display.Focus();
                mw.display.SelectAll();
            })
            .AddTo(disposables);
            FomulaListObj.Clear();
            Display.Value = "0";
            clearFlag = true;
            DisplayTextChangedCommand.Subscribe(e =>
            {
                if (equalFlag)
                    return;
                var textbox = e.Source as TextBox;
                if (ctrlV)
                {
                    FomulaListObj.Append(textbox.Text);
                }
                else
                {
                    FomulaListObj.ReplaceLastString(textbox.Text);
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
                FomulaListObj.RemoveLastChar();
            })
            .AddTo(disposables);
            ClearEntryCommand.Subscribe(() =>
            {
                FomulaListObj.Initialize();
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
                FomulaListObj.RemoveLastString();
                ctrlV = false;
                Display.Value = "0";
                clearFlag = true;
                equalFlag = false;
            })
            .AddTo(disposables);
            DivideCommand.Subscribe(() =>
            {
                RemovePreviousAddedOperatorIfPreviousCharIsOperator();
                FomulaListObj.Add("÷");
                ctrlV = false;
                clearFlag = true;
                equalFlag = false;
            })
            .AddTo(disposables);
            MultipleCommand.Subscribe(() =>
            {
                RemovePreviousAddedOperatorIfPreviousCharIsOperator();
                FomulaListObj.Add("×");
                ctrlV = false;
                clearFlag = true;
                equalFlag = false;
            })
            .AddTo(disposables);
            MinusCommand.Subscribe(() =>
            {
                RemovePreviousAddedOperatorIfPreviousCharIsOperator();
                FomulaListObj.Add("-");
                ctrlV = false;
                clearFlag = true;
                equalFlag = false;
            })
            .AddTo(disposables);
            PlusCommand.Subscribe(() =>
            {
                RemovePreviousAddedOperatorIfPreviousCharIsOperator();
                FomulaListObj.Add("+");
                ctrlV = false;
                clearFlag = true;
                equalFlag = false;
            })
            .AddTo(disposables);
            PlusMinusCommand.Subscribe(() =>
            {
                var temp = Display.Value;
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
                FomulaListObj.ReplaceLastString(Display.Value);
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
                if (string.IsNullOrEmpty(result.ToString()))
                {
                    return default(T);
                }
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
                FomulaListObj.RemoveLastChar();
            }
        }

        private bool IfPreviousCharIsOperator()
        {
            if (Fomula.Value.Length == 0)
            {
                return false;
            }
            return Fomula.Value.Last() == '+'
                || Fomula.Value.Last() == '-'
                || Fomula.Value.Last() == '×'
                || Fomula.Value.Last() == '÷';
        }
    }
}
