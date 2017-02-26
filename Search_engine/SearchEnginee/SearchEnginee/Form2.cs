using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace SearchEnginee
{
    public partial class Form2 : Form
    {

        private int maxLinkPerPage = 5;
        private List<LinkLabel> linkLabels = new List<LinkLabel>();
        private int page = 1;

        public Form2()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Controls.Clear();
            button3.Enabled = true;

            if(page == 2){
                button1.Enabled = false;
            }
            page--;

            int startIndex = (page - 1) * maxLinkPerPage;
            int lastIndex = (page * maxLinkPerPage) - 1;

            for (int i = startIndex; i <= lastIndex;i++ )
            {
                tableLayoutPanel1.Controls.Add(linkLabels[i]);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Program.getForm1().Show();
            this.Close();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            tableLayoutPanel1.Controls.Clear();
            button1.Enabled = true;

            int startIndex = page * maxLinkPerPage;;
            int lastIndex;
            if (page == (int)(Math.Ceiling((double)(linkLabels.Count) / (double)(maxLinkPerPage)) - 1))
            {
                button3.Enabled = false;                
                lastIndex = linkLabels.Count - 1;
                page++;
            }
            else {
                lastIndex = page++ * maxLinkPerPage - 1;
            }
            

            for (int i = startIndex; i <= lastIndex; i++)
            {
                tableLayoutPanel1.Controls.Add(linkLabels[i]);
            }
        }

        public void setLinkLabels(List<Doocument> documents) { 
            foreach(Doocument document in documents){
                LinkLabel linkLabel = new LinkLabel();
                linkLabel.Text = document.getPath();
                linkLabel.Width = 500;
                linkLabel.LinkClicked += OnLinkClicked;
                linkLabels.Add(linkLabel);
            }
        }



        void OnLinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel lk = (LinkLabel)(sender);
            Process.Start(lk.Text);
            //Console.WriteLine("babajide");
            //Process.Start(@"C:\Users\AKEJU  FATAI\Desktop\c# work\C:\Users\AKEJU  FATAI\Desktop\c# work\Visual C# 2012 How to Program, Paul Deitel - Harvey Deitel, 5th Edition, Prentice Hall, 2014.pdf");
        }

        public void setFirstPage() {
            

            button1.Enabled = false;
            int linkNumber = maxLinkPerPage;
            if(linkLabels.Count <= maxLinkPerPage){
                linkNumber = linkLabels.Count;
                button3.Enabled = false;
            }
            for (int i = 0; i < linkNumber;i++ )
            {
                linkLabels[i].Width = 1000;
                tableLayoutPanel1.Controls.Add(linkLabels[i]);
            }

        }

        private void tableLayoutPanel1_TabStopChanged(object sender, EventArgs e)
        {

        }

    }
}
