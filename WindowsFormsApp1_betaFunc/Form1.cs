using System;
using System.Windows.Forms;
using OxyPlot.Series;
using OxyPlot;
using OxyPlot.WindowsForms; // Windowsフォームアプリケーション用

namespace WindowsFormsApp1_betaFunc
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            PlotView plotView = new PlotView { Dock = System.Windows.Forms.DockStyle.Fill };
            this.Controls.Add(plotView);

            var model = new PlotModel { Title = "y = IntegrateA(x)" };
            var series = new LineSeries { Title = "y = ∫[0, x] func dt" };

            // 0～1の間で計算し、グラフにデータを追加
            for (double x = 0; x <= 1; x += 0.01) // 0.01刻み
            {
                series.Points.Add(new DataPoint(x, IntegrateA(x,int.Parse(comboBox1.Text))));
            }
            model.Series.Add(series);
            plotView.Model = model;

        }

        // 台形法による積分の関数
        static double Integrate(double a, double b, int n, Func<double, double> f)
        {
            double h = (b - a) / n; // 各区間の幅
            double sum = 0.5 * (f(a) + f(b)); // 両端の値を足す

            for (int i = 1; i < n; i++)
            {
                double x = a + i * h;
                sum += f(x);
            }
            return sum * h; // 積分結果を返す
        }

        // y = IntegrateA(x) の関数
        static double IntegrateA(double b,int r)
        {
             return intg(b, r); //Ψ(x)で表示
            
          //  int n = 1000;
           // return intg(n*(b-0), r) * intg(n*(1-b), r);  //Φn(x)で表示
        }

        static double intg(double b,int r)
        {
            int n = 100; 
            if (b>=0 && b <= 1)
            {
                return Integrate(0, b, n, x => fc(x, r)) / Integrate(0, 1, n, x => fc(x, r));
            }
            else if(b>1)
            {
                return 1;
            }
            else
            {
                return 0;
            }
           
        }


        static double fc(double x,int r)
        {
            double rlt = 0;
            rlt = Math.Pow(x,r) * Math.Pow(1 - x,r);            
            return rlt;
        }
    }
}


