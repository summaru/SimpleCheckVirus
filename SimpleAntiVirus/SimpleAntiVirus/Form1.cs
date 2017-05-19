using System;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.IO;
using System.Windows.Forms;


namespace SimpleAntiVirus
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

 

        private void SimpleAntiVirus_Load(object sender, EventArgs e)
        {

        }

        private string md5_hash(string file_name)
        {
            return hash_generator("md5", file_name);
        }

        private string hash_generator(string hash_type,string file_name)
        {
            using (var hash = MD5.Create())
            {
                FileStream filestream = File.OpenRead(file_name);
                filestream.Position = 0;
                var hashValue = hash.ComputeHash(filestream);
                string hash_hex = PrintByteArray(hashValue);

                filestream.Close();
                return hash_hex;
            }
        }

        public string PrintByteArray(byte[] array)
        {
            string hex_value = "";
            hex_value = string.Join("",array.Select(i => i.ToString("x2")));

            return hex_value.ToLower();
        }

        private void btnBrowes_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                string path = openFileDialog1.FileName;
                txtFilePath.Text = path;

                string sample = md5_hash(path);
                txtMD5.Text = md5_hash(path);
                string[] lines = File.ReadAllLines("0.md5");
                foreach (var line in lines)
                {
                    if (line == sample)
                    {
                        lblResult.ForeColor = Color.Red;
                        lblResult.Text = "infected!";
                    }
                    else
                    {
                        lblResult.ForeColor = Color.Green;
                        lblResult.Text = "Clean";
                    }

                }
                
            }
        }


    }
}
