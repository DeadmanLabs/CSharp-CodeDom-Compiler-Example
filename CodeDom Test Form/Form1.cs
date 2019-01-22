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
using System.CodeDom.Compiler;
using Microsoft.CSharp;
using System.CodeDom;

namespace CodeDom_Test_Form
{
    public partial class Form1 : Form
    {
        public String OutputFileName;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog Compile = new SaveFileDialog())
            {
                Compile.RestoreDirectory = true;
                Compile.InitialDirectory = System.Reflection.Assembly.GetExecutingAssembly().Location;
                Compile.Title = "Save Executable As...";
                Compile.Filter = "Executable File (*.exe)|*.exe";
                Compile.ShowDialog();
                OutputFileName = Compile.FileName;
            }
            CompilerParameters ConstructionRules = new CompilerParameters();
            ConstructionRules.GenerateExecutable = true;
            ConstructionRules.OutputAssembly = OutputFileName;
            ConstructionRules.ReferencedAssemblies.Add("System.dll");
            ConstructionRules.ReferencedAssemblies.Add("System.Linq.dll");
            ConstructionRules.ReferencedAssemblies.Add("System.Threading.Tasks.dll");
            ConstructionRules.ReferencedAssemblies.Add("System.Windows.Forms.dll");
            ConstructionRules.ReferencedAssemblies.Add("System.Core.dll");
            ConstructionRules.ReferencedAssemblies.Add("System.Data.dll");
            ConstructionRules.ReferencedAssemblies.Add("mscorlib.dll");
            //ConstructionRules.ReferencedAssemblies.Add("System.CodeDom.dll");
            //ConstructionRules.ReferencedAssemblies.Add("System.CodeDom.Compiler.dll");
            ConstructionRules.CompilerOptions = "/t:winexe";
            ConstructionRules.GenerateInMemory = false;
            CSharpCodeProvider csCode = new CSharpCodeProvider();
            String CodeContainer = CodeDom_Test_Form.Properties.Resources.StubSource.Replace("&&&MESSAGE&&&", textBox1.Text);
            CompilerResults Return = csCode.CompileAssemblyFromSource(ConstructionRules, CodeContainer.ToString());
            List<CompilerError> CErrors = Return.Errors.Cast<CompilerError>().ToList();
            foreach (CompilerError CurrError in CErrors)
            {
                MessageBox.Show(CurrError.ToString());
            }
        }
    }
}
