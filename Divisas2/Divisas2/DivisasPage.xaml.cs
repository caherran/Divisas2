using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Divisas2
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class DivisasPage : ContentPage
	{
        private ExchangeRates ExchangeRates;

        public DivisasPage ()
		{
			InitializeComponent();

            sourcePicker.Items.Add("Coronas Danesas");
            sourcePicker.Items.Add("Dolares Canadienses");
            sourcePicker.Items.Add("Dolares Estadounidenses");
            sourcePicker.Items.Add("Euros");
            sourcePicker.Items.Add("Francos Suizos");
            sourcePicker.Items.Add("Libras Esterlinas");
            sourcePicker.Items.Add("Pesos Chilenos");
            sourcePicker.Items.Add("Pesos Colombianos");
            sourcePicker.Items.Add("Pesos Mexicanos");
            sourcePicker.Items.Add("Reales Brasileros");
            sourcePicker.Items.Add("Rupias Indias");
            sourcePicker.Items.Add("Yenes Japoneses");

            targetPicker.Items.Add("Coronas Danesas");
            targetPicker.Items.Add("Dolares Canadienses");
            targetPicker.Items.Add("Dolares Estadounidenses");
            targetPicker.Items.Add("Euros");
            targetPicker.Items.Add("Francos Suizos");
            targetPicker.Items.Add("Libras Esterlinas");
            targetPicker.Items.Add("Pesos Chilenos");
            targetPicker.Items.Add("Pesos Colombianos");
            targetPicker.Items.Add("Pesos Mexicanos");
            targetPicker.Items.Add("Reales Brasileros");
            targetPicker.Items.Add("Rupias Indias");
            targetPicker.Items.Add("Yenes Japoneses");

            this.LoadRates();

            convertButton.Clicked += ConvertButton_Clicked;
        }

        private async void ConvertButton_Clicked(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(amountEntry.Text))
            {
                await DisplayAlert("Error", "Debe ingresar un valor", "Aceptar");
                amountEntry.Focus();
                return;
            }

            if(sourcePicker.SelectedIndex == -1)
            {
                await DisplayAlert("Error", "Debe seleccionar una moneda origen", "Aceptar");
                sourcePicker.Focus();
                return;
            }

            if (targetPicker.SelectedIndex == -1)
            {
                await DisplayAlert("Error", "Debe seleccionar una moneda destino", "Aceptar");
                targetPicker.Focus();
                return;
            }

            var amount = decimal.Parse(amountEntry.Text);
            var amountConverted = this.Convert(amount, sourcePicker.SelectedIndex, targetPicker.SelectedIndex);

            convertedLabel.Text = string.Format("{0:C2} {1} = {2:C2} {3}", 
                                        amount, 
                                        sourcePicker.Items[sourcePicker.SelectedIndex], 
                                        amountConverted, 
                                        targetPicker.Items[targetPicker.SelectedIndex]);
        }

        private decimal Convert(decimal amount, int source, int target)
        {
            var rateSource = this.GetRate(source);
            var rateTarget = this.GetRate(target);

            return (amount / (decimal)rateSource) * (decimal)rateTarget;
        }

        private double GetRate(int index)
        {
            switch (index)
            {
                case 0: return ExchangeRates.Rates.DKK;
                case 1: return ExchangeRates.Rates.CAD;
                case 2: return ExchangeRates.Rates.USD;
                case 3: return ExchangeRates.Rates.EUR;
                case 4: return ExchangeRates.Rates.CHF;
                case 5: return ExchangeRates.Rates.GBP;
                case 6: return ExchangeRates.Rates.CLP;
                case 7: return ExchangeRates.Rates.COP;
                case 8: return ExchangeRates.Rates.MXN;
                case 9: return ExchangeRates.Rates.BRL;
                case 10: return ExchangeRates.Rates.INR;
                case 11: return ExchangeRates.Rates.JPY;
                default: return 1;
            }
        }

        private async void LoadRates()
        {
            waitActivityIndicator.IsRunning = true;
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://openexchangerates.org");
                var url = "/api/latest.json?app_id=8cee4ae2e99c4b57bd554c010d1d1c19";
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();
                this.ExchangeRates = JsonConvert.DeserializeObject<ExchangeRates>(result);

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message, "Aceptar");
                throw;
            }
            waitActivityIndicator.IsRunning = false;
        }
    }
}