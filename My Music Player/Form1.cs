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

namespace My_Music_Player
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonLoadFile_Click(object sender, EventArgs e)
        {
            Stream loadFile = null;
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = "e:\\";
            openFileDialog.Filter = "MP3 Audio file (*.mp3)|*.mp3|Windows Media Audio File (*.wma)|*.wma|WAV Audio File (*.wav)|*.wav|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.RestoreDirectory = false;
            openFileDialog.Multiselect = true;

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    if ((loadFile = openFileDialog.OpenFile()) != null)
                    {
                        using (loadFile)
                        {
                            string[] fileNameAndPath = openFileDialog.FileNames;
                            string[] filename = openFileDialog.SafeFileNames;

                            for (int i = 0; i < openFileDialog.SafeFileName.Count(); i++)
                            {
                                //adding each file to the list view
                                //str[0]=main file or the safe file name
                                //str[1]=secontd main file or the absolute path used by media player
                                string[] str = new string[2];
                                str[0] = filename[i];
                                str[1] = fileNameAndPath[i];

                                ListViewItem listViewItem = new ListViewItem(str);
                                listView1.Items.Add(listViewItem);
                            }
                        }
                    }
                }
                //if file not found
                catch (Exception)
                {
                    MessageBox.Show("File not find", "Warning!");
                }
            }
        }

        private void buttonPlayAll_Click(object sender, EventArgs e)
        {
            WMPLib.IWMPPlaylist play = axWindowsMediaPlayer1.playlistCollection.newPlaylist("myplaylist");
            WMPLib.IWMPMedia media;

            for (int i = 0; i < listView1.Items.Count; i++)
            {
                int j = 1;

                //add subitems to the playlist
                //playlist=basic text file contain absolute path to the rext files
                media = axWindowsMediaPlayer1.newMedia(listView1.Items[i].SubItems[j].Text);
                play.appendItem(media);
                j++;

                //set the media player current playlist to the one which we have created
                axWindowsMediaPlayer1.currentPlaylist = play;

                //play the files
                axWindowsMediaPlayer1.Ctlcontrols.play();
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            //play the selected files
            string select = listView1.FocusedItem.SubItems[1].Text;
            axWindowsMediaPlayer1.URL = @select;
        }
    }
}
