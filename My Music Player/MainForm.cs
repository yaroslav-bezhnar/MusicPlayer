using System;
using System.Linq;
using System.Windows.Forms;

namespace MusicPlayer
{
    public partial class MainForm : Form
    {
        #region Constructors

        public MainForm()
        {
            InitializeComponent();
        }

        #endregion

        #region Private Methods

        private void buttonLoadFile_Click( object sender, EventArgs e )
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = @"e:\\";
            openFileDialog.Filter =
                @"MP3 Audio file (*.mp3)|*.mp3|Windows Media Audio File (*.wma)|*.wma|WAV Audio File (*.wav)|*.wav|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = false;
            openFileDialog.Multiselect = true;

            if ( openFileDialog.ShowDialog() == DialogResult.OK )
                try
                {
                    using ( openFileDialog.OpenFile() )
                    {
                        var fileNameAndPath = openFileDialog.FileNames;
                        var filename = openFileDialog.SafeFileNames;

                        for ( var i = 0; i < openFileDialog.SafeFileNames.Length; i++ )
                        {
                            var str = new string[2];
                            str[0] = filename[i];
                            str[1] = fileNameAndPath[i];

                            var listViewItem = new ListViewItem( str );
                            listView1.Items.Add( listViewItem );
                        }
                    }
                }
                catch
                {
                    MessageBox.Show( @"File not find", @"Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning );
                }
        }

        private void buttonPlayAll_Click( object sender, EventArgs e )
        {
            var play = axWindowsMediaPlayer1.playlistCollection.newPlaylist( @"myplaylist" );
            var allItems = listView1.Items.Cast<ListViewItem>().Select( el => el.SubItems[1].Text );

            foreach ( var item in allItems )
            {
                var media = axWindowsMediaPlayer1.newMedia( item );
                play.appendItem( media );
            }

            //set the media player current playlist to the one which we have created
            axWindowsMediaPlayer1.currentPlaylist = play;

            //play the files
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        private void listView1_DoubleClick( object sender, EventArgs e )
        {
            //play the selected files
            var select = listView1.FocusedItem.SubItems[1].Text;
            axWindowsMediaPlayer1.URL = select;
        }

        #endregion
    }
}