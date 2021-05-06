using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slotmachine
{
    class Symbol
    {
        private string name;
        private double payout2;
        private double payout3;
        private double payout4;
        private double payout5;

        public string Name { get => name; }
        public double Payout2 { get => payout2; }
        public double Payout3 { get => payout3; }
        public double Payout4 { get => payout4; }
        public double Payout5 { get => payout5; }

        public Symbol(string name, double payout2, double payout3, double payout4, double payout5)
        {
            this.name = name;
            this.payout2 = payout2;
            this.payout3 = payout3;
            this.payout4 = payout4;
            this.payout5 = payout5;
        }

        public override String ToString()
        {
            return String.Format("{0}\n{1} {2} {3} {4}\n",this.name,this.payout2,this.payout3,this.payout4,this.payout5);
        }
    }
}
