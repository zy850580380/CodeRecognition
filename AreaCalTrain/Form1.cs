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
            HD.ProcessImage(hWindowControl1.HalconWindow, ImagePath);
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
                labelMessage.Text += "合格";
            }
            else
            {
                if (HD.hv_ErrorCounts == 1)
                {
                    labelMessage.Text += "不合格, 第一行缺失字符";
                }
                else if (HD.hv_ErrorCounts == 2)
                {
                    labelMessage.Text += "不合格, 第二行缺失字符";
                }
                else
                {
                    labelMessage.Text += "不合格, 缺失多个字符";
                }
            }
            textBoxPriorChar.BackColor = Color.Blue;
            label6.Text = "";
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
                label6.Text = "已保存";
                saveValue(sender, e);
            }
            else
            {
                label6.Text = "不用保存";
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

            HObject ho_Image, ho_ImageEmphasize0, ho_Region0;
            HObject ho_RegionClosing0, ho_ConnectedRegions0, ho_SelectedRegions0;
            HObject ho_SelectedRegionsStd0, ho_RegionIntersection0;
            HObject ho_ImageRotate0, ho_ImageEmphasize01, ho_ImageInvert01;
            HObject ho_Region01, ho_RegionClosing01, ho_RegionOpening01;
            HObject ho_ConnectedRegions01, ho_SelectedRegions01, ho_RegionIntersection01;
            HObject ho_Rectangle01, ho_ImageReduced01, ho_Region02;
            HObject ho_RegionClosing02, ho_ConnectedRegions02, ho_SelectedRegions1;
            HObject ho_RegionIntersection1, ho_RegionClosing1, ho_ConnectedRegions1;
            HObject ho_RegionTrans1, ho_Partitioned1, ho_RegionIntersection11;
            HObject ho_SortedRegions1, ho_SelectedRegions2, ho_RegionIntersection2;
            HObject ho_RegionClosing2, ho_ConnectedRegions2, ho_RegionTrans2;
            HObject ho_Partitioned2, ho_RegionIntersection21, ho_SortedRegions2;


            // Local control variables 

            HTuple hv_ErrorCount, hv_Number0;
            HTuple hv_Row0, hv_Column0, hv_Phi0, hv_Length01, hv_Length02;
            HTuple hv_Deg0, hv_Number01, hv_Row01, hv_Column01, hv_Phi01;
            HTuple hv_Length011, hv_Length012, hv_Number1, hv_Area11;
            HTuple hv_Row11, hv_Column11, hv_Number2, hv_Area12, hv_Row12;
            HTuple hv_Column12;

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Image);
            HOperatorSet.GenEmptyObj(out ho_ImageEmphasize0);
            HOperatorSet.GenEmptyObj(out ho_Region0);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing0);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions0);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions0);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegionsStd0);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection0);
            HOperatorSet.GenEmptyObj(out ho_ImageRotate0);
            HOperatorSet.GenEmptyObj(out ho_ImageEmphasize01);
            HOperatorSet.GenEmptyObj(out ho_ImageInvert01);
            HOperatorSet.GenEmptyObj(out ho_Region01);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing01);
            HOperatorSet.GenEmptyObj(out ho_RegionOpening01);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions01);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions01);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection01);
            HOperatorSet.GenEmptyObj(out ho_Rectangle01);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced01);
            HOperatorSet.GenEmptyObj(out ho_Region02);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing02);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions02);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection1);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing1);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_RegionTrans1);
            HOperatorSet.GenEmptyObj(out ho_Partitioned1);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection11);
            HOperatorSet.GenEmptyObj(out ho_SortedRegions1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions2);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection2);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing2);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions2);
            HOperatorSet.GenEmptyObj(out ho_RegionTrans2);
            HOperatorSet.GenEmptyObj(out ho_Partitioned2);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection21);
            HOperatorSet.GenEmptyObj(out ho_SortedRegions2);

            ho_Image.Dispose();
            HOperatorSet.ReadImage(out ho_Image, ImageFile);
            ho_ImageEmphasize0.Dispose();
            HOperatorSet.Emphasize(ho_Image, out ho_ImageEmphasize0, 7, 7, 5);
            ho_Region0.Dispose();
            HOperatorSet.Threshold(ho_ImageEmphasize0, out ho_Region0, 0, 105);
            ho_RegionClosing0.Dispose();
            HOperatorSet.ClosingCircle(ho_Region0, out ho_RegionClosing0, 10);
            ho_ConnectedRegions0.Dispose();
            HOperatorSet.Connection(ho_RegionClosing0, out ho_ConnectedRegions0);
            ho_SelectedRegions0.Dispose();
            HOperatorSet.SelectShape(ho_ConnectedRegions0, out ho_SelectedRegions0, (new HTuple("width")).TupleConcat(
                "height"), "and", (new HTuple(80)).TupleConcat(40), (new HTuple(180)).TupleConcat(
                140));
            ho_SelectedRegionsStd0.Dispose();
            HOperatorSet.SelectShapeStd(ho_SelectedRegions0, out ho_SelectedRegionsStd0,
                "max_area", 70);
            hv_ErrorCount = 0;
            HOperatorSet.CountObj(ho_SelectedRegionsStd0, out hv_Number0);
            if ((int)(new HTuple(hv_Number0.TupleNotEqual(1))) != 0)
            {
                hv_ErrorCounts = 10;
                ho_Image.Dispose();
                ho_ImageEmphasize0.Dispose();
                ho_Region0.Dispose();
                ho_RegionClosing0.Dispose();
                ho_ConnectedRegions0.Dispose();
                ho_SelectedRegions0.Dispose();
                ho_SelectedRegionsStd0.Dispose();
                ho_RegionIntersection0.Dispose();
                ho_ImageRotate0.Dispose();
                ho_ImageEmphasize01.Dispose();
                ho_ImageInvert01.Dispose();
                ho_Region01.Dispose();
                ho_RegionClosing01.Dispose();
                ho_RegionOpening01.Dispose();
                ho_ConnectedRegions01.Dispose();
                ho_SelectedRegions01.Dispose();
                ho_RegionIntersection01.Dispose();
                ho_Rectangle01.Dispose();
                ho_ImageReduced01.Dispose();
                ho_Region02.Dispose();
                ho_RegionClosing02.Dispose();
                ho_ConnectedRegions02.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_RegionIntersection1.Dispose();
                ho_RegionClosing1.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_RegionTrans1.Dispose();
                ho_Partitioned1.Dispose();
                ho_RegionIntersection11.Dispose();
                ho_SortedRegions1.Dispose();
                ho_SelectedRegions2.Dispose();
                ho_RegionIntersection2.Dispose();
                ho_RegionClosing2.Dispose();
                ho_ConnectedRegions2.Dispose();
                ho_RegionTrans2.Dispose();
                ho_Partitioned2.Dispose();
                ho_RegionIntersection21.Dispose();
                ho_SortedRegions2.Dispose();

                return;
            }
            ho_RegionIntersection0.Dispose();
            HOperatorSet.Intersection(ho_SelectedRegionsStd0, ho_Region0, out ho_RegionIntersection0
                );
            //旋转图像
            HOperatorSet.SmallestRectangle2(ho_RegionIntersection0, out hv_Row0, out hv_Column0,
                out hv_Phi0, out hv_Length01, out hv_Length02);
            HOperatorSet.TupleDeg(hv_Phi0, out hv_Deg0);
            ho_ImageRotate0.Dispose();
            HOperatorSet.RotateImage(ho_Image, out ho_ImageRotate0, (-hv_Deg0) - 1.5, "constant");
            //处理旋转后的图像
            ho_ImageEmphasize01.Dispose();
            HOperatorSet.Emphasize(ho_ImageRotate0, out ho_ImageEmphasize01, 7, 7, 5);
            ho_ImageInvert01.Dispose();
            HOperatorSet.InvertImage(ho_ImageEmphasize01, out ho_ImageInvert01);
            ho_Region01.Dispose();
            HOperatorSet.Threshold(ho_ImageInvert01, out ho_Region01, 140, 255);
            ho_RegionClosing01.Dispose();
            HOperatorSet.ClosingCircle(ho_Region01, out ho_RegionClosing01, 10);
            ho_RegionOpening01.Dispose();
            HOperatorSet.OpeningCircle(ho_RegionClosing01, out ho_RegionOpening01, 5);
            ho_ConnectedRegions01.Dispose();
            HOperatorSet.Connection(ho_RegionOpening01, out ho_ConnectedRegions01);
            ho_SelectedRegions01.Dispose();
            HOperatorSet.SelectShape(ho_ConnectedRegions01, out ho_SelectedRegions01, (new HTuple("width")).TupleConcat(
                "height"), "and", (new HTuple(70)).TupleConcat(40), (new HTuple(140)).TupleConcat(
                75));
            HOperatorSet.CountObj(ho_SelectedRegions01, out hv_Number01);
            if ((int)(new HTuple(hv_Number01.TupleNotEqual(1))) != 0)
            {
                hv_ErrorCounts = 10;
                ho_Image.Dispose();
                ho_ImageEmphasize0.Dispose();
                ho_Region0.Dispose();
                ho_RegionClosing0.Dispose();
                ho_ConnectedRegions0.Dispose();
                ho_SelectedRegions0.Dispose();
                ho_SelectedRegionsStd0.Dispose();
                ho_RegionIntersection0.Dispose();
                ho_ImageRotate0.Dispose();
                ho_ImageEmphasize01.Dispose();
                ho_ImageInvert01.Dispose();
                ho_Region01.Dispose();
                ho_RegionClosing01.Dispose();
                ho_RegionOpening01.Dispose();
                ho_ConnectedRegions01.Dispose();
                ho_SelectedRegions01.Dispose();
                ho_RegionIntersection01.Dispose();
                ho_Rectangle01.Dispose();
                ho_ImageReduced01.Dispose();
                ho_Region02.Dispose();
                ho_RegionClosing02.Dispose();
                ho_ConnectedRegions02.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_RegionIntersection1.Dispose();
                ho_RegionClosing1.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_RegionTrans1.Dispose();
                ho_Partitioned1.Dispose();
                ho_RegionIntersection11.Dispose();
                ho_SortedRegions1.Dispose();
                ho_SelectedRegions2.Dispose();
                ho_RegionIntersection2.Dispose();
                ho_RegionClosing2.Dispose();
                ho_ConnectedRegions2.Dispose();
                ho_RegionTrans2.Dispose();
                ho_Partitioned2.Dispose();
                ho_RegionIntersection21.Dispose();
                ho_SortedRegions2.Dispose();

                return;
            }
            ho_RegionIntersection01.Dispose();
            HOperatorSet.Intersection(ho_SelectedRegions01, ho_Region01, out ho_RegionIntersection01
                );
            HOperatorSet.SmallestRectangle2(ho_RegionIntersection01, out hv_Row01, out hv_Column01,
                out hv_Phi01, out hv_Length011, out hv_Length012);
            ho_Rectangle01.Dispose();
            HOperatorSet.GenRectangle2(out ho_Rectangle01, hv_Row01 - 1, hv_Column01 - 2, 0,
                hv_Length011 + 4, hv_Length012);
            //缩小区域
            ho_ImageReduced01.Dispose();
            HOperatorSet.ReduceDomain(ho_ImageInvert01, ho_Rectangle01, out ho_ImageReduced01
                );
            ho_Region02.Dispose();
            HOperatorSet.CloseEdges(ho_Rectangle01, ho_ImageReduced01, out ho_Region02, 90);
            //median_image (ImageReduced01, ImageMedian01, 'circle', 2, 'mirrored')
            //threshold (ImageMedian01, Region02, 60, 255)
            ho_RegionClosing02.Dispose();
            HOperatorSet.ClosingRectangle1(ho_Region02, out ho_RegionClosing02, 30, 1);
            ho_ConnectedRegions02.Dispose();
            HOperatorSet.Connection(ho_RegionClosing02, out ho_ConnectedRegions02);
            //第一行****************************
            ho_SelectedRegions1.Dispose();
            HOperatorSet.SelectShape(ho_ConnectedRegions02, out ho_SelectedRegions1, ((new HTuple("width")).TupleConcat(
                "height")).TupleConcat("row"), "and", ((new HTuple(40)).TupleConcat(15)).TupleConcat(
                hv_Row01 - 20), ((new HTuple(160)).TupleConcat(35)).TupleConcat(hv_Row01));
            ho_RegionIntersection1.Dispose();
            HOperatorSet.Intersection(ho_SelectedRegions1, ho_Region02, out ho_RegionIntersection1
                );
            ho_RegionClosing1.Dispose();
            HOperatorSet.ClosingRectangle1(ho_RegionIntersection1, out ho_RegionClosing1,
                2, 50);
            ho_ConnectedRegions1.Dispose();
            HOperatorSet.Connection(ho_RegionClosing1, out ho_ConnectedRegions1);
            ho_RegionTrans1.Dispose();
            HOperatorSet.ShapeTrans(ho_ConnectedRegions1, out ho_RegionTrans1, "rectangle1");
            ho_Partitioned1.Dispose();
            HOperatorSet.PartitionRectangle(ho_RegionTrans1, out ho_Partitioned1, 12, 32);
            ho_SelectedRegions1.Dispose();
            HOperatorSet.SelectShape(ho_Partitioned1, out ho_SelectedRegions1, ((new HTuple("area")).TupleConcat(
                "width")).TupleConcat("height"), "and", ((new HTuple(100)).TupleConcat(7)).TupleConcat(
                18), ((new HTuple(550)).TupleConcat(20)).TupleConcat(35));
            ho_RegionIntersection11.Dispose();
            HOperatorSet.Intersection(ho_SelectedRegions1, ho_Region02, out ho_RegionIntersection11
                );
            //选中字符
            //排序
            ho_SortedRegions1.Dispose();
            HOperatorSet.SortRegion(ho_RegionIntersection11, out ho_SortedRegions1, "first_point",
                "true", "column");
            HOperatorSet.CountObj(ho_SortedRegions1, out hv_Number1);
            HOperatorSet.AreaCenter(ho_SortedRegions1, out hv_Area11, out hv_Row11, out hv_Column11);
            if ((int)(new HTuple(hv_Number1.TupleNotEqual(8))) != 0)
            {
                hv_ErrorCounts = 1;
                ho_Image.Dispose();
                ho_ImageEmphasize0.Dispose();
                ho_Region0.Dispose();
                ho_RegionClosing0.Dispose();
                ho_ConnectedRegions0.Dispose();
                ho_SelectedRegions0.Dispose();
                ho_SelectedRegionsStd0.Dispose();
                ho_RegionIntersection0.Dispose();
                ho_ImageRotate0.Dispose();
                ho_ImageEmphasize01.Dispose();
                ho_ImageInvert01.Dispose();
                ho_Region01.Dispose();
                ho_RegionClosing01.Dispose();
                ho_RegionOpening01.Dispose();
                ho_ConnectedRegions01.Dispose();
                ho_SelectedRegions01.Dispose();
                ho_RegionIntersection01.Dispose();
                ho_Rectangle01.Dispose();
                ho_ImageReduced01.Dispose();
                ho_Region02.Dispose();
                ho_RegionClosing02.Dispose();
                ho_ConnectedRegions02.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_RegionIntersection1.Dispose();
                ho_RegionClosing1.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_RegionTrans1.Dispose();
                ho_Partitioned1.Dispose();
                ho_RegionIntersection11.Dispose();
                ho_SortedRegions1.Dispose();
                ho_SelectedRegions2.Dispose();
                ho_RegionIntersection2.Dispose();
                ho_RegionClosing2.Dispose();
                ho_ConnectedRegions2.Dispose();
                ho_RegionTrans2.Dispose();
                ho_Partitioned2.Dispose();
                ho_RegionIntersection21.Dispose();
                ho_SortedRegions2.Dispose();

                return;
            }

            //第二行***********************
            ho_SelectedRegions2.Dispose();
            HOperatorSet.SelectShape(ho_ConnectedRegions02, out ho_SelectedRegions2, ((new HTuple("width")).TupleConcat(
                "height")).TupleConcat("row"), "and", ((new HTuple(40)).TupleConcat(15)).TupleConcat(
                hv_Row01), ((new HTuple(160)).TupleConcat(35)).TupleConcat(hv_Row01 + 20));
            ho_RegionIntersection2.Dispose();
            HOperatorSet.Intersection(ho_SelectedRegions2, ho_Region02, out ho_RegionIntersection2
                );
            ho_RegionClosing2.Dispose();
            HOperatorSet.ClosingRectangle1(ho_RegionIntersection2, out ho_RegionClosing2,
                2, 50);
            ho_ConnectedRegions2.Dispose();
            HOperatorSet.Connection(ho_RegionClosing2, out ho_ConnectedRegions2);
            ho_RegionTrans2.Dispose();
            HOperatorSet.ShapeTrans(ho_ConnectedRegions2, out ho_RegionTrans2, "rectangle1");
            ho_Partitioned2.Dispose();
            HOperatorSet.PartitionRectangle(ho_RegionTrans2, out ho_Partitioned2, 12, 32);
            ho_SelectedRegions2.Dispose();
            HOperatorSet.SelectShape(ho_Partitioned2, out ho_SelectedRegions2, ((new HTuple("area")).TupleConcat(
                "width")).TupleConcat("height"), "and", ((new HTuple(100)).TupleConcat(7)).TupleConcat(
                18), ((new HTuple(550)).TupleConcat(20)).TupleConcat(35));
            ho_RegionIntersection21.Dispose();
            HOperatorSet.Intersection(ho_SelectedRegions2, ho_Region02, out ho_RegionIntersection21
                );
            //选中字符
            //排序
            ho_SortedRegions2.Dispose();
            HOperatorSet.SortRegion(ho_RegionIntersection21, out ho_SortedRegions2, "first_point",
                "true", "column");
            HOperatorSet.CountObj(ho_SortedRegions2, out hv_Number2);
            HOperatorSet.AreaCenter(ho_SortedRegions2, out hv_Area12, out hv_Row12, out hv_Column12);
            if ((int)(new HTuple(hv_Number2.TupleNotEqual(7))) != 0)
            {
                hv_ErrorCounts = 2;
                ho_Image.Dispose();
                ho_ImageEmphasize0.Dispose();
                ho_Region0.Dispose();
                ho_RegionClosing0.Dispose();
                ho_ConnectedRegions0.Dispose();
                ho_SelectedRegions0.Dispose();
                ho_SelectedRegionsStd0.Dispose();
                ho_RegionIntersection0.Dispose();
                ho_ImageRotate0.Dispose();
                ho_ImageEmphasize01.Dispose();
                ho_ImageInvert01.Dispose();
                ho_Region01.Dispose();
                ho_RegionClosing01.Dispose();
                ho_RegionOpening01.Dispose();
                ho_ConnectedRegions01.Dispose();
                ho_SelectedRegions01.Dispose();
                ho_RegionIntersection01.Dispose();
                ho_Rectangle01.Dispose();
                ho_ImageReduced01.Dispose();
                ho_Region02.Dispose();
                ho_RegionClosing02.Dispose();
                ho_ConnectedRegions02.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_RegionIntersection1.Dispose();
                ho_RegionClosing1.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_RegionTrans1.Dispose();
                ho_Partitioned1.Dispose();
                ho_RegionIntersection11.Dispose();
                ho_SortedRegions1.Dispose();
                ho_SelectedRegions2.Dispose();
                ho_RegionIntersection2.Dispose();
                ho_RegionClosing2.Dispose();
                ho_ConnectedRegions2.Dispose();
                ho_RegionTrans2.Dispose();
                ho_Partitioned2.Dispose();
                ho_RegionIntersection21.Dispose();
                ho_SortedRegions2.Dispose();

                return;
            }

            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.ClearWindow(HDevWindowStack.GetActive());
            }
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.SetColor(HDevWindowStack.GetActive(), "white");
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
            ho_ImageEmphasize0.Dispose();
            ho_Region0.Dispose();
            ho_RegionClosing0.Dispose();
            ho_ConnectedRegions0.Dispose();
            ho_SelectedRegions0.Dispose();
            ho_SelectedRegionsStd0.Dispose();
            ho_RegionIntersection0.Dispose();
            ho_ImageRotate0.Dispose();
            ho_ImageEmphasize01.Dispose();
            ho_ImageInvert01.Dispose();
            ho_Region01.Dispose();
            ho_RegionClosing01.Dispose();
            ho_RegionOpening01.Dispose();
            ho_ConnectedRegions01.Dispose();
            ho_SelectedRegions01.Dispose();
            ho_RegionIntersection01.Dispose();
            ho_Rectangle01.Dispose();
            ho_ImageReduced01.Dispose();
            ho_Region02.Dispose();
            ho_RegionClosing02.Dispose();
            ho_ConnectedRegions02.Dispose();
            ho_SelectedRegions1.Dispose();
            ho_RegionIntersection1.Dispose();
            ho_RegionClosing1.Dispose();
            ho_ConnectedRegions1.Dispose();
            ho_RegionTrans1.Dispose();
            ho_Partitioned1.Dispose();
            ho_RegionIntersection11.Dispose();
            ho_SortedRegions1.Dispose();
            ho_SelectedRegions2.Dispose();
            ho_RegionIntersection2.Dispose();
            ho_RegionClosing2.Dispose();
            ho_ConnectedRegions2.Dispose();
            ho_RegionTrans2.Dispose();
            ho_Partitioned2.Dispose();
            ho_RegionIntersection21.Dispose();
            ho_SortedRegions2.Dispose();

            Area = new HTuple();
            Area = Area.TupleConcat(hv_Area11);
            Area = Area.TupleConcat(hv_Area12);

        }


    }
}
