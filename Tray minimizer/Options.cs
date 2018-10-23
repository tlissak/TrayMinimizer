using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Tray_minimizer {
    public partial class Options : Form {

        uint showmod;
        uint showkey;

        uint hidemod;
        uint hidekey;

        uint hideactmod;
        uint hideactkey;

        bool startup;
        bool ignoretitle;

        bool showbaloontip;

        public bool Showbaloontip {      get { return showbaloontip; }      set { showbaloontip = value; }  }
        public bool Ignoretitle {        get { return ignoretitle; }        set { ignoretitle = value; }    }
        public bool Startup {            get { return startup; }            set { startup = value; }        }

        public uint Showmod { get { return showmod; } set { showmod = value; } }
        public uint Showkey { get { return showkey; } set { showkey = value; } }

        public uint Hidemod { get { return hidemod; } set { hidemod = value; } }
        public uint Hidekey { get { return hidekey; } set { hidekey = value; } }

        public uint Hideactmod { get { return hideactmod; } set { hideactmod = value; } }
        public uint Hideactkey { get { return hideactkey; } set { hideactkey = value; } }

        public Options() {
            InitializeComponent();
        }

        private void Options_Load(object sender, EventArgs e) {
            for (int i = 65; i < 91; i++) {
                showkeycombo.Items.Add((char)i);
                hidekeycombo.Items.Add((char)i);
                hideactkeycombo.Items.Add((char)i);
            }

            if (System.IO.File.Exists("Tray minimizer.exe.config")) {
                showctrlcheck.Checked = ((int)winapi.KeyModifiers.Control & showmod) > 0;
                showaltcheck.Checked = ((int)winapi.KeyModifiers.Alt & showmod) > 0;
                showshiftcheck.Checked = ((int)winapi.KeyModifiers.Shift & showmod) > 0;

                hidectrlcheck.Checked = ((int)winapi.KeyModifiers.Control & hidemod) > 0;
                hidealtcheck.Checked = ((int)winapi.KeyModifiers.Alt & hidemod) > 0;
                hideshiftcheck.Checked = ((int)winapi.KeyModifiers.Shift & hidemod) > 0;

                hideactctrlcheck.Checked = ((int)winapi.KeyModifiers.Control & hideactmod) > 0;
                hideactaltcheck.Checked = ((int)winapi.KeyModifiers.Alt & hideactmod) > 0;
                hideactshiftcheck.Checked = ((int)winapi.KeyModifiers.Shift & hideactmod) > 0;

                showkeycombo.SelectedIndex = (int)showkey;
                hidekeycombo.SelectedIndex = (int)hidekey;
                hideactkeycombo.SelectedIndex = (int)hideactkey;
            }

            startcheck.Checked = startup;
            ignoretitlecheck.Checked = ignoretitle;
        }

        private void getnewhotkey() {
            showmod = 0;
            hidemod = 0;
            hideactmod = 0;

            if (this.showshiftcheck.Checked) { showmod = showmod | (uint)winapi.KeyModifiers.Shift; }
            if (this.showctrlcheck.Checked) { showmod = showmod | (uint)winapi.KeyModifiers.Control; }
            if (this.showaltcheck.Checked) { showmod = showmod | (uint)winapi.KeyModifiers.Alt; }

            if (this.hideshiftcheck.Checked) { hidemod = hidemod | (uint)winapi.KeyModifiers.Shift; }
            if (this.hidectrlcheck.Checked) { hidemod = hidemod | (uint)winapi.KeyModifiers.Control; }
            if (this.hidealtcheck.Checked) { hidemod = hidemod | (uint)winapi.KeyModifiers.Alt; }

            if (this.hideactshiftcheck.Checked) { hideactmod = hideactmod | (uint)winapi.KeyModifiers.Shift; }
            if (this.hideactctrlcheck.Checked) { hideactmod = hideactmod | (uint)winapi.KeyModifiers.Control; }
            if (this.hideactaltcheck.Checked) { hideactmod = hideactmod | (uint)winapi.KeyModifiers.Alt; }

            showkey = (uint)this.showkeycombo.SelectedIndex;
            hidekey = (uint)this.hidekeycombo.SelectedIndex;
            hideactkey = (uint)this.hideactkeycombo.SelectedIndex;
        }

        private void button1_Click(object sender, EventArgs e) {
            getnewhotkey();

            if ((showkeycombo.SelectedIndex == hidekeycombo.SelectedIndex && showmod == hidemod && showmod > 0) ||
                (showkeycombo.SelectedIndex == hideactkeycombo.SelectedIndex && showmod == hideactmod && showmod > 0) ||
                (hidekeycombo.SelectedIndex == hideactkeycombo.SelectedIndex && hidemod == hideactmod && hidemod > 0)

                ) {
                MessageBox.Show("Hotkeys cannot be the same", "Error", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

                return;
            }

            this.DialogResult = DialogResult.OK;
            startup = startcheck.Checked;
            ignoretitle = ignoretitlecheck.Checked;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e) {
            this.Close();
        }
    }
}