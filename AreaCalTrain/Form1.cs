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

            HObject ho_Image, ho_ImageEmphasize, ho_ImageBinomial0;
            HObject ho_ImageMean1, ho_Regions, ho_ConnectedRegions3;
            HObject ho_SelectedRegions8, ho_RegionUnion4, ho_RegionClosing5;
            HObject ho_ConnectedRegions6, ho_SelectedRegions7, ho_SelectedRegions5;
            HObject ho_rotateImage, ho_ImageEmphasize1, ho_ImageBinomia1;
            HObject ho_ImageMean, ho_Region, ho_ConnectedRegions, ho_SelectedRegions;
            HObject ho_RegionUnion, ho_RegionClosing, ho_RegionClosing7;
            HObject ho_RegionIntersection6, ho_ConnectedRegions1, ho_SelectedRegions1;
            HObject ho_RegionUnion1, ho_RegionIntersection, ho_RectChar1;
            HObject ho_RectChar2, ho_RegionIntersection2, ho_RegionClosing3;
            HObject ho_RegionClosing1, ho_ConnectedRegions4, ho_RegionTrans;
            HObject ho_Partitioned, ho_SelectedRegions3, ho_RegionIntersection3;
            HObject ho_SortedRegions, ho_RegionIntersection4, ho_RegionClosing4;
            HObject ho_RegionClosing2, ho_ConnectedRegions2, ho_RegionTrans1;
            HObject ho_Partitioned1, ho_SelectedRegions4, ho_SortedRegions2;


            // Local control variables 

            HTuple hv_Number0, hv_Row;
            HTuple hv_Column, hv_Phi, hv_Length1, hv_Length2, hv_Deg;
            HTuple hv_Number01, hv_RowChar1, hv_ColumnChar1, hv_RowChar2;
            HTuple hv_ColumnChar2, hv_Area11, hv_Row11, hv_Column11;
            HTuple hv_Number1, hv_Area12, hv_Row12, hv_Column12, hv_Number2;

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Image);
            HOperatorSet.GenEmptyObj(out ho_ImageEmphasize);
            HOperatorSet.GenEmptyObj(out ho_ImageBinomial0);
            HOperatorSet.GenEmptyObj(out ho_ImageMean1);
            HOperatorSet.GenEmptyObj(out ho_Regions);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions3);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions8);
            HOperatorSet.GenEmptyObj(out ho_RegionUnion4);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing5);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions6);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions7);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions5);
            HOperatorSet.GenEmptyObj(out ho_rotateImage);
            HOperatorSet.GenEmptyObj(out ho_ImageEmphasize1);
            HOperatorSet.GenEmptyObj(out ho_ImageBinomia1);
            HOperatorSet.GenEmptyObj(out ho_ImageMean);
            HOperatorSet.GenEmptyObj(out ho_Region);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions);
            HOperatorSet.GenEmptyObj(out ho_RegionUnion);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing7);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection6);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_RegionUnion1);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection);
            HOperatorSet.GenEmptyObj(out ho_RectChar1);
            HOperatorSet.GenEmptyObj(out ho_RectChar2);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection2);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing3);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing1);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions4);
            HOperatorSet.GenEmptyObj(out ho_RegionTrans);
            HOperatorSet.GenEmptyObj(out ho_Partitioned);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions3);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection3);
            HOperatorSet.GenEmptyObj(out ho_SortedRegions);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection4);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing4);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing2);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions2);
            HOperatorSet.GenEmptyObj(out ho_RegionTrans1);
            HOperatorSet.GenEmptyObj(out ho_Partitioned1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions4);
            HOperatorSet.GenEmptyObj(out ho_SortedRegions2);

            ho_Image.Dispose();
            HOperatorSet.ReadImage(out ho_Image, ImageFile);

            ho_ImageEmphasize.Dispose();
            HOperatorSet.Emphasize(ho_Image, out ho_ImageEmphasize, 7, 7, 9);
            ho_ImageBinomial0.Dispose();
            HOperatorSet.BinomialFilter(ho_ImageEmphasize, out ho_ImageBinomial0, 5, 5);
            ho_ImageMean1.Dispose();
            HOperatorSet.MeanImage(ho_ImageBinomial0, out ho_ImageMean1, 31, 31);
            ho_Regions.Dispose();
            HOperatorSet.DynThreshold(ho_ImageBinomial0, ho_ImageMean1, out ho_Regions, 15,
                "dark");

            ho_ConnectedRegions3.Dispose();
            HOperatorSet.Connection(ho_Regions, out ho_ConnectedRegions3);
            ho_SelectedRegions8.Dispose();
            HOperatorSet.SelectShape(ho_ConnectedRegions3, out ho_SelectedRegions8, ((new HTuple("width")).TupleConcat(
                "height")).TupleConcat("area"), "and", ((new HTuple(3)).TupleConcat(20)).TupleConcat(
                50), ((new HTuple(24)).TupleConcat(34)).TupleConcat(500));

            ho_RegionUnion4.Dispose();
            HOperatorSet.Union1(ho_SelectedRegions8, out ho_RegionUnion4);
            ho_RegionClosing5.Dispose();
            HOperatorSet.ClosingCircle(ho_RegionUnion4, out ho_RegionClosing5, 7.5);
            ho_ConnectedRegions6.Dispose();
            HOperatorSet.Connection(ho_RegionClosing5, out ho_ConnectedRegions6);

            ho_SelectedRegions7.Dispose();
            HOperatorSet.SelectShape(ho_ConnectedRegions6, out ho_SelectedRegions7, (new HTuple("width")).TupleConcat(
                "height"), "and", (new HTuple(109.28)).TupleConcat(50.96), (new HTuple(242.23)).TupleConcat(
                113.25));
            ho_SelectedRegions5.Dispose();
            HOperatorSet.SelectShapeStd(ho_SelectedRegions7, out ho_SelectedRegions5, "max_area",
                70);
            HOperatorSet.CountObj(ho_SelectedRegions5, out hv_Number0);
            hv_ErrorCounts = 0;
            if ((int)(new HTuple(hv_Number0.TupleNotEqual(1))) != 0)
            {
                hv_ErrorCounts = 10;
                ho_Image.Dispose();
                ho_ImageEmphasize.Dispose();
                ho_ImageBinomial0.Dispose();
                ho_ImageMean1.Dispose();
                ho_Regions.Dispose();
                ho_ConnectedRegions3.Dispose();
                ho_SelectedRegions8.Dispose();
                ho_RegionUnion4.Dispose();
                ho_RegionClosing5.Dispose();
                ho_ConnectedRegions6.Dispose();
                ho_SelectedRegions7.Dispose();
                ho_SelectedRegions5.Dispose();
                ho_rotateImage.Dispose();
                ho_ImageEmphasize1.Dispose();
                ho_ImageBinomia1.Dispose();
                ho_ImageMean.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_RegionUnion.Dispose();
                ho_RegionClosing.Dispose();
                ho_RegionClosing7.Dispose();
                ho_RegionIntersection6.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_RegionUnion1.Dispose();
                ho_RegionIntersection.Dispose();
                ho_RectChar1.Dispose();
                ho_RectChar2.Dispose();
                ho_RegionIntersection2.Dispose();
                ho_RegionClosing3.Dispose();
                ho_RegionClosing1.Dispose();
                ho_ConnectedRegions4.Dispose();
                ho_RegionTrans.Dispose();
                ho_Partitioned.Dispose();
                ho_SelectedRegions3.Dispose();
                ho_RegionIntersection3.Dispose();
                ho_SortedRegions.Dispose();
                ho_RegionIntersection4.Dispose();
                ho_RegionClosing4.Dispose();
                ho_RegionClosing2.Dispose();
                ho_ConnectedRegions2.Dispose();
                ho_RegionTrans1.Dispose();
                ho_Partitioned1.Dispose();
                ho_SelectedRegions4.Dispose();
                ho_SortedRegions2.Dispose();

                return;
            }

            HOperatorSet.SmallestRectangle2(ho_SelectedRegions5, out hv_Row, out hv_Column,
                out hv_Phi, out hv_Length1, out hv_Length2);
            HOperatorSet.TupleDeg(hv_Phi, out hv_Deg);
            ho_rotateImage.Dispose();
            HOperatorSet.RotateImage(ho_Image, out ho_rotateImage, -hv_Deg, "constant");

            ho_ImageEmphasize1.Dispose();
            HOperatorSet.Emphasize(ho_rotateImage, out ho_ImageEmphasize1, 7, 7, 9);
            ho_ImageBinomia1.Dispose();
            HOperatorSet.BinomialFilter(ho_ImageEmphasize1, out ho_ImageBinomia1, 5, 5);
            ho_ImageMean.Dispose();
            HOperatorSet.MeanImage(ho_ImageBinomia1, out ho_ImageMean, 31, 31);

            ho_Region.Dispose();
            HOperatorSet.DynThreshold(ho_ImageBinomia1, ho_ImageMean, out ho_Region, 15,
                "dark");

            ho_ConnectedRegions.Dispose();
            HOperatorSet.Connection(ho_Region, out ho_ConnectedRegions);
            ho_SelectedRegions.Dispose();
            HOperatorSet.SelectShape(ho_ConnectedRegions, out ho_SelectedRegions, "area",
                "and", 50, 300);
            ho_RegionUnion.Dispose();
            HOperatorSet.Union1(ho_SelectedRegions, out ho_RegionUnion);
            ho_RegionClosing.Dispose();
            HOperatorSet.ClosingRectangle1(ho_RegionUnion, out ho_RegionClosing, 55, 3);
            //*****************
            ho_RegionClosing7.Dispose();
            HOperatorSet.ClosingCircle(ho_RegionUnion, out ho_RegionClosing7, 33.5);
            ho_RegionIntersection6.Dispose();
            HOperatorSet.Intersection(ho_RegionClosing7, ho_RegionClosing, out ho_RegionIntersection6
                );

            ho_ConnectedRegions1.Dispose();
            HOperatorSet.Connection(ho_RegionIntersection6, out ho_ConnectedRegions1);
            ho_SelectedRegions1.Dispose();
            HOperatorSet.SelectShape(ho_ConnectedRegions1, out ho_SelectedRegions1, (new HTuple("height")).TupleConcat(
                "area"), "and", (new HTuple(20)).TupleConcat(3000), (new HTuple(45)).TupleConcat(
                99999));

            ho_RegionUnion1.Dispose();
            HOperatorSet.Union1(ho_SelectedRegions1, out ho_RegionUnion1);
            ho_RegionIntersection.Dispose();
            HOperatorSet.Intersection(ho_RegionUnion1, ho_RegionUnion, out ho_RegionIntersection
                );
            HOperatorSet.CountObj(ho_RegionIntersection, out hv_Number01);
            if ((int)(new HTuple(hv_Number01.TupleNotEqual(1))) != 0)
            {
                hv_ErrorCounts = 10;
                ho_Image.Dispose();
                ho_ImageEmphasize.Dispose();
                ho_ImageBinomial0.Dispose();
                ho_ImageMean1.Dispose();
                ho_Regions.Dispose();
                ho_ConnectedRegions3.Dispose();
                ho_SelectedRegions8.Dispose();
                ho_RegionUnion4.Dispose();
                ho_RegionClosing5.Dispose();
                ho_ConnectedRegions6.Dispose();
                ho_SelectedRegions7.Dispose();
                ho_SelectedRegions5.Dispose();
                ho_rotateImage.Dispose();
                ho_ImageEmphasize1.Dispose();
                ho_ImageBinomia1.Dispose();
                ho_ImageMean.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_RegionUnion.Dispose();
                ho_RegionClosing.Dispose();
                ho_RegionClosing7.Dispose();
                ho_RegionIntersection6.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_RegionUnion1.Dispose();
                ho_RegionIntersection.Dispose();
                ho_RectChar1.Dispose();
                ho_RectChar2.Dispose();
                ho_RegionIntersection2.Dispose();
                ho_RegionClosing3.Dispose();
                ho_RegionClosing1.Dispose();
                ho_ConnectedRegions4.Dispose();
                ho_RegionTrans.Dispose();
                ho_Partitioned.Dispose();
                ho_SelectedRegions3.Dispose();
                ho_RegionIntersection3.Dispose();
                ho_SortedRegions.Dispose();
                ho_RegionIntersection4.Dispose();
                ho_RegionClosing4.Dispose();
                ho_RegionClosing2.Dispose();
                ho_ConnectedRegions2.Dispose();
                ho_RegionTrans1.Dispose();
                ho_Partitioned1.Dispose();
                ho_SelectedRegions4.Dispose();
                ho_SortedRegions2.Dispose();

                return;
            }

            HOperatorSet.SmallestRectangle1(ho_RegionUnion1, out hv_RowChar1, out hv_ColumnChar1,
                out hv_RowChar2, out hv_ColumnChar2);
            ho_RectChar1.Dispose();
            HOperatorSet.GenRectangle1(out ho_RectChar1, hv_RowChar1, hv_ColumnChar1, (hv_RowChar1 + hv_RowChar2) / 2,
                hv_ColumnChar2);
            ho_RectChar2.Dispose();
            HOperatorSet.GenRectangle1(out ho_RectChar2, (hv_RowChar1 + hv_RowChar2) / 2, hv_ColumnChar1,
                hv_RowChar2, hv_ColumnChar2);

            ho_RegionIntersection2.Dispose();
            HOperatorSet.Intersection(ho_RegionIntersection, ho_RectChar1, out ho_RegionIntersection2
                );
            ho_RegionClosing3.Dispose();
            HOperatorSet.ClosingRectangle1(ho_RegionIntersection2, out ho_RegionClosing3,
                1, 12);
            ho_RegionClosing1.Dispose();
            HOperatorSet.ClosingCircle(ho_RegionClosing3, out ho_RegionClosing1, 1.5);
            ho_ConnectedRegions4.Dispose();
            HOperatorSet.Connection(ho_RegionClosing1, out ho_ConnectedRegions4);
            ho_RegionTrans.Dispose();
            HOperatorSet.ShapeTrans(ho_ConnectedRegions4, out ho_RegionTrans, "rectangle1");
            ho_Partitioned.Dispose();
            HOperatorSet.PartitionRectangle(ho_RegionTrans, out ho_Partitioned, 19, 29);
            ho_SelectedRegions3.Dispose();
            HOperatorSet.SelectShape(ho_Partitioned, out ho_SelectedRegions3, "height", "and",
                19, 32);
            ho_RegionIntersection3.Dispose();
            HOperatorSet.Intersection(ho_SelectedRegions3, ho_RegionIntersection2, out ho_RegionIntersection3
                );
            ho_SortedRegions.Dispose();
            HOperatorSet.SortRegion(ho_RegionIntersection3, out ho_SortedRegions, "character",
                "true", "column");
            HOperatorSet.AreaCenter(ho_SortedRegions, out hv_Area11, out hv_Row11, out hv_Column11);
            HOperatorSet.CountObj(ho_SortedRegions, out hv_Number1);
            if ((int)(new HTuple(hv_Number1.TupleNotEqual(8))) != 0)
            {
                hv_ErrorCounts = 1;
                ho_Image.Dispose();
                ho_ImageEmphasize.Dispose();
                ho_ImageBinomial0.Dispose();
                ho_ImageMean1.Dispose();
                ho_Regions.Dispose();
                ho_ConnectedRegions3.Dispose();
                ho_SelectedRegions8.Dispose();
                ho_RegionUnion4.Dispose();
                ho_RegionClosing5.Dispose();
                ho_ConnectedRegions6.Dispose();
                ho_SelectedRegions7.Dispose();
                ho_SelectedRegions5.Dispose();
                ho_rotateImage.Dispose();
                ho_ImageEmphasize1.Dispose();
                ho_ImageBinomia1.Dispose();
                ho_ImageMean.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_RegionUnion.Dispose();
                ho_RegionClosing.Dispose();
                ho_RegionClosing7.Dispose();
                ho_RegionIntersection6.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_RegionUnion1.Dispose();
                ho_RegionIntersection.Dispose();
                ho_RectChar1.Dispose();
                ho_RectChar2.Dispose();
                ho_RegionIntersection2.Dispose();
                ho_RegionClosing3.Dispose();
                ho_RegionClosing1.Dispose();
                ho_ConnectedRegions4.Dispose();
                ho_RegionTrans.Dispose();
                ho_Partitioned.Dispose();
                ho_SelectedRegions3.Dispose();
                ho_RegionIntersection3.Dispose();
                ho_SortedRegions.Dispose();
                ho_RegionIntersection4.Dispose();
                ho_RegionClosing4.Dispose();
                ho_RegionClosing2.Dispose();
                ho_ConnectedRegions2.Dispose();
                ho_RegionTrans1.Dispose();
                ho_Partitioned1.Dispose();
                ho_SelectedRegions4.Dispose();
                ho_SortedRegions2.Dispose();

                return;
            }

            ho_RegionIntersection4.Dispose();
            HOperatorSet.Intersection(ho_RegionIntersection, ho_RectChar2, out ho_RegionIntersection4
                );
            ho_RegionClosing4.Dispose();
            HOperatorSet.ClosingRectangle1(ho_RegionIntersection4, out ho_RegionClosing4,
                1, 12);
            ho_RegionClosing2.Dispose();
            HOperatorSet.ClosingCircle(ho_RegionClosing4, out ho_RegionClosing2, 1.5);
            ho_ConnectedRegions2.Dispose();
            HOperatorSet.Connection(ho_RegionClosing2, out ho_ConnectedRegions2);
            ho_RegionTrans1.Dispose();
            HOperatorSet.ShapeTrans(ho_ConnectedRegions2, out ho_RegionTrans1, "rectangle1");
            ho_Partitioned1.Dispose();
            HOperatorSet.PartitionRectangle(ho_RegionTrans1, out ho_Partitioned1, 19, 29);
            ho_SelectedRegions4.Dispose();
            HOperatorSet.SelectShape(ho_Partitioned1, out ho_SelectedRegions4, "height",
                "and", 20, 34);
            ho_RegionIntersection2.Dispose();
            HOperatorSet.Intersection(ho_SelectedRegions4, ho_RegionIntersection4, out ho_RegionIntersection2
                );
            ho_SortedRegions2.Dispose();
            HOperatorSet.SortRegion(ho_RegionIntersection2, out ho_SortedRegions2, "character",
                "true", "column");
            HOperatorSet.AreaCenter(ho_SortedRegions2, out hv_Area12, out hv_Row12, out hv_Column12);
            HOperatorSet.CountObj(ho_SortedRegions2, out hv_Number2);
            if ((int)(new HTuple(hv_Number2.TupleNotEqual(7))) != 0)
            {
                hv_ErrorCounts = 2;
                ho_Image.Dispose();
                ho_ImageEmphasize.Dispose();
                ho_ImageBinomial0.Dispose();
                ho_ImageMean1.Dispose();
                ho_Regions.Dispose();
                ho_ConnectedRegions3.Dispose();
                ho_SelectedRegions8.Dispose();
                ho_RegionUnion4.Dispose();
                ho_RegionClosing5.Dispose();
                ho_ConnectedRegions6.Dispose();
                ho_SelectedRegions7.Dispose();
                ho_SelectedRegions5.Dispose();
                ho_rotateImage.Dispose();
                ho_ImageEmphasize1.Dispose();
                ho_ImageBinomia1.Dispose();
                ho_ImageMean.Dispose();
                ho_Region.Dispose();
                ho_ConnectedRegions.Dispose();
                ho_SelectedRegions.Dispose();
                ho_RegionUnion.Dispose();
                ho_RegionClosing.Dispose();
                ho_RegionClosing7.Dispose();
                ho_RegionIntersection6.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_RegionUnion1.Dispose();
                ho_RegionIntersection.Dispose();
                ho_RectChar1.Dispose();
                ho_RectChar2.Dispose();
                ho_RegionIntersection2.Dispose();
                ho_RegionClosing3.Dispose();
                ho_RegionClosing1.Dispose();
                ho_ConnectedRegions4.Dispose();
                ho_RegionTrans.Dispose();
                ho_Partitioned.Dispose();
                ho_SelectedRegions3.Dispose();
                ho_RegionIntersection3.Dispose();
                ho_SortedRegions.Dispose();
                ho_RegionIntersection4.Dispose();
                ho_RegionClosing4.Dispose();
                ho_RegionClosing2.Dispose();
                ho_ConnectedRegions2.Dispose();
                ho_RegionTrans1.Dispose();
                ho_Partitioned1.Dispose();
                ho_SelectedRegions4.Dispose();
                ho_SortedRegions2.Dispose();

                return;
            }

            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_SortedRegions, HDevWindowStack.GetActive());
            }
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.DispObj(ho_SortedRegions2, HDevWindowStack.GetActive());
            }


            ho_Image.Dispose();
            ho_ImageEmphasize.Dispose();
            ho_ImageBinomial0.Dispose();
            ho_ImageMean1.Dispose();
            ho_Regions.Dispose();
            ho_ConnectedRegions3.Dispose();
            ho_SelectedRegions8.Dispose();
            ho_RegionUnion4.Dispose();
            ho_RegionClosing5.Dispose();
            ho_ConnectedRegions6.Dispose();
            ho_SelectedRegions7.Dispose();
            ho_SelectedRegions5.Dispose();
            ho_rotateImage.Dispose();
            ho_ImageEmphasize1.Dispose();
            ho_ImageBinomia1.Dispose();
            ho_ImageMean.Dispose();
            ho_Region.Dispose();
            ho_ConnectedRegions.Dispose();
            ho_SelectedRegions.Dispose();
            ho_RegionUnion.Dispose();
            ho_RegionClosing.Dispose();
            ho_RegionClosing7.Dispose();
            ho_RegionIntersection6.Dispose();
            ho_ConnectedRegions1.Dispose();
            ho_SelectedRegions1.Dispose();
            ho_RegionUnion1.Dispose();
            ho_RegionIntersection.Dispose();
            ho_RectChar1.Dispose();
            ho_RectChar2.Dispose();
            ho_RegionIntersection2.Dispose();
            ho_RegionClosing3.Dispose();
            ho_RegionClosing1.Dispose();
            ho_ConnectedRegions4.Dispose();
            ho_RegionTrans.Dispose();
            ho_Partitioned.Dispose();
            ho_SelectedRegions3.Dispose();
            ho_RegionIntersection3.Dispose();
            ho_SortedRegions.Dispose();
            ho_RegionIntersection4.Dispose();
            ho_RegionClosing4.Dispose();
            ho_RegionClosing2.Dispose();
            ho_ConnectedRegions2.Dispose();
            ho_RegionTrans1.Dispose();
            ho_Partitioned1.Dispose();
            ho_SelectedRegions4.Dispose();
            ho_SortedRegions2.Dispose();

            Area = new HTuple();
            Area = Area.TupleConcat(hv_Area11);
            Area = Area.TupleConcat(hv_Area12);

        }


    }
}
