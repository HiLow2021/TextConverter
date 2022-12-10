using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using My.IO;
using My.Security;

namespace TextConverter
{
    public enum MyEncoding
    {
        Shift_JIS = 932,
        EUC_JP = 51932,
        US_ASCII = 20127,
        UTF_16 = 1200,
        UTF_8 = 65001
    }

    public partial class Form1 : Form
    {
        readonly string _ConfigPath = @"config.dat";
        MyAppSettings _AppSettings = new MyAppSettings();

        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadConfigFile();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            SaveConfigFile();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Index = comboBox1.SelectedIndex;

            if (Index == 2)
            {
                checkBox2.Enabled = true;
                checkBox3.Enabled = true;
                button4.Enabled = true;
            }
            else
            {
                checkBox2.Enabled = false;
                checkBox3.Enabled = false;
                button4.Enabled = false;
            }
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Index = comboBox2.SelectedIndex;

            switch (Index)
            {
                case 0:
                    _AppSettings.ReadEncoding = MyEncoding.Shift_JIS;
                    break;

                case 1:
                    _AppSettings.ReadEncoding = MyEncoding.UTF_8;
                    break;

                case 2:
                    _AppSettings.ReadEncoding = MyEncoding.UTF_16;
                    break;

                case 3:
                    _AppSettings.ReadEncoding = MyEncoding.EUC_JP;
                    break;

                case 4:
                    _AppSettings.ReadEncoding = MyEncoding.US_ASCII;
                    break;
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {
            int Index = comboBox3.SelectedIndex;

            switch (Index)
            {
                case 0:
                    _AppSettings.WriteEncoding = MyEncoding.Shift_JIS;
                    break;

                case 1:
                    _AppSettings.WriteEncoding = MyEncoding.UTF_8;
                    break;

                case 2:
                    _AppSettings.WriteEncoding = MyEncoding.UTF_16;
                    break;

                case 3:
                    _AppSettings.WriteEncoding = MyEncoding.EUC_JP;
                    break;

                case 4:
                    _AppSettings.WriteEncoding = MyEncoding.US_ASCII;
                    break;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            string SaveFilePath = textBox2.Text;

            if (string.IsNullOrEmpty(SaveFilePath))
            {
                return;
            }

            if (checkBox1.Checked)
            {
                SaveFilePath = AddOutToFullPath(SaveFilePath);
            }
            else
            {
                SaveFilePath = RemoveOutFromFullPath(SaveFilePath);
            }

            textBox2.Text = SaveFilePath;
        }

        private void textBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                (sender as TextBox).SelectAll();

                e.SuppressKeyPress = true;
                e.Handled = true;
            }
        }

