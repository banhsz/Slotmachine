using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Slotmachine
{
    public partial class Form1 : Form
    {
        private List<Symbol> symbols;
        public Form1()
        {
            InitializeComponent();
            InitSymbols();
        }

        public void InitSymbols()
        {
            symbols = new List<Symbol>();
            //commons
            symbols.Add(new Symbol("ten", 0, 0.5, 5, 10));
            symbols.Add(new Symbol("jack", 0, 0.5, 5, 12.5));
            symbols.Add(new Symbol("queen", 0, 0.5, 5, 15));
            symbols.Add(new Symbol("king", 0, 0.8, 6, 20));
            symbols.Add(new Symbol("ace", 0, 0.8, 6, 25));

            //rares
            symbols.Add(new Symbol("blue", 0.2, 1, 7, 50));
            symbols.Add(new Symbol("purple", 0.3, 1.2, 7.5, 75));
            symbols.Add(new Symbol("pink", 0.4, 1.4, 8, 100));
            symbols.Add(new Symbol("red", 0.5, 1.6, 9, 150));
            symbols.Add(new Symbol("gold", 1, 2, 10, 500));

            foreach (Symbol item in symbols)
            {
                Console.WriteLine(item.ToString());
            }
        }
    }
}
