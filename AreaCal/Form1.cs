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
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            HDevelopExport HD = new HDevelopExport();
            string str = textBox3.Text;
            HD.ProcessImage(hWindowControl1.HalconWindow, ImagePath, str);
                if (!HD.hv_ErrorCount)
                {
                    label1.Text = "合格";
                    string str1 = HD.ShowStr1.ToString().Replace(";", "");
                    string str2 = HD.ShowStr2.ToString().Replace(";", "");
                    textBox1.Text = str1;
                    textBox2.Text = str2;
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
            HDevelopExport HDShow = new HDevelopExport();
            HDShow.ShowImage(hWindowControl1.HalconWindow, ImagePath);
            label1.Text = ImagePath;
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
        public HTuple ShowStr1 = new HTuple(), ShowStr2 = new HTuple();
        public HTuple hv_KnownStr;
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
                hv_KnownStr[i] = int.Parse(KnownStr[i].ToString());
            }
            action();
        }

        private void action()
        {

            // Local iconic variables 

            HObject ho_Image, ho_ImageEmphasize0, ho_ImageInvert0;
            HObject ho_Region0, ho_RegionClosing0, ho_ConnectedRegions0;
            HObject ho_SelectedRegions0, ho_SelectedRegionsStd0, ho_RegionIntersection0;
            HObject ho_ImageRotate0, ho_ImageEmphasize01, ho_ImageInvert01;
            HObject ho_ImageBinomial01, ho_Region01, ho_RegionClosing01;
            HObject ho_RegionOpening01, ho_ConnectedRegions01, ho_SelectedRegions01;
            HObject ho_RegionIntersection01, ho_Rectangle01, ho_ImageReduced01;
            HObject ho_Region02, ho_RegionClosing02, ho_ConnectedRegions02;
            HObject ho_SelectedRegions1, ho_RegionIntersection1, ho_RegionClosing1;
            HObject ho_ConnectedRegions1, ho_RegionTrans1, ho_Partitioned1;
            HObject ho_RegionIntersection11, ho_SortedRegions1, ho_SelectedRegions2;
            HObject ho_RegionIntersection2, ho_RegionClosing2, ho_ConnectedRegions2;
            HObject ho_RegionTrans2, ho_Partitioned2, ho_RegionIntersection21;
            HObject ho_SortedRegions2;


            // Local control variables 

            HTuple hv_Number0, hv_Row0;
            HTuple hv_Column0, hv_Phi0, hv_Length01, hv_Length02, hv_Deg0;
            HTuple hv_Number01, hv_Row01, hv_Column01, hv_Phi01, hv_Length011;
            HTuple hv_Length012, hv_Number1, hv_Area11, hv_Row11, hv_Column11;
            HTuple hv_AreaPrior, hv_AreaDelta, hv_Index1;
            HTuple hv_Number2, hv_Area12, hv_Row12, hv_Column12, hv_Index2;

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Image);
            HOperatorSet.GenEmptyObj(out ho_ImageEmphasize0);
            HOperatorSet.GenEmptyObj(out ho_ImageInvert0);
            HOperatorSet.GenEmptyObj(out ho_Region0);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing0);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions0);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions0);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegionsStd0);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection0);
            HOperatorSet.GenEmptyObj(out ho_ImageRotate0);
            HOperatorSet.GenEmptyObj(out ho_ImageEmphasize01);
            HOperatorSet.GenEmptyObj(out ho_ImageInvert01);
            HOperatorSet.GenEmptyObj(out ho_ImageBinomial01);
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
            ho_ImageInvert0.Dispose();
            HOperatorSet.InvertImage(ho_ImageEmphasize0, out ho_ImageInvert0);
            ho_Region0.Dispose();
            HOperatorSet.Threshold(ho_ImageInvert0, out ho_Region0, 150, 255);
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
                hv_ErrorCount = 10;
                ho_Image.Dispose();
                ho_ImageEmphasize0.Dispose();
                ho_ImageInvert0.Dispose();
                ho_Region0.Dispose();
                ho_RegionClosing0.Dispose();
                ho_ConnectedRegions0.Dispose();
                ho_SelectedRegions0.Dispose();
                ho_SelectedRegionsStd0.Dispose();
                ho_RegionIntersection0.Dispose();
                ho_ImageRotate0.Dispose();
                ho_ImageEmphasize01.Dispose();
                ho_ImageInvert01.Dispose();
                ho_ImageBinomial01.Dispose();
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
            ho_ImageBinomial01.Dispose();
            HOperatorSet.BinomialFilter(ho_ImageInvert01, out ho_ImageBinomial01, 5, 5);
            ho_Region01.Dispose();
            HOperatorSet.Threshold(ho_ImageBinomial01, out ho_Region01, 80, 255);
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
                hv_ErrorCount = 10;
                ho_Image.Dispose();
                ho_ImageEmphasize0.Dispose();
                ho_ImageInvert0.Dispose();
                ho_Region0.Dispose();
                ho_RegionClosing0.Dispose();
                ho_ConnectedRegions0.Dispose();
                ho_SelectedRegions0.Dispose();
                ho_SelectedRegionsStd0.Dispose();
                ho_RegionIntersection0.Dispose();
                ho_ImageRotate0.Dispose();
                ho_ImageEmphasize01.Dispose();
                ho_ImageInvert01.Dispose();
                ho_ImageBinomial01.Dispose();
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
                hv_ErrorCount = 1;
                ho_Image.Dispose();
                ho_ImageEmphasize0.Dispose();
                ho_ImageInvert0.Dispose();
                ho_Region0.Dispose();
                ho_RegionClosing0.Dispose();
                ho_ConnectedRegions0.Dispose();
                ho_SelectedRegions0.Dispose();
                ho_SelectedRegionsStd0.Dispose();
                ho_RegionIntersection0.Dispose();
                ho_ImageRotate0.Dispose();
                ho_ImageEmphasize01.Dispose();
                ho_ImageInvert01.Dispose();
                ho_ImageBinomial01.Dispose();
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
            hv_AreaPrior = new HTuple();
            hv_AreaPrior[0] = 130;
            hv_AreaPrior[1] = 78;
            hv_AreaPrior[2] = 100;
            hv_AreaPrior[3] = 90;
            hv_AreaPrior[4] = 90;
            hv_AreaPrior[5] = 105;
            hv_AreaPrior[6] = 115;
            hv_AreaPrior[7] = 78;
            hv_AreaPrior[8] = 128;
            hv_AreaPrior[9] = 125;
            hv_AreaDelta = 0.3;
            for (hv_Index1 = 0; hv_Index1.Continue(hv_Number1 - 1, 1); hv_Index1 = hv_Index1.TupleAdd(1))
            {
                if ((int)(new HTuple(((hv_Area11.TupleSelect(hv_Index1))).TupleLess((hv_AreaPrior.TupleSelect(
                    hv_KnownStr.TupleSelect(hv_Index1))) * (1 - hv_AreaDelta)))) != 0)
                {
                    hv_ErrorCount = 1;
                    ho_Image.Dispose();
                    ho_ImageEmphasize0.Dispose();
                    ho_ImageInvert0.Dispose();
                    ho_Region0.Dispose();
                    ho_RegionClosing0.Dispose();
                    ho_ConnectedRegions0.Dispose();
                    ho_SelectedRegions0.Dispose();
                    ho_SelectedRegionsStd0.Dispose();
                    ho_RegionIntersection0.Dispose();
                    ho_ImageRotate0.Dispose();
                    ho_ImageEmphasize01.Dispose();
                    ho_ImageInvert01.Dispose();
                    ho_ImageBinomial01.Dispose();
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
                hv_ErrorCount = 2;
                ho_Image.Dispose();
                ho_ImageEmphasize0.Dispose();
                ho_ImageInvert0.Dispose();
                ho_Region0.Dispose();
                ho_RegionClosing0.Dispose();
                ho_ConnectedRegions0.Dispose();
                ho_SelectedRegions0.Dispose();
                ho_SelectedRegionsStd0.Dispose();
                ho_RegionIntersection0.Dispose();
                ho_ImageRotate0.Dispose();
                ho_ImageEmphasize01.Dispose();
                ho_ImageInvert01.Dispose();
                ho_ImageBinomial01.Dispose();
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
            for (hv_Index2 = 0; hv_Index2.Continue(hv_Number2 - 1, 1); hv_Index2 = hv_Index2.TupleAdd(1))
            {
                if ((int)(new HTuple(((hv_Area12.TupleSelect(hv_Index2))).TupleLess((hv_AreaPrior.TupleSelect(
                    hv_KnownStr.TupleSelect(hv_Index2 + 8))) * (1 - hv_AreaDelta)))) != 0)
                {
                    hv_ErrorCount = 2;
                    ho_Image.Dispose();
                    ho_ImageEmphasize0.Dispose();
                    ho_ImageInvert0.Dispose();
                    ho_Region0.Dispose();
                    ho_RegionClosing0.Dispose();
                    ho_ConnectedRegions0.Dispose();
                    ho_SelectedRegions0.Dispose();
                    ho_SelectedRegionsStd0.Dispose();
                    ho_RegionIntersection0.Dispose();
                    ho_ImageRotate0.Dispose();
                    ho_ImageEmphasize01.Dispose();
                    ho_ImageInvert01.Dispose();
                    ho_ImageBinomial01.Dispose();
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
            ho_ImageInvert0.Dispose();
            ho_Region0.Dispose();
            ho_RegionClosing0.Dispose();
            ho_ConnectedRegions0.Dispose();
            ho_SelectedRegions0.Dispose();
            ho_SelectedRegionsStd0.Dispose();
            ho_RegionIntersection0.Dispose();
            ho_ImageRotate0.Dispose();
            ho_ImageEmphasize01.Dispose();
            ho_ImageInvert01.Dispose();
            ho_ImageBinomial01.Dispose();
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

            for (byte i = 0; i < hv_Area11.Length; i++)
            {
                hv_Area11[i] = hv_Area11.TupleSelect(i) * 100 / hv_AreaPrior.TupleSelect(hv_KnownStr.TupleSelect(i));
            }
            for (byte i = 0; i < 7; i++)
            {
                hv_Area12[i] = hv_Area12.TupleSelect(i) * 100 / hv_AreaPrior.TupleSelect(hv_KnownStr.TupleSelect(hv_Area11.Length + i));
            }
            ShowStr1 = hv_Area11;
            ShowStr2 = hv_Area12;
        }


    }
}