        private void textBox_TextChanged(object sender, EventArgs e)
        {
            (sender as TextBox).SelectionStart = (sender as TextBox).Text.Length;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "テキスト形式|*.txt;*.log|CSV形式|*.csv";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string LoadFilePath = openFileDialog1.FileName;
                string SaveFilePath;

                if (checkBox1.Checked)
                {
                    SaveFilePath = AddOutToFullPath(LoadFilePath);
                }
                else
                {
                    SaveFilePath = LoadFilePath;
                }

                textBox1.Text = LoadFilePath;
                textBox2.Text = SaveFilePath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                string SaveFilePath = textBox2.Text;

                SaveFilePath = folderBrowserDialog1.SelectedPath + Path.DirectorySeparatorChar + Path.GetFileName(SaveFilePath);
                textBox2.Text = SaveFilePath;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string LoadFilePath = textBox1.Text;

            if (File.Exists(LoadFilePath))
            {
                textBox5.Text = File.ReadAllText(LoadFilePath, Encoding.GetEncoding((int)_AppSettings.ReadEncoding));
                textBox6.Text = string.Empty;
            }
            else
            {
                textBox6.Text = "【エラーが発生しました】" + Environment.NewLine + "ファイルが見つかりませんでした。";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            string SaveFilePath = textBox2.Text;

            if (!Directory.Exists(Path.GetDirectoryName(SaveFilePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(SaveFilePath));
            }

            File.WriteAllText(SaveFilePath, ActRegex(), Encoding.GetEncoding((int)_AppSettings.WriteEncoding));
        }

        private void button5_Click(object sender, EventArgs e)
        {
            textBox6.Text = ActRegex();
        }

        private string ActRegex()
        {
            int Index = comboBox1.SelectedIndex;
            StringBuilder InputText = new StringBuilder(textBox5.Text);
            StringBuilder ResultText = new StringBuilder();
            string SearchPattern = textBox3.Text;
            string ReplacePattern = textBox4.Text;
            RegexOptions PatternOptions = GetRegexOptions();

            try
            {
                if (Index == 0)
                {
                    MatchCollection Results;

                    Results = Regex.Matches(InputText.ToString(), SearchPattern, PatternOptions);

                    if (Results.Count <= InputText.Length)
                    {
                        ResultText.Capacity = Results.Count * 52;
                    }
                    else
                    {
                        throw new TooMuchMatchesException();
                    }

                    for (int i = 0; i < Results.Count; i++)
                    {
                        ResultText.Append("Matches[");
                        ResultText.Append(i.ToString());
                        ResultText.Append("]:");
                        ResultText.AppendLine(Results[i].Value);

                        for (int j = 0; j < Results[i].Groups.Count; j++)
                        {
                            ResultText.Append("   Groups[");
                            ResultText.Append(j.ToString());
                            ResultText.Append("]:");
                            ResultText.AppendLine(Results[i].Groups[j].Value);

                            for (int k = 0; k < Results[i].Groups[j].Captures.Count; k++)
                            {
                                ResultText.Append("      Captures[");
                                ResultText.Append(k.ToString());
                                ResultText.Append("]:");
                                ResultText.AppendLine(Results[i].Groups[j].Captures[k].Value);
                            }
                        }

                        ResultText.AppendLine();
                    }
                }
                else if (Index == 1)
                {
                    ResultText.Append(Regex.IsMatch(InputText.ToString(), SearchPattern, PatternOptions).ToString());
                }
                else if (Index == 2)
                {
                    if (checkBox2.Checked && SearchPattern != string.Empty)
                    {
                        string[] SearchPatterns = textBox3.Lines;
                        string[] ReplacePatterns = textBox4.Lines;

                        for (int i = 0; i < SearchPatterns.Length; i++)
                        {
                            do
                            {
                                ResultText.Clear();

                                if (i < ReplacePatterns.Length)
                                {
                                    if (checkBox3.Checked && !IsRepeat(SearchPatterns[i], ReplacePatterns[i], PatternOptions))
                                    {
                                        throw new CanNotRepeatException();
                                    }

                                    ResultText.Append(Regex.Replace(InputText.ToString(), SearchPatterns[i], ReplacePatterns[i], PatternOptions));
                                }
                                else
                                {
                                    if (checkBox3.Checked && !IsRepeat(SearchPatterns[i], string.Empty, PatternOptions))
                                    {
                                        throw new CanNotRepeatException();
                                    }

                                    ResultText.Append(Regex.Replace(InputText.ToString(), SearchPatterns[i], string.Empty, PatternOptions));
                                }

                                InputText.Clear();
                                InputText.Append(ResultText.ToString());
                            }
                            while (checkBox3.Checked && Regex.IsMatch(InputText.ToString(), SearchPatterns[i], PatternOptions));
                        }
                    }
                    else
                    {
                        do
                        {
                            if (checkBox3.Checked && !IsRepeat(SearchPattern, ReplacePattern, PatternOptions))
                            {
                                throw new CanNotRepeatException();
                            }

                            ResultText.Clear();
                            ResultText.Append(Regex.Replace(InputText.ToString(), SearchPattern, ReplacePattern, PatternOptions));
                            InputText.Clear();
                            InputText.Append(ResultText.ToString());
                        }
                        while (checkBox3.Checked && Regex.IsMatch(InputText.ToString(), SearchPattern, PatternOptions));
                    }
                }
                else
                {
                    string[] Results;

                    Results = Regex.Split(InputText.ToString(), SearchPattern, PatternOptions);
                    ResultText.Capacity = Results.Length;

                    foreach (string Result in Results)
                    {
                        ResultText.AppendLine(Result);
                    }
                }
            }
            catch (Exception e)
            {
                if (e is ArgumentException || e is TooMuchMatchesException || e is CanNotRepeatException)
                {
                    ResultText.AppendLine("【エラーが発生しました】");
                    ResultText.Append(e.Message);
                }
            }

            return ResultText.ToString();
        }

        private RegexOptions GetRegexOptions()
        {
            RegexOptions RO = RegexOptions.None;

            if (checkedListBox1.GetItemChecked(0))
            {
                RO |= RegexOptions.None;
            }
            if (checkedListBox1.GetItemChecked(1))
            {
                RO |= RegexOptions.IgnoreCase;
            }
            if (checkedListBox1.GetItemChecked(2))
            {
                RO |= RegexOptions.Multiline;
            }
            if (checkedListBox1.GetItemChecked(3))
            {
                RO |= RegexOptions.ExplicitCapture;
            }
            if (checkedListBox1.GetItemChecked(4))
            {
                RO |= RegexOptions.Compiled;
            }
            if (checkedListBox1.GetItemChecked(5))
            {
                RO |= RegexOptions.Singleline;
            }
            if (checkedListBox1.GetItemChecked(6))
            {
                RO |= RegexOptions.IgnorePatternWhitespace;
            }
            if (checkedListBox1.GetItemChecked(7))
            {
                RO |= RegexOptions.RightToLeft;
            }
            if (checkedListBox1.GetItemChecked(8))
            {
                RO |= RegexOptions.ECMAScript;
            }
            if (checkedListBox1.GetItemChecked(9))
            {
                RO |= RegexOptions.CultureInvariant;
            }

            return RO;
        }

        private bool IsRepeat(string SearchPattern, string ReplacePattern, RegexOptions PatternOptions)
        {
            return !Regex.IsMatch(ReplacePattern, SearchPattern, PatternOptions);
        }

        private string AddOutToFullPath(string FullPath)
        {
            Match M;
            string NewFileNameWithoutExtension;
            string OldFileNameWithoutExtension;
            string SearchPattern = @"(_out)(\d*)$";
            string ReplacePattern;

            OldFileNameWithoutExtension = Path.GetFileNameWithoutExtension(FullPath);

            if (Regex.IsMatch(OldFileNameWithoutExtension, SearchPattern))
            {
                M = Regex.Match(OldFileNameWithoutExtension, SearchPattern);

                if (M.Groups[2].Value == string.Empty)
                {
                    ReplacePattern = M.Groups[1].Value + "2";
                }
                else
                {
                    ReplacePattern = M.Groups[1].Value + (int.Parse(M.Groups[2].Value) + 1).ToString();
                }

                NewFileNameWithoutExtension = Regex.Replace(OldFileNameWithoutExtension, SearchPattern, ReplacePattern);
            }
            else
            {
                NewFileNameWithoutExtension = OldFileNameWithoutExtension + "_out";
            }

            return Path.GetDirectoryName(FullPath) + Path.DirectorySeparatorChar + NewFileNameWithoutExtension + Path.GetExtension(FullPath);
        }

        private string RemoveOutFromFullPath(string FullPath)
        {
            Match M;
            string NewFileNameWithoutExtension;
            string OldFileNameWithoutExtension;
            string SearchPattern = @"(_out)(\d*)$";
            string ReplacePattern;

            OldFileNameWithoutExtension = Path.GetFileNameWithoutExtension(FullPath);

            if (Regex.IsMatch(OldFileNameWithoutExtension, SearchPattern))
            {
                M = Regex.Match(OldFileNameWithoutExtension, SearchPattern);

                if (M.Groups[2].Value == string.Empty)
                {
                    ReplacePattern = string.Empty;
                }
                else if (M.Groups[2].Value == "2")
                {
                    ReplacePattern = M.Groups[1].Value;
                }
                else
                {
                    ReplacePattern = M.Groups[1].Value + (int.Parse(M.Groups[2].Value) - 1).ToString();
                }

                NewFileNameWithoutExtension = Regex.Replace(OldFileNameWithoutExtension, SearchPattern, ReplacePattern);
            }
            else
            {
                NewFileNameWithoutExtension = OldFileNameWithoutExtension;
            }

            return Path.GetDirectoryName(FullPath) + Path.DirectorySeparatorChar + NewFileNameWithoutExtension + Path.GetExtension(FullPath);
        }

        private void LoadConfigFile() // 設定ファイルの読み込みメソッド。
        {
            if (FileAdvanced.Exists(_ConfigPath))
            {
                byte[] bs = FileAdvanced.LoadFromBinaryFile<byte[]>(_ConfigPath);

                _AppSettings = Cryptography.Decrypt<MyAppSettings>(bs);
                TopMost = _AppSettings.IsTopMost;

                if (_AppSettings.IsFixedWindowsPos)
                {
                    Left = _AppSettings.Left;
                    Top = _AppSettings.Top;
                }

                comboBox1.SelectedIndex = _AppSettings.MethodMode;
                comboBox2.SelectedIndex = _AppSettings.ReadEncodingIndex;
                comboBox3.SelectedIndex = _AppSettings.WriteEncodingIndex;
                checkBox1.Checked = _AppSettings.IsAddOut;
                checkBox2.Checked = _AppSettings.IsDifferentPattern;
                checkBox3.Checked = _AppSettings.IsRepeatMode;
                textBox1.Text = _AppSettings.LoadFilePath;
                textBox2.Text = _AppSettings.SaveFilePath;
            }
        }

        private void SaveConfigFile() // 設定ファイルへの書き込みメソッド。
        {
            if (WindowState == FormWindowState.Normal)
            {
                _AppSettings.Left = Left;
                _AppSettings.Top = Top;
            }

            _AppSettings.MethodMode = comboBox1.SelectedIndex;
            _AppSettings.ReadEncodingIndex = comboBox2.SelectedIndex;
            _AppSettings.WriteEncodingIndex = comboBox3.SelectedIndex;
            _AppSettings.IsAddOut = checkBox1.Checked;
            _AppSettings.IsDifferentPattern = checkBox2.Checked;
            _AppSettings.IsRepeatMode = checkBox3.Checked;
            _AppSettings.LoadFilePath = textBox1.Text;
            _AppSettings.SaveFilePath = textBox2.Text;

            FileAdvanced.SaveToBinaryFile(_ConfigPath, Cryptography.Encrypt(_AppSettings));
        }
    }
}
