using System;
using System.Windows.Forms;

namespace SarkuteriAdmin.Forms
{
  public partial class FrmPasswordPrompt : Form
  {
    public string Password { get; private set; }

    public FrmPasswordPrompt()
    {
      InitializeComponent();
    }

    private void FrmPasswordPrompt_Load(object sender, EventArgs e)
    {
      txtPassword.Focus();
    }

    private void btnOK_Click(object sender, EventArgs e)
    {
      Password = txtPassword.Text;
      this.DialogResult = DialogResult.OK;
      this.Close();
    }

    private void btnCancel_Click(object sender, EventArgs e)
    {
      Password = string.Empty;
      this.DialogResult = DialogResult.Cancel;
      this.Close();
    }

    private void txtPassword_KeyDown(object sender, KeyEventArgs e)
    {
      if (e.KeyCode == Keys.Enter)
      {
        e.Handled = true;
        e.SuppressKeyPress = true;
        btnOK_Click(sender, e);
      }
      else if (e.KeyCode == Keys.Escape)
      {
        e.Handled = true;
        e.SuppressKeyPress = true;
        btnCancel_Click(sender, e);
      }
    }
  }
}
