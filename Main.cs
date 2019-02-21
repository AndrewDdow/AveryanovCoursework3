using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using ZedGraph;

namespace AveryanovCoursework3
{
    public partial class Main : Form
    {
        private Formules formules = new Formules();
        private double M0 = 2, l = 1000, E = 190000, yieldStrengthMax = 180, density = 7.8, h = 60, b = 40, d = 20, M_example_min = 0, M_example_max = 2, h_0 =60, b_0 = 40, d_0 = 20, mass_full, mass_empty;
        public Main()
        {
            InitializeComponent();
            tb_Mo.Text = tb_Mo_3_max.Text = Convert.ToString(M0);
            tb_Mo_2_min.Text = tb_Mo_3_min.Text = tb_Mo_4_min.Text = Convert.ToString(M_example_min);
            tb_Mo_2_max.Text = tb_Mo_3_max.Text = tb_Mo_4_max.Text = Convert.ToString(M_example_max);
            tb_l_0.Text =  tb_l.Text = Convert.ToString(l);
            tb_E.Text =  Convert.ToString(E);
            tb_yieldStrengthMax.Text =  Convert.ToString(yieldStrengthMax);
            tb_density.Text =  Convert.ToString(density);
            tb_h_0.Text =  tb_h_1.Text = tb_h_3.Text = tb_h_5.Text = tb_h.Text = Convert.ToString(h);
            tb_b_0.Text =  tb_b_1.Text = tb_b_3.Text = tb_b_4.Text = tb_b.Text = Convert.ToString(b);
            tb_d_0.Text = tb_d_4.Text = tb_d.Text = Convert.ToString(d);
            tb_h_min.Text = Convert.ToString(0);
            tb_h_max.Text = Convert.ToString(h_0 * 2);
            tb_h_result.Text = tb_b_result.Text = tb_d_result.Text = tb_m_result.Text = "";
            zedGraphControl1.GraphPane.Title.Text = "Q";
            zedGraphControl1.GraphPane.XAxis.Title.Text = "Ось Z [м]";
            zedGraphControl1.GraphPane.YAxis.Title.Text = "Ось W [кН]";
            zedGraphControl2.GraphPane.Title.Text = "M";
            zedGraphControl2.GraphPane.XAxis.Title.Text = "Ось Z [м]";
            zedGraphControl2.GraphPane.YAxis.Title.Text = "Ось W [кН * м]";
            zedGraphControl1.GraphPane.Title.Text = "Q";
            zedGraphControl1.GraphPane.XAxis.Title.Text = "Ось Z [м]";
            zedGraphControl1.GraphPane.YAxis.Title.Text = "Ось W [кН]";
            zedGraphControl2.GraphPane.Title.Text = "M";
            zedGraphControl2.GraphPane.XAxis.Title.Text = "Ось Z [м]";
            zedGraphControl2.GraphPane.YAxis.Title.Text = "Ось W [кН * м]";
            zedGraphControl3.GraphPane.Title.Text = "График W(z)";
            zedGraphControl3.GraphPane.XAxis.Title.Text = "Ось Z [м]";
            zedGraphControl3.GraphPane.YAxis.Title.Text = "Ось W [мм]";
            zedGraphControl4.GraphPane.Title.Text = "График θ(z)";
            zedGraphControl4.GraphPane.XAxis.Title.Text = "Ось Z [м]";
            zedGraphControl4.GraphPane.YAxis.Title.Text = "Ось W [рад]";
            zedGraphControl5.GraphPane.Title.Text = "График W(z)";
            zedGraphControl5.GraphPane.XAxis.Title.Text = "Ось Z [м]";
            zedGraphControl5.GraphPane.YAxis.Title.Text = "Ось W [мм]";
            zedGraphControl6.GraphPane.Title.Text = "График θ(z)";
            zedGraphControl6.GraphPane.XAxis.Title.Text = "Ось Z [м]";
            zedGraphControl6.GraphPane.YAxis.Title.Text = "Ось W [рад]";
            zedGraphControl7.GraphPane.Title.Text = "График f()";
            zedGraphControl7.GraphPane.XAxis.Title.Text = "Ось Mo [Н * мм]";
            zedGraphControl7.GraphPane.YAxis.Title.Text = "Ось f [мм]";
            zedGraphControl8.GraphPane.Title.Text = "График σ() (сплошное сечение)";
            zedGraphControl8.GraphPane.XAxis.Title.Text = "Ось Mo [Н * мм]";
            zedGraphControl8.GraphPane.YAxis.Title.Text = "Ось σ [МПа]";
            zedGraphControl9.GraphPane.Title.Text = "График σ() (полое сечение)";
            zedGraphControl9.GraphPane.XAxis.Title.Text = "Ось Mo [Н * мм]";
            zedGraphControl9.GraphPane.YAxis.Title.Text = "Ось σ [МПа]";
            zedGraphControl10.GraphPane.Title.Text = "График B(H)";
            zedGraphControl10.GraphPane.XAxis.Title.Text = "Ось H [мм]";
            zedGraphControl10.GraphPane.YAxis.Title.Text = "Ось B [мм]";

        }
        private void draw_1_Click(object sender, EventArgs e)
        {
            zedGraphControl1.GraphPane.CurveList.Clear();
            zedGraphControl2.GraphPane.CurveList.Clear();
            PointPairList list = new PointPairList(), list1 = new PointPairList();
            for (double z = 0; z <= l; z += l / 50)
            {
                list.Add(z / 1000, formules.Q());
                list1.Add(z / 1000, formules.M(M0));
            }
            LineItem MyLine = zedGraphControl1.GraphPane.AddCurve("Q max " + Convert.ToString(formules.Q_max()), list, Color.Black, SymbolType.None);
            LineItem MyLine1 = zedGraphControl2.GraphPane.AddCurve("M max " + Convert.ToString(formules.M_max(M0)), list1, Color.Black, SymbolType.None);
            MyLine.Line.Fill = MyLine1.Line.Fill = new Fill(Color.Black);
            MyLine.Symbol.IsVisible = MyLine1.Symbol.IsVisible = false;
            zedGraphControl1.RestoreScale(zedGraphControl1.GraphPane);
            zedGraphControl2.RestoreScale(zedGraphControl2.GraphPane);
        }
        private void zedGraphControl2_ZoomEvent(ZedGraphControl sender, ZoomState oldState, ZoomState newState)
        {
            GraphPane pane = sender.GraphPane;
            pane.XAxis.Scale.Min = 0;
            pane.YAxis.Scale.Max = M0 * 1.5;
        }
        private void draw_2_Click(object sender, EventArgs e)
        {
            zedGraphControl3.GraphPane.CurveList.Clear();
            zedGraphControl4.GraphPane.CurveList.Clear();
            PointPairList list = new PointPairList(), list1 = new PointPairList();
            double w_max = 0, w = 0, o_max = 0, o = 0;
            for (double z = 0; z <= l; z += l / 16)
            {
                w = formules.w(M0 , l / 1000, E * Math.Pow(10, 6), formules.Jx(b / 1000, h / 1000), z / 1000) * 1000;
                o = formules.o(M0 * 1000, l / 1000, E * Math.Pow(10, 6), formules.Jx(b / 1000, h / 1000), z / 1000);
                list.Add(z / 1000, w);
                list1.Add(z / 1000, o);
                if (w_max > w) w_max = w;
                if (o > o_max) o_max = o;
            }
            tb_w_max.Text = Convert.ToString(w_max);
            tb_O_max.Text = Convert.ToString(o_max);
            LineItem MyLine = zedGraphControl3.GraphPane.AddCurve("", list, Color.Green, SymbolType.Circle);
            LineItem MyLine1 = zedGraphControl4.GraphPane.AddCurve("", list1, Color.Blue, SymbolType.Circle);
            zedGraphControl3.RestoreScale(zedGraphControl3.GraphPane);
            zedGraphControl4.RestoreScale(zedGraphControl4.GraphPane);
        }
        private void draw_3_Click(object sender, EventArgs e)
        {
            zedGraphControl5.GraphPane.CurveList.Clear();
            zedGraphControl6.GraphPane.CurveList.Clear();
            PointPairList list = new PointPairList(), list1 = new PointPairList();
            double w_max = 0, w = 0, o_max = 0, o = 0;
            for (double z = 0; z <= l; z += l / 16)
            {
                w = formules.w(M0, l / 1000, E * Math.Pow(10, 6), formules.Jx(b / 1000, h / 1000, d / 1000), z / 1000) * 1000;
                o = formules.o(M0 * 1000, l / 1000, E * Math.Pow(10, 6), formules.Jx(b / 1000, h / 1000, d / 1000), z / 1000);
                list.Add(z / 1000, w);
                list1.Add(z / 1000, o);
                if (w_max > w) w_max = w;
                if (o > o_max) o_max = o;
            }
            tb_w_max_1.Text = Convert.ToString(w_max);
            tb_O_max_1.Text = Convert.ToString(o_max);
            LineItem MyLine = zedGraphControl5.GraphPane.AddCurve("", list, Color.Green, SymbolType.Circle);
            LineItem MyLine1 = zedGraphControl6.GraphPane.AddCurve("", list1, Color.Blue, SymbolType.Circle);
            zedGraphControl5.RestoreScale(zedGraphControl5.GraphPane);
            zedGraphControl6.RestoreScale(zedGraphControl6.GraphPane);
        }
        private void draw_4_Click(object sender, EventArgs e)
        {
            PointPairList list = new PointPairList(), list1 = new PointPairList();
            for (double i = M_example_min; i <= M_example_max; i += M_example_max / 50)
            {
                list.Add(i, - formules.f() * i);
                list1.Add(i, - formules.f() * i);
            }
            LineItem MyLine = zedGraphControl7.GraphPane.AddCurve("Сплошное сечение", list, Color.Red, SymbolType.Circle);
            LineItem MyLine1 = zedGraphControl7.GraphPane.AddCurve("Полое сечение", list1, Color.Yellow, SymbolType.Circle);
            zedGraphControl7.RestoreScale(zedGraphControl7.GraphPane);
        }
        private void clear_4_Click(object sender, EventArgs e)
        {
            zedGraphControl7.GraphPane.CurveList.Clear();
            zedGraphControl7.RestoreScale(zedGraphControl7.GraphPane);
        }
        private void draw_5_Click(object sender, EventArgs e)
        {
            PointPairList list = new PointPairList();
            for (double M0 = M_example_min; M0 <= M_example_max; M0 += M_example_max / 50) list.Add(M0, formules.stress(formules.M_max(M0), h / 2 / 1000, formules.Jx(b / 1000, h / 1000)) * Math.Pow(10, -6));
            LineItem MyLine = zedGraphControl8.GraphPane.AddCurve("", list, Color.Red, SymbolType.Circle);
            zedGraphControl8.RestoreScale(zedGraphControl8.GraphPane);
        }
        private void clear_5_Click(object sender, EventArgs e)
        {
            zedGraphControl8.GraphPane.CurveList.Clear();
            zedGraphControl8.RestoreScale(zedGraphControl8.GraphPane);
        }
        private void draw_6_Click(object sender, EventArgs e)
        {
            PointPairList list = new PointPairList();
            for (double M0 = M_example_min; M0 <= M_example_max; M0 += M_example_max / 50) list.Add(M0, formules.stress(formules.M_max(M0), h / 2 / 1000, formules.Jx(b / 1000, h / 1000, d / 1000)) * Math.Pow(10, -6));
            LineItem MyLine = zedGraphControl9.GraphPane.AddCurve("", list, Color.Red, SymbolType.Circle);
            zedGraphControl9.RestoreScale(zedGraphControl9.GraphPane);
        }
        private void clear_6_Click(object sender, EventArgs e)
        {
            zedGraphControl9.GraphPane.CurveList.Clear();
            zedGraphControl9.RestoreScale(zedGraphControl9.GraphPane);
        }
        private void draw_7_Click(object sender, EventArgs e)
        {
            Random random = new Random();
            Color color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            PointPairList list = new PointPairList(), list1 = new PointPairList(), list2 = new PointPairList(), list3 = new PointPairList();
            for (double i = 0; i <= h_0 * 2; i += h_0 / 25) list.Add(i, formules.b(i, b_0, h_0, d_0));
            list1.Add(0, b_0 * 2);
            list1.Add(h_0 * 2, b_0 * 2);
            list1.Add(h_0 * 2, 0);
            list2.Add(h_0, 0);
            list2.Add(h_0, b_0 * 2);
            list3.Add(0, b_0);
            list3.Add(h_0 * 2, b_0);
            LineItem line = zedGraphControl10.GraphPane.AddCurve("", list, color, SymbolType.Circle);
            LineItem line1 = zedGraphControl10.GraphPane.AddCurve("", list1, Color.Black, SymbolType.None);
            LineItem line2 = zedGraphControl10.GraphPane.AddCurve("", list2, Color.Black, SymbolType.None);
            LineItem line3 = zedGraphControl10.GraphPane.AddCurve("", list3, Color.Black, SymbolType.None);
            zedGraphControl10.RestoreScale(zedGraphControl10.GraphPane);
        }

