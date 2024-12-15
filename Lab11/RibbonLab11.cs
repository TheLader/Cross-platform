using Microsoft.Office.Tools.Ribbon;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Lab11
{
    public partial class RibbonLab11
    {
        private void RibbonLab11_Load(object sender, RibbonUIEventArgs e)
        {

        }

        private void button1_Click(object sender, RibbonControlEventArgs e)
        {
            this.playLab("Lab1");
        }

        private void playLab(string labName)
        {
            try
            {
                string result = "";
                LabRunner labRunner = new LabRunner();
                string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"..", @"..", "input.txt");
                switch (labName)
                {
                    case "Lab1":
                        result = labRunner.RunLab1(filePath);
                        break;
                    case "Lab2":
                        result = labRunner.RunLab2(filePath);
                        break;
                    case "Lab3":
                        result = labRunner.RunLab3(filePath);
                        break;
                    default:
                        throw new ArgumentException("Nothing was selected");
                }

                MessageBox.Show("Answear: " + result);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, RibbonControlEventArgs e)
        {
            this.playLab("Lab2");
        }

        private void button3_Click(object sender, RibbonControlEventArgs e)
        {
            this.playLab("Lab3");
        }
    }
}
