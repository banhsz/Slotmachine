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
        private Dictionary<int, PictureBox> tiles;
        private List<String> tickets;
        private Random rand;

        public Form1()
        {
            InitializeComponent();
            InitSymbols();
            InitTiles();
            InitTickets();
            rand = new Random();
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
            tickets = new List<string>();
            foreach (Symbol item in symbols)
            {
                for (int i = 0; i < Math.Round(1000 / item.Payout5); i++)
                {
                    tickets.Add(item.Name);
                }
            }
            /*
            foreach(String item in tickets)
            {
                Console.WriteLine(item);
            }
            */
        }
        private void button1_MouseClick(object sender, MouseEventArgs e)
        {
            Spin();
            //RevealTiles();
        }
        public async void Spin()
        {
            //Clear previous spin
            foreach (KeyValuePair<int, PictureBox> item in tiles)
            {
                item.Value.Image = Image.FromFile("loading.jpg");
            }

            //Generate next roll. i = coloumn
            for (int i = 0; i < 5; i++)
            {
                //item.Value.Visible = false;
                //We randomly select tickets and set the current tiles of the current coloumn(i)
                int ticket = rand.Next(0, tickets.Count());
                String symbol = tickets[ticket];
                tiles[1 + i].Image = Image.FromFile(symbol + ".jpg");

                ticket = rand.Next(0, tickets.Count());
                symbol = tickets[ticket];
                tiles[6 + i].Image = Image.FromFile(symbol + ".jpg");

                ticket = rand.Next(0, tickets.Count());
                symbol = tickets[ticket];
                tiles[11 + i].Image = Image.FromFile(symbol + ".jpg");
                await Task.Delay(50);
            }
        }

        /*
        public async void RevealTiles()
        {
            for (int i = 0; i < 5; i++)
            {
                tiles[1+i].Visible = true;
                tiles[6+i].Visible = true;
                tiles[11+i].Visible = true;
                await Task.Delay(300);
            }
        }
        */
    }
}
