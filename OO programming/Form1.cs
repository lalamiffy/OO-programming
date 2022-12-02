using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace OO_programming
{
  
    public partial class Form1 : Form
    {
        private List<PaySlip> employeeList = new List<PaySlip>();
        private PaySlip selectedEmp = new PaySlip(); 



        public Form1()
        {
            InitializeComponent();

            // Add code below to complete the implementation to populate the listBox
            // by reading the employee.csv file into a List of PaySlip objects, then binding this to the ListBox.
            // CSV file format: <employee ID>, <first name>, <last name>, <hourly rate>,<taxthreshold>



            /// <summary>
            /// read the csv to get the employee's detail
            /// </summary>

            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                HasHeaderRecord = false
            };

            using (var reader = new StreamReader("../../../employee.csv"))
            {
                using (var csv = new CsvReader(reader, config))
                {
                    while (csv.Read())
                    {   
                        PaySlip emp = new PaySlip();
                        emp.employeeId = (csv.GetField<int>(0));
                        emp.fName = csv.GetField<string>(1);    
                        emp.lName = csv.GetField<string>(2);    
                        emp.hourRate = (csv.GetField<int>(3));
                        emp.taxThreshold = csv.GetField<char>(4);
                        employeeList.Add(emp);
                    }
                }
            }
            listBox1.DataSource = employeeList;

        }

      
        private void button1_Click(object sender, EventArgs e)
        {
            // Add code below to complete the implementation to populate the
            // payment summary (textBox2) using the PaySlip and PayCalculatorNoThreshold
            // and PayCalculatorWithThresholds classes object and methods.

            selectedEmp = listBox1.SelectedItem as PaySlip;

            selectedEmp.hour = int.Parse(textBox1.Text);


            /// <summary>
            ///check if the selectEmp is  with taxthreshold or no taxthreshold and then store the data in different class
            /// <summary>
            if (selectedEmp.taxThreshold == 'Y')
            {


                PayCalculatorWithThreshold emp1 = new PayCalculatorWithThreshold();
                PayCalculatorWithThreshold.ReadData();

                emp1.emp = selectedEmp;
                emp1.CalGrossPay();
                emp1.FindTax();
                emp1.CalSuper();
                emp1.CalNetPay();



            }

            else if (selectedEmp.taxThreshold == 'N')
            {



                PayCalculatorNoThreshold emp1 = new PayCalculatorNoThreshold();

                PayCalculatorNoThreshold.ReadData();


                emp1.emp = selectedEmp;
                emp1.CalGrossPay();
                emp1.FindTax();
                emp1.CalSuper();
                emp1.CalNetPay();

            }






            //display employee detail
            string newline = Environment.NewLine;
            textbox2.Text ="Employee ID:" + selectedEmp.employeeId + newline +
                           "Full Name: " + selectedEmp.fName + " " + selectedEmp.lName + newline +
                           "Hour worked: " + selectedEmp.hour + newline +
                           "Hour Rate: $" + selectedEmp.hourRate + "/hr"+ newline +
                           "TaxThreshold:" + selectedEmp.taxThreshold + newline +
                           "Gross Pay: $" + selectedEmp.grossPay + newline +
                           "Tax: $" + selectedEmp.tax + newline +
                           "Super: $" + selectedEmp.super + newline +
                           "Net Pay: $" + selectedEmp.netPay;


        }


        /// <summary>
        ///save payslip detail and export to csv 
        /// <summary>
        private void button2_Click(object sender, EventArgs e)
        {
            // Add code below to complete the implementation for saving the
            // calculated payment data into a csv file.
            // File naming convention: Pay_<full name>_<datetimenow>.csv
            // Data fields expected - EmployeeId, Full Name, Hours Worked, Hourly Rate, Tax Threshold, Gross Pay, Tax, Net Pay, Superannuation

            
            var record = new List<PaySlip>();
            record.Add(selectedEmp);

            using (var writer = new StreamWriter($"../../../Pay-{selectedEmp.employeeId}-{selectedEmp.fName}-{selectedEmp.lName}-{DateTime.Now.Ticks}.csv"))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(record);
            }



        }


    }
}
