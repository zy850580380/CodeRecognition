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
            DateTime beforDT = System.DateTime.Now;
            HDevelopExport HD = new HDevelopExport(hWindowControl1.HalconWindow, ImagePath);
            DateTime afterDT = System.DateTime.Now;
            TimeSpan ts = afterDT.Subtract(beforDT);
            if (HD.RecognitionStr1.TupleLength() > 0)
            {
                textBox1.Text = HD.RecognitionStr1.ToString().Replace(";", "");
                textBox2.Text = "合格： " + ts.TotalMilliseconds.ToString() + "ms";
            }
            else
            {
                textBox2.Text = "不合格";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            openFileDialog1.ShowDialog();
            ImagePath = openFileDialog1.FileName;
            label1.Text = ImagePath;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }

    public partial class HDevelopExport
    {
#if !NO_EXPORT_APP_MAIN
        public HDevelopExport(HTuple HDWindow, string ImagePath_)
        {
            // Default settings used in HDevelop 
            HOperatorSet.SetSystem("do_low_error", "false");
            action(HDWindow, ImagePath_);
        }
#endif
        public HTuple RecognitionStr1 = new HTuple(), RecognitionStr2 = new HTuple();
        public HTuple errorCounts;
        private void action(HTuple HDWindow_, string imgPath)
        {
            HDevWindowStack.Push(HDWindow_);
            // Local iconic variables 

            HObject ho_Image, ho_DotImage0, ho_Region0;
            HObject ho_RegionClosing0, ho_RegionClosing001, ho_RegionClosing002;
            HObject ho_RegionFillUp0, ho_ConnectedRegions0, ho_SelectedRegions0;
            HObject ho_SelectedRegionsStd0, ho_RegionIntersection0;
            HObject ho_Rectangle0, ho_ImageReduced0, ho_ImagePart0;
            HObject ho_ImageReduced1, ho_ImagePart1, ho_ImageRotate0;
            HObject ho_ImageRotate1, ho_DotImage2, ho_ImageMean2, ho_RegionDynThresh2;
            HObject ho_Rectangle2, ho_RegionClosing2, ho_Rectangle21;
            HObject ho_RegionClosing21, ho_RegionClosing22, ho_ConnectedRegions1;
            HObject ho_RegionTrans1, ho_Partitioned1, ho_SelectedRegions1;
            HObject ho_RegionIntersection1, ho_SortedRegions1;


            // Local control variables 

            HTuple hv_imgPath = new HTuple(), hv_Width0;
            HTuple hv_Height0, hv_Number0, hv_errorCounts, hv_Row0;
            HTuple hv_Column0, hv_Phi0, hv_Length01, hv_Length02, hv_Pointer;
            HTuple hv_Type, hv_Width, hv_Height, hv_HomMat2DIdentity;
            HTuple hv_HomMat2DRotate, hv_Number1, hv_FontName, hv_OCRHandle1;
            HTuple hv_RecNum1, hv_Confidence1;

            // Initialize local and output iconic variables 
            HOperatorSet.GenEmptyObj(out ho_Image);
            HOperatorSet.GenEmptyObj(out ho_DotImage0);
            HOperatorSet.GenEmptyObj(out ho_Region0);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing0);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing001);
            HOperatorSet.GenEmptyObj(out ho_RegionClosing002);
            HOperatorSet.GenEmptyObj(out ho_RegionFillUp0);
            HOperatorSet.GenEmptyObj(out ho_ConnectedRegions0);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegions0);
            HOperatorSet.GenEmptyObj(out ho_SelectedRegionsStd0);
            HOperatorSet.GenEmptyObj(out ho_RegionIntersection0);
            HOperatorSet.GenEmptyObj(out ho_Rectangle0);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced0);
            HOperatorSet.GenEmptyObj(out ho_ImagePart0);
            HOperatorSet.GenEmptyObj(out ho_ImageReduced1);
            HOperatorSet.GenEmptyObj(out ho_ImagePart1);
            HOperatorSet.GenEmptyObj(out ho_ImageRotate0);
            HOperatorSet.GenEmptyObj(out ho_ImageRotate1);
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

            ho_Image.Dispose();
            HOperatorSet.ReadImage(out ho_Image, imgPath);
            HOperatorSet.GetImageSize(ho_Image, out hv_Width0, out hv_Height0);
            ho_DotImage0.Dispose();
            HOperatorSet.DotsImage(ho_Image, out ho_DotImage0, 3, "dark", 2);
            ho_Region0.Dispose();
            HOperatorSet.Threshold(ho_DotImage0, out ho_Region0, 150, 255);
            ho_RegionClosing0.Dispose();
            HOperatorSet.ClosingRectangle1(ho_Region0, out ho_RegionClosing0, 10, 10);
            ho_RegionClosing001.Dispose();
            HOperatorSet.ClosingCircle(ho_RegionClosing0, out ho_RegionClosing001, 7.5);
            ho_RegionClosing002.Dispose();
            HOperatorSet.ClosingRectangle1(ho_RegionClosing001, out ho_RegionClosing002,
                20, 1);
            ho_RegionFillUp0.Dispose();
            HOperatorSet.FillUp(ho_RegionClosing002, out ho_RegionFillUp0);
            ho_ConnectedRegions0.Dispose();
            HOperatorSet.Connection(ho_RegionFillUp0, out ho_ConnectedRegions0);
            ho_SelectedRegions0.Dispose();
            HOperatorSet.SelectShape(ho_ConnectedRegions0, out ho_SelectedRegions0, ((new HTuple("height")).TupleConcat(
                "width")).TupleConcat("area"), "and", ((new HTuple(25)).TupleConcat(360)).TupleConcat(
                10000), ((new HTuple(60)).TupleConcat(440)).TupleConcat(24000));
            ho_SelectedRegionsStd0.Dispose();
            HOperatorSet.SelectShapeStd(ho_SelectedRegions0, out ho_SelectedRegionsStd0,
                "max_area", 70);
            HOperatorSet.CountObj(ho_SelectedRegionsStd0, out hv_Number0);
            hv_errorCounts = 0;
            if ((int)(new HTuple(hv_Number0.TupleNotEqual(1))) != 0)
            {
                hv_errorCounts = 10;
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
                    HOperatorSet.DispObj(ho_Image, HDevWindowStack.GetActive());
                }
                ho_Image.Dispose();
                ho_DotImage0.Dispose();
                ho_Region0.Dispose();
                ho_RegionClosing0.Dispose();
                ho_RegionClosing001.Dispose();
                ho_RegionClosing002.Dispose();
                ho_RegionFillUp0.Dispose();
                ho_ConnectedRegions0.Dispose();
                ho_SelectedRegions0.Dispose();
                ho_SelectedRegionsStd0.Dispose();
                ho_RegionIntersection0.Dispose();
                ho_Rectangle0.Dispose();
                ho_ImageReduced0.Dispose();
                ho_ImagePart0.Dispose();
                ho_ImageReduced1.Dispose();
                ho_ImagePart1.Dispose();
                ho_ImageRotate0.Dispose();
                ho_ImageRotate1.Dispose();
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

                return;
            }
            ho_RegionIntersection0.Dispose();
            HOperatorSet.Intersection(ho_SelectedRegionsStd0, ho_Region0, out ho_RegionIntersection0
                );
            //鏃嬭浆鍥惧儚
            HOperatorSet.SmallestRectangle2(ho_RegionIntersection0, out hv_Row0, out hv_Column0,
                out hv_Phi0, out hv_Length01, out hv_Length02);
            ho_Rectangle0.Dispose();
            HOperatorSet.GenRectangle2(out ho_Rectangle0, hv_Row0, hv_Column0 - 1, hv_Phi0,
                hv_Length01 + 4, hv_Length02 + 4);

            ho_ImageReduced0.Dispose();
            HOperatorSet.ReduceDomain(ho_Image, ho_Rectangle0, out ho_ImageReduced0);
            ho_ImagePart0.Dispose();
            HOperatorSet.CropDomain(ho_ImageReduced0, out ho_ImagePart0);

            ho_ImageReduced1.Dispose();
            HOperatorSet.ReduceDomain(ho_DotImage0, ho_Rectangle0, out ho_ImageReduced1);
            ho_ImagePart1.Dispose();
            HOperatorSet.CropDomain(ho_ImageReduced1, out ho_ImagePart1);

            HOperatorSet.GetImagePointer1(ho_ImagePart1, out hv_Pointer, out hv_Type, out hv_Width,
                out hv_Height);
            HOperatorSet.HomMat2dIdentity(out hv_HomMat2DIdentity);
            HOperatorSet.HomMat2dRotate(hv_HomMat2DIdentity, -hv_Phi0, hv_Width / 2, hv_Height / 2,
                out hv_HomMat2DRotate);
            ho_ImageRotate0.Dispose();
            HOperatorSet.AffineTransImage(ho_ImagePart0, out ho_ImageRotate0, hv_HomMat2DRotate,
                "nearest_neighbor", "true");
            ho_ImageRotate1.Dispose();
            HOperatorSet.AffineTransImage(ho_ImagePart1, out ho_ImageRotate1, hv_HomMat2DRotate,
                "nearest_neighbor", "true");
            //澶勭悊鏃嬭浆鍚庣殑鍥惧儚
            ho_DotImage2.Dispose();
            HOperatorSet.DotsImage(ho_ImageRotate1, out ho_DotImage2, 3, "light", 2);
            ho_ImageMean2.Dispose();
            HOperatorSet.MeanImage(ho_DotImage2, out ho_ImageMean2, 7, 7);
            ho_RegionDynThresh2.Dispose();
            HOperatorSet.DynThreshold(ho_DotImage2, ho_ImageMean2, out ho_RegionDynThresh2,
                60, "light");
            //褰㈡€佸澶勭悊
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
                50));
            ho_RegionIntersection1.Dispose();
            HOperatorSet.Intersection(ho_SelectedRegions1, ho_RegionDynThresh2, out ho_RegionIntersection1
                );
            ho_SortedRegions1.Dispose();
            HOperatorSet.SortRegion(ho_RegionIntersection1, out ho_SortedRegions1, "first_point",
                "true", "column");
            HOperatorSet.CountObj(ho_SortedRegions1, out hv_Number1);
            if ((int)(new HTuple(hv_Number1.TupleNotEqual(16))) != 0)
            {
                hv_errorCounts = 1;
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
                    HOperatorSet.DispObj(ho_Image, HDevWindowStack.GetActive());
                }
                ho_Image.Dispose();
                ho_DotImage0.Dispose();
                ho_Region0.Dispose();
                ho_RegionClosing0.Dispose();
                ho_RegionClosing001.Dispose();
                ho_RegionClosing002.Dispose();
                ho_RegionFillUp0.Dispose();
                ho_ConnectedRegions0.Dispose();
                ho_SelectedRegions0.Dispose();
                ho_SelectedRegionsStd0.Dispose();
                ho_RegionIntersection0.Dispose();
                ho_Rectangle0.Dispose();
                ho_ImageReduced0.Dispose();
                ho_ImagePart0.Dispose();
                ho_ImageReduced1.Dispose();
                ho_ImagePart1.Dispose();
                ho_ImageRotate0.Dispose();
                ho_ImageRotate1.Dispose();
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

                return;
            }

            hv_FontName = "D:/github/CodeRecognition/MengniuWords.omc";
            HOperatorSet.ReadOcrClassMlp(hv_FontName, out hv_OCRHandle1);
            HOperatorSet.DoOcrMultiClassMlp(ho_SortedRegions1, ho_ImageRotate0, hv_OCRHandle1,
                out hv_RecNum1, out hv_Confidence1);

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
            ho_RegionClosing0.Dispose();
            ho_RegionClosing001.Dispose();
            ho_RegionClosing002.Dispose();
            ho_RegionFillUp0.Dispose();
            ho_ConnectedRegions0.Dispose();
            ho_SelectedRegions0.Dispose();
            ho_SelectedRegionsStd0.Dispose();
            ho_RegionIntersection0.Dispose();
            ho_Rectangle0.Dispose();
            ho_ImageReduced0.Dispose();
            ho_ImagePart0.Dispose();
            ho_ImageReduced1.Dispose();
            ho_ImagePart1.Dispose();
            ho_ImageRotate0.Dispose();
            ho_ImageRotate1.Dispose();
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

            errorCounts = hv_errorCounts;
            RecognitionStr1 = hv_RecNum1;
        }
    }
}
