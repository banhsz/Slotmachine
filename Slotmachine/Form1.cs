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
        //visuals
        private List<Symbol> symbols;
        private Dictionary<int, PictureBox> tiles;

        //logic
        private List<Symbol> tickets;
        private Random rand;
        private Symbol[,] board;
        private List<Coords[]> winlines;
        private double bet;
        private double balance;
        private string confirm;

        public Form1()
        {
            InitializeComponent();
            InitSymbols();
            InitTiles();
            InitTickets();
            InitWinLines();
            rand = new Random();

            bet = 50;
            balance = 100000;
            labelBet.Text = String.Format("{0}", 50);
            labelBalance.Text = String.Format("{0}",balance);

            confirm = "";
        }

        public void InitSymbols()
        {
            symbols = new List<Symbol>();
            //commons
            symbols.Add(new Symbol(0,"ten", 0, 0.5, 5, 10));
            symbols.Add(new Symbol(1,"jack", 0, 0.5, 5, 12.5));
            symbols.Add(new Symbol(2,"queen", 0, 0.5, 5, 15));
            symbols.Add(new Symbol(3,"king", 0, 0.8, 6, 20));
            symbols.Add(new Symbol(4,"ace", 0, 0.8, 6, 25));

            //rares
            symbols.Add(new Symbol(5,"blue", 0.2, 1, 7, 50));
            symbols.Add(new Symbol(6,"purple", 0.3, 1.2, 7.5, 75));
            symbols.Add(new Symbol(7,"pink", 0.4, 1.4, 8, 100));
            symbols.Add(new Symbol(8,"red", 0.5, 1.6, 9, 150));
            symbols.Add(new Symbol(9,"gold", 1, 2, 10, 500));

            //Show all symbol data in console
            /*
            foreach (Symbol item in symbols)
            {
                Console.WriteLine(item.ToString());
            }
            */
        }
        public void InitTiles()
        {
            //Tiles in panels
            tiles = new Dictionary<int, PictureBox>();
            tiles.Add(1, pictureBox1);
            tiles.Add(2, pictureBox2);
            tiles.Add(3, pictureBox3);
            tiles.Add(4, pictureBox4);
            tiles.Add(5, pictureBox5);
            tiles.Add(6, pictureBox6);
            tiles.Add(7, pictureBox7);
            tiles.Add(8, pictureBox8);
            tiles.Add(9, pictureBox9);
            tiles.Add(10, pictureBox10);
            tiles.Add(11, pictureBox11);
            tiles.Add(12, pictureBox12);
            tiles.Add(13, pictureBox13);
            tiles.Add(14, pictureBox14);
            tiles.Add(15, pictureBox15);
            //Tiles picture fit mode
            foreach (KeyValuePair<int, PictureBox> item in tiles)
            {
                item.Value.SizeMode = PictureBoxSizeMode.Zoom;
                item.Value.Image = Image.FromFile("loading.jpg");
            }

        }
        public void InitTickets()
        {
            //We fill a list with tickets that represent the symbols
            //Each symbol has 1000/payout5 tickets. Rare symbol -> less tickets
            tickets = new List<Symbol>();
            foreach (Symbol item in symbols)
            {
                for (int i = 0; i < Math.Round(1000 / item.Payout5); i++)
                {
                    tickets.Add(item);
                }
            }
            /*
            foreach(String item in tickets)
            {
                Console.WriteLine(item);
            }
            */
        }
        public void InitWinLines()
        {
            winlines = new List<Coords[]>();
            //line 1
            winlines.Add(new Coords[] { new Coords(1, 0), new Coords(1, 1), new Coords(1, 2), new Coords(1, 3), new Coords(1, 4) });
            //line 2
            winlines.Add(new Coords[] { new Coords(0, 0), new Coords(0, 1), new Coords(0, 2), new Coords(0, 3), new Coords(0, 4) });
            //line 3
            winlines.Add(new Coords[] { new Coords(2, 0), new Coords(2, 1), new Coords(2, 2), new Coords(2, 3), new Coords(2, 4) });

            //line 4
            winlines.Add(new Coords[] { new Coords(0, 0), new Coords(1, 1), new Coords(2, 2), new Coords(1, 3), new Coords(0, 4) });
            //line 5
            winlines.Add(new Coords[] { new Coords(2, 0), new Coords(1, 1), new Coords(0, 2), new Coords(1, 3), new Coords(2, 4) });

            //line 6
            winlines.Add(new Coords[] { new Coords(0, 0), new Coords(1, 1), new Coords(0, 2), new Coords(1, 3), new Coords(0, 4) });
            //line 7
            winlines.Add(new Coords[] { new Coords(1, 0), new Coords(2, 1), new Coords(1, 2), new Coords(2, 3), new Coords(1, 4) });
            //line 8
            winlines.Add(new Coords[] { new Coords(1, 0), new Coords(0, 1), new Coords(1, 2), new Coords(0, 3), new Coords(1, 4) });
            //line 9
            winlines.Add(new Coords[] { new Coords(2, 0), new Coords(1, 1), new Coords(2, 2), new Coords(1, 3), new Coords(2, 4) });

            //line 10
            winlines.Add(new Coords[] { new Coords(0, 0), new Coords(1, 1), new Coords(2, 2), new Coords(2, 3), new Coords(2, 4) });
        }
        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            Spin();
        }
        public async void Spin()
        {
            if (balance-bet>=0)
            {
                confirm = "";

                //Take away bet
                balance -= bet;
                labelBalance.Text = String.Format("{0}",balance);

                //Clear previous spin
                foreach (KeyValuePair<int, PictureBox> item in tiles)
                {
                    item.Value.Image = Image.FromFile("loading.jpg");
                }
                board = new Symbol[3, 5];
                labelWin.Text = "";

                //Generate next roll. i = coloumn
                for (int i = 0; i < 5; i++)
                {
                    //item.Value.Visible = false;
                    //We randomly select tickets and set the current tiles of the current coloumn(i). also store the values in out symbol matrix
                    int ticket = rand.Next(0, tickets.Count());
                    Symbol symbol = tickets[ticket];
                    tiles[1 + i].Image = Image.FromFile(symbol.Name + ".jpg");
                    board[0, i] = symbol;

                    ticket = rand.Next(0, tickets.Count());
                    symbol = tickets[ticket];
                    tiles[6 + i].Image = Image.FromFile(symbol.Name + ".jpg");
                    board[1, i] = symbol;

                    ticket = rand.Next(0, tickets.Count());
                    symbol = tickets[ticket];
                    tiles[11 + i].Image = Image.FromFile(symbol.Name + ".jpg");
                    board[2, i] = symbol;
                    await Task.Delay(100);
                }

                //console log
                for (int j = 0; j < 3 ; j++)
                {
                    for (int k = 0; k < 5; k++)
                    {
                        //Console.Write(board[j,k].Name+"\t");
                        Console.Write(String.Format("{0,-10}", board[j, k].Name));
                    }
                    Console.WriteLine();
                }

                //payout lines
                int lineIndex = 1;
                double totalPayout = 0;
                Console.WriteLine("Bet: " + bet);
                confirm += "Bet: " + bet + "\n\n";
                foreach (var item in winlines)
                {
                    int comboLength = 1;
                    Symbol comboSymbol = board[item[0].X, item[0].Y];
                    bool inACombo = true;
                    for (int i = 1; i < item.Length; i++)
                    {
                        //Console.Write(board[item[i].X, item[i].Y].Name);
                        if (inACombo && board[item[i].X, item[i].Y] == comboSymbol)
                        {
                            comboLength++;
                        }
                        else
                        {
                            inACombo = false;
                        }
                    }
                    Console.WriteLine("Line " + lineIndex);
                    confirm += "Line " + lineIndex + "\n";
                    Console.WriteLine("Combo Symbol: {0}, Combo length: {1}", comboSymbol.Name, comboLength);
                    confirm += String.Format("Combo Symbol: {0}, Combo length: {1}\n", comboSymbol.Name, comboLength);

                    double linePayout = 0;
                    switch (comboLength)
                    {
                        case 1:
                            Console.WriteLine("Multiplier: 0");
                            confirm += String.Format("Multiplier: 0\n");
                            break;
                        case 2:
                            Console.WriteLine("Multiplier: {0}", comboSymbol.Payout2);
                            confirm += String.Format("Multiplier: {0}\n", comboSymbol.Payout2);
                            linePayout = bet * 1.00 * comboSymbol.Payout2; 
                            break;
                        case 3:
                            Console.WriteLine("Multiplier: {0}", comboSymbol.Payout3);
                            confirm += String.Format("Multiplier: {0}\n", comboSymbol.Payout3);
                            linePayout = bet * 1.00 * comboSymbol.Payout3;
                            break;
                        case 4:
                            Console.WriteLine("Multiplier: {0}", comboSymbol.Payout4);
                            confirm += String.Format("Multiplier: {0}\n", comboSymbol.Payout4);
                            linePayout = bet * 1.00 * comboSymbol.Payout4;
                            break;
                        case 5:
                            Console.WriteLine("Multiplier: {0}", comboSymbol.Payout5);
                            confirm += String.Format("Multiplier: {0}\n", comboSymbol.Payout5);
                            linePayout = bet * 1.00 * comboSymbol.Payout5;
                            break;
                        default:
                            break;
                    }
                    Console.WriteLine("Line payout: " + linePayout);
                    confirm += String.Format("Line payout: " + linePayout + "\n\n");
                    totalPayout += linePayout;
                    Console.WriteLine();
                    lineIndex++;
                }
                Console.WriteLine("Total win: " + totalPayout);
                confirm += String.Format("Total win: " + totalPayout + "\n");
                labelWin.Text = String.Format("You won {0}", totalPayout);

                //give win
                balance += totalPayout;
                labelBalance.Text = String.Format("{0}", balance);

                //if confirm invisible make it visible
                if (buttonConfirm.Visible == false)
                {
                    buttonConfirm.Visible = true;
                }
            }
            else
            {
                MessageBox.Show("You have insufficient funds. Lower your bet or top up your balance!","Information");
            }
        }
        private void buttonLower_Click(object sender, EventArgs e)
        {
            if (bet>50)
            {
                bet -= 50;
                labelBet.Text = String.Format("{0}",bet);
            }
        }

        private void buttonHigher_Click(object sender, EventArgs e)
        {
            if (bet < 1000)
            {
                bet += 50;
                labelBet.Text = String.Format("{0}", bet);
            }
        }

        private void buttonConfirm_Click(object sender, EventArgs e)
        {
            MessageBox.Show(confirm, "Information");
        }

        private void buttonHelp_Click(object sender, EventArgs e)
        {
            Help form = new Help();
            form.Show();
        }
    }
}
