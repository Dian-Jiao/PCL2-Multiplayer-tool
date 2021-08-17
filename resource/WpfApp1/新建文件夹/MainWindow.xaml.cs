using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.IO;
using MahApps.Metro.Controls;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow
    {
        public string rootPath;
        public string minecraftPath;
        public string selectVersion;
        public MainWindow()
        {
            InitializeComponent();
            rootPath = Directory.GetCurrentDirectory();
            
            string pclFeatureIni = File.ReadAllText(rootPath + "\\PCL\\Setup.ini");
            int pathStart = pclFeatureIni.IndexOf("LaunchFolderSelect")+19;
            int pathEnd = 0;
            for (pathEnd = pathStart; pathEnd < pclFeatureIni.Length; pathEnd++)
                if (pclFeatureIni[pathEnd] == '\n') break;
            minecraftPath = pclFeatureIni.Substring(pathStart, pathEnd - pathStart - 1);
            if (minecraftPath == "")
            {
                MessageBox.Show("请将该程序与 PCL2 置于同一目录中！");
                return ;
            }
            if (minecraftPath == "$.minecraft\\") minecraftPath = rootPath + "\\.minecraft\\";
            /// MessageBox.Show(minecraftPath);
            DirectoryInfo folder = new DirectoryInfo(minecraftPath + "versions");
            foreach (DirectoryInfo file in folder.GetDirectories())
            {
                listBox1.Items.Add(file.Name);
            }

        }

        private void GetPrivateProfileString(string v1, string v2, string v3, StringBuilder dir, int v4, string v5)
        {
            throw new NotImplementedException();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           selectVersion = listBox1.SelectedItem.ToString();
            text1.Content = selectVersion;
            toggle.IsEnabled = true;
            string pclFeatureIni = File.ReadAllText(minecraftPath + "versions\\" + selectVersion + "\\PCL\\Setup.ini");
            int loginStart = pclFeatureIni.IndexOf("VersionServerLogin");
            if (loginStart != -1 && pclFeatureIni[loginStart + 19] == '4') toggle.IsOn = true;
            else toggle.IsOn = false;
            /// else MessageBox.Show(serverStart.ToString() + pclFeatureIni[serverStart + 19].ToString());
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/Dian-Jiao/PCL2-Multiplayer-tool");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://mcskin.littleservice.cn/user/player");
        }

        

        private void button2_Click(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://mcskin.littleservice.cn/user/closet");
        }

        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch toggleSwitch = sender as ToggleSwitch;
            if (toggleSwitch != null)
            {
                if (toggleSwitch.IsOn == true)
                {
                    
                    string pclFeatureIni = File.ReadAllText(minecraftPath + "versions\\" + selectVersion + "\\PCL\\Setup.ini");
                    /// MessageBox.Show(pclFeatureIni);
                    int loginStart = pclFeatureIni.IndexOf("VersionServerLogin");
                    if (loginStart == -1) pclFeatureIni = pclFeatureIni.Insert(pclFeatureIni.Length, "\nVersionServerLogin:4");
                    else
                    {
                        int loginEnd = 0;
                        for (loginEnd = loginStart; loginEnd < pclFeatureIni.Length; loginEnd++)
                            if (pclFeatureIni[loginEnd] == '\n') break;
                       pclFeatureIni = pclFeatureIni.Replace(pclFeatureIni.Substring(loginStart, loginEnd - loginStart), "VersionServerLogin:4");
                    }
                    int authStart = pclFeatureIni.IndexOf("VersionServerAuthServer");
                    if (authStart == -1) pclFeatureIni = pclFeatureIni.Insert(pclFeatureIni.Length, "\nVersionServerAuthServer:https://mcskin.littleservice.cn/api/yggdrasil");
                    else
                    {
                        int authEnd = 0;
                        for (authEnd = authStart; authEnd < pclFeatureIni.Length; authEnd++)
                            if (pclFeatureIni[authEnd] == '\n') break;
                        pclFeatureIni = pclFeatureIni.Replace(pclFeatureIni.Substring(authStart, authEnd - authStart - 1), "VersionServerAuthServer:https://mcskin.littleservice.cn/api/yggdrasil");
                       
                    }
                    int registerStart = pclFeatureIni.IndexOf("VersionServerAuthRegister");
                    if (registerStart == -1) pclFeatureIni = pclFeatureIni.Insert(pclFeatureIni.Length, "\nVersionServerAuthRegister:https://mcskin.littleservice.cn/auth/register");
                    else
                    {
                        int registerEnd = 0;
                        for (registerEnd = registerStart; registerEnd < pclFeatureIni.Length; registerEnd++)
                            if (pclFeatureIni[registerEnd] == '\n') break;
                        pclFeatureIni = pclFeatureIni.Replace(pclFeatureIni.Substring(registerStart, registerEnd - registerStart - 1), "VersionServerAuthRegister:https://mcskin.littleservice.cn/auth/register");
                    }
                    int nameStart = pclFeatureIni.IndexOf("VersionServerAuthName");
                    if (nameStart == -1) pclFeatureIni = pclFeatureIni.Insert(pclFeatureIni.Length, "\nVersionServerAuthName:联机登录 - Little Skin");
                    else
                    {
                        int nameEnd = 0;
                        for (nameEnd = nameStart; nameEnd < pclFeatureIni.Length; nameEnd++)
                            if (pclFeatureIni[nameEnd] == '\n') break;
                        pclFeatureIni = pclFeatureIni.Replace(pclFeatureIni.Substring(nameStart, nameEnd - nameStart - 1), "VersionServerAuthName:联机登录 - Little Skin");
                    }
                    File.WriteAllText(minecraftPath + "versions\\" + selectVersion + "\\PCL\\Setup.ini", pclFeatureIni);
                    /// MessageBox.Show(pclFeatureIni);
                    /// MessageBox.Show("ON");
                }
                else
                {
                    string pclFeatureIni = File.ReadAllText(minecraftPath + "versions\\" + selectVersion + "\\PCL\\Setup.ini");
                    /// MessageBox.Show(pclFeatureIni);
                    int loginStart = pclFeatureIni.IndexOf("VersionServerLogin");
                    if (loginStart != -1)
                    {
                        int loginEnd = 0;
                        for (loginEnd = loginStart; loginEnd < pclFeatureIni.Length; loginEnd++)
                            if (pclFeatureIni[loginEnd] == '\n') break;
                        pclFeatureIni = pclFeatureIni.Replace(pclFeatureIni.Substring(loginStart, loginEnd - loginStart), "VersionServerLogin:0");
                    }
                    File.WriteAllText(minecraftPath + "versions\\" + selectVersion + "\\PCL\\Setup.ini", pclFeatureIni);
                    /// MessageBox.Show("OFF");
                }
            }
        }
    }
}
;
