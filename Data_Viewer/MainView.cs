using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Data_Viewer
{
    public partial class MainView : Form
    {
        public const string wayside = @"Mrs. Gorf, Mrs. Jewles, Joe, Sharie, Todd, 
            Bebe, Calvin, Myron, Maurecia, Paul, Dana, Jason, Rondi, Sammy, Deedee, 
            D.J, John, Leslie, Miss Zarves, Kathy, Ron, Eric Fry, Eric Bacon, 
            Eric Ovens, Alison, Dameon, Jenny, Terrence, Joy, Nancy, Stephen, Louis";

        public const string smash = @"Mario, Luigi, Peach, Bowser, Yoshi, Wario,
            Donky Kong, Diddy Kong, Link, Zelda, Sheik, Gannondorf, Toon Link,
            Samus, Kirby, Meta Knight, King Dedede, Olimar, Fox, Falco, Wolf,
            Captain Falcon, Pikachu, Jigglypuff, Pokemon Trainer, Lucario, Ness,
            Lucas, Marth, Ike, Pit, Ice Climbers, R.O.B., Mr. Game & Watch, Snake,
            Sonic, Mewtwo, Dr. Mario, Young Link, Roy, Pichu";

        public const string treck = @"Pichard, Jean-Luc; Riker, William Thomas;
            Data; La Forge, Geordi; Worf; Crusher, Beverly; Pulaski, Katherine;
            Troi, Deanna; Yar, Natasha; Crusher, Wesley; Barclay, Reginald; Guinan;
            Q; Sisko, Benjamin; Kira, Nerys; Odo; Bashir, Julian; Dax, Jadzia;
            Dax, Ezri; O'Brien, Miles; Sisko, Jake; Quark; Janeway, Kathryn;
            Chakotay; Tuvok; Torres, B'Elanna; Paris, Tom; Kim, Harry; Doctor;
            Neelix; Kes; Seven of Nine; Seska; Vorik; Wildman, Naomi;
            Wildman, Smantha; Paris, Owen";

        public MainView()
        {
            InitializeComponent();
        }

        private void smashBrosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeViewer.ClearAll();
            treeViewer.InsertItems(smash);
        }

        private void starTreckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeViewer.ClearAll();
            treeViewer.InsertItems(treck);
        }

        private void waysideScholToolStripMenuItem_Click(object sender, EventArgs e)
        {
            treeViewer.ClearAll();
            treeViewer.InsertItems(wayside);
        }
    }
}
