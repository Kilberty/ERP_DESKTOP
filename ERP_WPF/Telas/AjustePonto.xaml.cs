using ERP_WPF.Models;
using System.Collections.Generic;
using System.Windows;

namespace ERP_WPF.Telas
{
    public partial class AjustePonto : Window
    {
        public AjustePonto(List<Ponto> pontos)
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            datagridAjuste.ItemsSource = pontos;
        }

        private void SelectAllCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            SetAllCheckboxes(true);
        }

        private void SelectAllCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            SetAllCheckboxes(false);
        }

        private void SetAllCheckboxes(bool isChecked)
        {
            foreach (var item in datagridAjuste.Items)
            {
                if (item is Ponto ponto)
                {
                    ponto.IsSelected = isChecked;
                }
            }

            datagridAjuste.Items.Refresh();
        }
    }
}