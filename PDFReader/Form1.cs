using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;

namespace PDFReader
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog() { Filter = "PDF Files|*.pdf", ValidateNames = true, Multiselect = false })
            {
                if(ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        PdfReader reader = new PdfReader(ofd.FileName);
                        List<string> questoes = new List<string>();
                        int numQuestoes = 0;
                        //StringBuilder sb = new StringBuilder();

                        for (int i=1; i<=reader.NumberOfPages; i++)
                        {
                            //string pattern = "(\n\\d{2})(.+\n+)+?(\\(E\\)).+";

                            StringBuilder sb = new StringBuilder();

                            sb.Append(PdfTextExtractor.GetTextFromPage(reader, i));

                            string pageText = sb.ToString();
                            richTextBox1.Text += pageText;

                            /*while (Regex.IsMatch(pageText, pattern))
                            {
                                foreach(Match match in Regex.Matches(pageText, pattern))
                                {
                                    pageText = Regex.Replace(pageText, pattern, string.Empty);
                                    numQuestoes++;
                                    richTextBox1.Text += match.Value + '\n';
                                    sb.Replace(match.Value, string.Empty);
                                }
                            }*/
                        }

                        //MessageBox.Show("Numero de questoes - " + numQuestoes.ToString());

                        reader.Close();
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}