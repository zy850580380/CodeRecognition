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

        HDevelopExport HD = new HDevelopExport();
        string knownStr = "";
        const string orderCharStr ="0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        char[] orderChar = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        int[] areaMin = new int[62] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] areaMinLast = new int[62] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        int[] areaMinUlt = new int[62] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
        //int[] areaMin = new int[62] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61 };
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
            if (!HD.hv_ErrorCount)
            {
                labelMessage.Text = "合格";
                saveValue(sender, e);
            }
            else
            {
                if (HD.hv_ErrorCount == 1)
                {
                    labelMessage.Text = "不合格, 第一行缺失字符";
                }
                else if (HD.hv_ErrorCount == 2)
                {
                    labelMessage.Text = "不合格, 第二行缺失字符";
                }
                else
                {
                    labelMessage.Text = "不合格, 缺失多个字符";
                }
            }
            saveValue(sender, e);
            textBoxPriorChar.BackColor = Color.White;
        }

        private void button2_Click(object sender, EventArgs e)
        {

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
            }
            string str = "";
            for (int i = 0; i < textBoxPriorCharAll.Text.Length; i++)
            {
                str += areaMin[Array.IndexOf(orderChar, textBoxPriorCharAll.Text[i])].ToString() + ", ";
            }
            textBoxAreaMin.Text = str;
            str = "";
            labelMessage.Text += "   已保存";
        }

        private void buttonProcMultiImg_Click(object sender, EventArgs e)
        {
            --fileListIndex;
            if (fileListIndex != 0)
            {
                ImagePath = fileList[--fileListIndex];
                HD.ShowImage(hWindowControl1.HalconWindow, ImagePath);
            }
            else
            {
                return;
            }
            labelMessage.Text = ImagePath;
            textBoxPriorChar.BackColor = Color.Blue;
        }

        private void buttonOpenDir_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog()==DialogResult.OK)
            {
                DirectoryInfo folder = new DirectoryInfo(folderBrowserDialog1.SelectedPath);
                labelMessage.Text = folderBrowserDialog1.SelectedPath;

                foreach (FileInfo file in folder.GetFiles("*.bmp"))
                {
                    fileList[fileListIndex++]=file.FullName;
                }
                fileListIndex = 0;
            }
        }

        private void buttonNextImg_Click(object sender, EventArgs e)
        {
            ImagePath = fileList[fileListIndex++];
            if (ImagePath != "")
            {
                HD.ShowImage(hWindowControl1.HalconWindow, ImagePath);
            }
            else
            {
                return;
            }
            labelMessage.Text = ImagePath;
            textBoxPriorChar.BackColor = Color.Blue;
        }

        private void buttonRestoreLast_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < areaMin.Length; i++)
            {
                areaMin[i] = areaMinUlt[i];
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
            }
        }

        private void buttonShowResults_Click(object sender, EventArgs e)
        {
            string str = "";
            for (int i = 0; i < textBoxPriorCharAll.Text.Length; i++)
            {
                str += areaMinUlt[Array.IndexOf(orderChar, textBoxPriorCharAll.Text[i])].ToString() + ", ";
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
        public HTuple hv_ErrorCount = 0;
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
            HObject ho_ImageMin0, ho_Region0, ho_RegionClosing00, ho_ConnectedRegions0;
            HObject ho_SelectedRegions0, ho_RegionIntersection0, ho_Rectangle0;
            HObject ho_ImageReduced0, ho_ImagePart0, ho_ImageRotate0;
            HObject ho_Region01, ho_Rectangle02, ho_ImageReduced02;
            HObject ho_Region02, ho_Rectangle1, ho_RegionClosing1, ho_Rectangle11;
            HObject ho_RegionClosing11, ho_RegionClosing12, ho_ConnectedRegions1;
            HObject ho_RegionTrans1, ho_Partitioned1, ho_SelectedRegions1;
            HObject ho_RegionIntersection11, ho_SortedRegions1, ho_SelectedRegions2;
            HObject ho_RegionIntersection21, ho_SortedRegions2;


            // Local control variables 

            HTuple hv_KnownStr, hv_Number0, hv_Row0, hv_Column0;
            HTuple hv_Phi0, hv_Length01, hv_Length02, hv_Row11, hv_Column11;
            HTuple hv_Row12, hv_Column12, hv_Width, hv_Height, hv_HomMat2DIdentity;
            HTuple hv_HomMat2DRotate, hv_Row21, hv_Column21, hv_Row22;
            HTuple hv_Column22, hv_Area, hv_Row, hv_Column, hv_Number1;
            HTuple hv_Area11, hv_orderChar, hv_AreaMax, hv_AreaMin;
            HTuple hv_AreaDelta, hv_Index1, hv_Indices = new HTuple();
            HTuple hv_ErrorCount = new HTuple(), hv_Number2, hv_Area12;
            HTuple hv_Index2;

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Image);
            HOperatorSet.GenEmptyObj(out ho_ImageEmphasize);
            HOperatorSet.GenEmptyObj(out ho_ImageBinomial0);
            HOperatorSet.GenEmptyObj(out ho_ImageMin0);
            HOperatorSet.GenEmptyObj(out ho_Region0);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing00);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions0);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions0);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection0);
            HOperatorSet.GenEmptyObj(out ho_Rectangle0);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced0);
            HOperatorSet.GenEmptyObj(out ho_ImagePart0);
            HOperatorSet.GenEmptyObj(out ho_ImageRotate0);
            HOperatorSet.GenEmptyObj(out ho_Region01);
            HOperatorSet.GenEmptyObj(out ho_Rectangle02);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced02);
            HOperatorSet.GenEmptyObj(out ho_Region02);
            HOperatorSet.GenEmptyObj(out ho_Rectangle1);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing1);
            HOperatorSet.GenEmptyObj(out ho_Rectangle11);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing11);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing12);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_RegionTrans1);
            HOperatorSet.GenEmptyObj(out ho_Partitioned1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection11);
            HOperatorSet.GenEmptyObj(out ho_SortedRegions1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions2);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection21);
            HOperatorSet.GenEmptyObj(out ho_SortedRegions2);

            ho_Image.Dispose();
            HOperatorSet.ReadImage(out ho_Image, ImageFile);
            hv_KnownStr = new HTuple();
            hv_KnownStr[0] = "2";
            hv_KnownStr[1] = "0";
            hv_KnownStr[2] = "1";
            hv_KnownStr[3] = "8";
            hv_KnownStr[4] = "0";
            hv_KnownStr[5] = "8";
            hv_KnownStr[6] = "2";
            hv_KnownStr[7] = "7";
            hv_KnownStr[8] = "1";
            hv_KnownStr[9] = "0";
            hv_KnownStr[10] = "0";
            hv_KnownStr[11] = "7";
            hv_KnownStr[12] = "1";
            hv_KnownStr[13] = "3";
            hv_KnownStr[14] = "E";
            ho_ImageEmphasize.Dispose();
            HOperatorSet.Emphasize(ho_Image, out ho_ImageEmphasize, 7, 7, 3);
            ho_ImageBinomial0.Dispose();
            HOperatorSet.BinomialFilter(ho_ImageEmphasize, out ho_ImageBinomial0, 5, 5);
            ho_ImageMin0.Dispose();
            HOperatorSet.GrayErosionShape(ho_ImageBinomial0, out ho_ImageMin0, 11, 11, "octagon");
            ho_Region0.Dispose();
            HOperatorSet.BinThreshold(ho_ImageMin0, out ho_Region0);
            ho_RegionClosing00.Dispose();
            HOperatorSet.ClosingRectangle1(ho_Region0, out ho_RegionClosing00, 20, 20);
            ho_ConnectedRegions0.Dispose();
            HOperatorSet.Connection(ho_RegionClosing00, out ho_ConnectedRegions0);
            ho_SelectedRegions0.Dispose();
            HOperatorSet.SelectShape(ho_ConnectedRegions0, out ho_SelectedRegions0, (new HTuple("width")).TupleConcat(
                "height"), "and", (new HTuple(160)).TupleConcat(55), (new HTuple(250)).TupleConcat(
                180));
            HOperatorSet.CountObj(ho_SelectedRegions0, out hv_Number0);
            if ((int)(new HTuple(hv_Number0.TupleNotEqual(1))) != 0)
            {
                return;
            }
            ho_RegionIntersection0.Dispose();
            HOperatorSet.Intersection(ho_SelectedRegions0, ho_Region0, out ho_RegionIntersection0
                );
            //旋转图像
            HOperatorSet.SmallestRectangle2(ho_RegionIntersection0, out hv_Row0, out hv_Column0,
                out hv_Phi0, out hv_Length01, out hv_Length02);
            HOperatorSet.SmallestRectangle1(ho_RegionIntersection0, out hv_Row11, out hv_Column11,
                out hv_Row12, out hv_Column12);
            ho_Rectangle0.Dispose();
            HOperatorSet.GenRectangle1(out ho_Rectangle0, hv_Row11, hv_Column11, hv_Row12,
                hv_Column12);
            ho_ImageReduced0.Dispose();
            HOperatorSet.ReduceDomain(ho_ImageBinomial0, ho_Rectangle0, out ho_ImageReduced0
                );
            ho_ImagePart0.Dispose();
            HOperatorSet.CropDomain(ho_ImageReduced0, out ho_ImagePart0);
            HOperatorSet.GetImageSize(ho_ImagePart0, out hv_Width, out hv_Height);
            HOperatorSet.HomMat2dIdentity(out hv_HomMat2DIdentity);
            HOperatorSet.HomMat2dRotate(hv_HomMat2DIdentity, -hv_Phi0, hv_Width / 2, hv_Height / 2,
                out hv_HomMat2DRotate);
            ho_ImageRotate0.Dispose();
            HOperatorSet.AffineTransImage(ho_ImagePart0, out ho_ImageRotate0, hv_HomMat2DRotate,
                "constant", "true");
            //处理旋转后的图
            ho_Region01.Dispose();
            HOperatorSet.BinThreshold(ho_ImageRotate0, out ho_Region01);
            HOperatorSet.SmallestRectangle1(ho_Region01, out hv_Row21, out hv_Column21, out hv_Row22,
                out hv_Column22);
            ho_Rectangle02.Dispose();
            HOperatorSet.GenRectangle1(out ho_Rectangle02, hv_Row21, hv_Column21, hv_Row22,
                hv_Column22);
            ho_ImageReduced02.Dispose();
            HOperatorSet.ReduceDomain(ho_ImageRotate0, ho_Rectangle02, out ho_ImageReduced02
                );
            ho_Region02.Dispose();
            HOperatorSet.BinThreshold(ho_ImageReduced02, out ho_Region02);
            HOperatorSet.AreaCenter(ho_Region02, out hv_Area, out hv_Row, out hv_Column);
            ho_Rectangle1.Dispose();
            HOperatorSet.GenRectangle2(out ho_Rectangle1, 10, 10, (new HTuple(45)).TupleRad()
                , 2, 0);
            ho_RegionClosing1.Dispose();
            HOperatorSet.Closing(ho_Region02, ho_Rectangle1, out ho_RegionClosing1);
            ho_Rectangle11.Dispose();
            HOperatorSet.GenRectangle2(out ho_Rectangle11, 10, 10, (new HTuple(135)).TupleRad()
                , 2, 0);
            ho_RegionClosing11.Dispose();
            HOperatorSet.Closing(ho_RegionClosing1, ho_Rectangle11, out ho_RegionClosing11
                );
            ho_RegionClosing12.Dispose();
            HOperatorSet.ClosingRectangle1(ho_RegionClosing11, out ho_RegionClosing12, 5,
                7);
            ho_ConnectedRegions1.Dispose();
            HOperatorSet.Connection(ho_RegionClosing12, out ho_ConnectedRegions1);
            ho_RegionTrans1.Dispose();
            HOperatorSet.ShapeTrans(ho_ConnectedRegions1, out ho_RegionTrans1, "rectangle1");
            ho_Partitioned1.Dispose();
            HOperatorSet.PartitionRectangle(ho_RegionTrans1, out ho_Partitioned1, 20, 28);
            //第一行
            ho_SelectedRegions1.Dispose();
            HOperatorSet.SelectShape(ho_Partitioned1, out ho_SelectedRegions1, ((new HTuple("width")).TupleConcat(
                "height")).TupleConcat("row"), "and", ((new HTuple(6)).TupleConcat(20)).TupleConcat(
                hv_Row - 30), ((new HTuple(35)).TupleConcat(40)).TupleConcat(hv_Row));
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
                hv_ErrorCount = 1;
                return;
            }
            hv_orderChar = new HTuple();
            hv_orderChar[0] = "0";
            hv_orderChar[1] = "1";
            hv_orderChar[2] = "2";
            hv_orderChar[3] = "3";
            hv_orderChar[4] = "4";
            hv_orderChar[5] = "5";
            hv_orderChar[6] = "6";
            hv_orderChar[7] = "7";
            hv_orderChar[8] = "8";
            hv_orderChar[9] = "9";
            hv_orderChar[10] = "E";
            hv_AreaMax = new HTuple();
            hv_AreaMax[0] = 157;
            hv_AreaMax[1] = 83;
            hv_AreaMax[2] = 140;
            hv_AreaMax[3] = 120;
            hv_AreaMax[4] = 131;
            hv_AreaMax[5] = 128;
            hv_AreaMax[6] = 225;
            hv_AreaMax[7] = 103;
            hv_AreaMax[8] = 162;
            hv_AreaMax[9] = 136;
            hv_AreaMax[10] = 153;
            hv_AreaMin = new HTuple();
            hv_AreaMin[0] = 114;
            hv_AreaMin[1] = 52;
            hv_AreaMin[2] = 107;
            hv_AreaMin[3] = 112;
            hv_AreaMin[4] = 0;
            hv_AreaMin[5] = 118;
            hv_AreaMin[6] = 128;
            hv_AreaMin[7] = 90;
            hv_AreaMin[8] = 122;
            hv_AreaMin[9] = 132;
            hv_AreaMin[10] = 134;
            hv_AreaDelta = 10;
            for (hv_Index1 = 0; hv_Index1.Continue(hv_Number1 - 1, 1); hv_Index1 = hv_Index1.TupleAdd(1))
            {
                HOperatorSet.TupleFind(hv_orderChar, hv_KnownStr.TupleSelect(hv_Index1), out hv_Indices);
                if ((int)(new HTuple(((hv_Area11.TupleSelect(hv_Index1))).TupleLess((hv_AreaMin.TupleSelect(
                    hv_Indices)) - hv_AreaDelta))) != 0)
                {
                    hv_ErrorCount = 1;
                    return;
                }
            }

            //第二行***********************
            ho_SelectedRegions2.Dispose();
            HOperatorSet.SelectShape(ho_Partitioned1, out ho_SelectedRegions2, ((new HTuple("width")).TupleConcat(
                "height")).TupleConcat("row"), "and", ((new HTuple(6)).TupleConcat(20)).TupleConcat(
                hv_Row), ((new HTuple(35)).TupleConcat(40)).TupleConcat(hv_Row + 30));
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
                hv_ErrorCount = 2;
                return;
            }
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.ClearWindow(HDevWindowStack.GetActive());
            }
            for (hv_Index2 = 0; hv_Index2.Continue(hv_Number2 - 1, 1); hv_Index2 = hv_Index2.TupleAdd(1))
            {
                HOperatorSet.TupleFind(hv_orderChar, hv_KnownStr.TupleSelect(hv_Index2 + 8),
                    out hv_Indices);
                if ((int)(new HTuple(((hv_Area12.TupleSelect(hv_Index2))).TupleLess((hv_AreaMin.TupleSelect(
                    hv_Indices)) - hv_AreaDelta))) != 0)
                {
                    hv_ErrorCount = 2;
                    return;
                }
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
            ho_ImageEmphasize.Dispose();
            ho_ImageBinomial0.Dispose();
            ho_ImageMin0.Dispose();
            ho_Region0.Dispose();
            ho_RegionClosing00.Dispose();
            ho_ConnectedRegions0.Dispose();
            ho_SelectedRegions0.Dispose();
            ho_RegionIntersection0.Dispose();
            ho_Rectangle0.Dispose();
            ho_ImageReduced0.Dispose();
            ho_ImagePart0.Dispose();
            ho_ImageRotate0.Dispose();
            ho_Region01.Dispose();
            ho_Rectangle02.Dispose();
            ho_ImageReduced02.Dispose();
            ho_Region02.Dispose();
            ho_Rectangle1.Dispose();
            ho_RegionClosing1.Dispose();
            ho_Rectangle11.Dispose();
            ho_RegionClosing11.Dispose();
            ho_RegionClosing12.Dispose();
            ho_ConnectedRegions1.Dispose();
            ho_RegionTrans1.Dispose();
            ho_Partitioned1.Dispose();
            ho_SelectedRegions1.Dispose();
            ho_RegionIntersection11.Dispose();
            ho_SortedRegions1.Dispose();
            ho_SelectedRegions2.Dispose();
            ho_RegionIntersection21.Dispose();
            ho_SortedRegions2.Dispose();

            Area = new HTuple();
            Area = Area.TupleConcat(hv_Area11);
            Area = Area.TupleConcat(hv_Area12);

        }


    }
}
