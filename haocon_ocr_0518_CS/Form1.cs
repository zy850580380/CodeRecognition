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
            textBox2.Text = "";
            textBox1.Text = "";
            DateTime beforDT = System.DateTime.Now;
            HDevelopExport HD = new HDevelopExport(hWindowControl1.HalconWindow, ImagePath);
            DateTime afterDT = System.DateTime.Now;
            TimeSpan ts = afterDT.Subtract(beforDT);
            if (HD.RecognitionStr1.TupleLength() > 0)
            {
                textBox1.Text = HD.RecognitionStr1.ToString().Replace(";", "");
                string str = HD.RecognitionStr2.ToString().Replace(";", "");
                for (int i = 0; i < 12; i++)
                {
                    textBox2.Text += str[i].ToString();
                }
                textBox2.Text += "d";
                label1.Text = "合格： " + ts.TotalMilliseconds.ToString() + "ms";
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

            HObject ho_Image, ho_DotImage, ho_Region, ho_RegionDilation;
            HObject ho_RegionClosing, ho_ConnectedRegions, ho_SelectedRegions;
            HObject ho_RegionUnion, ho_RegionClosing2, ho_ConnectedRegions2;
            HObject ho_SelectedRegions5, ho_RegionIntersection, ho_RegionAffineTrans1;
            HObject ho_rotateImage, ho_RegionDilation1, ho_RegionClosing1;
            HObject ho_RegionOpening, ho_ConnectedRegions1, ho_SelectedRegions1;
            HObject ho_RegionTrans, ho_Partitioned, ho_SelectedRegions2;
            HObject ho_RegionIntersection1, ho_SortedRegions1, ho_SelectedRegions3;
            HObject ho_RegionTrans1, ho_Partitioned1, ho_SelectedRegions4;
            HObject ho_RegionIntersection2, ho_SortedRegions2;


            // Local control variables 

            HTuple hv_Row, hv_Column, hv_Phi, hv_Length1;
            HTuple hv_Length2, hv_HomMat2DIdentity, hv_HomMat2DRotate;
            HTuple hv_Area, hv_Row1, hv_Column1, hv_Area11, hv_Row11;
            HTuple hv_Column11, hv_Number1, hv_OCRHandle, hv_Chars1;
            HTuple hv_Confidence1, hv_Area12, hv_Row12, hv_Column12;
            HTuple hv_Number2, hv_Chars2, hv_Confidence2;

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
            HOperatorSet.GenEmptyObj(out ho_rotateImage);
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
            HOperatorSet.ReadImage(out ho_Image, imgPath);
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
            ho_rotateImage.Dispose();
            HOperatorSet.AffineTransImage(ho_Image, out ho_rotateImage, hv_HomMat2DRotate,
                "bilinear", "false");
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
            HOperatorSet.CountObj(ho_SortedRegions1, out hv_Number1);
            //FontFile := 'D:/github/CodeRecognition/tzb1112.omc'
            HOperatorSet.ReadOcrClassMlp("C:/Program Files/MVTec/HALCON-10.0/ocr/DotPrint_0-9A-Z.omc",
                out hv_OCRHandle);
            HOperatorSet.DoOcrMultiClassMlp(ho_SortedRegions1, ho_rotateImage, hv_OCRHandle,
                out hv_Chars1, out hv_Confidence1);

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
            HOperatorSet.CountObj(ho_SortedRegions2, out hv_Number2);
            HOperatorSet.DoOcrMultiClassMlp(ho_SortedRegions2, ho_rotateImage, hv_OCRHandle,
                out hv_Chars2, out hv_Confidence2);

            //word := ['d']
            //TrainFile := 'D:/github/CodeRecognition/tzb1112.trf'
            //dev_set_check ('~give_error')
            //delete_file (TrainFile)
            //dev_set_check ('~give_error')
            //for i := 7 to Number2 by 1
            //select_obj (SortedRegions2, SingleWord, i)
            //append_ocr_trainf (SingleWord, rotateImage, word, TrainFile)
            //endfor
            //read_ocr_trainf_names (TrainFile, CharacterNames, CharacterCount)
            //trainf_ocr_class_mlp (OCRHandle, TrainFile, 200, 1, 0.01, Error, ErrorLog)
            //write_ocr_class_mlp (OCRHandle, FontFile)

            //120, 68, 147, 183, 188, 0, 0, 107, 162, 197, 138, 0, 123
            //120, 68, 147, 183, 146, 202, 0, 107, 162, 197, 138, 169, 123
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
            ho_rotateImage.Dispose();
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

            //errorCounts = hv_errorCounts;
            RecognitionStr1 = hv_Chars1;
            RecognitionStr2 = hv_Chars2;
        }
    }
}
