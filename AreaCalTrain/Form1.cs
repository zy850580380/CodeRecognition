using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HalconDotNet;

namespace haocon_ocr_0518
{
    public partial class Form1 : Form
    {
        string ImagePath;
        HDevelopExport HD = new HDevelopExport();
        bool formLoadFlag = true;
        string knownStr = "";
        const string orderCharStr ="0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        char[] orderChar = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z', 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't', 'u', 'v', 'w', 'x', 'y', 'z' };
        int[] areaMin = new int[62]{0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61};
        int[] areaMax = new int[62] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31, 32, 33, 34, 35, 36, 37, 38, 39, 40, 41, 42, 43, 44, 45, 46, 47, 48, 49, 50, 51, 52, 53, 54, 55, 56, 57, 58, 59, 60, 61 };
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            knownStr = textBox3.Text;
            HD.ProcessImage(hWindowControl1.HalconWindow, ImagePath, knownStr);
                if (!HD.hv_ErrorCount)
                {
                    label1.Text = "合格";
                    //string str1 = HD.ShowStr1.ToString().Replace(";", "");
                    //string str2 = HD.ShowStr2.ToString().Replace(";", "");
                    //textBox1.Text = str1;
                    //textBox2.Text = str2;
                    //int[] iNums = Array.ConvertAll(sNums, int.Parse);
                }
                else
                {
                    if (HD.hv_ErrorCount == 1)
                    {
                        label1.Text = "不合格, 第一行缺失字符";
                    }
                    else if (HD.hv_ErrorCount == 2)
                    {
                        label1.Text = "不合格, 第二行缺失字符";
                    }
                    else
                    {
                        label1.Text = "不合格, 缺失多个字符";
                    }
                }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            openFileDialog1.ShowDialog();
            ImagePath = openFileDialog1.FileName;
            HD.ShowImage(hWindowControl1.HalconWindow, ImagePath);
            label1.Text = ImagePath;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (formLoadFlag)
            {
                formLoadFlag = false;
                //textBox1.Text = HD.hv_area1.ToString();
                for (int i = 0; i < HD.hv_area1.Length; i++)
                {
                    areaMin[Array.IndexOf(orderChar, knownStr[i])] = HD.hv_area1[i];
                    areaMax[Array.IndexOf(orderChar, knownStr[i])] = HD.hv_area1[i];
                }
            }
            else
            {
                for (int i = 0; i < HD.hv_area1.Length; i++)
                {
                    if (areaMin[Array.IndexOf(orderChar, HD.hv_KnownStr[i])] > HD.hv_area1[i])
                    {
                        areaMin[Array.IndexOf(orderChar, HD.hv_KnownStr[i])] = HD.hv_area1[i];
                    }
                    if (areaMax[Array.IndexOf(orderChar, HD.hv_KnownStr[i])] < HD.hv_area1[i])
                    {
                        areaMax[Array.IndexOf(orderChar, HD.hv_KnownStr[i])] = HD.hv_area1[i];
                    }
                }
            }
            string str = "";
            for (int i = 0; i < areaMin.Length; i++)
                str += areaMin[i] + ", ";
            textBox1.Text = str;
            for (int i = 0; i < areaMax.Length; i++)
                str += areaMax[i] + ", ";
            textBox2.Text = str;
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
        public HTuple hv_area1 = new HTuple(), hv_area2 = new HTuple();
        public HTuple hv_KnownStr = new HTuple();
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

        public void ProcessImage(HTuple HDWindow_, string ImageFile_, string KnownStr)
        {
            HDevWindowStack.Push(HDWindow_);
            ImageFile = ImageFile_;
            hv_KnownStr = new HTuple();
            for (int i = 0; i < KnownStr.Length; i++)
            {
                hv_KnownStr[i] = KnownStr[i];
            }
            action();
        }