        private void clear_7_Click(object sender, EventArgs e)
        {
            zedGraphControl10.GraphPane.CurveList.Clear();
            zedGraphControl10.RestoreScale(zedGraphControl10.GraphPane);
        }
        private void search_Click(object sender, EventArgs e)
        {
            double b = b_0, mass = formules.mass(l / 1000, b_0 / 1000, h_0 / 1000, density * 1000), bestH, bestB, bestD, bestMass;
            bestH = h_0;
            bestB = b_0;
            bestD = d_0;
            bestMass = mass;
            for (double h = h_0; h <= h_0 * 2; h++)
                for (double d = 0; d < h / 3 && d < b; d++)
                {
                    b = formules.b(h, b_0, h_0, d);
                    if (b < b_0) break;
                    mass = formules.mass(l / 1000, b / 1000, h / 1000, density * 1000, d / 1000);
                    if (bestMass > mass)
                    {
                        bestH = h;
                        bestB = b;
                        bestD = d;
                        bestMass = mass;
                    }
                }
            tb_h_result.Text = Convert.ToString(bestH);
            tb_b_result.Text = Convert.ToString(bestB);
            tb_d_result.Text = Convert.ToString(bestD);
            tb_m_result.Text = Convert.ToString(bestMass);
        }
        private void mass()
        {
            mass_full = formules.mass(l / 1000, b_0 / 1000, h_0 / 1000, density * 1000);
            tb_mass_full.Text = Convert.ToString(mass_full);
            mass_empty = formules.mass(l / 1000, b_0 / 1000, h_0 / 1000, density * 1000, d_0 / 1000);
            tb_mass_empty.Text = Convert.ToString(mass_empty);
        }
        private double checkTextBoxChange(TextBox textBox, double defaultDouble) // Предотвращает ввод не double
        {
            try
            {
                return Convert.ToDouble(textBox.Text);
            }
            catch
            {
                textBox.Text = Convert.ToString(defaultDouble);
                MessageBox.Show("При вводе числа была допущена ошибка (например, была введена буква). Вернулось первоначальное значение '" + defaultDouble + "'.", "Ошибка при заполнении поля");
                return defaultDouble;
            }
        }
        private void tb_Mo_TextChanged(object sender, EventArgs e)
        {
            M0 = M_example_max = checkTextBoxChange(tb_Mo, 2);
        }
        private void tb_l_TextChanged(object sender, EventArgs e)
        {
            l = checkTextBoxChange(tb_l, 4500);
            tb_l_0.Text = tb_l.Text;
        }
        private void tb_h_TextChanged(object sender, EventArgs e)
        {
            h = checkTextBoxChange(tb_h, 175);
            tb_h_1.Text = tb_h_3.Text = tb_h_4.Text = tb_h.Text;
        }
        private void tb_b_TextChanged(object sender, EventArgs e)
        {
            b = checkTextBoxChange(tb_b, 100);
            tb_b_1.Text = tb_b_3.Text = tb_b_4.Text = tb_b.Text;
        }
        private void tb_d_TextChanged(object sender, EventArgs e)
        {
            d = checkTextBoxChange(tb_d, 58);
            tb_d_4.Text = tb_d.Text;
        }
        
        private void tb_h_0_TextChanged(object sender, EventArgs e)
        {
            h_0 = checkTextBoxChange(tb_h_0, 175);
            mass();
            tb_h_max.Text = Convert.ToString(h_0 * 2);
        }
        private void tb_b_0_TextChanged(object sender, EventArgs e)
        {
            b_0 = checkTextBoxChange(tb_b_0, 100);
            mass();
        }
        private void tb_d_0_TextChanged(object sender, EventArgs e)
        {
            d_0 = checkTextBoxChange(tb_d_0, 0);
            mass();
        }
        private void zedGraphControl10_ZoomEvent(ZedGraphControl sender, ZoomState oldState, ZoomState newState)
        {
            GraphPane pane = sender.GraphPane;
            if (pane.XAxis.Scale.Min <= -25) pane.XAxis.Scale.Min = -25;
            if (pane.XAxis.Scale.Max >= h_0 * 2 + 25) pane.XAxis.Scale.Max = h_0 * 2 + 25;
            if (pane.YAxis.Scale.Min <= -25) pane.YAxis.Scale.Min = -5;
            if (pane.YAxis.Scale.Max >= b_0 * 2 + 25) pane.YAxis.Scale.Max = b_0 * 2 + 25;
        }
    }
}
