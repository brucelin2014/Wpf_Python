using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using Python.Runtime;

namespace Wpf_Python
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                Runtime.PythonDLL = "python311.dll";
                PythonEngine.Initialize();
                using (Py.GIL())
                {
                    // C#代码运行Python脚本
                    using var scope = Py.CreateScope();
                    scope.Exec("print('hello world from python!')");

                    // C#代码调用Python函数
                    var firstInt = 123;
                    var secondInt = 234;
                    using dynamic scope_dy = Py.CreateScope();
                    scope_dy.Exec("def add(a, b): return a + b");
                    var sum = scope_dy.add(firstInt, secondInt);
                    Console.WriteLine($"Sum: {sum}");

                    // 对象互操作
                    scope.Exec("number_list = [1, 2, 3, 4, 5]");
                    var pythonListObj = scope.Eval("number_list");
                    var csharpListObj = pythonListObj.As<int[]>();

                    Console.WriteLine("The numbers from python are:");
                    foreach (var value in csharpListObj)
                    {
                        Console.WriteLine(value);
                    }

                    // 调用第三方库
                    dynamic np = Py.Import("numpy");
                    Console.WriteLine(np.cos(np.pi * 2));

                    dynamic sin = np.sin;
                    Console.WriteLine(sin(5));

                    // 加载py脚本
                    dynamic np2 = Py.Import("PyNet");
                    int r = np2.add(1, 2);
                    Console.WriteLine($"计算结果{r}");

                    np2.open_process();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
