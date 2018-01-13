using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
using Emgu.CV;
namespace ImageQuantization
{

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        struct crak_node
        {
            public string S;
            public int T;
        };
        public static long seed1, tmpoooo;
        public static long tap2;
        public static long seed2_vedio= -1, seed1_vedio = -1, tap_vedio = -1,seed_len_vedio;
        public static int index_vedio=0,index_vedio_res=0;
        public static string[] images_name=new string[300000];
        static char[] Crack, FinalCrack;
        static crak_node[] all = new crak_node[100000];
        static int all_sz = 0;
        static int TapPosition, FinalTapPosition;
        static long TMP, CNTA;
        public static RGBPixel[,] OriginalImage;
        public static string elapsedTime;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
        public static void all_possible()
        {
            for (int f = 0; f < all_sz; f++)
            {
                string Seed = all[f].S;
                long Tap = all[f].T;
                long Key = 255, Power = 1;
                long seed = 0;
                Tap = Seed.Length - Tap - 1;
                int len = Seed.Length;
                long zero = (long)Math.Pow(2, len);
                for (int i = Seed.Length - 1; i > -1; i--)
                {
                    if (Seed[i] == '1') seed += Power;
                    Power *= 2;
                }
                string calc_Tap = "";
                for (int i = 0; i < Seed.Length; i++)
                {
                    if (i == Tap)
                        calc_Tap += "1";
                    else
                        calc_Tap += "0";
                }
                Tap = 0; Power = 1;
                for (int i = calc_Tap.Length - 1; i > -1; i--)
                {
                    if (calc_Tap[i] == '1') Tap += Power;
                    Power *= 2;
                }
                long variable = 1;
                variable = (long)Math.Pow(2, len - 1);
                int Width = OriginalImage.GetLength(0);
                int Height = OriginalImage.GetLength(1);
                for (int l = 0; l < Width; l++)
                {
                    for (int j = 0; j < Height; j++)
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            for (int i = 0; i < 8; i++)
                            {
                                long copy_variable = variable;
                                long copy_Tap = Tap;
                                int num = 0;
                                variable = variable & seed;
                                Tap = Tap & seed;
                                if (variable > 0 && Tap == 0)
                                    num = 1;
                                else if (variable == 0 && Tap > 0)
                                    num = 1;
                                long copy_seed = seed;
                                seed = seed << 1;
                                if (seed >= zero)
                                    seed = seed - zero;
                                seed += num;
                                Tap = copy_Tap;
                                variable = copy_variable;
                            }
                            long XOR = seed & Key;
                            if (k == 1)
                                OriginalImage[l, j].green = (byte)(OriginalImage[l, j].green ^ XOR);
                            else if (k == 2)
                                OriginalImage[l, j].blue = (byte)(OriginalImage[l, j].blue ^ XOR);
                            else
                                OriginalImage[l, j].red = (byte)(OriginalImage[l, j].red ^ XOR);
                        }
                    }
                }
                int H = OriginalImage.GetLength(0);
                int W = OriginalImage.GetLength(1);
                Bitmap ImageBMP = new Bitmap(W, H, PixelFormat.Format24bppRgb);
                unsafe
                {
                    BitmapData bmd = ImageBMP.LockBits(new Rectangle(0, 0, W, H), ImageLockMode.ReadWrite, ImageBMP.PixelFormat);
                    int nWidth = 0;
                    nWidth = W * 3;
                    int nOffset = bmd.Stride - nWidth;
                    byte* p = (byte*)bmd.Scan0;
                    for (int i = 0; i < H; i++)
                    {
                        for (int j = 0; j < W; j++)
                        {
                            p[2] = OriginalImage[i, j].red;
                            p[1] = OriginalImage[i, j].green;
                            p[0] = OriginalImage[i, j].blue;
                            p += 3;
                        }

                        p += nOffset;
                    }
                    ImageBMP.UnlockBits(bmd);
                }
                ImageBMP.Save(@"C:\Users\JOE\Desktop\Final-IsA_1 - Copy (4) - Copy - Copy\Final IsA\TEMPLATE-ImageEncryptCompress_4\[TEMPLATE] ImageEncryptCompress\ImageEncryptCompress\bin\images/" + all[f].S + "_" + Convert.ToString(all[f].T) + ".bmp");
    