        private void action()
        {

            // Local iconic variables 

            HObject ho_Image, ho_DotImage0, ho_Region0;
            HObject ho_RegionLines, ho_RegionLinesDilation, ho_BinImage;
            HObject ho_ImageAddResult, ho_DotImage01, ho_Region01, ho_RegionClosing01;
            HObject ho_RegionOpening01, ho_RegionClosing012, ho_RegionFillUp0;
            HObject ho_ConnectedRegions0, ho_SelectedRegions0, ho_SelectedRegionsStd0;
            HObject ho_RegionIntersection0, ho_ImageRotate0, ho_ImageRotate1;
            HObject ho_RegionClosing02, ho_RegionFillUp01, ho_RegionOpening02;
            HObject ho_RegionClosing021, ho_ConnectedRegions01, ho_SelectedRegions01;
            HObject ho_SelectedRegionsStd01, ho_RegionIntersection01;
            HObject ho_Rectangle01, ho_ImageReduced1, ho_DotImage2;
            HObject ho_ImageMean2, ho_RegionDynThresh2, ho_Rectangle2;
            HObject ho_RegionClosing2, ho_Rectangle21, ho_RegionClosing21;
            HObject ho_RegionClosing22, ho_ConnectedRegions1, ho_RegionTrans1;
            HObject ho_Partitioned1, ho_SelectedRegions1, ho_RegionIntersection1;
            HObject ho_SortedRegions1, ho_ImageReduced2, ho_DotImage3;
            HObject ho_Region3, ho_Rectangle3, ho_RegionClosing3, ho_Rectangle31;
            HObject ho_RegionClosing31, ho_RegionClosing32, ho_ConnectedRegions3;


            // Local control variables 

            HTuple hv_Width0, hv_Height0, hv_Angle0, hv_Dist0;
            HTuple hv_Length, hv_Number0, hv_Row0, hv_Column0;
            HTuple hv_Phi0, hv_Length01, hv_Length02, hv_Deg0, hv_Number01;
            HTuple hv_Row11, hv_Column11, hv_Row12, hv_Column12, hv_Number1;
            HTuple hv_Number3, hv_Area11;

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Image);
            HOperatorSet.GenEmptyObj(out ho_DotImage0);
            HOperatorSet.GenEmptyObj(out ho_Region0);
            HOperatorSet.GenEmptyObj(out ho_RegionLines);
            HOperatorSet.GenEmptyObj(out ho_RegionLinesDilation);
            HOperatorSet.GenEmptyObj(out ho_BinImage);
            HOperatorSet.GenEmptyObj(out ho_ImageAddResult);
            HOperatorSet.GenEmptyObj(out ho_DotImage01);
            HOperatorSet.GenEmptyObj(out ho_Region01);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing01);
            HOperatorSet.GenEmptyObj(out ho_RegionOpening01);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing012);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp0);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions0);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions0);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegionsStd0);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection0);
            HOperatorSet.GenEmptyObj(out ho_ImageRotate0);
            HOperatorSet.GenEmptyObj(out ho_ImageRotate1);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing02);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp01);
            HOperatorSet.GenEmptyObj(out ho_RegionOpening02);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing021);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions01);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions01);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegionsStd01);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection01);
            HOperatorSet.GenEmptyObj(out ho_Rectangle01);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced1);
            HOperatorSet.GenEmptyObj(out ho_DotImage2);
            HOperatorSet.GenEmptyObj(out ho_ImageMean2);
            HOperatorSet.GenEmptyObj(out ho_RegionDynThresh2);
            HOperatorSet.GenEmptyObj(out ho_Rectangle2);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing2);
            HOperatorSet.GenEmptyObj(out ho_Rectangle21);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing21);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing22);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_RegionTrans1);
            HOperatorSet.GenEmptyObj(out ho_Partitioned1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection1);
            HOperatorSet.GenEmptyObj(out ho_SortedRegions1);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced2);
            HOperatorSet.GenEmptyObj(out ho_DotImage3);
            HOperatorSet.GenEmptyObj(out ho_Region3);
            HOperatorSet.GenEmptyObj(out ho_Rectangle3);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing3);
            HOperatorSet.GenEmptyObj(out ho_Rectangle31);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing31);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing32);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions3);

            ho_Image.Dispose();
            HOperatorSet.ReadImage(out ho_Image, ImageFile);
            HOperatorSet.GetImageSize(ho_Image, out hv_Width0, out hv_Height0);
            ho_DotImage0.Dispose();
            HOperatorSet.DotsImage(ho_Image, out ho_DotImage0, 3, "dark", 2);
            ho_Region0.Dispose();
            HOperatorSet.Threshold(ho_DotImage0, out ho_Region0, 140, 255);
            HOperatorSet.HoughLines(ho_Region0, 8, 800, 5, 5, out hv_Angle0, out hv_Dist0);
            HOperatorSet.TupleLength(hv_Angle0, out hv_Length);
            if ((int)(new HTuple(hv_Length.TupleLess(1))) != 0)
            {
                HOperatorSet.HoughLines(ho_Region0, 4, 800, 5, 5, out hv_Angle0, out hv_Dist0);
                HOperatorSet.TupleLength(hv_Angle0, out hv_Length);
                if ((int)(new HTuple(hv_Length.TupleLess(1))) != 0)
                {
                    HOperatorSet.HoughLines(ho_Region0, 2, 800, 5, 5, out hv_Angle0, out hv_Dist0);
                }
            }
            ho_RegionLines.Dispose();
            HOperatorSet.GenRegionHline(out ho_RegionLines, hv_Angle0, hv_Dist0);
            ho_RegionLinesDilation.Dispose();
            HOperatorSet.DilationCircle(ho_RegionLines, out ho_RegionLinesDilation, 2.5);
            ho_BinImage.Dispose();
            HOperatorSet.RegionToBin(ho_RegionLinesDilation, out ho_BinImage, 255, 0, hv_Width0,
                hv_Height0);
            ho_ImageAddResult.Dispose();
            HOperatorSet.AddImage(ho_Image, ho_BinImage, out ho_ImageAddResult, 0.5, 127);
            ho_DotImage01.Dispose();
            HOperatorSet.DotsImage(ho_ImageAddResult, out ho_DotImage01, 3, "dark", 2);
            ho_Region01.Dispose();
            HOperatorSet.Threshold(ho_DotImage01, out ho_Region01, 140, 255);
            ho_RegionClosing01.Dispose();
            HOperatorSet.ClosingRectangle1(ho_Region01, out ho_RegionClosing01, 30, 10);
            ho_RegionOpening01.Dispose();
            HOperatorSet.OpeningCircle(ho_RegionClosing01, out ho_RegionOpening01, 3.5);
            ho_RegionClosing012.Dispose();
            HOperatorSet.ClosingCircle(ho_RegionOpening01, out ho_RegionClosing012, 7.5);
            ho_RegionFillUp0.Dispose();
            HOperatorSet.FillUp(ho_RegionClosing012, out ho_RegionFillUp0);
            ho_ConnectedRegions0.Dispose();
            HOperatorSet.Connection(ho_RegionFillUp0, out ho_ConnectedRegions0);
            ho_SelectedRegions0.Dispose();
            HOperatorSet.SelectShape(ho_ConnectedRegions0, out ho_SelectedRegions0, (new HTuple("width")).TupleConcat(
                "area"), "and", (new HTuple(360)).TupleConcat(10000), (new HTuple(440)).TupleConcat(
                24000));
            ho_SelectedRegionsStd0.Dispose();
            HOperatorSet.SelectShapeStd(ho_SelectedRegions0, out ho_SelectedRegionsStd0,
                "max_area", 70);
            HOperatorSet.CountObj(ho_SelectedRegionsStd0, out hv_Number0);
            hv_ErrorCount = 0;
            if ((int)(new HTuple(hv_Number0.TupleNotEqual(1))) != 0)
            {
                ho_Image.Dispose();
                ho_DotImage0.Dispose();
                ho_Region0.Dispose();
                ho_RegionLines.Dispose();
                ho_RegionLinesDilation.Dispose();
                ho_BinImage.Dispose();
                ho_ImageAddResult.Dispose();
                ho_DotImage01.Dispose();
                ho_Region01.Dispose();
                ho_RegionClosing01.Dispose();
                ho_RegionOpening01.Dispose();
                ho_RegionClosing012.Dispose();
                ho_RegionFillUp0.Dispose();
                ho_ConnectedRegions0.Dispose();
                ho_SelectedRegions0.Dispose();
                ho_SelectedRegionsStd0.Dispose();
                ho_RegionIntersection0.Dispose();
                ho_ImageRotate0.Dispose();
                ho_ImageRotate1.Dispose();
                ho_RegionClosing02.Dispose();
                ho_RegionFillUp01.Dispose();
                ho_RegionOpening02.Dispose();
                ho_RegionClosing021.Dispose();
                ho_ConnectedRegions01.Dispose();
                ho_SelectedRegions01.Dispose();
                ho_SelectedRegionsStd01.Dispose();
                ho_RegionIntersection01.Dispose();
                ho_Rectangle01.Dispose();
                ho_ImageReduced1.Dispose();
                ho_DotImage2.Dispose();
                ho_ImageMean2.Dispose();
                ho_RegionDynThresh2.Dispose();
                ho_Rectangle2.Dispose();
                ho_RegionClosing2.Dispose();
                ho_Rectangle21.Dispose();
                ho_RegionClosing21.Dispose();
                ho_RegionClosing22.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_RegionTrans1.Dispose();
                ho_Partitioned1.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_RegionIntersection1.Dispose();
                ho_SortedRegions1.Dispose();
                ho_ImageReduced2.Dispose();
                ho_DotImage3.Dispose();
                ho_Region3.Dispose();
                ho_Rectangle3.Dispose();
                ho_RegionClosing3.Dispose();
                ho_Rectangle31.Dispose();
                ho_RegionClosing31.Dispose();
                ho_RegionClosing32.Dispose();
                ho_ConnectedRegions3.Dispose();
                hv_ErrorCount = 10;
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
            HOperatorSet.RotateImage(ho_Image, out ho_ImageRotate0, -hv_Deg0, "constant");
            //处理旋转后的图像
            ho_ImageRotate1.Dispose();
            HOperatorSet.RotateImage(ho_DotImage01, out ho_ImageRotate1, -hv_Deg0, "constant");
            ho_Region01.Dispose();
            HOperatorSet.Threshold(ho_ImageRotate1, out ho_Region01, 140, 255);
            ho_RegionClosing02.Dispose();
            HOperatorSet.ClosingRectangle1(ho_Region01, out ho_RegionClosing02, 30, 10);
            ho_RegionFillUp01.Dispose();
            HOperatorSet.FillUp(ho_RegionClosing02, out ho_RegionFillUp01);
            ho_RegionOpening02.Dispose();
            HOperatorSet.OpeningCircle(ho_RegionFillUp01, out ho_RegionOpening02, 3.5);
            ho_RegionClosing021.Dispose();
            HOperatorSet.ClosingCircle(ho_RegionOpening02, out ho_RegionClosing021, 7.5);
            //closing_rectangle1 (RegionClosing02, RegionClosing03, 20, 1)
            ho_ConnectedRegions01.Dispose();
            HOperatorSet.Connection(ho_RegionClosing021, out ho_ConnectedRegions01);
            ho_SelectedRegions01.Dispose();
            HOperatorSet.SelectShape(ho_ConnectedRegions01, out ho_SelectedRegions01, (new HTuple("width")).TupleConcat(
                "area"), "and", (new HTuple(360)).TupleConcat(9000), (new HTuple(440)).TupleConcat(
                16000));
            ho_SelectedRegionsStd01.Dispose();
            HOperatorSet.SelectShapeStd(ho_SelectedRegions01, out ho_SelectedRegionsStd01,
                "max_area", 70);
            HOperatorSet.CountObj(ho_SelectedRegions01, out hv_Number01);
            if ((int)(new HTuple(hv_Number01.TupleNotEqual(1))) != 0)
            {
                ho_Image.Dispose();
                ho_DotImage0.Dispose();
                ho_Region0.Dispose();
                ho_RegionLines.Dispose();
                ho_RegionLinesDilation.Dispose();
                ho_BinImage.Dispose();
                ho_ImageAddResult.Dispose();
                ho_DotImage01.Dispose();
                ho_Region01.Dispose();
                ho_RegionClosing01.Dispose();
                ho_RegionOpening01.Dispose();
                ho_RegionClosing012.Dispose();
                ho_RegionFillUp0.Dispose();
                ho_ConnectedRegions0.Dispose();
                ho_SelectedRegions0.Dispose();
                ho_SelectedRegionsStd0.Dispose();
                ho_RegionIntersection0.Dispose();
                ho_ImageRotate0.Dispose();
                ho_ImageRotate1.Dispose();
                ho_RegionClosing02.Dispose();
                ho_RegionFillUp01.Dispose();
                ho_RegionOpening02.Dispose();
                ho_RegionClosing021.Dispose();
                ho_ConnectedRegions01.Dispose();
                ho_SelectedRegions01.Dispose();
                ho_SelectedRegionsStd01.Dispose();
                ho_RegionIntersection01.Dispose();
                ho_Rectangle01.Dispose();
                ho_ImageReduced1.Dispose();
                ho_DotImage2.Dispose();
                ho_ImageMean2.Dispose();
                ho_RegionDynThresh2.Dispose();
                ho_Rectangle2.Dispose();
                ho_RegionClosing2.Dispose();
                ho_Rectangle21.Dispose();
                ho_RegionClosing21.Dispose();
                ho_RegionClosing22.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_RegionTrans1.Dispose();
                ho_Partitioned1.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_RegionIntersection1.Dispose();
                ho_SortedRegions1.Dispose();
                ho_ImageReduced2.Dispose();
                ho_DotImage3.Dispose();
                ho_Region3.Dispose();
                ho_Rectangle3.Dispose();
                ho_RegionClosing3.Dispose();
                ho_Rectangle31.Dispose();
                ho_RegionClosing31.Dispose();
                ho_RegionClosing32.Dispose();
                ho_ConnectedRegions3.Dispose();
                hv_ErrorCount = 10;
                return;
                
            }
            ho_RegionIntersection01.Dispose();
            HOperatorSet.Intersection(ho_SelectedRegionsStd01, ho_Region01, out ho_RegionIntersection01
                );
            HOperatorSet.SmallestRectangle1(ho_RegionIntersection01, out hv_Row11, out hv_Column11,
                out hv_Row12, out hv_Column12);
            ho_Rectangle01.Dispose();
            HOperatorSet.GenRectangle1(out ho_Rectangle01, hv_Row11 - 2, hv_Column11 - 4, hv_Row12 + 2,
                hv_Column12 + 2);
            //缩小区域
            ho_ImageReduced1.Dispose();
            HOperatorSet.ReduceDomain(ho_ImageRotate1, ho_Rectangle01, out ho_ImageReduced1
                );
            ho_DotImage2.Dispose();
            HOperatorSet.DotsImage(ho_ImageReduced1, out ho_DotImage2, 3, "light", 2);
            ho_ImageMean2.Dispose();
            HOperatorSet.MeanImage(ho_DotImage2, out ho_ImageMean2, 7, 7);
            ho_RegionDynThresh2.Dispose();
            HOperatorSet.DynThreshold(ho_DotImage2, ho_ImageMean2, out ho_RegionDynThresh2,
                60, "light");
            //形态学处理
            ho_Rectangle2.Dispose();
            HOperatorSet.GenRectangle2(out ho_Rectangle2, 10, 10, (new HTuple(45)).TupleRad()
                , 3, 0);
            ho_RegionClosing2.Dispose();
            HOperatorSet.Closing(ho_RegionDynThresh2, ho_Rectangle2, out ho_RegionClosing2
                );
            ho_Rectangle21.Dispose();
            HOperatorSet.GenRectangle2(out ho_Rectangle21, 10, 10, (new HTuple(135)).TupleRad()
                , 3, 0);
            ho_RegionClosing21.Dispose();
            HOperatorSet.Closing(ho_RegionClosing2, ho_Rectangle21, out ho_RegionClosing21
                );
            ho_RegionClosing22.Dispose();
            HOperatorSet.ClosingRectangle1(ho_RegionClosing21, out ho_RegionClosing22, 5,
                30);

            ho_ConnectedRegions1.Dispose();
            HOperatorSet.Connection(ho_RegionClosing22, out ho_ConnectedRegions1);
            ho_RegionTrans1.Dispose();
            HOperatorSet.ShapeTrans(ho_ConnectedRegions1, out ho_RegionTrans1, "rectangle1");
            ho_Partitioned1.Dispose();
            HOperatorSet.PartitionRectangle(ho_RegionTrans1, out ho_Partitioned1, 26, 33);
            ho_SelectedRegions1.Dispose();
            HOperatorSet.SelectShape(ho_Partitioned1, out ho_SelectedRegions1, (new HTuple("width")).TupleConcat(
                "height"), "and", (new HTuple(6)).TupleConcat(30), (new HTuple(28)).TupleConcat(
                40));
            ho_RegionIntersection1.Dispose();
            HOperatorSet.Intersection(ho_SelectedRegions1, ho_RegionDynThresh2, out ho_RegionIntersection1
                );
            ho_SortedRegions1.Dispose();
            HOperatorSet.SortRegion(ho_RegionIntersection1, out ho_SortedRegions1, "first_point",
                "true", "column");
            HOperatorSet.CountObj(ho_SortedRegions1, out hv_Number1);
            if ((int)(new HTuple(hv_Number1.TupleNotEqual(16))) != 0)
            {
                ho_Image.Dispose();
                ho_DotImage0.Dispose();
                ho_Region0.Dispose();
                ho_RegionLines.Dispose();
                ho_RegionLinesDilation.Dispose();
                ho_BinImage.Dispose();
                ho_ImageAddResult.Dispose();
                ho_DotImage01.Dispose();
                ho_Region01.Dispose();
                ho_RegionClosing01.Dispose();
                ho_RegionOpening01.Dispose();
                ho_RegionClosing012.Dispose();
                ho_RegionFillUp0.Dispose();
                ho_ConnectedRegions0.Dispose();
                ho_SelectedRegions0.Dispose();
                ho_SelectedRegionsStd0.Dispose();
                ho_RegionIntersection0.Dispose();
                ho_ImageRotate0.Dispose();
                ho_ImageRotate1.Dispose();
                ho_RegionClosing02.Dispose();
                ho_RegionFillUp01.Dispose();
                ho_RegionOpening02.Dispose();
                ho_RegionClosing021.Dispose();
                ho_ConnectedRegions01.Dispose();
                ho_SelectedRegions01.Dispose();
                ho_SelectedRegionsStd01.Dispose();
                ho_RegionIntersection01.Dispose();
                ho_Rectangle01.Dispose();
                ho_ImageReduced1.Dispose();
                ho_DotImage2.Dispose();
                ho_ImageMean2.Dispose();
                ho_RegionDynThresh2.Dispose();
                ho_Rectangle2.Dispose();
                ho_RegionClosing2.Dispose();
                ho_Rectangle21.Dispose();
                ho_RegionClosing21.Dispose();
                ho_RegionClosing22.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_RegionTrans1.Dispose();
                ho_Partitioned1.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_RegionIntersection1.Dispose();
                ho_SortedRegions1.Dispose();
                ho_ImageReduced2.Dispose();
                ho_DotImage3.Dispose();
                ho_Region3.Dispose();
                ho_Rectangle3.Dispose();
                ho_RegionClosing3.Dispose();
                ho_Rectangle31.Dispose();
                ho_RegionClosing31.Dispose();
                ho_RegionClosing32.Dispose();
                ho_ConnectedRegions3.Dispose();
                hv_ErrorCount = 1;
                return;
                
            }
            //判断字符是否在直线上
            ho_ImageReduced2.Dispose();
            HOperatorSet.ReduceDomain(ho_ImageRotate0, ho_Rectangle01, out ho_ImageReduced2
                );
            ho_DotImage3.Dispose();
            HOperatorSet.DotsImage(ho_ImageReduced2, out ho_DotImage3, 3, "dark", 2);
            ho_Region3.Dispose();
            HOperatorSet.Threshold(ho_DotImage3, out ho_Region3, 100, 255);

            ho_Rectangle3.Dispose();
            HOperatorSet.GenRectangle2(out ho_Rectangle3, 10, 10, (new HTuple(45)).TupleRad()
                , 3, 0);
            ho_RegionClosing3.Dispose();
            HOperatorSet.Closing(ho_Region3, ho_Rectangle3, out ho_RegionClosing3);
            ho_Rectangle31.Dispose();
            HOperatorSet.GenRectangle2(out ho_Rectangle31, 10, 10, (new HTuple(135)).TupleRad()
                , 3, 0);
            ho_RegionClosing31.Dispose();
            HOperatorSet.Closing(ho_RegionClosing3, ho_Rectangle31, out ho_RegionClosing31
                );
            ho_RegionClosing32.Dispose();
            HOperatorSet.ClosingRectangle1(ho_RegionClosing31, out ho_RegionClosing32, 5,
                30);

            ho_ConnectedRegions3.Dispose();
            HOperatorSet.Connection(ho_RegionClosing32, out ho_ConnectedRegions3);
            HOperatorSet.CountObj(ho_ConnectedRegions3, out hv_Number3);
            if ((int)(new HTuple(hv_Number3.TupleLess(8))) != 0)
            {
                ho_Image.Dispose();
                ho_DotImage0.Dispose();
                ho_Region0.Dispose();
                ho_RegionLines.Dispose();
                ho_RegionLinesDilation.Dispose();
                ho_BinImage.Dispose();
                ho_ImageAddResult.Dispose();
                ho_DotImage01.Dispose();
                ho_Region01.Dispose();
                ho_RegionClosing01.Dispose();
                ho_RegionOpening01.Dispose();
                ho_RegionClosing012.Dispose();
                ho_RegionFillUp0.Dispose();
                ho_ConnectedRegions0.Dispose();
                ho_SelectedRegions0.Dispose();
                ho_SelectedRegionsStd0.Dispose();
                ho_RegionIntersection0.Dispose();
                ho_ImageRotate0.Dispose();
                ho_ImageRotate1.Dispose();
                ho_RegionClosing02.Dispose();
                ho_RegionFillUp01.Dispose();
                ho_RegionOpening02.Dispose();
                ho_RegionClosing021.Dispose();
                ho_ConnectedRegions01.Dispose();
                ho_SelectedRegions01.Dispose();
                ho_SelectedRegionsStd01.Dispose();
                ho_RegionIntersection01.Dispose();
                ho_Rectangle01.Dispose();
                ho_ImageReduced1.Dispose();
                ho_DotImage2.Dispose();
                ho_ImageMean2.Dispose();
                ho_RegionDynThresh2.Dispose();
                ho_Rectangle2.Dispose();
                ho_RegionClosing2.Dispose();
                ho_Rectangle21.Dispose();
                ho_RegionClosing21.Dispose();
                ho_RegionClosing22.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_RegionTrans1.Dispose();
                ho_Partitioned1.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_RegionIntersection1.Dispose();
                ho_SortedRegions1.Dispose();
                ho_ImageReduced2.Dispose();
                ho_DotImage3.Dispose();
                ho_Region3.Dispose();
                ho_Rectangle3.Dispose();
                ho_RegionClosing3.Dispose();
                ho_Rectangle31.Dispose();
                ho_RegionClosing31.Dispose();
                ho_RegionClosing32.Dispose();
                ho_ConnectedRegions3.Dispose();
                hv_ErrorCount = 2;
                return;
            }

            HOperatorSet.AreaCenter(ho_SortedRegions1, out hv_Area11, out hv_Row11, out hv_Column11);

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


            ho_Image.Dispose();
            ho_DotImage0.Dispose();
            ho_Region0.Dispose();
            ho_RegionLines.Dispose();
            ho_RegionLinesDilation.Dispose();
            ho_BinImage.Dispose();
            ho_ImageAddResult.Dispose();
            ho_DotImage01.Dispose();
            ho_Region01.Dispose();
            ho_RegionClosing01.Dispose();
            ho_RegionOpening01.Dispose();
            ho_RegionClosing012.Dispose();
            ho_RegionFillUp0.Dispose();
            ho_ConnectedRegions0.Dispose();
            ho_SelectedRegions0.Dispose();
            ho_SelectedRegionsStd0.Dispose();
            ho_RegionIntersection0.Dispose();
            ho_ImageRotate0.Dispose();
            ho_ImageRotate1.Dispose();
            ho_RegionClosing02.Dispose();
            ho_RegionFillUp01.Dispose();
            ho_RegionOpening02.Dispose();
            ho_RegionClosing021.Dispose();
            ho_ConnectedRegions01.Dispose();
            ho_SelectedRegions01.Dispose();
            ho_SelectedRegionsStd01.Dispose();
            ho_RegionIntersection01.Dispose();
            ho_Rectangle01.Dispose();
            ho_ImageReduced1.Dispose();
            ho_DotImage2.Dispose();
            ho_ImageMean2.Dispose();
            ho_RegionDynThresh2.Dispose();
            ho_Rectangle2.Dispose();
            ho_RegionClosing2.Dispose();
            ho_Rectangle21.Dispose();
            ho_RegionClosing21.Dispose();
            ho_RegionClosing22.Dispose();
            ho_ConnectedRegions1.Dispose();
            ho_RegionTrans1.Dispose();
            ho_Partitioned1.Dispose();
            ho_SelectedRegions1.Dispose();
            ho_RegionIntersection1.Dispose();
            ho_SortedRegions1.Dispose();
            ho_ImageReduced2.Dispose();
            ho_DotImage3.Dispose();
            ho_Region3.Dispose();
            ho_Rectangle3.Dispose();
            ho_RegionClosing3.Dispose();
            ho_Rectangle31.Dispose();
            ho_RegionClosing31.Dispose();
            ho_RegionClosing32.Dispose();
            ho_ConnectedRegions3.Dispose();

            hv_area1 = hv_Area11;
        }


    }
}
