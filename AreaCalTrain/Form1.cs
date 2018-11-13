using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HalconDotNet;
using System.IO;

namespace haocon_ocr_0518
{
    public partial class Form1 : Form
    {
        string ImagePath;
        string[] fileList = new string[666];
        int fileListIndex = 0;
        int fileCount = 0;
        bool batchFlag;

        HDevelopExport HD = new HDevelopExport();
        string knownStr = "";
        const string orderCharStr ="0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        char[] orderChar = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        int[] areaMin = new int[62] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] areaMinLast = new int[62] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] areaMinUlt = new int[62] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] areaMaxUlt = new int[62] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] areaMax = new int[62] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DateTime beforDT = System.DateTime.Now;
            HD.ProcessImage(hWindowControl1.HalconWindow, ImagePath);
            DateTime afterDT = System.DateTime.Now;
            TimeSpan ts = afterDT.Subtract(beforDT);
            string str = "";
            str = textBoxPriorChar.Text;
            labelChar.Text = "";
            for (int i = 0; i < str.Length; i++)
            {
                labelChar.Text += str[i] + "    ";
            }
            str = "";
            for (int i = 0; i < HD.Area.Length; i++)
                str += HD.Area[i].I.ToString().PadRight(5, ' ');
            textBoxMeasureValue.Text = str;
            str = "";
            for (int i = 0; i < textBoxPriorChar.Text.Length; i++)
            {
                str += areaMin[Array.IndexOf(orderChar, textBoxPriorChar.Text[i])].ToString().PadRight(5, ' ');
            }
            textBoxSettingValue.Text = str;
            if (!HD.hv_ErrorCounts)
            {
                //labelMessage.Text += "合格";
                textBoxAreaMin.Text = "喷码：合格" + "\r\n";
                textBoxAreaMin.Text += "时间：" + ts.TotalMilliseconds.ToString() + "ms";
            }
            else
            {
                if (HD.hv_ErrorCounts == 1)
                {
                    textBoxAreaMin.Text += "不合格, 第一行缺失字符";
                }
                else if (HD.hv_ErrorCounts == 2)
                {
                    textBoxAreaMin.Text += "不合格, 第二行缺失字符";
                }
                else
                {
                    labelMessage.Text += "不合格, 缺失多个字符";
                }
            }
            textBoxPriorChar.BackColor = Color.Blue;
            //label6.Text = "";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            batchFlag = false;
            openFileDialog1.ShowDialog();
            ImagePath = openFileDialog1.FileName;
            HD.ShowImage(hWindowControl1.HalconWindow, ImagePath);
            labelMessage.Text = ImagePath;
        }

        private void saveValue(object sender, EventArgs e)
        {
            for (int i = 0; i < areaMin.Length; i++)
            {
                areaMinLast[i] = areaMin[i];
            }
            knownStr = textBoxPriorChar.Text;
            for (int i = 0; i < HD.Area.Length; i++)
            {
                if ((areaMin[Array.IndexOf(orderChar, knownStr[i])] > HD.Area[i]) || areaMin[Array.IndexOf(orderChar, knownStr[i])]==0)
                {
                    areaMin[Array.IndexOf(orderChar, knownStr[i])] = HD.Area[i];
                }
                if ((areaMax[Array.IndexOf(orderChar, knownStr[i])] < HD.Area[i]) || areaMax[Array.IndexOf(orderChar, knownStr[i])] == 0)
                {
                    areaMax[Array.IndexOf(orderChar, knownStr[i])] = HD.Area[i];
                }
            }
            string str = "";
            for (int i = 0; i < textBoxPriorCharAll.Text.Length; i++)
            {
                str += areaMin[Array.IndexOf(orderChar, textBoxPriorCharAll.Text[i])].ToString() + ", ";
            }
            textBoxAreaMin.Text = str;
            str = "";
            //labelMessage.Text += "   已保存";
        }

        private void buttonProcMultiImg_Click(object sender, EventArgs e)
        {
            if (!HD.hv_ErrorCounts)
            {
                label6.Text = "上次已保存";
                saveValue(sender, e);
            }
            else
            {
                label6.Text = "上次未保存";
            }
            textBoxPriorChar.BackColor = Color.White;
            if (batchFlag)
            {
                if (fileListIndex < fileCount)
                {
                    ImagePath = fileList[fileListIndex++];
                    HD.ShowImage(hWindowControl1.HalconWindow, ImagePath);
                    System.Threading.Thread.Sleep(500);
                    buttonNextImg_Click(sender, e);
                }
                else
                {
                    labelMessage.Text = "图片已加载完";
                }
            }
        }

