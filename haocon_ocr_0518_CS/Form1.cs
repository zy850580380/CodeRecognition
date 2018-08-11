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
            HDevelopExport HD = new HDevelopExport(hWindowControl1.HalconWindow, ImagePath);
            if (HD.RecognitionStr1.TupleLength() > 0)
            {
                textBox1.Text = HD.RecognitionStr1.ToString();
            }
            else
            {
                textBox1.Text = "字符缺失";
            }
            if (HD.RecognitionStr2.TupleLength() > 0)
            {
                textBox2.Text = HD.RecognitionStr2.ToString();
            }
            else
            {
                textBox2.Text = "字符缺失";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            openFileDialog1.ShowDialog();
            ImagePath = openFileDialog1.FileName;
            label1.Text = ImagePath;
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

        public void disp_message (HTuple hv_WindowHandle, HTuple hv_String, HTuple hv_CoordSystem, 
      HTuple hv_Row, HTuple hv_Column, HTuple hv_Color, HTuple hv_Box)
      {


        // Local control variables 

        HTuple hv_Red, hv_Green, hv_Blue, hv_Row1Part;
        HTuple hv_Column1Part, hv_Row2Part, hv_Column2Part, hv_RowWin;
        HTuple hv_ColumnWin, hv_WidthWin, hv_HeightWin, hv_MaxAscent;
        HTuple hv_MaxDescent, hv_MaxWidth, hv_MaxHeight, hv_R1=new HTuple();
        HTuple hv_C1=new HTuple(), hv_FactorRow=new HTuple(), hv_FactorColumn=new HTuple();
        HTuple hv_Width=new HTuple(), hv_Index=new HTuple(), hv_Ascent=new HTuple();
        HTuple hv_Descent=new HTuple(), hv_W=new HTuple(), hv_H=new HTuple();
        HTuple hv_FrameHeight=new HTuple(), hv_FrameWidth=new HTuple();
        HTuple hv_R2=new HTuple(), hv_C2=new HTuple(), hv_DrawMode=new HTuple();
        HTuple hv_Exception=new HTuple(), hv_CurrentColor=new HTuple();

        HTuple   hv_Color_COPY_INP_TMP = hv_Color.Clone();
        HTuple   hv_Column_COPY_INP_TMP = hv_Column.Clone();
        HTuple   hv_Row_COPY_INP_TMP = hv_Row.Clone();
        HTuple   hv_String_COPY_INP_TMP = hv_String.Clone();

        // Initialize local and output iconic variables 

        //This procedure displays text in a graphics window.
        //
        //Input parameters:
        //WindowHandle: The WindowHandle of the graphics window, where
        //   the message should be displayed
        //String: A tuple of strings containing the text message to be displayed
        //CoordSystem: If set to 'window', the text position is given
        //   with respect to the window coordinate system.
        //   If set to 'image', image coordinates are used.
        //   (This may be useful in zoomed images.)
        //Row: The row coordinate of the desired text position
        //   If set to -1, a default value of 12 is used.
        //Column: The column coordinate of the desired text position
        //   If set to -1, a default value of 12 is used.
        //Color: defines the color of the text as string.
        //   If set to [], '' or 'auto' the currently set color is used.
        //   If a tuple of strings is passed, the colors are used cyclically
        //   for each new textline.
        //Box: If set to 'true', the text is written within a white box.
        //
        //prepare window
        HOperatorSet.GetRgb(hv_WindowHandle, out hv_Red, out hv_Green, out hv_Blue);
        HOperatorSet.GetPart(hv_WindowHandle, out hv_Row1Part, out hv_Column1Part, out hv_Row2Part, 
            out hv_Column2Part);
        HOperatorSet.GetWindowExtents(hv_WindowHandle, out hv_RowWin, out hv_ColumnWin, 
            out hv_WidthWin, out hv_HeightWin);
        HOperatorSet.SetPart(hv_WindowHandle, 0, 0, hv_HeightWin-1, hv_WidthWin-1);
        //
        //default settings
        if ((int)(new HTuple(hv_Row_COPY_INP_TMP.TupleEqual(-1))) != 0)
        {
          hv_Row_COPY_INP_TMP = 12;
        }
        if ((int)(new HTuple(hv_Column_COPY_INP_TMP.TupleEqual(-1))) != 0)
        {
          hv_Column_COPY_INP_TMP = 12;
        }
        if ((int)(new HTuple(hv_Color_COPY_INP_TMP.TupleEqual(new HTuple()))) != 0)
        {
          hv_Color_COPY_INP_TMP = "";
        }
        //
        hv_String_COPY_INP_TMP = (((""+hv_String_COPY_INP_TMP)+"")).TupleSplit("\n");
        //
        //Estimate extentions of text depending on font size.
        HOperatorSet.GetFontExtents(hv_WindowHandle, out hv_MaxAscent, out hv_MaxDescent, 
            out hv_MaxWidth, out hv_MaxHeight);
        if ((int)(new HTuple(hv_CoordSystem.TupleEqual("window"))) != 0)
        {
          hv_R1 = hv_Row_COPY_INP_TMP.Clone();
          hv_C1 = hv_Column_COPY_INP_TMP.Clone();
        }
        else
        {
          //transform image to window coordinates
          hv_FactorRow = (1.0*hv_HeightWin)/((hv_Row2Part-hv_Row1Part)+1);
          hv_FactorColumn = (1.0*hv_WidthWin)/((hv_Column2Part-hv_Column1Part)+1);
          hv_R1 = ((hv_Row_COPY_INP_TMP-hv_Row1Part)+0.5)*hv_FactorRow;
          hv_C1 = ((hv_Column_COPY_INP_TMP-hv_Column1Part)+0.5)*hv_FactorColumn;
        }
        //
        //display text box depending on text size
        if ((int)(new HTuple(hv_Box.TupleEqual("true"))) != 0)
        {
          //calculate box extents
          hv_String_COPY_INP_TMP = (" "+hv_String_COPY_INP_TMP)+" ";
          hv_Width = new HTuple();
          for (hv_Index=0; (int)hv_Index<=(int)((new HTuple(hv_String_COPY_INP_TMP.TupleLength()
              ))-1); hv_Index = (int)hv_Index + 1)
          {
            HOperatorSet.GetStringExtents(hv_WindowHandle, hv_String_COPY_INP_TMP.TupleSelect(
                hv_Index), out hv_Ascent, out hv_Descent, out hv_W, out hv_H);
            hv_Width = hv_Width.TupleConcat(hv_W);
          }
          hv_FrameHeight = hv_MaxHeight*(new HTuple(hv_String_COPY_INP_TMP.TupleLength()
              ));
          hv_FrameWidth = (((new HTuple(0)).TupleConcat(hv_Width))).TupleMax();
          hv_R2 = hv_R1+hv_FrameHeight;
          hv_C2 = hv_C1+hv_FrameWidth;
          //display rectangles
          HOperatorSet.GetDraw(hv_WindowHandle, out hv_DrawMode);
          HOperatorSet.SetDraw(hv_WindowHandle, "fill");
          HOperatorSet.SetColor(hv_WindowHandle, "light gray");
          HOperatorSet.DispRectangle1(hv_WindowHandle, hv_R1+3, hv_C1+3, hv_R2+3, hv_C2+3);
          HOperatorSet.SetColor(hv_WindowHandle, "white");
          HOperatorSet.DispRectangle1(hv_WindowHandle, hv_R1, hv_C1, hv_R2, hv_C2);
          HOperatorSet.SetDraw(hv_WindowHandle, hv_DrawMode);
        }
        else if ((int)(new HTuple(hv_Box.TupleNotEqual("false"))) != 0)
        {
          hv_Exception = "Wrong value of control parameter Box";
          throw new HalconException(hv_Exception);
        }
        //Write text.
        for (hv_Index=0; (int)hv_Index<=(int)((new HTuple(hv_String_COPY_INP_TMP.TupleLength()
            ))-1); hv_Index = (int)hv_Index + 1)
        {
          hv_CurrentColor = hv_Color_COPY_INP_TMP.TupleSelect(hv_Index%(new HTuple(hv_Color_COPY_INP_TMP.TupleLength()
              )));
          if ((int)((new HTuple(hv_CurrentColor.TupleNotEqual(""))).TupleAnd(new HTuple(hv_CurrentColor.TupleNotEqual(
              "auto")))) != 0)
          {
            HOperatorSet.SetColor(hv_WindowHandle, hv_CurrentColor);
          }
          else
          {
            HOperatorSet.SetRgb(hv_WindowHandle, hv_Red, hv_Green, hv_Blue);
          }
          hv_Row_COPY_INP_TMP = hv_R1+(hv_MaxHeight*hv_Index);
          HOperatorSet.SetTposition(hv_WindowHandle, hv_Row_COPY_INP_TMP, hv_C1);
          HOperatorSet.WriteString(hv_WindowHandle, hv_String_COPY_INP_TMP.TupleSelect(
              hv_Index));
        }
        //reset changed window settings
        HOperatorSet.SetRgb(hv_WindowHandle, hv_Red, hv_Green, hv_Blue);
        HOperatorSet.SetPart(hv_WindowHandle, hv_Row1Part, hv_Column1Part, hv_Row2Part, 
            hv_Column2Part);

        return;
      }

      // Chapter: Graphics / Text
      // Short Description: Set font independent of OS
      public void set_display_font (HTuple hv_WindowHandle, HTuple hv_Size, HTuple hv_Font, 
          HTuple hv_Bold, HTuple hv_Slant)
      {


            // Local control variables 

            HTuple hv_OS, hv_Exception=new HTuple();
            HTuple hv_AllowedFontSizes=new HTuple(), hv_Distances=new HTuple();
            HTuple hv_Indices=new HTuple();

            HTuple   hv_Bold_COPY_INP_TMP = hv_Bold.Clone();
            HTuple   hv_Font_COPY_INP_TMP = hv_Font.Clone();
            HTuple   hv_Size_COPY_INP_TMP = hv_Size.Clone();
            HTuple   hv_Slant_COPY_INP_TMP = hv_Slant.Clone();

            // Initialize local and output iconic variables 

        //This procedure sets the text font of the current window with
        //the specified attributes.
        //It is assumed that following fonts are installed on the system:
        //Windows: Courier New, Arial Times New Roman
        //Linux: courier, helvetica, times
        //Because fonts are displayed smaller on Linux than on Windows,
        //a scaling factor of 1.25 is used the get comparable results.
        //For Linux, only a limited number of font sizes is supported,
        //to get comparable results, it is recommended to use one of the
        //following sizes: 9, 11, 14, 16, 20, 27
        //(which will be mapped internally on Linux systems to 11, 14, 17, 20, 25, 34)
        //
        //input parameters:
        //WindowHandle: The graphics window for which the font will be set
        //Size: The font size. If Size=-1, the default of 16 is used.
        //Bold: If set to 'true', a bold font is used
        //Slant: If set to 'true', a slanted font is used
        //
        HOperatorSet.GetSystem("operating_system", out hv_OS);
        if ((int)((new HTuple(hv_Size_COPY_INP_TMP.TupleEqual(new HTuple()))).TupleOr(
            new HTuple(hv_Size_COPY_INP_TMP.TupleEqual(-1)))) != 0)
        {
          hv_Size_COPY_INP_TMP = 16;
        }
        if ((int)(new HTuple((((hv_OS.TupleStrFirstN(2)).TupleStrLastN(0))).TupleEqual(
            "Win"))) != 0)
        {
          //set font on Windows systems
          if ((int)((new HTuple((new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("mono"))).TupleOr(
              new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("Courier"))))).TupleOr(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual(
              "courier")))) != 0)
          {
            hv_Font_COPY_INP_TMP = "Courier New";
          }
          else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("sans"))) != 0)
          {
            hv_Font_COPY_INP_TMP = "Arial";
          }
          else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("serif"))) != 0)
          {
            hv_Font_COPY_INP_TMP = "Times New Roman";
          }
          if ((int)(new HTuple(hv_Bold_COPY_INP_TMP.TupleEqual("true"))) != 0)
          {
            hv_Bold_COPY_INP_TMP = 1;
          }
          else if ((int)(new HTuple(hv_Bold_COPY_INP_TMP.TupleEqual("false"))) != 0)
          {
            hv_Bold_COPY_INP_TMP = 0;
          }
          else
          {
            hv_Exception = "Wrong value of control parameter Bold";
            throw new HalconException(hv_Exception);
          }
          if ((int)(new HTuple(hv_Slant_COPY_INP_TMP.TupleEqual("true"))) != 0)
          {
            hv_Slant_COPY_INP_TMP = 1;
          }
          else if ((int)(new HTuple(hv_Slant_COPY_INP_TMP.TupleEqual("false"))) != 0)
          {
            hv_Slant_COPY_INP_TMP = 0;
          }
          else
          {
            hv_Exception = "Wrong value of control parameter Slant";
            throw new HalconException(hv_Exception);
          }
          try
          {
            HOperatorSet.SetFont(hv_WindowHandle, ((((((("-"+hv_Font_COPY_INP_TMP)+"-")+hv_Size_COPY_INP_TMP)+"-*-")+hv_Slant_COPY_INP_TMP)+"-*-*-")+hv_Bold_COPY_INP_TMP)+"-");
          }
          // catch (Exception) 
          catch (HalconException HDevExpDefaultException1)
          {
            HDevExpDefaultException1.ToHTuple(out hv_Exception);
            throw new HalconException(hv_Exception);
          }
        }
        else
        {
          //set font for UNIX systems
          hv_Size_COPY_INP_TMP = hv_Size_COPY_INP_TMP*1.25;
          hv_AllowedFontSizes = new HTuple();
          hv_AllowedFontSizes[0] = 11;
          hv_AllowedFontSizes[1] = 14;
          hv_AllowedFontSizes[2] = 17;
          hv_AllowedFontSizes[3] = 20;
          hv_AllowedFontSizes[4] = 25;
          hv_AllowedFontSizes[5] = 34;
          if ((int)(new HTuple(((hv_AllowedFontSizes.TupleFind(hv_Size_COPY_INP_TMP))).TupleEqual(
              -1))) != 0)
          {
            hv_Distances = ((hv_AllowedFontSizes-hv_Size_COPY_INP_TMP)).TupleAbs();
            HOperatorSet.TupleSortIndex(hv_Distances, out hv_Indices);
            hv_Size_COPY_INP_TMP = hv_AllowedFontSizes.TupleSelect(hv_Indices.TupleSelect(
                0));
          }
          if ((int)((new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("mono"))).TupleOr(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual(
              "Courier")))) != 0)
          {
            hv_Font_COPY_INP_TMP = "courier";
          }
          else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("sans"))) != 0)
          {
            hv_Font_COPY_INP_TMP = "helvetica";
          }
          else if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("serif"))) != 0)
          {
            hv_Font_COPY_INP_TMP = "times";
          }
          if ((int)(new HTuple(hv_Bold_COPY_INP_TMP.TupleEqual("true"))) != 0)
          {
            hv_Bold_COPY_INP_TMP = "bold";
          }
          else if ((int)(new HTuple(hv_Bold_COPY_INP_TMP.TupleEqual("false"))) != 0)
          {
            hv_Bold_COPY_INP_TMP = "medium";
          }
          else
          {
            hv_Exception = "Wrong value of control parameter Bold";
            throw new HalconException(hv_Exception);
          }
          if ((int)(new HTuple(hv_Slant_COPY_INP_TMP.TupleEqual("true"))) != 0)
          {
            if ((int)(new HTuple(hv_Font_COPY_INP_TMP.TupleEqual("times"))) != 0)
            {
              hv_Slant_COPY_INP_TMP = "i";
            }
            else
            {
              hv_Slant_COPY_INP_TMP = "o";
            }
          }
          else if ((int)(new HTuple(hv_Slant_COPY_INP_TMP.TupleEqual("false"))) != 0)
          {
            hv_Slant_COPY_INP_TMP = "r";
          }
          else
          {
            hv_Exception = "Wrong value of control parameter Slant";
            throw new HalconException(hv_Exception);
          }
          try
          {
            HOperatorSet.SetFont(hv_WindowHandle, ((((((("-adobe-"+hv_Font_COPY_INP_TMP)+"-")+hv_Bold_COPY_INP_TMP)+"-")+hv_Slant_COPY_INP_TMP)+"-normal-*-")+hv_Size_COPY_INP_TMP)+"-*-*-*-*-*-*-*");
          }
          // catch (Exception) 
          catch (HalconException HDevExpDefaultException1)
          {
            HDevExpDefaultException1.ToHTuple(out hv_Exception);
            throw new HalconException(hv_Exception);
          }
        }

        return;
      }

      // Main procedure 
      private void action(HTuple HDWindow_, string ImageFile)
      {

        // Local iconic variables 

        HObject ho_Image, ho_ImageEmphasize0, ho_ImageInvert0;
        HObject ho_Region0, ho_RegionClosing0, ho_ConnectedRegions0;
        HObject ho_SelectedRegions0, ho_SelectedRegionsStd0, ho_RegionIntersection0;
        HObject ho_ImageRotate0, ho_ImageEmphasize01, ho_ImageInvert01;
        HObject ho_Region01, ho_RegionClosing01, ho_RegionOpening01;
        HObject ho_ConnectedRegions01, ho_SelectedRegions01, ho_RegionIntersection01;
        HObject ho_Rectangle01, ho_ImageReduced01, ho_ImageMedian01;
        HObject ho_Region02, ho_RegionClosing02, ho_ConnectedRegions02;
        HObject ho_SelectedRegions1, ho_RegionIntersection1, ho_RegionClosing1;
        HObject ho_ConnectedRegions1, ho_RegionTrans1, ho_Partitioned1;
        HObject ho_RegionIntersection11, ho_SortedRegions1, ho_SelectedRegions2;
        HObject ho_RegionIntersection2, ho_RegionClosing2, ho_ConnectedRegions2;
        HObject ho_RegionTrans2, ho_Partitioned2, ho_RegionIntersection21;
        HObject ho_SortedRegions2;


        // Local control variables 

        HTuple hv_Row0, hv_Column0, hv_Phi0;
        HTuple hv_Length01, hv_Length02, hv_Deg0;
        HTuple hv_Row01, hv_Column01, hv_Phi01, hv_Length011, hv_Length012;
        HTuple hv_RegionPriorWidth, hv_RegionPriorHeight, hv_Area1, hv_Number0, hv_Number01;
        HTuple hv_Row1, hv_Column1, hv_FontName, hv_OCRHandle1;
        HTuple hv_RecNum1, hv_Confidence1, hv_Area2, hv_Row2, hv_Column2;
        HTuple hv_OCRHandle2, hv_RecNum2, hv_Confidence2;

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
        HOperatorSet.GenEmptyObj(out ho_Region01);
        HOperatorSet.GenEmptyObj(out ho_RegionClosing01);
        HOperatorSet.GenEmptyObj(out ho_RegionOpening01);
        HOperatorSet.GenEmptyObj(out ho_ConnectedRegions01);
        HOperatorSet.GenEmptyObj(out ho_SelectedRegions01);
        HOperatorSet.GenEmptyObj(out ho_RegionIntersection01);
        HOperatorSet.GenEmptyObj(out ho_Rectangle01);
        HOperatorSet.GenEmptyObj(out ho_ImageReduced01);
        HOperatorSet.GenEmptyObj(out ho_ImageMedian01);
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

        try
        {
          HDevWindowStack.Push(HDWindow_);
          ho_Image.Dispose();
          HOperatorSet.ReadImage(out ho_Image, ImageFile);
          if (HDevWindowStack.IsOpen())
          {
              HOperatorSet.DispObj(ho_Image, HDevWindowStack.GetActive());
          }
          ho_ImageEmphasize0.Dispose();
          HOperatorSet.Emphasize(ho_Image, out ho_ImageEmphasize0, 7, 7, 5);
          ho_ImageInvert0.Dispose();
          HOperatorSet.InvertImage(ho_ImageEmphasize0, out ho_ImageInvert0);
          //mean_image (ImageInvert, ImageMean, 5, 5)
          //dyn_threshold (ImageInvert, ImageMean, Region, 80, 'light')
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
          HOperatorSet.CountObj(ho_SelectedRegionsStd0, out hv_Number0);
          if ((int)(new HTuple(hv_Number0.TupleNotEqual(1))) != 0)
          {
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
              ho_Region01.Dispose();
              ho_RegionClosing01.Dispose();
              ho_RegionOpening01.Dispose();
              ho_ConnectedRegions01.Dispose();
              ho_SelectedRegions01.Dispose();
              ho_RegionIntersection01.Dispose();
              ho_Rectangle01.Dispose();
              ho_ImageReduced01.Dispose();
              ho_ImageMedian01.Dispose();
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
          HOperatorSet.RotateImage(ho_Image, out ho_ImageRotate0, (-hv_Deg0)-1.5, "constant");
          //处理旋转后的图像
          ho_ImageEmphasize01.Dispose();
          HOperatorSet.Emphasize(ho_ImageRotate0, out ho_ImageEmphasize01, 7, 7, 5);
          ho_ImageInvert01.Dispose();
          HOperatorSet.InvertImage(ho_ImageEmphasize01, out ho_ImageInvert01);
          //mean_image (ImageRotate0, ImageMean, 3, 3)
          //dyn_threshold (ImageInvert01, ImageMean, Region, 30, 'light')
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
              ho_Region01.Dispose();
              ho_RegionClosing01.Dispose();
              ho_RegionOpening01.Dispose();
              ho_ConnectedRegions01.Dispose();
              ho_SelectedRegions01.Dispose();
              ho_RegionIntersection01.Dispose();
              ho_Rectangle01.Dispose();
              ho_ImageReduced01.Dispose();
              ho_ImageMedian01.Dispose();
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
          hv_RegionPriorWidth = 58;
          hv_RegionPriorHeight = 28;
          //62,28
          //gen_rectangle2 (Rectangle01, Row01-1, Column01+Length011/2-RegionPriorWidth/2, 0, RegionPriorWidth, RegionPriorHeight)
          ho_Rectangle01.Dispose();
          HOperatorSet.GenRectangle2(out ho_Rectangle01, hv_Row01-1, hv_Column01-2, 0, 
              hv_Length011+4, hv_Length012);
          //缩小区域
          ho_ImageReduced01.Dispose();
          HOperatorSet.ReduceDomain(ho_ImageInvert01, ho_Rectangle01, out ho_ImageReduced01
              );
          ho_ImageMedian01.Dispose();
          HOperatorSet.MedianImage(ho_ImageReduced01, out ho_ImageMedian01, "circle", 
              2, "mirrored");
          ho_Region02.Dispose();
          HOperatorSet.Threshold(ho_ImageMedian01, out ho_Region02, 60, 255);
          ho_RegionClosing02.Dispose();
          HOperatorSet.ClosingRectangle1(ho_Region02, out ho_RegionClosing02, 30, 1);
          //opening_rectangle1 (RegionClosing02, RegionOpening02, 3, 2)
          ho_ConnectedRegions02.Dispose();
          HOperatorSet.Connection(ho_RegionClosing02, out ho_ConnectedRegions02);
          //第一行****************************
          ho_SelectedRegions1.Dispose();
          HOperatorSet.SelectShape(ho_ConnectedRegions02, out ho_SelectedRegions1, ((new HTuple("width")).TupleConcat(
              "height")).TupleConcat("row"), "and", ((new HTuple(40)).TupleConcat(15)).TupleConcat(
              hv_Row01-20), ((new HTuple(160)).TupleConcat(35)).TupleConcat(hv_Row01));
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
              "width")).TupleConcat("height"), "and", ((new HTuple(100)).TupleConcat(
              7)).TupleConcat(18), ((new HTuple(550)).TupleConcat(20)).TupleConcat(35));
          ho_RegionIntersection11.Dispose();
          HOperatorSet.Intersection(ho_SelectedRegions1, ho_Region02, out ho_RegionIntersection11
              );
          //选中字符
          //排序
          ho_SortedRegions1.Dispose();
          HOperatorSet.SortRegion(ho_RegionIntersection11, out ho_SortedRegions1, "first_point", 
              "true", "column");
          HOperatorSet.AreaCenter(ho_SortedRegions1, out hv_Area1, out hv_Row1, out hv_Column1);
          hv_FontName = "D:/github/CodeRecognition/0518Words.omc";
          HOperatorSet.ReadOcrClassMlp(hv_FontName, out hv_OCRHandle1);
          HOperatorSet.DoOcrMultiClassMlp(ho_SortedRegions1, ho_ImageRotate0, hv_OCRHandle1, 
              out hv_RecNum1, out hv_Confidence1);
          //第二行***********************
          ho_SelectedRegions2.Dispose();
          HOperatorSet.SelectShape(ho_ConnectedRegions02, out ho_SelectedRegions2, ((new HTuple("width")).TupleConcat(
              "height")).TupleConcat("row"), "and", ((new HTuple(40)).TupleConcat(15)).TupleConcat(
              hv_Row01), ((new HTuple(160)).TupleConcat(35)).TupleConcat(hv_Row01+20));
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
              "width")).TupleConcat("height"), "and", ((new HTuple(100)).TupleConcat(
              7)).TupleConcat(18), ((new HTuple(550)).TupleConcat(20)).TupleConcat(35));
          ho_RegionIntersection21.Dispose();
          HOperatorSet.Intersection(ho_SelectedRegions2, ho_Region02, out ho_RegionIntersection21
              );
          //选中字符
          //排序
          ho_SortedRegions2.Dispose();
          HOperatorSet.SortRegion(ho_RegionIntersection21, out ho_SortedRegions2, "first_point", 
              "true", "column");
          HOperatorSet.AreaCenter(ho_SortedRegions2, out hv_Area2, out hv_Row2, out hv_Column2);
          //识别
          HOperatorSet.ReadOcrClassMlp(hv_FontName, out hv_OCRHandle2);
          HOperatorSet.DoOcrMultiClassMlp(ho_SortedRegions2, ho_ImageRotate0, hv_OCRHandle2, 
              out hv_RecNum2, out hv_Confidence2);
        }
        catch (HalconException HDevExpDefaultException)
        {
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
          ho_Region01.Dispose();
          ho_RegionClosing01.Dispose();
          ho_RegionOpening01.Dispose();
          ho_ConnectedRegions01.Dispose();
          ho_SelectedRegions01.Dispose();
          ho_RegionIntersection01.Dispose();
          ho_Rectangle01.Dispose();
          ho_ImageReduced01.Dispose();
          ho_ImageMedian01.Dispose();
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

          throw HDevExpDefaultException;
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
        ho_Region01.Dispose();
        ho_RegionClosing01.Dispose();
        ho_RegionOpening01.Dispose();
        ho_ConnectedRegions01.Dispose();
        ho_SelectedRegions01.Dispose();
        ho_RegionIntersection01.Dispose();
        ho_Rectangle01.Dispose();
        ho_ImageReduced01.Dispose();
        ho_ImageMedian01.Dispose();
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
        RecognitionStr1 = hv_RecNum1;
        RecognitionStr2 = hv_RecNum2;
      }
    }
}
