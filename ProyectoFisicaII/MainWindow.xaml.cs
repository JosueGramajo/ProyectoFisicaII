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

namespace ProyectoFisicaII
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        String distancia = " mt(s)";
        String dimensional = "C";

        public MainWindow()
        {
            InitializeComponent();

            btnCalcular.Visibility = Visibility.Hidden;
            btnReingresar.Visibility = Visibility.Hidden;
        }
        
        private void q1_carga_DropDownClosed(object sender, EventArgs e)
        {
            ComboBoxItem selected = (ComboBoxItem)q1_carga.SelectedValue;

            
            if (selected.Content.Equals("Positivo"))
            {
                q1_img.Source = new BitmapImage(new Uri("Content/assets/images/positiveChargeIcon.png", UriKind.Relative));
            }else
            {
                q1_img.Source = new BitmapImage(new Uri("Content/assets/images/negativeChargeIcon.png", UriKind.Relative));
            }
        }

        private void q2_carga_DropDownClosed(object sender, EventArgs e)
        {
            ComboBoxItem selected = (ComboBoxItem)q2_carga.SelectedValue;


            if (selected.Content.Equals("Positivo"))
            {
                q2_img.Source = new BitmapImage(new Uri("Content/assets/images/positiveChargeIcon.png", UriKind.Relative));
            }
            else
            {
                q2_img.Source = new BitmapImage(new Uri("Content/assets/images/negativeChargeIcon.png", UriKind.Relative));
            }

        }

        private float notationResult(float value) {
            float result = value;
            switch(dimensional){
                case "C":
                    result = value;
                    break;
                case "mC":
                    result = value * ((float)Math.Pow(10,-3));
                    break;
                case "µC":
                    result = value * ((float)Math.Pow(10, -6));
                    break;
                case "nC":
                    result = value * ((float)Math.Pow(10, -9));
                    break;
                default: break;
            }

            return result;
        }

        private float distanceResult(float value) {
            float result = value;
            switch(distancia){
                case "m":
                    result = value;
                    break;
                case "cm":
                    result = (value / 100);
                    break;
                case "in":
                    result = (float)(value * 0.0254);
                    break;
                default: break;
            }
            return result;
        }

        private void btnCalcular_Click(object sender, RoutedEventArgs e)
        {
            float k = 9000000000;
            float q1 = float.Parse(q1_valor.Text.ToString());
            q1 = notationResult(q1);
            float distance1SquarePow = distanceResult(float.Parse(distanciaQ1QP.Text.ToString()));//(float)Math.Pow(double.Parse(distanciaQ1QP.Text.ToString()), 2);
            distance1SquarePow = (float)Math.Pow(distance1SquarePow, 2);
            float e1 = (k * q1) / distance1SquarePow;

            float q2 = float.Parse(q2_valor.Text.ToString());
            q2 = notationResult(q2);
            float distance2SquarePow = distanceResult(float.Parse(distanciaQPQ2.Text.ToString()));
            distance2SquarePow = (float)Math.Pow(distance2SquarePow, 2);
            float e2 = (k * q2) / distance2SquarePow;

            float res = (float)Math.Sqrt((((float)Math.Pow(e1, 2)) + ((float)Math.Pow(e2, 2))));

           // MessageBox.Show("result: "+res.ToString().Replace("E+"," x10^") + " N/c");

            ComboBoxItem selected1 = (ComboBoxItem)q1_carga.SelectedValue;
            String signoQ1 = selected1.Content.ToString();

            ComboBoxItem selected2 = (ComboBoxItem)q2_carga.SelectedValue;
            String signoQ2 = selected2.Content.ToString();

            e1 = signoQ1 == "Negativo" ? (e1 * -1) : e1;

            e2 = signoQ2 == "Negativo" ? (e2 * -1) : e2;

            float angulo = (float)Math.Atan((e1 / e2));

           // MessageBox.Show("Angulo: " + RadianToDegree(angulo) + "°");

            String field = res.ToString().Replace("E+", " x10^") + " N/c";
            String degrees = RadianToDegree(angulo) + "°";

            ResultWindow rs = new ResultWindow(field,degrees,graphicType());
            rs.Show();
        }

        double RadianToDegree(double angle) { return angle * (180.0 / Math.PI); }

        private int graphicType() {
            ComboBoxItem selected1 = (ComboBoxItem)q1_carga.SelectedValue;
            String signoQ1 = selected1.Content.ToString();

            ComboBoxItem selected2 = (ComboBoxItem)q2_carga.SelectedValue;
            String signoQ2 = selected2.Content.ToString();

            if(signoQ1 == "Positivo" && signoQ2 == "Positivo"){
                return 1;
            }
            else if (signoQ1 == "Negativo" && signoQ2 == "Positivo")
            {
                return 2;
            }
            else if (signoQ1 == "Positivo" && signoQ2 == "Negativo")
            {
                return 3;
            }
            else if (signoQ1 == "Negativo" && signoQ2 == "Negativo")
            {
                return 4;
            }
            return 0;
        }

        private void distanciaQ2QPrueba_TextChanged(object sender, TextChangedEventArgs e)
        {
            /*float newVal = 0;

            bool val = float.TryParse(distanciaQPQ2.Text.ToString(), out newVal);

            distancia_q2_qprueba.Content = newVal + distancia;
            distanciaQPQ2.Text = newVal.ToString();*/
        }

        private void distanciaQ1Q2_TextChanged(object sender, TextChangedEventArgs e)
        {
            /*float newVal = 0;
            bool val = float.TryParse(distanciaQ1QP.Text.ToString(), out newVal);

            distancia_qp_q2.Content = newVal + distancia;
            distanciaQ1QP.Text = newVal.ToString();*/
        }

        private void unidadDistancia_DropDownClosed(object sender, EventArgs e)
        {
            ComboBoxItem selected = (ComboBoxItem)unidadDistancia.SelectedValue;


            if (selected.Content.Equals("Metros"))
            {
                distancia = "m";
            }
            else if(selected.Content.Equals("Centimetros"))
            {
                distancia = "cm";
            }else if(selected.Content.Equals("Pulgadas")){
                distancia = "in";
            }

        }

        private void q1_valor_TextChanged(object sender, TextChangedEventArgs e)
        {
           /* String val = q1_valor.Text.ToString();

            if(val != ""){
                if (val.EndsWith("."))
                {
                    if (val.Length == 1)
                    {
                        q1_valor.Text = "0.";
                        q1_lbl.Content = "Carga 1 = 0. c";
                    }
                    else {
                        if (val.Substring(0, val.Length - 1).Contains("."))
                        {
                            q1_valor.Text = val.Substring(0, val.Length - 1);
                        }
                        else
                        {
                            q1_valor.Text = val;
                            q1_lbl.Content = "Carga 1 = " + val + "c";
                        }                    
                    }
                }
                else
                {
                    float newVal = 0;
                    bool bval = float.TryParse(val, out newVal);

                    if (bval == false)
                    {
                        q1_valor.Text = val.Length == 1 ? "" : val.Substring(0, val.Length - 1);
                        q1_lbl.Content = "Carga 1 = " + ((val.Length == 1) ? "" : val.Substring(0, val.Length - 1)) + "c";
                    }
                    else
                    {
                        q1_valor.Text = newVal.ToString();
                        q1_lbl.Content = "Carga 1 = " + newVal.ToString() + "c";
                    }
                }            
            }
            q1_valor.SelectionStart = q1_valor.Text.Length == 0 ? 0 : q1_valor.Text.Length;*/
        }

        private void q2_valor_TextChanged(object sender, TextChangedEventArgs e)
        {
            /*String val = q2_valor.Text.ToString();

            if (val != "")
            {
                if (val.EndsWith("."))
                {
                    if (val.Length == 1)
                    {
                        q2_valor.Text = "0.";

                        q2_lbl.Content = "Carga 1 = 0. c";
                    }
                    else
                    {
                        if (val.Substring(0, val.Length - 1).Contains("."))
                        {
                            q2_valor.Text = val.Substring(0, val.Length - 1);
                        }
                        else
                        {
                            q2_valor.Text = val;
                            q2_lbl.Content = "Carga 1 = " + val + "c";
                        }
                    }
                }
                else
                {
                    float newVal = 0;
                    bool bval = float.TryParse(val, out newVal);

                    if (bval == false)
                    {
                        q2_valor.Text = val.Length == 1 ? "" : val.Substring(0, val.Length - 1);
                        q2_lbl.Content = "Carga 1 = " + ((val.Length == 1) ? "" : val.Substring(0, val.Length - 1)) + "c";
                    }
                    else
                    {
                        q2_valor.Text = newVal.ToString();
                        q2_lbl.Content = "Carga 1 = " + newVal.ToString() + "c";
                    }
                }
            }
            q2_valor.SelectionStart = q2_valor.Text.Length == 0 ? 0 : q2_valor.Text.Length;*/
        }

        private void btnValidar_Click(object sender, RoutedEventArgs e)
        {
            if(!validateEmptyFields()){
                MessageBox.Show("Todos los campos son obligatorios");
                
            }
            else if (!validateNumericFields())
            {
                MessageBox.Show("Los campos solo pueden ser numericos");
            }
            else {
                placeValues();        
            }
        }

        private void placeValues() {
            q1_lbl.Content = "Carga 1 = " + q1_valor.Text.ToString() + dimensional;
            q1_valor.IsEnabled = false;

            q2_lbl.Content = "Carga 2 = " + q2_valor.Text.ToString() + dimensional;
            q2_valor.IsEnabled = false;

            distancia_q1_qp.Content = distanciaQ1QP.Text.ToString() + distancia;
            distanciaQ1QP.IsEnabled = false;

            distancia_qp_q2.Content = distanciaQPQ2.Text.ToString() + distancia;
            distanciaQPQ2.IsEnabled = false;

            btnCalcular.Visibility = Visibility.Visible;
            btnReingresar.Visibility = Visibility.Visible;
            btnValidar.Visibility = Visibility.Hidden;

            cb_Dimensionales.IsEnabled = false;
            unidadDistancia.IsEnabled = false;
            q1_carga.IsEnabled = false;
            q2_carga.IsEnabled = false;
        }

        private bool validateNumericFields() {
            if (!isNumeric(q1_valor.Text.ToString()))
            {
                return false;
            }
            if (!isNumeric(q2_valor.Text.ToString()))
            {
                return false;
            }
            if (!isNumeric(distanciaQ1QP.Text.ToString()))
            {
                return false;
            }
            if (!isNumeric(distanciaQPQ2.Text.ToString()))
            {
                return false;
            }
            return true;
        }

        private bool validateEmptyFields() { 
            if(q1_valor.Text.ToString() == "" 
                || q2_valor.Text.ToString() == "" 
                || distanciaQ1QP.Text.ToString() == ""
                || distanciaQPQ2.Text.ToString() == ""){


                    return false;
            }

            return true;
        }

        private bool isNumeric(String value) {
            try {
                float test = float.Parse(value);
                return true;
            }catch(Exception ex){
                return false;
            }
        }

        private void btnReingresar_Click(object sender, RoutedEventArgs e)
        {
            q1_lbl.Content = "Carga 1";
            q1_valor.Text = "";
            q1_valor.IsEnabled = true;

            q2_lbl.Content = "Carga 2";
            q2_valor.Text = "";
            q2_valor.IsEnabled = true;

            distancia_q1_qp.Content = "Distancia";
            distanciaQ1QP.Text = "";
            distanciaQ1QP.IsEnabled = true;

            distancia_qp_q2.Content ="Distancia";
            distanciaQPQ2.Text = "";
            distanciaQPQ2.IsEnabled = true;

            btnCalcular.Visibility = Visibility.Hidden;
            btnReingresar.Visibility = Visibility.Hidden;
            btnValidar.Visibility = Visibility.Visible;

            cb_Dimensionales.IsEnabled = true;
            unidadDistancia.IsEnabled = true;
            q1_carga.IsEnabled = true;
            q2_carga.IsEnabled = true;
        }

        private void cb_Dimensionales_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selected = (ComboBoxItem)cb_Dimensionales.SelectedValue;
            dimensional = selected.Content.ToString();
        }

    }
}
