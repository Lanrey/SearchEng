using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Collections.Generic;
using SearchEnginee;

namespace SearchEngineeTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {//Check if its reading the files
            int id = 100;
            string path = @"";
            Doocument document = new Doocument(id,path);
            FileReader fileReader = new FileReader();
            string actual = fileReader.readFile(document.getPath());
            string expected = "new new new home sales top forecasts forecasts forecasts top";
            Assert.AreEqual(expected,actual);

        }

        [TestMethod]
        public void TestMethod2()
        {//Ckeck if its tokenizing
            string text = "sola is not a boy";
            string[] actual = Tokenizer.tokenizeString(text);
            string[] expected = { "sola","is","not","a","boy"};
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestMethod3()
        {//Ckeck if its tokenizing
            string text = "";
            string[] actual = Tokenizer.tokenizeString(text);
            
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void TestMethod4()
        {//Ckeck if its tokenizing
            string text = null;
            string[] actual = Tokenizer.tokenizeString(text);

        }

        [TestMethod]
        public void TestMethod5()
        {//Ckeck if its extracting keywords
            string[] text = {"sola","is", "not", "a", "boy"};
            List<string> actual = StopwordExtractor.work(text);
            List<string> expected = new List<string>();
            expected.Add("sola");
            expected.Add("is");
            expected.Add("boy");

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod6()
        {//Ckeck if its tokenizing
            Doocument d1 = new Doocument(1,"");
            d1.setRankValue(1.23);
            Doocument d2 = new Doocument(2,"");
            d2.setRankValue(1.02);
            Doocument d3 = new Doocument(3,"");
            d3.setRankValue(1.20);
            Doocument d4 = new Doocument(4,"");
            d4.setRankValue(1.65);

            List<Doocument> actual = new List<Doocument>();
            actual.Add(d1);
            actual.Add(d2);
            actual.Add(d3);
            actual.Add(d4);

            Sort.mergeSort(actual,Order.DESCENDING,0,3);

            List<Doocument> expected = new List<Doocument>();
            expected.Add(d4);
            expected.Add(d1);
            expected.Add(d3);
            expected.Add(d2);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestMethod7()
        {//Ckeck if its tokenizing
            Doocument d1 = new Doocument(1, "");
            d1.setRankValue(1);
            Doocument d2 = new Doocument(2, "");
            d2.setRankValue(1);
            Doocument d3 = new Doocument(3, "");
            d3.setRankValue(1);
            Doocument d4 = new Doocument(4, "");
            d4.setRankValue(1);

            List<Doocument> actual = new List<Doocument>();
            actual.Add(d1);
            actual.Add(d2);
            actual.Add(d3);
            actual.Add(d4);

            Sort.mergeSort(actual, Order.DESCENDING, 0, 3);

            List<Doocument> expected = new List<Doocument>();
            expected.Add(d4);
            expected.Add(d3);
            expected.Add(d2);
            expected.Add(d1);

            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestMethod8()
        {//Ckeck if its tokenizing
            Doocument d1 = new Doocument(1, "");
            d1.setRankValue(-1);
            Doocument d2 = new Doocument(2, "");
            d2.setRankValue(1);
            Doocument d3 = new Doocument(3, "");
            d3.setRankValue(1);
            Doocument d4 = new Doocument(4, "");
            d4.setRankValue(1);

            List<Doocument> actual = new List<Doocument>();
            actual.Add(d1);
            actual.Add(d2);
            actual.Add(d3);
            actual.Add(d4);

            Sort.mergeSort(actual, Order.DESCENDING, 0, 3);

            List<Doocument> expected = new List<Doocument>();
            expected.Add(d4);
            expected.Add(d3);
            expected.Add(d2);
            expected.Add(d1);

            CollectionAssert.AreEqual(expected, actual);
        }

    }
}
