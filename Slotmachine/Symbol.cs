using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slotmachine
{
    class Symbol
    {
        private int id;
        private string name;
        private double payout2;
        private double payout3;
        private double payout4;
        private double payout5;

         public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public double Payout2 { get => payout2; set => payout2 = value; }
        public double Payout3 { get => payout3; set => payout3 = value; }
        public double Payout4 { get => payout4; set => payout4 = value; }
        public double Payout5 { get => payout5; set => payout5 = value; }


        public Symbol(int id, string name, double payout2, double payout3, double payout4, double payout5)
        {
            this.id = id;
            this.name = name;
            this.payout2 = payout2;
            this.payout3 = payout3;
            this.payout4 = payout4;
            this.payout5 = payout5;
        }

      
        public override String ToString()
        {
            return String.Format("{0}\n{1}\n {2} {3} {4} {5}\n",this.id,this.name,this.payout2,this.payout3,this.payout4,this.payout5);
        }
    }
}