                ///////////
                Seed = all[f].S;
                Tap = all[f].T;
                Key = 255; Power = 1;
                seed = 0;
                Tap = Seed.Length - Tap - 1;
                len = Seed.Length;
                zero = (long)Math.Pow(2, len);
                for (int i = Seed.Length - 1; i > -1; i--)
                {
                    if (Seed[i] == '1') seed += Power;
                    Power *= 2;
                }
                calc_Tap = "";
                for (int i = 0; i < Seed.Length; i++)
                {
                    if (i == Tap)
                        calc_Tap += "1";
                    else
                        calc_Tap += "0";
                }
                Tap = 0; Power = 1;
                for (int i = calc_Tap.Length - 1; i > -1; i--)
                {
                    if (calc_Tap[i] == '1') Tap += Power;
                    Power *= 2;
                }
                variable = 1;
                variable = (long)Math.Pow(2, len - 1);
                Width = OriginalImage.GetLength(0);
                Height = OriginalImage.GetLength(1);
                for (int l = 0; l < Width; l++)
                {
                    for (int j = 0; j < Height; j++)
                    {
                        for (int k = 0; k < 3; k++)
                        {
                            for (int i = 0; i < 8; i++)
                            {
                                long copy_variable = variable;
                                long copy_Tap = Tap;
                                int num = 0;
                                variable = variable & seed;
                                Tap = Tap & seed;
                                if (variable > 0 && Tap == 0)
                                    num = 1;
                                else if (variable == 0 && Tap > 0)
                                    num = 1;
                                long copy_seed = seed;
                                seed = seed << 1;
                                if (seed >= zero)
                                    seed = seed - zero;
                                seed += num;
                                Tap = copy_Tap;
                                variable = copy_variable;
                            }
                            long XOR = seed & Key;
                            if (k == 1)
                                OriginalImage[l, j].green = (byte)(OriginalImage[l, j].green ^ XOR);
                            else if (k == 2)
                                OriginalImage[l, j].blue = (byte)(OriginalImage[l, j].blue ^ XOR);
                            else
                                OriginalImage[l, j].red = (byte)(OriginalImage[l, j].red ^ XOR);
                        }
                    }
                }


            }
        }
        public static void ApplyEncryptionOrDecryption(string Seed, long Tap)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            long Key = 255, Power = 1;
            long seed1 = 0, seed2 = 0, seed = 0;
            int len = Seed.Length;
            //to know we need 2 variables or no
            bool _1 = true, _2 = false;
            //calc zero index
            if (len > 63)
            {
                _2 = true;
                len = len - 63;
            }
            long zero;
            if (len<9&&!_2)
             zero = (long)Math.Pow(2, 9-1) - 1;
            else
                zero = (long)Math.Pow(2, len - 1) - 1;

            int currunt = 0;
            //Tap_bool to know what seed should i take tap from
            bool Tap_bool=false;
            //calc seed1,seed2
            for (int i = Seed.Length - 1; i > -1; i--)
            {
                if (Seed[i] == '1') seed += Power;
                Power *= 2;
                if (currunt < 63 )
                {
                    seed1 = seed;
                }
                if (currunt == 62)
                {
                    Power = 1;
                    seed = 0;
                }
                if (currunt >= 63)
                {
                    seed2 = seed;
                }
                currunt++;
            }
            //we can only calc 2^62 as max value other it will overeflow
            if (Tap >= 63)
            {
                Tap -= 63;
                Tap_bool = true;
            }
            //we calc index 0 only 1 time
            if(Tap_bool)
            Tap = (long)Math.Pow(2, Tap-1);
            else
            Tap = (long)Math.Pow(2, Tap);
            long variable = 1;
            variable = (long)Math.Pow(2, len - 1);
            int Width = OriginalImage.GetLength(0);
            int Height = OriginalImage.GetLength(1);
            for (int l = 0; l < Width; l++)
            {
                for (int j = 0; j < Height; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            long copy_variable = variable;
                            long copy_Tap = Tap;
                            int num = 0,num2=0;
                            if (_2)
                            {
                                variable = variable & seed2;
                                if (Tap_bool)
                                    Tap = Tap & seed2;
                                else
                                    Tap = Tap & seed1;
                                if (variable > 0 && Tap == 0)
                                    num = 1;
                                else if (variable == 0 && Tap > 0)
                                    num = 1;
                                if ((seed1 & 4611686018427387904) > 0)
                                    num2++;
                                seed1 = seed1 & 4611686018427387903;
                                seed2 = seed2 & zero;
                            }
                            else
                            {
                                variable = variable & seed1;
                                Tap = Tap & seed1;
                                if (variable > 0 && Tap == 0)
                                    num = 1;
                                else if (variable == 0 && Tap > 0)
                                    num = 1;
                                seed1 = seed1 & zero;
                            }
                            seed1 = seed1 << 1;
                            seed2 = seed2 << 1;
                            seed1 += num;
                            seed2 += num2;
                            Tap = copy_Tap;
                            variable = copy_variable;
                        }
                        long XOR =(long) seed1 & (long)Key;
                        if (k == 1)
                            OriginalImage[l, j].green = (byte)(OriginalImage[l, j].green ^ XOR);
                        else if (k == 2)
                            OriginalImage[l, j].blue = (byte)(OriginalImage[l, j].blue ^ XOR);
                        else
                            OriginalImage[l, j].red = (byte)(OriginalImage[l, j].red ^ XOR);
                    }
                }
            }
            stopwatch.Stop();
            TimeSpan ts = stopwatch.Elapsed;
            elapsedTime = String.Format
                ("{0:00}:{1:00}:{2:00}.{3:00}",
                ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
        }

        public static void ApplyEncryptionOrDecryption1()
        {
         
            long Key = 255, Power = 1;
            //to know we need 2 variables or no
            bool _1 = true, _2 = false;
            //calc zero index
            if (seed_len_vedio > 63)
            {
                _2 = true;
                seed_len_vedio = seed_len_vedio - 63;
            }
            long zero;
            if (seed_len_vedio < 9 && !_2)
                zero = (long)Math.Pow(2, 9 - 1) - 1;
            else
                zero = (long)Math.Pow(2, seed_len_vedio - 1) - 1;

            //Tap_bool to know what seed should i take tap from
            bool Tap_bool = false;
            //calc seed1,seed2
            
            //we can only calc 2^62 as max value other it will overeflow
            if (tap_vedio >= 63)
            {
                tap_vedio -= 63;
                Tap_bool = true;
            }
            //we calc index 0 only 1 time
            if (Tap_bool)
                tap_vedio = (long)Math.Pow(2, tap_vedio - 1);
            else
                tap_vedio = (long)Math.Pow(2, tap_vedio);
            long variable = 1;
            variable = (long)Math.Pow(2, seed_len_vedio - 1);
            int Width = OriginalImage.GetLength(0);
            int Height = OriginalImage.GetLength(1);
            for (int l = 0; l < Width; l++)
            {
                for (int j = 0; j < Height; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            long copy_variable = variable;
                            long copy_Tap = tap_vedio;
                            int num = 0, num2 = 0;
                            if (_2)
                            {
                                variable = variable & seed2_vedio;
                                if (Tap_bool)
                                    tap_vedio = tap_vedio & seed2_vedio;
                                else
                                    tap_vedio = tap_vedio & seed1_vedio;
                                if (variable > 0 && tap_vedio == 0)
                                    num = 1;
                                else if (variable == 0 && tap_vedio > 0)
                                    num = 1;
                                if ((seed1_vedio & 4611686018427387904) > 0)
                                    num2++;
                                seed1_vedio = seed1_vedio & 4611686018427387903;
                                seed2_vedio = seed2_vedio & zero;
                            }
                            else
                            {
                                variable = variable & seed1_vedio;
                                tap_vedio = tap_vedio & seed1_vedio;
                                if (variable > 0 && tap_vedio == 0)
                                    num = 1;
                                else if (variable == 0 && tap_vedio > 0)
                                    num = 1;
                                seed1_vedio = seed1_vedio & zero;
                            }
                            seed1_vedio = seed1_vedio << 1;
                            seed2_vedio = seed2_vedio << 1;
                            seed1_vedio += num;
                            seed2_vedio += num2;
                            tap_vedio = copy_Tap;
                            variable = copy_variable;
                        }
                        long XOR = (long)seed1_vedio & (long)Key;
                        if (k == 1)
                            OriginalImage[l, j].green = (byte)(OriginalImage[l, j].green ^ XOR);
                        else if (k == 2)
                            OriginalImage[l, j].blue = (byte)(OriginalImage[l, j].blue ^ XOR);
                        else
                            OriginalImage[l, j].red = (byte)(OriginalImage[l, j].red ^ XOR);
                    }
                }
            }
            int H = OriginalImage.GetLength(0);
            int W = OriginalImage.GetLength(1);
            Bitmap ImageBMP = new Bitmap(W, H, PixelFormat.Format24bppRgb);
            unsafe
            {
                BitmapData bmd = ImageBMP.LockBits(new Rectangle(0, 0, W, H), ImageLockMode.ReadWrite, ImageBMP.PixelFormat);
                int nWidth = 0;
                nWidth = W * 3;
                int nOffset = bmd.Stride - nWidth;
                byte* p = (byte*)bmd.Scan0;
                for (int i = 0; i < H; i++)
                {
                    for (int j = 0; j < W; j++)
                    {
                        p[2] = OriginalImage[i, j].red;
                        p[1] = OriginalImage[i, j].green;
                        p[0] = OriginalImage[i, j].blue;
                        p += 3;
                    }

                    p += nOffset;
                }
                ImageBMP.UnlockBits(bmd); 
            }
            ImageBMP.Save(@"C:\Users\JOE\Desktop\[TEMPLATE] ImageEncryptCompress\Put Pictures/" + index_vedio_res+ ".bmp");
            index_vedio_res++;
        }
        public static RGBPixel[,] ApplyEncryptionOrDecryption2(string Seed, long Tap)
        {


            long Key = 255, Power = 1;
            long seed1 = 0, seed2 = 0, seed = 0;
            int len = Seed.Length;
            //to know we need 2 variables or no
            bool _1 = true, _2 = false;
            //calc zero index
            if (len > 63)
            {
                _2 = true;
                len = len - 63;
            }
            long zero;
            if (len < 9 && !_2)
                zero = (long)Math.Pow(2, 9 - 1) - 1;
            else
                zero = (long)Math.Pow(2, len - 1) - 1;
            int currunt = 0;
            //Tap_bool to know what seed should i take tap from
            bool Tap_bool = false;
            //calc seed1,seed2
            for (int i = Seed.Length - 1; i > -1; i--)
            {
                if (Seed[i] == '1') seed += Power;
                Power *= 2;
                if (currunt < 63)
                {
                    seed1 = seed;
                }
                if (currunt == 62)
                {
                    Power = 1;
                    seed = 0;
                }
                if (currunt >= 63)
                {
                    seed2 = seed;
                }
                currunt++;
            }
            //we can only calc 2^62 as max value other it will overeflow
            if (Tap >= 63)
            {
                Tap -= 63;
                Tap_bool = true;
            }
            //we calc index 0 only 1 time
            if (Tap_bool)
                Tap = (long)Math.Pow(2, Tap - 1);
            else
                Tap = (long)Math.Pow(2, Tap);
            long variable = 1;
            variable = (long)Math.Pow(2, len - 1);
            int Width = OriginalImage.GetLength(0);
            int Height = OriginalImage.GetLength(1);
            RGBPixel[,] tempImage = new RGBPixel[Height, Width];

            for (int l = 0; l < Width; l++)
            {
                for (int j = 0; j < Height; j++)
                {
                    for (int k = 0; k < 3; k++)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            long copy_variable = variable;
                            long copy_Tap = Tap;
                            int num = 0, num2 = 0;
                            if (_2)
                            {
                                variable = variable & seed2;
                                if (Tap_bool)
                                    Tap = Tap & seed2;
                                else
                                    Tap = Tap & seed1;
                                if (variable > 0 && Tap == 0)
                                    num = 1;
                                else if (variable == 0 && Tap > 0)
                                    num = 1;
                                if ((seed1 & 4611686018427387904) > 0)
                                    num2++;
                                seed1 = seed1 & 4611686018427387903;
                                seed2 = seed2 & zero;
                            }
                            else
                            {
                                variable = variable & seed1;
                                Tap = Tap & seed1;
                                if (variable > 0 && Tap == 0)
                                    num = 1;
                                else if (variable == 0 && Tap > 0)
                                    num = 1;
                                seed1 = seed1 & zero;
                            }
                            seed1 = seed1 << 1;
                            seed2 = seed2 << 1;
                            seed1 += num;
                            seed2 += num2;
                            Tap = copy_Tap;
                            variable = copy_variable;
                        }
                        long XOR = (long)seed1 & (long)Key;
                        if (k == 1)
                            tempImage[l, j].green = (byte)(OriginalImage[l, j].green ^ XOR);
                        else if (k == 2)
                            tempImage[l, j].blue = (byte)(OriginalImage[l, j].blue ^ XOR);
                        else
                            tempImage[l, j].red = (byte)(OriginalImage[l, j].red ^ XOR);
                    }
                }
            }

            return tempImage;
        }

        public static string ConvertAlpha(string Seed)
        {
            string New = "";
            int Len = Seed.Length;
            int Eights = Math.Max(0, Len - 4);
            int DeletedEights = 8 * Eights;
            int Quantity = DeletedEights / Len, Quantity2 = DeletedEights / Len + 1;
            int ModQuantity = Len - (DeletedEights % Len), ModQuantity2 = (DeletedEights % Len);
            int CurQuantity = Quantity, CurModQuantity = ModQuantity;
            for (int i = 0; i < Len; i++)
            {
                if (CurModQuantity == 0)
                {
                    CurModQuantity = ModQuantity2;
                    CurQuantity = Quantity2;
                }
                CurModQuantity--;
                string Temp = "";
                for (int j = 0; j < 8 - CurQuantity; j++)
                {
                    if (((Seed[i] >> j) & 1) != 0) Temp += '1';
                    else Temp += '0';
                }
                for (int j = Temp.Length - 1; j > -1; j--) New += Temp[j];
            }
            return New;
        }
        public static void sum(int N, RGBPixel[,] image, string crack, int tapPosition)
        {
            long sz = 0;
            int[] array_red = new int[256];
            int[] array_green = new int[256];
            int[] array_blue = new int[256];
            int Height = ImageOperations.GetHeight(image);
            int Width = ImageOperations.GetWidth(image);
            for (int l = 0; l < Height; l++)
            {
                for (int j = 0; j < Width; j++)
                {
                    if (array_red[image[l, j].red] == 0)
                        sz++;
                    if (array_green[image[l, j].green] == 0)
                        sz++;
                    if (array_blue[image[l, j].blue] == 0)
                        sz++;
                    array_red[image[l, j].red]++;
                    array_green[image[l, j].green]++;
                    array_blue[image[l, j].blue]++;
                }
            }

            lock (all)
            {
                TMP = sz;
                TMP = 768 - TMP;
                if (TMP > CNTA)
                {
                    all_sz = 0;
                    all[all_sz].S = crack;
                    all[all_sz].T = tapPosition;
                    all_sz++;

                    //FinalCrack = string.Copy(crack).ToCharArray();
                    CNTA = TMP;
                    //for (int i = 0; i < N; i++)
                    //    FinalCrack[i] = Crack[i];
                    //FinalTapPosition = tapPosition;
                }
                else if (TMP == CNTA)
                {
                    all[all_sz].S = crack;
                    all[all_sz].T = tapPosition;
                    all_sz++;
                    //FinalCrack = string.Copy(crack).ToCharArray();
                    CNTA = TMP;
                    //for (int i = 0; i < N; i++)
                    //    FinalCrack[i] = Crack[i];
                    //FinalTapPosition = tapPosition;
                }
            }
        }
        public static void GenerateSeed(int N)
        {

            CNTA = 0;
            Crack = new char[N];
            FinalCrack = new char[N];
            GenerateAllSeeds(0, N);
            //       ApplyEncryptionOrDecryption(new string(FinalCrack), FinalTapPosition);


        }
        public static void GenerateAllSeeds(int index, int N)
        {

            if (index == N)
            {
                for (TapPosition = 0; TapPosition < N; TapPosition++)
                {
                    TMP = 0;
                    var image = ApplyEncryptionOrDecryption2(new string(Crack), TapPosition);
                    //sum(N, image, new string(Crack), TapPosition);
                    int x = TapPosition;
                    Thread sumThread = new Thread(unused => sum(N, image, new string(Crack), x));
                    sumThread.Start();

                    //sum(N);                   
                    //ApplyEncryptionOrDecryption(new string(Crack), TapPosition);
                }
                return;
            }
            Crack[index] = '0';
            GenerateAllSeeds(index + 1, N);
            Crack[index] = '1';
            GenerateAllSeeds(index + 1, N);

        }
    }
}