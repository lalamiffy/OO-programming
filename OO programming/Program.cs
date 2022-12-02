using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;


namespace OO_programming
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }


    /// <summary>
    /// Class a capture details accociated with an employee's pay slip record
    /// </summary>
    public class PaySlip
    {
        public int employeeId { get; set; }
        public string fName { get; set; }
        public string lName { get; set; }
        public double hourRate { get; set; }
        public char taxThreshold { get; set; }
        public double hour { get; set; }

    
        public double grossPay { get; set; }

        public double tax { get; set; }

        public double super { get; set; }

        public double netPay { get; set; }




        /// <summary>
        /// method to display the string in the list box
        /// </summary>
        public override string ToString()
        {
            return "ID" + employeeId + " " + "Name:" + fName + " " + lName;
        }



    }

    /// <summary>
    /// Base class to hold all Pay calculation functions
    /// Default class behaviour is tax calculated with tax threshold applied
    /// </summary>
    public class PayCalculator
    {

        public static int[] _minPay;
        public static int[] _maxPay;
        public static double[] _a;
        public static double[] _b;

        public PaySlip emp { get; set; }


        /// <summary>
        /// method to cacluate the gross pay of the employee
        /// </summary>
        public void CalGrossPay()
        {

            emp.grossPay = emp.hourRate * emp.hour;

           
            

        }


        /// <summary>
        /// method to find the tax rate and calculate the tax of the employee
        /// </summary>
        public void FindTax()

        {

            

            for (int i = 0; i < _a.Length; i++)


            {
                if (emp.grossPay >= _minPay[i] && emp.grossPay <= _maxPay[i])
                {
                    emp.tax = (emp.grossPay + 0.99) * _a[i] - _b[i];

                  
                

                }

              

            }

         
        }


        /// <summary>
        /// method to calucate the super of the employee
        /// </summary>

        public void CalSuper()
        {

            emp.super = emp.grossPay * 0.105;


        }

        /// <summary>
        ///method to calucate the net pay of the employee
        /// </summary>

        public void CalNetPay()
        {

            emp.netPay = emp.grossPay - emp.tax;


            
        }




    }

    /// <summary>
    /// Extends PayCalculator class handling No tax threshold
    /// </summary>
    public class PayCalculatorNoThreshold : PayCalculator
    {
        public static void ReadData()
        {
      

            List<int> minPay = new List<int>();
            List<int> maxPay = new List<int>();
            List<double> a = new List<double>();
            List<double> b = new List<double>();

            /// <summary>
            /// read the csv to calucate NoThreshold tax rate 
            /// </summary>
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false
            };


            using (var reader = new StreamReader("../../../taxrate-nothreshold.csv"))
            {
                using (var csv = new CsvReader(reader, config))

                    while (csv.Read())
                    {
                        minPay.Add(csv.GetField<int>(0));
                        maxPay.Add(csv.GetField<int>(1));
                        a.Add(csv.GetField<double>(2));
                        b.Add(csv.GetField<double>(3));

                    }

                _minPay = minPay.ToArray();
                _maxPay = maxPay.ToArray();
                _a = a.ToArray();
                _b = b.ToArray();



            }

        }


    }

    /// <summary>
    /// Extends PayCalculator class handling With tax threshold
    /// </summary>
    public class PayCalculatorWithThreshold : PayCalculator


    {
        /// <summary>
        /// read the csv to calucate WithThreshold tax rate 
        /// </summary>
        public static void ReadData()
        {

            List<int> minPay = new List<int>();
            List<int> maxPay = new List<int>();
            List<double> a = new List<double>();
            List<double> b = new List<double>();


            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false
            };


            using (var reader = new StreamReader("../../../taxrate-withthreshold.csv"))
            {
                using (var csv = new CsvReader(reader, config))

                    while (csv.Read())
                    {
                        minPay.Add(csv.GetField<int>(0));
                        maxPay.Add(csv.GetField<int>(1));
                        a.Add(csv.GetField<double>(2));
                        b.Add(csv.GetField<double>(3));

                    }

                _minPay = minPay.ToArray();
                _maxPay = maxPay.ToArray();
                _a = a.ToArray();
                _b = b.ToArray();



            }

        }
    }

}

 



