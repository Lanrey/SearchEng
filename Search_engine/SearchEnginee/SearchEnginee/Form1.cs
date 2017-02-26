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
using System.Security.AccessControl;

namespace SearchEnginee
{
    public partial class Form1 : Form
    {

        private static AutoCompleteStringCollection data = new AutoCompleteStringCollection();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Storage.initializeSavedInfo();
            textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            textBox1.AutoCompleteCustomSource = data;
            textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string query = textBox1.Text;

            //Stopwatch stopwatch = new Stopwatch();
            //stopwatch.Start();

            if (query != "")
            {
                data.Add(textBox1.Text);
                
                this.Hide();
                Ranker ranker = new Ranker();
                ranker.Rank(query);

                /*stopwatch.Stop();
                TimeSpan ts = stopwatch.Elapsed;
                string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}.{3:00}",
                    ts.Hours, ts.Minutes, ts.Seconds,
                    ts.Milliseconds / 10);
                Console.WriteLine("Runtime" + elapsedTime);*/

                List<Doocument> documentList = Storage.getStorageDetails().getDocumentList();
                uint docLength = (uint)documentList.Count - 1;
                Sort.mergeSort(documentList,Order.DESCENDING,0,docLength);

                Form2 result = new Form2();
                result.Show();
                result.setLinkLabels(documentList);
                result.setFirstPage();

            }

        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //Console.WriteLine("nanananananananannanananananaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa goodbye");
            //StorageDetailsSR storageDetailsSR = new StorageDetailsSR();
            //storageDetailsSR.Save(Storage.getStorageDetails());
        }

        private void Form1_BackgroundImageChanged(object sender, EventArgs e)
        {

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Console.WriteLine("nanananananananannanananananaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa goodbye");
            StorageDetailsSR storageDetailsSR = new StorageDetailsSR();
            storageDetailsSR.Save(Storage.getStorageDetails());
            storageDetailsSR.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DialogResult result = openFileDialog.ShowDialog();
            if(result == DialogResult.OK){
                Console.WriteLine(openFileDialog.FileName);
                string source = @"C:\seeSharpSE123";
                string destination = openFileDialog.FileName;
                File.Copy(source,destination);
                DirectorySecurity dc = Directory.GetAccessControl(source);
                //dc.
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Console.WriteLine(openFileDialog.FileName);
                string destination = openFileDialog.FileName;
                string defaultFolder = @"C:\seeSharpSE\";
                int pathLength = defaultFolder.Length;
                if(defaultFolder == destination.Substring(0,pathLength)){
                    File.Delete(destination);
                }
                
            }
        }

    }
}











/*
 private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DialogResult result = openFileDialog.ShowDialog();
            if(result == DialogResult.OK){
                Console.WriteLine(openFileDialog.FileName);
                string source = @"C:\seeSharpSE123";
                string destination = openFileDialog.FileName;
                File.Copy(source,destination);
                DirectorySecurity dc = Directory.GetAccessControl(source);
                //dc.
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            DialogResult result = openFileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Console.WriteLine(openFileDialog.FileName);
                string destination = openFileDialog.FileName;
                string defaultFolder = @"C:\seeSharpSE\";
                int pathLength = defaultFolder.Length;
                if(defaultFolder == destination.Substring(0,pathLength)){
                    File.Delete(destination);
                }
                
            }
        }
*/
