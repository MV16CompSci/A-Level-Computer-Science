using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Web;
using System.Diagnostics;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

namespace TescoAPI2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            buttonSearch.Enabled = false;
            numericUpDownMax.Value = 10;
            fitdata();            
        }

        private void TescoSelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount != 0)
            {
                Cursor.Current = Cursors.WaitCursor;
                string strFileName = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                Debug.WriteLine(strFileName);
                pictureBox1.Load(strFileName);
                Cursor.Current = Cursors.Default;
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", "ocp key goes here");
                Debug.WriteLine("https://dev.tescolabs.com/grocery/products/?query=" + textBoxSearch.Text + "&offset=0&limit=" + numericUpDownMax.Value);
                var response = client.GetAsync("https://dev.tescolabs.com/grocery/products/?query=" + textBoxSearch.Text + "&offset=0&limit=" + numericUpDownMax.Value).Result;

                if (response.IsSuccessStatusCode)
                {
                    // by calling .Result you are performing a synchronous call
                    var responseContent = response.Content;

                    // by calling .Result you are synchronously reading the result
                    string responseString = responseContent.ReadAsStringAsync().Result;

                    //Converts the string provided above, jsonData, into a dynamic object.
                    dynamic results = JsonConvert.DeserializeObject<dynamic>(responseString);

                    dataGridView1.Rows.Clear();
                    dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

                    string strNum;
                    string strDesc;
                    string strImage;
                    string strPrice;
                    try
                    {
                        // A bug in the TESCO API means that sometimes the XML returned does not match the format
                        //  described in the documentation. Use try/catch avoid such occurances.
                        foreach (var result in results.uk.ghs.products.results)
                        {
                            //Induvidual JSON elements are called using objectName, followed by the chidren included in the JSON data.

                            strNum = "Item " + result;
                            Debug.WriteLine(strNum);
                            strDesc = result.description[0];
                            Debug.WriteLine("Description: " + strDesc);
                            strImage = result.image;
                            Debug.WriteLine("Image link: " + strImage);
                            strPrice = String.Format("{0:C}", result.price);
                    
                            Debug.WriteLine(strPrice);
                            Debug.WriteLine("");

                            dataGridView1.Rows.Add(
                                strDesc,
                                strPrice,
                                strImage);
                        }
                    }
                    catch (Exception ee)
                    {
                        Debug.WriteLine("An error occurred: '{0}'", ee);
                    }
                }
                
                Cursor.Current = Cursors.Default;
            }
        }

        private void textBoxSearch_TextChanged(object sender, EventArgs e)
        {
            if(textBoxSearch.Text.Length == 0)
                buttonSearch.Enabled = false;
            else
                buttonSearch.Enabled = true;
        }

        private void fitdata()
        {
            dataGridView1.AutoSizeColumnsMode =
                DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            dataGridView1.AutoResizeColumns();
        }

        private void endResize(object sender, EventArgs e)
        {
            fitdata();
        }
    }
}
