using Microsoft.VisualStudio.TestTools.UnitTesting;
using LogisticsSystem.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogisticsSystem.Tools.Tests
{
    [TestClass()]
    public class DPPTests
    {
        [TestMethod()]
        public void GetSulotionTest()
        {

        }

        [TestMethod()]
        public void QuickSortTest()
        {
            DPP dPP = new DPP();
            Saving[] savings = new Saving[]
            {
                new Saving(){start = 1, end = 3,savingValue = "12345"},
                new Saving(){start = 2, end = 3,savingValue = "12525"},
                new Saving(){start = 3, end = 3,savingValue = "21345"},
                new Saving(){start = 4, end = 3,savingValue = "10045"},
                new Saving(){start = 5, end = 3,savingValue = "12745"}
            };
            dPP.QuickSort(ref savings, 0, savings.Length - 1);
            Saving[] saving2 = new Saving[]
            {
                new Saving(){start = 4, end = 3,savingValue = "10045"},
                new Saving(){start = 1, end = 3,savingValue = "12345"},
                new Saving(){start = 2, end = 3,savingValue = "12525"},
                new Saving(){start = 5, end = 3,savingValue = "12745"},
                new Saving(){start = 3, end = 3,savingValue = "21345"}
            };
            Assert.AreEqual(savings[0].savingValue, saving2[0].savingValue);
        }
    }
}