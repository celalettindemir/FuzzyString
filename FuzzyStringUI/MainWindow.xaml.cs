using FuzzyString;
using FuzzyString.Enums;
using FuzzyStringUI.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Emit;
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

namespace FuzzyStringUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int number = 0;
        List<KeyModel> _bindingList;
        public MainWindow()
        {
            InitializeComponent();
            _bindingList = new List<KeyModel>()
            {
                new KeyModel{Text="Kapali"},
                new KeyModel{Text="Kagithane"},
                new KeyModel{Text="Kâgithane"},
                new KeyModel{Text="KÂGITHANE"},
                new KeyModel{Text="hastahane"},
                new KeyModel{Text="Kagıthane"}
            };
        }

        private double tolare = 0;
        private void SearchText()
        {

            if (TxtSearch.Text.Length == 0)
            {
                return;
            }
            List<FuzzyStringComparisonOptions> options = new List<FuzzyStringComparisonOptions>();

            // Choose which algorithms should weigh in for the comparison
            options.Add(FuzzyStringComparisonOptions.UseOverlapCoefficient);
            options.Add(FuzzyStringComparisonOptions.UseLongestCommonSubsequence);
            options.Add(FuzzyStringComparisonOptions.UseLongestCommonSubstring);
            options.Add(FuzzyStringComparisonOptions.UseJaccardDistance);

            // Choose the relative strength of the comparison - is it almost exactly equal? or is it just close?
            FuzzyStringComparisonTolerance tolerance = FuzzyStringComparisonTolerance.Normal;

             var result = _bindingList.Where(x => x.Text.ApproximatelyEquals(TxtSearch.Text, tolare, options.ToArray())).ToList();
            searchText.ItemsSource = result;
        }
        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchText();
        }
        private void tolaranceSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            tolare = (tolaranceSlider.Value / 1000);
            TxtTolarance.Text = String.Format("Tolarance: {0:0.###}", (tolaranceSlider.Value/1000));
            SearchText();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (TxtSearch.Text.Length == 0)
            {
                return;
            }
            _bindingList.Add(new KeyModel
            {
                Text = addKeyTxtBox.Text
            });
        }
    }
}
