using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Paint
{
    public partial class Form1 : Form
    {
        private Painter p;

        
        private Graphics g;
        private TextBox textBox;
        public Form1()
        {
            InitializeComponent();
            g = panel1.CreateGraphics();
            p = new Painter(g);
            colorDialog.AllowFullOpen = false;
            p.backgroundColor = panel1.BackColor;          
            panelMainColor.BackColor = p.mainColor;
            panelBackgroundColor.BackColor = p.backgroundColor;
            groupBox5.Visible = true;
            groupBox4.Visible = false;
            button10.Location = new Point(panel1.Location.X + panel1.Width,
                                          panel1.Location.Y + panel1.Height);
            comboBox1.SelectedIndex = 3;
            buttonPen.BackColor = Color.LightGray;
        }

        private void CreateTextBox()
        {
            textBox = new TextBox();
            textBox.Location = new Point(p.lastPoint.X-50,
                                         p.lastPoint.Y-11);
            textBox.Name = "texBox";
            textBox.Size = new System.Drawing.Size(100, 22);
            textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(textBox_KeyPress);
            panel1.Controls.Add(textBox);
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            
            if (e.Button == MouseButtons.Left)
            {
                p.IsPressed = true;
                p.lastPoint = e.Location;
                
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            toolStripStatusLocationMouse.Text = String.Format("{0} X {1}", e.Location.X, e.Location.Y);
            p.PaintMouseMove(e.Location, ModifierKeys == Keys.Shift);
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            p.PaintMouseUp(ModifierKeys == Keys.Shift);

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            p.Paint(panel1);
        }

        private void buttonPen_Click(object sender, EventArgs e)
        {
            p.selection = 1;
            groupBox5.Visible = true;
            groupBox4.Visible = false;
            buttonPen.BackColor = Color.LightGray;
            buttonEraser.BackColor = groupBox1.BackColor;
            buttonLine.BackColor = groupBox1.BackColor;
            buttonText.BackColor = groupBox1.BackColor;
            buttonLineArrow.BackColor = groupBox1.BackColor;
            buttonСircle.BackColor = groupBox1.BackColor;
            button1.BackColor = groupBox1.BackColor;
            button12.BackColor = groupBox1.BackColor;
            button13.BackColor = groupBox1.BackColor;
        }
        private void buttonEraser_Click(object sender, EventArgs e)
        {
            p.selection = 2;
            groupBox5.Visible = true;
            groupBox4.Visible = false;
            buttonPen.BackColor = groupBox1.BackColor;
            buttonEraser.BackColor = Color.LightGray;
            buttonLine.BackColor = groupBox1.BackColor;
            buttonText.BackColor = groupBox1.BackColor;
            buttonLineArrow.BackColor = groupBox1.BackColor;
            buttonСircle.BackColor = groupBox1.BackColor;
            button1.BackColor = groupBox1.BackColor;
            button12.BackColor = groupBox1.BackColor;
            button13.BackColor = groupBox1.BackColor;
        }
        private void buttonLine_Click(object sender, EventArgs e)
        {
            p.selection = 3;
            groupBox5.Visible = true;
            groupBox4.Visible = false;
            buttonPen.BackColor = groupBox1.BackColor;
            buttonEraser.BackColor = groupBox1.BackColor;
            buttonLine.BackColor = Color.LightGray;
            buttonText.BackColor = groupBox1.BackColor;
            buttonLineArrow.BackColor = groupBox1.BackColor;
            buttonСircle.BackColor = groupBox1.BackColor;
            button1.BackColor = groupBox1.BackColor;
            button12.BackColor = groupBox1.BackColor;
            button13.BackColor = groupBox1.BackColor;
        }
        private void buttonText_Click(object sender, EventArgs e)
        {
            p.selection = 4;
            groupBox5.Visible = false;
            groupBox4.Visible = true;
            buttonPen.BackColor = groupBox1.BackColor;
            buttonEraser.BackColor = groupBox1.BackColor;
            buttonLine.BackColor = groupBox1.BackColor;
            buttonText.BackColor = Color.LightGray;
            buttonLineArrow.BackColor = groupBox1.BackColor;
            buttonСircle.BackColor = groupBox1.BackColor;
            button1.BackColor = groupBox1.BackColor;
            button12.BackColor = groupBox1.BackColor;
            button13.BackColor = groupBox1.BackColor;
        }
        private void buttonLineArrow_Click(object sender, EventArgs e)
        {
            p.selection = 5;
            groupBox5.Visible = true;
            groupBox4.Visible = false;
            buttonPen.BackColor = groupBox1.BackColor;
            buttonEraser.BackColor = groupBox1.BackColor;
            buttonLine.BackColor = groupBox1.BackColor;
            buttonText.BackColor = groupBox1.BackColor;
            buttonLineArrow.BackColor = Color.LightGray;
            buttonСircle.BackColor = groupBox1.BackColor;
            button1.BackColor = groupBox1.BackColor;
            button12.BackColor = groupBox1.BackColor;
            button13.BackColor = groupBox1.BackColor;
        }
        private void buttonСircle_Click(object sender, EventArgs e)
        {
            p.selection = 6;
            groupBox5.Visible = true;
            groupBox4.Visible = false;
            buttonPen.BackColor = groupBox1.BackColor;
            buttonEraser.BackColor = groupBox1.BackColor;
            buttonLine.BackColor = groupBox1.BackColor;
            buttonText.BackColor = groupBox1.BackColor;
            buttonLineArrow.BackColor = groupBox1.BackColor;
            buttonСircle.BackColor = Color.LightGray;
            button1.BackColor = groupBox1.BackColor;
            button12.BackColor = groupBox1.BackColor;
            button13.BackColor = groupBox1.BackColor;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            p.selection = 7;
            groupBox5.Visible = true;
            groupBox4.Visible = false;
            buttonPen.BackColor = groupBox1.BackColor;
            buttonEraser.BackColor = groupBox1.BackColor;
            buttonLine.BackColor = groupBox1.BackColor;
            buttonText.BackColor = groupBox1.BackColor;
            buttonLineArrow.BackColor = groupBox1.BackColor;
            buttonСircle.BackColor = groupBox1.BackColor;
            button1.BackColor = Color.LightGray;
            button12.BackColor = groupBox1.BackColor;
            button13.BackColor = groupBox1.BackColor;
        }
        

        private void buttonColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                p.mainColor = colorDialog.Color;
                panelMainColor.BackColor = p.mainColor;
            }
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            p.KeyPressed(e.KeyChar,panel1,textBox);        
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            if (p.IsPressed)
            {
                switch (p.selection)
                {
                    case 4:
                        CreateTextBox();
                        break;
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            p.mainColor = button2.BackColor;
            panelMainColor.BackColor = p.mainColor;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            p.mainColor = button3.BackColor;
            panelMainColor.BackColor = p.mainColor;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            p.mainColor = button6.BackColor;
            panelMainColor.BackColor = p.mainColor;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            p.mainColor = button5.BackColor;
            panelMainColor.BackColor = p.mainColor;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            p.mainColor = button4.BackColor;
            panelMainColor.BackColor = p.mainColor;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            p.mainColor = button8.BackColor;
            panelMainColor.BackColor = p.mainColor;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            p.mainColor = button7.BackColor;
            panelMainColor.BackColor = p.mainColor;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            p.mainColor = button9.BackColor;
            panelMainColor.BackColor = p.mainColor;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            p.sizePen = Convert.ToInt32(comboBox1.SelectedItem.ToString());
        }

       

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            savePictureDialog.OverwritePrompt = true;
            if (savePictureDialog.ShowDialog() == DialogResult.OK)
            {
                var filename = savePictureDialog.FileName;
                p.SaveImage(filename,panel1);
            }
        }

        private void загрузитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (openPictureDialog.ShowDialog() == DialogResult.OK)
            {
                var filename = openPictureDialog.FileName;                               
                p.LoadImage(Image.FromFile(filename),panel1);
                panel1.Refresh();
                button10.Location = new Point(panel1.Location.X + panel1.Width,
                                              panel1.Location.Y + panel1.Height);
                p.RefreshGraphicsSize(panel1);

            }
        }

        private void button10_MouseDown(object sender, MouseEventArgs e)
        {
            p.IsPressedButton = true;
        }

        private void button10_MouseMove(object sender, MouseEventArgs e)
        {
            if (p.IsPressedButton)
            {
 
                Point pos = new Point(Cursor.Position.X - button10.Width / 2,
                                                        Cursor.Position.Y - button10.Height / 2);
                button10.Location = PointToClient(pos);
                panel1.Size = new Size(button10.Location.X - panel1.Location.X,
                                        button10.Location.Y - panel1.Location.Y);
                p.RefreshGraphicsSize(panel1);

            }
        }

        private void button10_MouseUp(object sender, MouseEventArgs e)
        {
            p.IsPressedButton = false;
            button10.Location = new Point(panel1.Location.X + panel1.Width,
                                                            panel1.Location.Y + panel1.Height);
        }

        private void panel1_SizeChanged(object sender, EventArgs e)
        {
            toolStripStatusSizePanel.Text = String.Format("{0} X {1}",panel1.Width,panel1.Height);
            if (panel1.Width < 100)
            {
                panel1.Width = 100;
            }
            else if (panel1.Height < 100)
            {
                panel1.Height = 100;
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if(fontDialog1.ShowDialog() == DialogResult.OK)
            {
                p.font = fontDialog1.Font;
            }
        }

        

        private void panelBackgroundColor_MouseClick(object sender, MouseEventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                p.backgroundColor = colorDialog.Color;
                panelBackgroundColor.BackColor = p.backgroundColor;
                p.RefreshGraphicsSize(panel1);
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            p.selection = 8;
            groupBox5.Visible = true;
            groupBox4.Visible = false;
            buttonPen.BackColor = groupBox1.BackColor;
            buttonEraser.BackColor = groupBox1.BackColor;
            buttonLine.BackColor = groupBox1.BackColor;
            buttonText.BackColor = groupBox1.BackColor;
            buttonLineArrow.BackColor = groupBox1.BackColor;
            buttonСircle.BackColor = groupBox1.BackColor;
            button1.BackColor = groupBox1.BackColor;
            button12.BackColor = Color.LightGray;
            button13.BackColor = groupBox1.BackColor;
            
        }

        private void button13_Click(object sender, EventArgs e)
        {
            p.selection = 9;
            groupBox5.Visible = true;
            groupBox4.Visible = false;
            buttonPen.BackColor = groupBox1.BackColor;
            buttonEraser.BackColor = groupBox1.BackColor;
            buttonLine.BackColor = groupBox1.BackColor;
            buttonText.BackColor = groupBox1.BackColor;
            buttonLineArrow.BackColor = groupBox1.BackColor;
            buttonСircle.BackColor = groupBox1.BackColor;
            button1.BackColor = groupBox1.BackColor;
            button12.BackColor = groupBox1.BackColor;
            button13.BackColor = Color.LightGray;
        }

       
    }
    
}