        private void buttonOpenDir_Click(object sender, EventArgs e)
        {
            batchFlag = true;
            if (folderBrowserDialog1.ShowDialog()==DialogResult.OK)
            {
                fileListIndex = 0;
                fileCount = 0;
                DirectoryInfo folder = new DirectoryInfo(folderBrowserDialog1.SelectedPath);
                labelMessage.Text = folderBrowserDialog1.SelectedPath;
                string str = "";
                foreach (FileInfo file in folder.GetFiles("*.bmp"))
                {
                    fileList[fileListIndex++]=file.FullName;
                    fileCount++;
                    str += file.FullName+"\n";
                }
                fileListIndex = 0;
                ImagePath = fileList[0];
                textBoxAreaMin.Text = str;
            }
        }

        private void buttonNextImg_Click(object sender, EventArgs e)
        {
                
            label5.Text = (fileListIndex+1).ToString()+" /"+fileCount.ToString();
            labelMessage.Text = ImagePath;
            if (ImagePath != "")
            {
                button1_Click(sender, e);
            }
            else
            {
                labelMessage.Text = "路径为空";
            }
        }

        private void buttonRestoreLast_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < areaMin.Length; i++)
            {
                areaMin[i] = areaMinUlt[i];
                areaMax[i] = areaMaxUlt[i];
            }
            string str = "";
            for (int i = 0; i < textBoxPriorCharAll.Text.Length; i++)
            {
                str += areaMin[Array.IndexOf(orderChar, textBoxPriorCharAll.Text[i])].ToString() + ", ";
            }
            textBoxAreaMin.Text = str;
            str = "";
            labelMessage.Text += "   已回滚";
        }

        private void buttonSaveResults_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < areaMin.Length; i++)
            {
                areaMinUlt[i] = areaMin[i];
                areaMaxUlt[i] = areaMax[i];
            }
        }

        private void buttonShowResults_Click(object sender, EventArgs e)
        {
            string str = "";
            for (int i = 0; i < textBoxPriorCharAll.Text.Length; i++)
            {
                str += areaMinUlt[Array.IndexOf(orderChar, textBoxPriorCharAll.Text[i])].ToString() + ", ";
            }
            str += "\n\r***";
            for (int i = 0; i < textBoxPriorCharAll.Text.Length; i++)
            {
                str += areaMaxUlt[Array.IndexOf(orderChar, textBoxPriorCharAll.Text[i])].ToString() + ", ";
            }
            textBoxAreaMin.Text = str;
        }

        private void buttonJump_Click(object sender, EventArgs e)
        {
            if (!HD.hv_ErrorCounts)
            {
                label6.Text = "上次已跳过";
            }
            else
            {
                label6.Text = "上次已跳过";
            }
            textBoxPriorChar.BackColor = Color.White;
            if (batchFlag)
            {
                if (fileListIndex < fileCount)
                {
                    ImagePath = fileList[fileListIndex++];
                    HD.ShowImage(hWindowControl1.HalconWindow, ImagePath);
                    System.Threading.Thread.Sleep(500);
                    buttonNextImg_Click(sender, e);
                }
                else
                {
                    labelMessage.Text = "图片已加载完";
                }
            }
        }
    }

    public partial class HDevelopExport
    {
#if !NO_EXPORT_APP_MAIN
        public HDevelopExport()
        {
            // Default settings used in HDevelop 
            HOperatorSet.SetSystem("do_low_error", "false");
            //action(HDWindow, ImagePath_);
        }
#endif
        public HTuple hv_ErrorCounts = 0;
        // Main procedure 
        public HTuple Area = new HTuple();
        string ImageFile;

        public void ShowImage(HTuple HDWindow_, string ImageFile_)
        {
            HObject ho_Image;
            HOperatorSet.GenEmptyObj(out ho_Image);
            HDevWindowStack.Push(HDWindow_);
            ho_Image.Dispose();
            HOperatorSet.ReadImage(out ho_Image, ImageFile_);
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_Image, HDevWindowStack.GetActive());
            }
        }

        public void ProcessImage(HTuple HDWindow_, string ImageFile_)
        {
            HDevWindowStack.Push(HDWindow_);
            ImageFile = ImageFile_;
            action();
        }

        private void action()
        {
            // Local iconic variables 

            HObject ho_Image, ho_DotImage, ho_Region, ho_RegionDilation;
            HObject ho_RegionClosing, ho_ConnectedRegions, ho_SelectedRegions;
            HObject ho_RegionUnion, ho_RegionClosing2, ho_ConnectedRegions2;
            HObject ho_SelectedRegions5, ho_RegionIntersection, ho_RegionAffineTrans1;
            HObject ho_RegionDilation1, ho_RegionClosing1, ho_RegionOpening;
            HObject ho_ConnectedRegions1, ho_SelectedRegions1, ho_RegionTrans;
            HObject ho_Partitioned, ho_SelectedRegions2, ho_RegionIntersection1;
            HObject ho_SortedRegions1, ho_SelectedRegions3, ho_RegionTrans1;
            HObject ho_Partitioned1, ho_SelectedRegions4, ho_RegionIntersection2;
            HObject ho_SortedRegions2;


            // Local control variables 

            HTuple hv_Row, hv_Column, hv_Phi, hv_Length1;
            HTuple hv_Length2, hv_HomMat2DIdentity, hv_HomMat2DRotate;
            HTuple hv_Area, hv_Row1, hv_Column1, hv_Area11, hv_Row11;
            HTuple hv_Column11, hv_Area12, hv_Row12, hv_Column12;

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Image);
            HOperatorSet.GenEmptyObj(out ho_DotImage);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_RegionDilation);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_RegionUnion);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing2);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions2);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions5);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection);
            HOperatorSet.GenEmptyObj(out ho_RegionAffineTrans1);
            HOperatorSet.GenEmptyObj(out ho_RegionDilation1);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing1);
            HOperatorSet.GenEmptyObj(out ho_RegionOpening);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_RegionTrans);
            HOperatorSet.GenEmptyObj(out ho_Partitioned);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions2);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection1);
            HOperatorSet.GenEmptyObj(out ho_SortedRegions1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions3);
            HOperatorSet.GenEmptyObj(out ho_RegionTrans1);
            HOperatorSet.GenEmptyObj(out ho_Partitioned1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions4);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection2);
            HOperatorSet.GenEmptyObj(out ho_SortedRegions2);

            ho_Image.Dispose();
            HOperatorSet.ReadImage(out ho_Image, ImageFile);
            ho_DotImage.Dispose();
            HOperatorSet.DotsImage(ho_Image, out ho_DotImage, 5, "dark", 2);
            ho_Region.Dispose();
            HOperatorSet.Threshold(ho_DotImage, out ho_Region, 110, 255);
            ho_RegionDilation.Dispose();
            HOperatorSet.DilationCircle(ho_Region, out ho_RegionDilation, 2.5);
            ho_RegionClosing.Dispose();
            HOperatorSet.ClosingRectangle1(ho_RegionDilation, out ho_RegionClosing, 3, 3);
            ho_ConnectedRegions.Dispose();
            HOperatorSet.Connection(ho_RegionClosing, out ho_ConnectedRegions);
            ho_SelectedRegions.Dispose();
            HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, (new HTuple("width")).TupleConcat(
                "height"), "and", (new HTuple(15)).TupleConcat(15), (new HTuple(80)).TupleConcat(
                80));
            ho_RegionUnion.Dispose();
            HOperatorSet.Union1(ho_SelectedRegions, out ho_RegionUnion);
            ho_RegionClosing2.Dispose();
            HOperatorSet.ClosingRectangle1(ho_RegionUnion, out ho_RegionClosing2, 30, 20);
            ho_ConnectedRegions2.Dispose();
            HOperatorSet.Connection(ho_RegionClosing2, out ho_ConnectedRegions2);
            ho_SelectedRegions5.Dispose();
            HOperatorSet.SelectShape(ho_ConnectedRegions2, out ho_SelectedRegions5, "area",
                "and", 10000, 20000);

            ho_RegionIntersection.Dispose();
            HOperatorSet.Intersection(ho_Region, ho_SelectedRegions5, out ho_RegionIntersection
                );
            HOperatorSet.SmallestRectangle2(ho_RegionIntersection, out hv_Row, out hv_Column,
                out hv_Phi, out hv_Length1, out hv_Length2);
            HOperatorSet.HomMat2dIdentity(out hv_HomMat2DIdentity);
            HOperatorSet.HomMat2dRotate(hv_HomMat2DIdentity, -hv_Phi, hv_Column, hv_Row,
                out hv_HomMat2DRotate);
            ho_RegionAffineTrans1.Dispose();
            HOperatorSet.AffineTransRegion(ho_RegionIntersection, out ho_RegionAffineTrans1,
                hv_HomMat2DRotate, "true");
            ho_RegionDilation1.Dispose();
            HOperatorSet.DilationCircle(ho_RegionAffineTrans1, out ho_RegionDilation1, 2.5);
            ho_RegionClosing1.Dispose();
            HOperatorSet.ClosingRectangle1(ho_RegionDilation1, out ho_RegionClosing1, 3,
                3);
            ho_RegionOpening.Dispose();
            HOperatorSet.OpeningCircle(ho_RegionClosing1, out ho_RegionOpening, 2.5);
            ho_ConnectedRegions1.Dispose();
            HOperatorSet.Connection(ho_RegionOpening, out ho_ConnectedRegions1);
            HOperatorSet.AreaCenter(ho_RegionAffineTrans1, out hv_Area, out hv_Row1, out hv_Column1);

            ho_SelectedRegions1.Dispose();
            HOperatorSet.SelectShape(ho_ConnectedRegions1, out ho_SelectedRegions1, "row",
                "and", hv_Row1 - 30, hv_Row1);
            ho_RegionTrans.Dispose();
            HOperatorSet.ShapeTrans(ho_SelectedRegions1, out ho_RegionTrans, "rectangle1");
            ho_Partitioned.Dispose();
            HOperatorSet.PartitionDynamic(ho_RegionTrans, out ho_Partitioned, 20, 20);
            ho_SelectedRegions2.Dispose();
            HOperatorSet.SelectShape(ho_Partitioned, out ho_SelectedRegions2, "height", "and",
                25, 48);
            ho_RegionIntersection1.Dispose();
            HOperatorSet.Intersection(ho_SelectedRegions2, ho_RegionAffineTrans1, out ho_RegionIntersection1
                );
            ho_SortedRegions1.Dispose();
            HOperatorSet.SortRegion(ho_RegionIntersection1, out ho_SortedRegions1, "first_point",
                "true", "column");
            HOperatorSet.AreaCenter(ho_SortedRegions1, out hv_Area11, out hv_Row11, out hv_Column11);

            ho_SelectedRegions3.Dispose();
            HOperatorSet.SelectShape(ho_ConnectedRegions1, out ho_SelectedRegions3, "row",
                "and", hv_Row1, hv_Row1 + 40);
            ho_RegionTrans1.Dispose();
            HOperatorSet.ShapeTrans(ho_SelectedRegions3, out ho_RegionTrans1, "rectangle1");
            ho_Partitioned1.Dispose();
            HOperatorSet.PartitionDynamic(ho_RegionTrans1, out ho_Partitioned1, 20, 1);
            ho_SelectedRegions4.Dispose();
            HOperatorSet.SelectShape(ho_Partitioned1, out ho_SelectedRegions4, "height",
                "and", 25, 48);
            ho_RegionIntersection2.Dispose();
            HOperatorSet.Intersection(ho_SelectedRegions4, ho_RegionAffineTrans1, out ho_RegionIntersection2
                );
            ho_SortedRegions2.Dispose();
            HOperatorSet.SortRegion(ho_RegionIntersection2, out ho_SortedRegions2, "first_point",
                "true", "column");
            HOperatorSet.AreaCenter(ho_SortedRegions2, out hv_Area12, out hv_Row12, out hv_Column12);

            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.ClearWindow(HDevWindowStack.GetActive());
            }
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_Image, HDevWindowStack.GetActive());
            }
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.SetColor(HDevWindowStack.GetActive(), "blue");
            }
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_SortedRegions1, HDevWindowStack.GetActive());
            }
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_SortedRegions2, HDevWindowStack.GetActive());
            }

            ho_Image.Dispose();
            ho_DotImage.Dispose();
            ho_Region.Dispose();
            ho_RegionDilation.Dispose();
            ho_RegionClosing.Dispose();
            ho_ConnectedRegions.Dispose();
            ho_SelectedRegions.Dispose();
            ho_RegionUnion.Dispose();
            ho_RegionClosing2.Dispose();
            ho_ConnectedRegions2.Dispose();
            ho_SelectedRegions5.Dispose();
            ho_RegionIntersection.Dispose();
            ho_RegionAffineTrans1.Dispose();
            ho_RegionDilation1.Dispose();
            ho_RegionClosing1.Dispose();
            ho_RegionOpening.Dispose();
            ho_ConnectedRegions1.Dispose();
            ho_SelectedRegions1.Dispose();
            ho_RegionTrans.Dispose();
            ho_Partitioned.Dispose();
            ho_SelectedRegions2.Dispose();
            ho_RegionIntersection1.Dispose();
            ho_SortedRegions1.Dispose();
            ho_SelectedRegions3.Dispose();
            ho_RegionTrans1.Dispose();
            ho_Partitioned1.Dispose();
            ho_SelectedRegions4.Dispose();
            ho_RegionIntersection2.Dispose();
            ho_SortedRegions2.Dispose();

            Area = new HTuple();
            Area = Area.TupleConcat(hv_Area11);
            Area = Area.TupleConcat(hv_Area12);

        }


    }
}
