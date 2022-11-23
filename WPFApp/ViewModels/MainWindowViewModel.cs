using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reactive.Disposables;

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

        private bool clearFlag = true;

        public MainWindowViewModel()
        {
            Fomula.Value = string.Empty;
            Display.Value = "0";
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
                Display.Value = "0";
                clearFlag = true;
            })
            .AddTo(disposables);
            ClearCommand.Subscribe(() =>
            {
                if (Fomula.Value.Length == 0)
                    return;
                Fomula.Value = Fomula.Value.Substring(0, Fomula.Value.LastIndexOf(Display.Value));
                Display.Value = "0";
                clearFlag = true;
            })
            .AddTo(disposables);
            DivideCommand.Subscribe(() =>
            {
                RemovePreviousAddedOperatorIfPreviousCharIsOperator();
                Fomula.Value += "÷";
                clearFlag = true;
            })
            .AddTo(disposables);
            MultipleCommand.Subscribe(() =>
            {
                RemovePreviousAddedOperatorIfPreviousCharIsOperator();
                Fomula.Value += "×";
                clearFlag = true;
            })
            .AddTo(disposables);
            MinusCommand.Subscribe(() =>
            {
                RemovePreviousAddedOperatorIfPreviousCharIsOperator();
                Fomula.Value += "-";
                clearFlag = true;
            })
            .AddTo(disposables);
            PlusCommand.Subscribe(() =>
            {
                RemovePreviousAddedOperatorIfPreviousCharIsOperator();
                Fomula.Value += "+";
                clearFlag = true;
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
                Display.Value = StrCalc<double>(Fomula.Value.Replace("×", "*").Replace("÷", "/")).ToString();
            })
            .AddTo(disposables);
            OneCommand.Subscribe(() =>
            {
                Fomula.Value += "1";
                ClearDisplayIfFlagIsTrue();
                Display.Value += "1";
            })
            .AddTo(disposables);
            TwoCommand.Subscribe(() =>
            {
                Fomula.Value += "2";
                ClearDisplayIfFlagIsTrue();
                Display.Value += "2";
            })
            .AddTo(disposables);
            ThreeCommand.Subscribe(() =>
            {
                Fomula.Value += "3";
                ClearDisplayIfFlagIsTrue();
                Display.Value += "3";
            })
            .AddTo(disposables);
            FourCommand.Subscribe(() =>
            {
                Fomula.Value += "4";
                ClearDisplayIfFlagIsTrue();
                Display.Value += "4";
            })
            .AddTo(disposables);
            FiveCommand.Subscribe(() =>
            {
                Fomula.Value += "5";
                ClearDisplayIfFlagIsTrue();
                Display.Value += "5";
            })
            .AddTo(disposables);
            SixCommand.Subscribe(() =>
            {
                Fomula.Value += "6";
                ClearDisplayIfFlagIsTrue();
                Display.Value += "6";
            })
            .AddTo(disposables);
            SevenCommand.Subscribe(() =>
            {
                Fomula.Value += "7";
                ClearDisplayIfFlagIsTrue();
                Display.Value += "7";
            })
            .AddTo(disposables);
            EightCommand.Subscribe(() =>
            {
                Fomula.Value += "8";
                ClearDisplayIfFlagIsTrue();
                Display.Value += "8";
            })
            .AddTo(disposables);
            NineCommand.Subscribe(() =>
            {
                Fomula.Value += "9";
                ClearDisplayIfFlagIsTrue();
                Display.Value += "9";
            })
            .AddTo(disposables);
            ZeroCommand.Subscribe(() =>
            {
                Fomula.Value += "0";
                ClearDisplayIfFlagIsTrue();
                Display.Value += "0";
            })
            .AddTo(disposables);
            DotCommand.Subscribe(() =>
            {
                if (int.TryParse(Display.Value, out int i))
                {
                    Fomula.Value += ".";
                    Display.Value += ".";
                    clearFlag = false;
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
