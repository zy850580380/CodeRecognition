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
        int[] areaMin = new int[62] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
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
        }

        private void button2_Click(object sender, EventArgs e)
        {

            openFileDialog1.ShowDialog();
            ImagePath = openFileDialog1.FileName;
            HD.ShowImage(hWindowControl1.HalconWindow, ImagePath);
            labelMessage.Text = ImagePath;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            knownStr = textBoxPriorChar.Text;
            if (formLoadFlag)
            {
                formLoadFlag = false;
                //textBox1.Text = HD.hv_area1.ToString();
                for (int i = 0; i < HD.Area.Length; i++)
                {
                    areaMin[Array.IndexOf(orderChar, knownStr[i])] = HD.Area[i];
                }
            }
            else
            {
                for (int i = 0; i < HD.Area.Length; i++)
                {
                    if (areaMin[Array.IndexOf(orderChar, knownStr[i])] > HD.Area[i])
                    {
                        areaMin[Array.IndexOf(orderChar, knownStr[i])] = HD.Area[i];
                    }
                }
            }
            string str = "";
            for (int i = 0; i < textBoxPriorCharAll.Text.Length; i++)
            {
                str += areaMin[Array.IndexOf(orderChar, textBoxPriorCharAll.Text[i])].ToString() + ", ";
            }
            textBoxAreaMin.Text = str;
            str = "";
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
            HObject ho_ImageMean1, ho_Region01, ho_Rectangle1, ho_RegionClosing1;
            HObject ho_Rectangle11, ho_RegionClosing11, ho_RegionClosing12;
            HObject ho_ConnectedRegions1, ho_RegionTrans1, ho_Partitioned1;
            HObject ho_SelectedRegions1, ho_RegionIntersection11, ho_SortedRegions1;
            HObject ho_SelectedRegions2, ho_RegionIntersection21, ho_SortedRegions2;


            // Local control variables 

            HTuple hv_Number0, hv_ErrorCount, hv_Row0;
            HTuple hv_Column0, hv_Phi0, hv_Length01, hv_Length02, hv_Row11;
            HTuple hv_Column11, hv_Row12, hv_Column12, hv_Deg0, hv_Area;
            HTuple hv_Row, hv_Column, hv_Number1, hv_Area11, hv_Number2;
            HTuple hv_Area12;

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
            HOperatorSet.GenEmptyObj(out ho_ImageMean1);
            HOperatorSet.GenEmptyObj(out ho_Region01);
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
            ho_ImageEmphasize.Dispose();
            HOperatorSet.Emphasize(ho_Image, out ho_ImageEmphasize, 7, 7, 3);
            ho_ImageBinomial0.Dispose();
            HOperatorSet.BinomialFilter(ho_ImageEmphasize, out ho_ImageBinomial0, 5, 5);
            ho_ImageMin0.Dispose();
            HOperatorSet.GrayErosionShape(ho_ImageBinomial0, out ho_ImageMin0, 11, 11, "octagon");
            ho_Region0.Dispose();
            HOperatorSet.Threshold(ho_ImageMin0, out ho_Region0, 0, 100);
            ho_RegionClosing00.Dispose();
            HOperatorSet.ClosingRectangle1(ho_Region0, out ho_RegionClosing00, 22, 20);
            ho_ConnectedRegions0.Dispose();
            HOperatorSet.Connection(ho_RegionClosing00, out ho_ConnectedRegions0);
            ho_SelectedRegions0.Dispose();
            HOperatorSet.SelectShape(ho_ConnectedRegions0, out ho_SelectedRegions0, (new HTuple("width")).TupleConcat(
                "height"), "and", (new HTuple(160)).TupleConcat(55), (new HTuple(250)).TupleConcat(
                180));
            HOperatorSet.CountObj(ho_SelectedRegions0, out hv_Number0);
            hv_ErrorCount = 0;
            if ((int)(new HTuple(hv_Number0.TupleNotEqual(1))) != 0)
            {
                hv_ErrorCount = 10;
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
                ho_ImageMean1.Dispose();
                ho_Region01.Dispose();
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

                return;
            }
            ho_RegionIntersection0.Dispose();
            HOperatorSet.Intersection(ho_SelectedRegions0, ho_Region0, out ho_RegionIntersection0
                );
            //ѽתͼб
            HOperatorSet.SmallestRectangle2(ho_RegionIntersection0, out hv_Row0, out hv_Column0,
                out hv_Phi0, out hv_Length01, out hv_Length02);
            HOperatorSet.SmallestRectangle1(ho_RegionIntersection0, out hv_Row11, out hv_Column11,
                out hv_Row12, out hv_Column12);
            ho_Rectangle0.Dispose();
            HOperatorSet.GenRectangle1(out ho_Rectangle0, hv_Row11, hv_Column11, hv_Row12,
                hv_Column12);
            HOperatorSet.TupleDeg(hv_Phi0, out hv_Deg0);
            ho_ImageReduced0.Dispose();
            HOperatorSet.ReduceDomain(ho_ImageBinomial0, ho_Rectangle0, out ho_ImageReduced0
                );
            ho_ImagePart0.Dispose();
            HOperatorSet.CropDomain(ho_ImageReduced0, out ho_ImagePart0);
            ho_ImageRotate0.Dispose();
            HOperatorSet.RotateImage(ho_ImagePart0, out ho_ImageRotate0, -hv_Deg0, "constant");
            //Ԧmѽת۳քͼб
            ho_ImageMean1.Dispose();
            HOperatorSet.MeanImage(ho_ImageRotate0, out ho_ImageMean1, 7, 7);
            ho_Region01.Dispose();
            HOperatorSet.DynThreshold(ho_ImageRotate0, ho_ImageMean1, out ho_Region01, 15,
                "dark");
            //threshold (ImageRotate0, Region01, 0, 105)
            HOperatorSet.AreaCenter(ho_Region01, out hv_Area, out hv_Row, out hv_Column);
            ho_Rectangle1.Dispose();
            HOperatorSet.GenRectangle2(out ho_Rectangle1, 10, 10, (new HTuple(45)).TupleRad()
                , 3, 0);
            ho_RegionClosing1.Dispose();
            HOperatorSet.Closing(ho_Region01, ho_Rectangle1, out ho_RegionClosing1);
            ho_Rectangle11.Dispose();
            HOperatorSet.GenRectangle2(out ho_Rectangle11, 10, 10, (new HTuple(135)).TupleRad()
                , 3, 0);
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
            ho_SelectedRegions1.Dispose();
            HOperatorSet.SelectShape(ho_Partitioned1, out ho_SelectedRegions1, ((new HTuple("width")).TupleConcat(
                "height")).TupleConcat("row"), "and", ((new HTuple(6)).TupleConcat(20)).TupleConcat(
                hv_Row - 30), ((new HTuple(25)).TupleConcat(36)).TupleConcat(hv_Row));
            ho_RegionIntersection11.Dispose();
            HOperatorSet.Intersection(ho_SelectedRegions1, ho_Region01, out ho_RegionIntersection11
                );
            //ѡאؖػ
            //ƅѲ
            ho_SortedRegions1.Dispose();
            HOperatorSet.SortRegion(ho_RegionIntersection11, out ho_SortedRegions1, "first_point",
                "true", "column");
            HOperatorSet.CountObj(ho_SortedRegions1, out hv_Number1);
            HOperatorSet.AreaCenter(ho_SortedRegions1, out hv_Area11, out hv_Row11, out hv_Column11);
            if ((int)(new HTuple(hv_Number1.TupleNotEqual(8))) != 0)
            {
                hv_ErrorCount = 1;
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
                ho_ImageMean1.Dispose();
                ho_Region01.Dispose();
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

                return;
            }

            //֚׾ѐ***********************
            ho_SelectedRegions2.Dispose();
            HOperatorSet.SelectShape(ho_Partitioned1, out ho_SelectedRegions2, ((new HTuple("width")).TupleConcat(
                "height")).TupleConcat("row"), "and", ((new HTuple(6)).TupleConcat(20)).TupleConcat(
                hv_Row), ((new HTuple(25)).TupleConcat(36)).TupleConcat(hv_Row + 30));
            ho_RegionIntersection21.Dispose();
            HOperatorSet.Intersection(ho_SelectedRegions2, ho_Region01, out ho_RegionIntersection21
                );
            //ѡאؖػ
            //ƅѲ
            ho_SortedRegions2.Dispose();
            HOperatorSet.SortRegion(ho_RegionIntersection21, out ho_SortedRegions2, "first_point",
                "true", "column");
            HOperatorSet.CountObj(ho_SortedRegions2, out hv_Number2);
            HOperatorSet.AreaCenter(ho_SortedRegions2, out hv_Area12, out hv_Row12, out hv_Column12);
            if ((int)(new HTuple(hv_Number2.TupleNotEqual(7))) != 0)
            {
                hv_ErrorCount = 2;
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
                ho_ImageMean1.Dispose();
                ho_Region01.Dispose();
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

                return;
            }
            Area = new HTuple();
            Area = Area.TupleConcat(hv_Area11);
            Area = Area.TupleConcat(hv_Area12);
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
            ho_ImageMean1.Dispose();
            ho_Region01.Dispose();
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

        }


    }
}
