using System;
using System.Data;
using System.Windows.Forms;
using SarkuteriBizerba.Classes;

namespace SarkuteriBizerba.Forms
{
  public partial class FrmAbout : Form
  {
    public FrmAbout()
    {
      InitializeComponent();
    }

    private void FrmAbout_Load(object sender, EventArgs e)
    {
      // Form yüklendiğinde yapılacak işlemler
      lblVersion.Text = $"Versiyon: {Application.ProductVersion}";
    }

    private void btnOk_Click(object sender, EventArgs e)
    {
      this.Close();
    }

        private void button1_Click(object sender, EventArgs e)
        {

            UpdateHelper.GuncellemeBaslat();
        }
    }
}
