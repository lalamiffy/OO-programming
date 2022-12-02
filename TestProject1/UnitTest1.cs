using Microsoft.VisualStudio.TestTools.UnitTesting;
using OO_programming;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;


namespace OO_programming.Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestInitialize]
        public void Init()
        {

            

        }
       



        [TestMethod()]
    

    public void FindTaxTest()

    {
        PayCalculatorNoThreshold e = new PayCalculatorNoThreshold();
        PayCalculatorNoThreshold.ReadData();

         
            e.emp = new PaySlip();
            e.emp.grossPay = 87;

            double expResult = 16.5281;

            e.FindTax();
            double actResult = e.emp.tax;
            Assert.AreEqual(expResult, actResult);
    }

        [TestMethod()]
        public void FindGrosspayTest()

        {
            PayCalculator gtest = new PayCalculator();


            gtest.emp = new PaySlip();
            gtest.emp.hour = 20;
            gtest.emp.hourRate = 25;

            double expResult = 500;

            gtest.CalGrossPay();
            double actResult = gtest.emp.grossPay;
            Assert.AreEqual(expResult, actResult);
        }

        [TestMethod()]
        public void CalSuperTest()

        {
            PayCalculator stest = new PayCalculator();


            stest.emp = new PaySlip();

            stest.emp.grossPay = 1000;

            double expResult = 105;

            stest.CalSuper();
            double actResult = stest.emp.super;
            Assert.AreEqual(expResult, actResult);
        }

        [TestMethod()]
        public void CalNetpayTest()

        {
            PayCalculator ntest = new PayCalculator();


            ntest.emp = new PaySlip();

            ntest.emp.grossPay = 900;
            ntest.emp.tax = 248.844523;

            double expResult = 651.155477
;

            ntest.CalNetPay();
            double actResult = ntest.emp.netPay;
            Assert.AreEqual(expResult, actResult);
        }



    }
}