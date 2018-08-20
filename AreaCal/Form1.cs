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
            HD.ProcessImage(hWindowControl1.HalconWindow, ImagePath);
            if (HD.hv_OkFlag)
            {
                label1.Text = "合格";
                textBox1.Text = HD.ShowStr1.ToString().Replace(";", "");
                textBox2.Text = HD.ShowStr2.ToString().Replace(";", "");
            }
            else
            {
                label1.Text = "不合格";
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
        public HTuple hv_OkFlag = 0;
        // Main procedure 
        public HTuple ShowStr1 = new HTuple(), ShowStr2 = new HTuple();
        HTuple HDWindow;
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
            HDWindow = HDWindow_;
            ImageFile = ImageFile_;
            action();
        }

        private void action()
        {

            // Local iconic variables 

            HObject ho_Image, ho_ImageEmphasize0, ho_ImageBinomial0;
            HObject ho_ImageInvert0, ho_Region0, ho_RegionClosing0;
            HObject ho_ConnectedRegions0, ho_SelectedRegions0, ho_SelectedRegionsStd0;
            HObject ho_RegionIntersection0, ho_ImageRotate0, ho_ImageEmphasize01;
            HObject ho_ImageBinomia0l, ho_ImageInvert01, ho_Region01;
            HObject ho_RegionClosing01, ho_RegionOpening01, ho_ConnectedRegions01;
            HObject ho_SelectedRegions01, ho_RegionIntersection01, ho_Rectangle01;
            HObject ho_ImageReduced01, ho_Region02, ho_ImageMean01;
            HObject ho_RegionClosing02, ho_RegionOpening02, ho_ConnectedRegions02;
            HObject ho_SelectedRegions1, ho_RegionIntersection1, ho_RegionClosing1;
            HObject ho_RegionClosing11, ho_ConnectedRegions1, ho_RegionTrans1;
            HObject ho_Partitioned1, ho_RegionIntersection11, ho_SortedRegions1;
            HObject ho_SelectedRegions2, ho_RegionIntersection2, ho_RegionClosing2;
            HObject ho_RegionClosing21, ho_ConnectedRegions2, ho_RegionTrans2;
            HObject ho_Partitioned2, ho_RegionIntersection21, ho_SortedRegions2;


            // Local control variables 

            HTuple hv_Number0, hv_Row0, hv_Column0, hv_Phi0;
            HTuple hv_Length01, hv_Length02, hv_Deg0, hv_Number01;
            HTuple hv_Row01, hv_Column01, hv_Phi01, hv_Length011, hv_Length012;
            HTuple hv_RegionPriorWidth, hv_RegionPriorHeight, hv_Number1;
            HTuple hv_Area11, hv_Row11, hv_Column11, hv_KnownStr, hv_AreaPrior;
            HTuple hv_AreaDelta, hv_ErrorCount, hv_Index1, hv_Number2;
            HTuple hv_Area12, hv_Row12, hv_Column12, hv_Index2;

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Image);
            HOperatorSet.GenEmptyObj(out ho_ImageEmphasize0);
            HOperatorSet.GenEmptyObj(out ho_ImageBinomial0);
            HOperatorSet.GenEmptyObj(out ho_ImageInvert0);
            HOperatorSet.GenEmptyObj(out ho_Region0);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing0);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions0);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions0);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegionsStd0);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection0);
            HOperatorSet.GenEmptyObj(out ho_ImageRotate0);
            HOperatorSet.GenEmptyObj(out ho_ImageEmphasize01);
            HOperatorSet.GenEmptyObj(out ho_ImageBinomia0l);
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
            HOperatorSet.GenEmptyObj(out ho_ImageMean01);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing02);
            HOperatorSet.GenEmptyObj(out ho_RegionOpening02);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions02);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection1);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing1);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing11);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions1);
            HOperatorSet.GenEmptyObj(out ho_RegionTrans1);
            HOperatorSet.GenEmptyObj(out ho_Partitioned1);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection11);
            HOperatorSet.GenEmptyObj(out ho_SortedRegions1);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions2);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection2);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing2);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing21);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions2);
            HOperatorSet.GenEmptyObj(out ho_RegionTrans2);
            HOperatorSet.GenEmptyObj(out ho_Partitioned2);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection21);
            HOperatorSet.GenEmptyObj(out ho_SortedRegions2);

            ho_Image.Dispose();
            HOperatorSet.ReadImage(out ho_Image, ImageFile);
            ho_ImageEmphasize0.Dispose();
            HOperatorSet.Emphasize(ho_Image, out ho_ImageEmphasize0, 7, 7, 5);
            ho_ImageBinomial0.Dispose();
            HOperatorSet.BinomialFilter(ho_ImageEmphasize0, out ho_ImageBinomial0, 9, 9);
            ho_ImageInvert0.Dispose();
            HOperatorSet.InvertImage(ho_ImageBinomial0, out ho_ImageInvert0);
            //mean_image (ImageInvert, ImageMean, 5, 5)
            //dyn_threshold (ImageInvert, ImageMean, Region, 80, 'light')
            ho_Region0.Dispose();
            HOperatorSet.Threshold(ho_ImageInvert0, out ho_Region0, 135, 255);
            ho_RegionClosing0.Dispose();
            HOperatorSet.ClosingCircle(ho_Region0, out ho_RegionClosing0, 10);
            ho_ConnectedRegions0.Dispose();
            HOperatorSet.Connection(ho_RegionClosing0, out ho_ConnectedRegions0);
            ho_SelectedRegions0.Dispose();
            HOperatorSet.SelectShape(ho_ConnectedRegions0, out ho_SelectedRegions0, (new HTuple("width")).TupleConcat(
                "height"), "and", (new HTuple(90)).TupleConcat(40), (new HTuple(180)).TupleConcat(
                140));
            ho_SelectedRegionsStd0.Dispose();
            HOperatorSet.SelectShapeStd(ho_SelectedRegions0, out ho_SelectedRegionsStd0,
                "max_area", 70);
            HOperatorSet.CountObj(ho_SelectedRegionsStd0, out hv_Number0);
            if ((int)(new HTuple(hv_Number0.TupleNotEqual(1))) != 0)
            {
                ho_Image.Dispose();
                ho_ImageEmphasize0.Dispose();
                ho_ImageBinomial0.Dispose();
                ho_ImageInvert0.Dispose();
                ho_Region0.Dispose();
                ho_RegionClosing0.Dispose();
                ho_ConnectedRegions0.Dispose();
                ho_SelectedRegions0.Dispose();
                ho_SelectedRegionsStd0.Dispose();
                ho_RegionIntersection0.Dispose();
                ho_ImageRotate0.Dispose();
                ho_ImageEmphasize01.Dispose();
                ho_ImageBinomia0l.Dispose();
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
                ho_ImageMean01.Dispose();
                ho_RegionClosing02.Dispose();
                ho_RegionOpening02.Dispose();
                ho_ConnectedRegions02.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_RegionIntersection1.Dispose();
                ho_RegionClosing1.Dispose();
                ho_RegionClosing11.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_RegionTrans1.Dispose();
                ho_Partitioned1.Dispose();
                ho_RegionIntersection11.Dispose();
                ho_SortedRegions1.Dispose();
                ho_SelectedRegions2.Dispose();
                ho_RegionIntersection2.Dispose();
                ho_RegionClosing2.Dispose();
                ho_RegionClosing21.Dispose();
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
            HOperatorSet.RotateImage(ho_Image, out ho_ImageRotate0, -hv_Deg0, "constant");
            //处理旋转后的图像
            ho_ImageEmphasize01.Dispose();
            HOperatorSet.Emphasize(ho_ImageRotate0, out ho_ImageEmphasize01, 7, 7, 5);
            ho_ImageBinomia0l.Dispose();
            HOperatorSet.BinomialFilter(ho_ImageEmphasize01, out ho_ImageBinomia0l, 9, 9);
            ho_ImageInvert01.Dispose();
            HOperatorSet.InvertImage(ho_ImageBinomia0l, out ho_ImageInvert01);
            //mean_image (ImageRotate, ImageMean, 3, 3)
            //dyn_threshold (ImageInvert, ImageMean, Region, 30, 'light')
            ho_Region01.Dispose();
            HOperatorSet.Threshold(ho_ImageInvert01, out ho_Region01, 135, 255);
            ho_RegionClosing01.Dispose();
            HOperatorSet.ClosingCircle(ho_Region01, out ho_RegionClosing01, 10);
            ho_RegionOpening01.Dispose();
            HOperatorSet.OpeningCircle(ho_RegionClosing01, out ho_RegionOpening01, 5);
            ho_ConnectedRegions01.Dispose();
            HOperatorSet.Connection(ho_RegionOpening01, out ho_ConnectedRegions01);
            ho_SelectedRegions01.Dispose();
            HOperatorSet.SelectShape(ho_ConnectedRegions01, out ho_SelectedRegions01, (new HTuple("width")).TupleConcat(
                "height"), "and", (new HTuple(70)).TupleConcat(30), (new HTuple(160)).TupleConcat(
                110));
            HOperatorSet.CountObj(ho_SelectedRegions01, out hv_Number01);
            if ((int)(new HTuple(hv_Number01.TupleNotEqual(1))) != 0)
            {
                ho_Image.Dispose();
                ho_ImageEmphasize0.Dispose();
                ho_ImageBinomial0.Dispose();
                ho_ImageInvert0.Dispose();
                ho_Region0.Dispose();
                ho_RegionClosing0.Dispose();
                ho_ConnectedRegions0.Dispose();
                ho_SelectedRegions0.Dispose();
                ho_SelectedRegionsStd0.Dispose();
                ho_RegionIntersection0.Dispose();
                ho_ImageRotate0.Dispose();
                ho_ImageEmphasize01.Dispose();
                ho_ImageBinomia0l.Dispose();
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
                ho_ImageMean01.Dispose();
                ho_RegionClosing02.Dispose();
                ho_RegionOpening02.Dispose();
                ho_ConnectedRegions02.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_RegionIntersection1.Dispose();
                ho_RegionClosing1.Dispose();
                ho_RegionClosing11.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_RegionTrans1.Dispose();
                ho_Partitioned1.Dispose();
                ho_RegionIntersection11.Dispose();
                ho_SortedRegions1.Dispose();
                ho_SelectedRegions2.Dispose();
                ho_RegionIntersection2.Dispose();
                ho_RegionClosing2.Dispose();
                ho_RegionClosing21.Dispose();
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
            hv_RegionPriorWidth = 74;
            hv_RegionPriorHeight = 30;
            //62,26
            ho_Rectangle01.Dispose();
            HOperatorSet.GenRectangle2(out ho_Rectangle01, hv_Row01 - 1, (hv_Column01 + (hv_Length011 / 2)) - (hv_RegionPriorWidth / 2),
                0, hv_RegionPriorWidth, hv_RegionPriorHeight);
            //缩小区域
            ho_ImageReduced01.Dispose();
            HOperatorSet.ReduceDomain(ho_ImageInvert01, ho_Rectangle01, out ho_ImageReduced01
                );
            //threshold (ImageReduced01, Region02, 110, 255)
            ho_ImageMean01.Dispose();
            HOperatorSet.MeanImage(ho_ImageReduced01, out ho_ImageMean01, 3, 3);
            ho_Region02.Dispose();
            HOperatorSet.DynThreshold(ho_ImageReduced01, ho_ImageMean01, out ho_Region02,
                8, "light");
            ho_RegionClosing02.Dispose();
            HOperatorSet.ClosingRectangle1(ho_Region02, out ho_RegionClosing02, 30, 1);
            ho_RegionOpening02.Dispose();
            HOperatorSet.OpeningRectangle1(ho_RegionClosing02, out ho_RegionOpening02, 5,
                1);
            ho_ConnectedRegions02.Dispose();
            HOperatorSet.Connection(ho_RegionOpening02, out ho_ConnectedRegions02);
            //第一行***********************
            ho_SelectedRegions1.Dispose();
            HOperatorSet.SelectShape(ho_ConnectedRegions02, out ho_SelectedRegions1, ((new HTuple("width")).TupleConcat(
                "height")).TupleConcat("row"), "and", ((new HTuple(40)).TupleConcat(18)).TupleConcat(
                hv_Row01 - 20), ((new HTuple(160)).TupleConcat(35)).TupleConcat(hv_Row01));
            ho_RegionIntersection1.Dispose();
            HOperatorSet.Intersection(ho_SelectedRegions1, ho_Region02, out ho_RegionIntersection1
                );
            ho_RegionClosing1.Dispose();
            HOperatorSet.ClosingRectangle1(ho_RegionIntersection1, out ho_RegionClosing1,
                2, 50);
            ho_RegionClosing11.Dispose();
            HOperatorSet.ClosingCircle(ho_RegionClosing1, out ho_RegionClosing11, 2);
            ho_ConnectedRegions1.Dispose();
            HOperatorSet.Connection(ho_RegionClosing11, out ho_ConnectedRegions1);
            ho_RegionTrans1.Dispose();
            HOperatorSet.ShapeTrans(ho_ConnectedRegions1, out ho_RegionTrans1, "rectangle1");
            ho_Partitioned1.Dispose();
            HOperatorSet.PartitionRectangle(ho_RegionTrans1, out ho_Partitioned1, 14, 32);
            ho_SelectedRegions1.Dispose();
            HOperatorSet.SelectShape(ho_Partitioned1, out ho_SelectedRegions1, ((new HTuple("area")).TupleConcat(
                "width")).TupleConcat("height"), "and", ((new HTuple(100)).TupleConcat(6)).TupleConcat(
                18), ((new HTuple(600)).TupleConcat(20)).TupleConcat(40));
            ho_RegionIntersection11.Dispose();
            HOperatorSet.Intersection(ho_SelectedRegions1, ho_Region02, out ho_RegionIntersection11
                );
            //选中字符
            //排序
            ho_SortedRegions1.Dispose();
            HOperatorSet.SortRegion(ho_RegionIntersection11, out ho_SortedRegions1, "first_point",
                "true", "column");
            HOperatorSet.CountObj(ho_SortedRegions1, out hv_Number1);
            if ((int)(new HTuple(hv_Number1.TupleNotEqual(8))) != 0)
            {
                ho_Image.Dispose();
                ho_ImageEmphasize0.Dispose();
                ho_ImageBinomial0.Dispose();
                ho_ImageInvert0.Dispose();
                ho_Region0.Dispose();
                ho_RegionClosing0.Dispose();
                ho_ConnectedRegions0.Dispose();
                ho_SelectedRegions0.Dispose();
                ho_SelectedRegionsStd0.Dispose();
                ho_RegionIntersection0.Dispose();
                ho_ImageRotate0.Dispose();
                ho_ImageEmphasize01.Dispose();
                ho_ImageBinomia0l.Dispose();
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
                ho_ImageMean01.Dispose();
                ho_RegionClosing02.Dispose();
                ho_RegionOpening02.Dispose();
                ho_ConnectedRegions02.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_RegionIntersection1.Dispose();
                ho_RegionClosing1.Dispose();
                ho_RegionClosing11.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_RegionTrans1.Dispose();
                ho_Partitioned1.Dispose();
                ho_RegionIntersection11.Dispose();
                ho_SortedRegions1.Dispose();
                ho_SelectedRegions2.Dispose();
                ho_RegionIntersection2.Dispose();
                ho_RegionClosing2.Dispose();
                ho_RegionClosing21.Dispose();
                ho_ConnectedRegions2.Dispose();
                ho_RegionTrans2.Dispose();
                ho_Partitioned2.Dispose();
                ho_RegionIntersection21.Dispose();
                ho_SortedRegions2.Dispose();

                return;
            }
            HOperatorSet.AreaCenter(ho_SortedRegions1, out hv_Area11, out hv_Row11, out hv_Column11);
            hv_KnownStr = new HTuple();
            hv_KnownStr[0] = 2;
            hv_KnownStr[1] = 0;
            hv_KnownStr[2] = 1;
            hv_KnownStr[3] = 7;
            hv_KnownStr[4] = 0;
            hv_KnownStr[5] = 8;
            hv_KnownStr[6] = 2;
            hv_KnownStr[7] = 5;
            hv_KnownStr[8] = 3;
            hv_KnownStr[9] = 6;
            hv_KnownStr[10] = 2;
            hv_KnownStr[11] = 1;
            hv_KnownStr[12] = 7;
            hv_KnownStr[13] = 1;
            hv_KnownStr[14] = 9;
            hv_AreaPrior = new HTuple();
            hv_AreaPrior[0] = 115;
            hv_AreaPrior[1] = 54;
            hv_AreaPrior[2] = 95;
            hv_AreaPrior[3] = 100;
            hv_AreaPrior[4] = 100;
            hv_AreaPrior[5] = 103;
            hv_AreaPrior[6] = 122;
            hv_AreaPrior[7] = 78;
            hv_AreaPrior[8] = 124;
            hv_AreaPrior[9] = 120;
            hv_AreaDelta = 20;
            hv_ErrorCount = 0;
            for (hv_Index1 = 0; hv_Index1.Continue(hv_Number1 - 1, 1); hv_Index1 = hv_Index1.TupleAdd(1))
            {
                if ((int)(new HTuple(((hv_Area11.TupleSelect(hv_Index1))).TupleLess((hv_AreaPrior.TupleSelect(
                    hv_KnownStr.TupleSelect(hv_Index1))) - hv_AreaDelta))) != 0)
                {
                    hv_ErrorCount = hv_ErrorCount + 1;
                    ho_Image.Dispose();
                    ho_ImageEmphasize0.Dispose();
                    ho_ImageBinomial0.Dispose();
                    ho_ImageInvert0.Dispose();
                    ho_Region0.Dispose();
                    ho_RegionClosing0.Dispose();
                    ho_ConnectedRegions0.Dispose();
                    ho_SelectedRegions0.Dispose();
                    ho_SelectedRegionsStd0.Dispose();
                    ho_RegionIntersection0.Dispose();
                    ho_ImageRotate0.Dispose();
                    ho_ImageEmphasize01.Dispose();
                    ho_ImageBinomia0l.Dispose();
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
                    ho_ImageMean01.Dispose();
                    ho_RegionClosing02.Dispose();
                    ho_RegionOpening02.Dispose();
                    ho_ConnectedRegions02.Dispose();
                    ho_SelectedRegions1.Dispose();
                    ho_RegionIntersection1.Dispose();
                    ho_RegionClosing1.Dispose();
                    ho_RegionClosing11.Dispose();
                    ho_ConnectedRegions1.Dispose();
                    ho_RegionTrans1.Dispose();
                    ho_Partitioned1.Dispose();
                    ho_RegionIntersection11.Dispose();
                    ho_SortedRegions1.Dispose();
                    ho_SelectedRegions2.Dispose();
                    ho_RegionIntersection2.Dispose();
                    ho_RegionClosing2.Dispose();
                    ho_RegionClosing21.Dispose();
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
                "height")).TupleConcat("row"), "and", ((new HTuple(40)).TupleConcat(18)).TupleConcat(
                hv_Row01), ((new HTuple(160)).TupleConcat(35)).TupleConcat(hv_Row01 + 16));
            ho_RegionIntersection2.Dispose();
            HOperatorSet.Intersection(ho_SelectedRegions2, ho_Region02, out ho_RegionIntersection2
                );
            ho_RegionClosing2.Dispose();
            HOperatorSet.ClosingRectangle1(ho_RegionIntersection2, out ho_RegionClosing2,
                2, 50);
            ho_RegionClosing21.Dispose();
            HOperatorSet.ClosingCircle(ho_RegionClosing2, out ho_RegionClosing21, 2);
            ho_ConnectedRegions2.Dispose();
            HOperatorSet.Connection(ho_RegionClosing21, out ho_ConnectedRegions2);
            ho_RegionTrans2.Dispose();
            HOperatorSet.ShapeTrans(ho_ConnectedRegions2, out ho_RegionTrans2, "rectangle1");
            ho_Partitioned2.Dispose();
            HOperatorSet.PartitionRectangle(ho_RegionTrans2, out ho_Partitioned2, 14, 32);
            ho_SelectedRegions2.Dispose();
            HOperatorSet.SelectShape(ho_Partitioned2, out ho_SelectedRegions2, ((new HTuple("area")).TupleConcat(
                "width")).TupleConcat("height"), "and", ((new HTuple(100)).TupleConcat(6)).TupleConcat(
                18), ((new HTuple(600)).TupleConcat(20)).TupleConcat(40));
            ho_RegionIntersection21.Dispose();
            HOperatorSet.Intersection(ho_SelectedRegions2, ho_Region02, out ho_RegionIntersection21
                );
            //选中字符
            //排序
            ho_SortedRegions2.Dispose();
            HOperatorSet.SortRegion(ho_RegionIntersection21, out ho_SortedRegions2, "first_point",
                "true", "column");
            HOperatorSet.CountObj(ho_SortedRegions2, out hv_Number2);
            if ((int)(new HTuple(hv_Number2.TupleNotEqual(7))) != 0)
            {
                ho_Image.Dispose();
                ho_ImageEmphasize0.Dispose();
                ho_ImageBinomial0.Dispose();
                ho_ImageInvert0.Dispose();
                ho_Region0.Dispose();
                ho_RegionClosing0.Dispose();
                ho_ConnectedRegions0.Dispose();
                ho_SelectedRegions0.Dispose();
                ho_SelectedRegionsStd0.Dispose();
                ho_RegionIntersection0.Dispose();
                ho_ImageRotate0.Dispose();
                ho_ImageEmphasize01.Dispose();
                ho_ImageBinomia0l.Dispose();
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
                ho_ImageMean01.Dispose();
                ho_RegionClosing02.Dispose();
                ho_RegionOpening02.Dispose();
                ho_ConnectedRegions02.Dispose();
                ho_SelectedRegions1.Dispose();
                ho_RegionIntersection1.Dispose();
                ho_RegionClosing1.Dispose();
                ho_RegionClosing11.Dispose();
                ho_ConnectedRegions1.Dispose();
                ho_RegionTrans1.Dispose();
                ho_Partitioned1.Dispose();
                ho_RegionIntersection11.Dispose();
                ho_SortedRegions1.Dispose();
                ho_SelectedRegions2.Dispose();
                ho_RegionIntersection2.Dispose();
                ho_RegionClosing2.Dispose();
                ho_RegionClosing21.Dispose();
                ho_ConnectedRegions2.Dispose();
                ho_RegionTrans2.Dispose();
                ho_Partitioned2.Dispose();
                ho_RegionIntersection21.Dispose();
                ho_SortedRegions2.Dispose();

                return;
            }
            HOperatorSet.AreaCenter(ho_SortedRegions2, out hv_Area12, out hv_Row12, out hv_Column12);
            for (hv_Index2 = 0; hv_Index2.Continue(hv_Number2 - 1, 1); hv_Index2 = hv_Index2.TupleAdd(1))
            {
                if ((int)(new HTuple(((hv_Area12.TupleSelect(hv_Index2))).TupleLess((hv_AreaPrior.TupleSelect(
                    hv_KnownStr.TupleSelect(hv_Index2 + 8))) - hv_AreaDelta))) != 0)
                {
                    hv_ErrorCount = hv_ErrorCount + 1;
                    ho_Image.Dispose();
                    ho_ImageEmphasize0.Dispose();
                    ho_ImageBinomial0.Dispose();
                    ho_ImageInvert0.Dispose();
                    ho_Region0.Dispose();
                    ho_RegionClosing0.Dispose();
                    ho_ConnectedRegions0.Dispose();
                    ho_SelectedRegions0.Dispose();
                    ho_SelectedRegionsStd0.Dispose();
                    ho_RegionIntersection0.Dispose();
                    ho_ImageRotate0.Dispose();
                    ho_ImageEmphasize01.Dispose();
                    ho_ImageBinomia0l.Dispose();
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
                    ho_ImageMean01.Dispose();
                    ho_RegionClosing02.Dispose();
                    ho_RegionOpening02.Dispose();
                    ho_ConnectedRegions02.Dispose();
                    ho_SelectedRegions1.Dispose();
                    ho_RegionIntersection1.Dispose();
                    ho_RegionClosing1.Dispose();
                    ho_RegionClosing11.Dispose();
                    ho_ConnectedRegions1.Dispose();
                    ho_RegionTrans1.Dispose();
                    ho_Partitioned1.Dispose();
                    ho_RegionIntersection11.Dispose();
                    ho_SortedRegions1.Dispose();
                    ho_SelectedRegions2.Dispose();
                    ho_RegionIntersection2.Dispose();
                    ho_RegionClosing2.Dispose();
                    ho_RegionClosing21.Dispose();
                    ho_ConnectedRegions2.Dispose();
                    ho_RegionTrans2.Dispose();
                    ho_Partitioned2.Dispose();
                    ho_RegionIntersection21.Dispose();
                    ho_SortedRegions2.Dispose();

                    return;
                }
            }
            HDevWindowStack.Push(HDWindow);
            if (HDevWindowStack.IsOpen())
            {
                HOperatorSet.SetColor(HDevWindowStack.GetActive(), "white");
                HOperatorSet.ClearWindow(HDevWindowStack.GetActive());
                HOperatorSet.DispObj(ho_SortedRegions1, HDevWindowStack.GetActive());
                HOperatorSet.DispObj(ho_SortedRegions2, HDevWindowStack.GetActive());
            }
            ho_Image.Dispose();
            ho_ImageEmphasize0.Dispose();
            ho_ImageBinomial0.Dispose();
            ho_ImageInvert0.Dispose();
            ho_Region0.Dispose();
            ho_RegionClosing0.Dispose();
            ho_ConnectedRegions0.Dispose();
            ho_SelectedRegions0.Dispose();
            ho_SelectedRegionsStd0.Dispose();
            ho_RegionIntersection0.Dispose();
            ho_ImageRotate0.Dispose();
            ho_ImageEmphasize01.Dispose();
            ho_ImageBinomia0l.Dispose();
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
            ho_ImageMean01.Dispose();
            ho_RegionClosing02.Dispose();
            ho_RegionOpening02.Dispose();
            ho_ConnectedRegions02.Dispose();
            ho_SelectedRegions1.Dispose();
            ho_RegionIntersection1.Dispose();
            ho_RegionClosing1.Dispose();
            ho_RegionClosing11.Dispose();
            ho_ConnectedRegions1.Dispose();
            ho_RegionTrans1.Dispose();
            ho_Partitioned1.Dispose();
            ho_RegionIntersection11.Dispose();
            ho_SortedRegions1.Dispose();
            ho_SelectedRegions2.Dispose();
            ho_RegionIntersection2.Dispose();
            ho_RegionClosing2.Dispose();
            ho_RegionClosing21.Dispose();
            ho_ConnectedRegions2.Dispose();
            ho_RegionTrans2.Dispose();
            ho_Partitioned2.Dispose();
            ho_RegionIntersection21.Dispose();
            ho_SortedRegions2.Dispose();

            ShowStr1 = hv_Area11;
            ShowStr2 = hv_Area12;
            hv_OkFlag = 1;
        }


    }
}
