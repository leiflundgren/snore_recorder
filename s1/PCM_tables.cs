﻿using System;
using System.Collections.Generic;
using System.Text;

namespace s1
{
    public abstract class PCM_tables
    {
        /*
         These tables were generated by Harald Ljøen, Telenor R&D, Norway.
         Phone:  +47 63 84 86 70
         Fax:    +47 63 81 00 76
         E-mail: Harald.Ljoen@fou.telenor.no

         Liablity for possible damage occurring through the use of these tables
         shall be limited to the amount of money you paid for them, i e nothing :-)

         pcm_A2lin[Alawsample] returns the 16-bit linear sample corresponding to
           the A-law coded sample Alawsample.
         pcm_rA2lin[rAlawsample] returns the 16-bit linear sample corresponding
           to the *bitreversed* A-law coded sample rAlawsample, 'bitreversed' meaning
           that the A-law sample's most significent bit is in the least significant
           position in storage, etc..
         pcm_u2lin[] and pcm_ru2lin[] are the corresponding arrays for mu-law and
           bitreversed mu-law coded samples.
          
          
         Converted to C# by Leif         
        */


        /* A-law */
        public static readonly Int16[] pcm_A2lin;
        /* bitreversed A-law */
        public static readonly Int16[] pcm_rA2lin;
        /* mu-law */
        public static readonly Int16[] pcm_u2lin;
        /* bitreversed mu-law */
        public static readonly Int16[] pcm_ru2lin;


        static PCM_tables()
        {

            /* A-law */
            pcm_A2lin = new Int16[] {
             -5504, -5248, -6016, -5760, -4480, -4224, -4992, -4736, -7552, -7296, -8064,
             -7808, -6528, -6272, -7040, -6784, -2752, -2624, -3008, -2880, -2240, -2112,
             -2496, -2368, -3776, -3648, -4032, -3904, -3264, -3136, -3520, -3392,-22016,
            -20992,-24064,-23040,-17920,-16896,-19968,-18944,-30208,-29184,-32256,-31232,
            -26112,-25088,-28160,-27136,-11008,-10496,-12032,-11520, -8960, -8448, -9984,
             -9472,-15104,-14592,-16128,-15616,-13056,-12544,-14080,-13568,  -344,  -328,
              -376,  -360,  -280,  -264,  -312,  -296,  -472,  -456,  -504,  -488,  -408,
              -392,  -440,  -424,   -88,   -72,  -120,  -104,   -24,    -8,   -56,   -40,
              -216,  -200,  -248,  -232,  -152,  -136,  -184,  -168, -1376, -1312, -1504,
             -1440, -1120, -1056, -1248, -1184, -1888, -1824, -2016, -1952, -1632, -1568,
             -1760, -1696,  -688,  -656,  -752,  -720,  -560,  -528,  -624,  -592,  -944,
              -912, -1008,  -976,  -816,  -784,  -880,  -848,  5504,  5248,  6016,  5760,
              4480,  4224,  4992,  4736,  7552,  7296,  8064,  7808,  6528,  6272,  7040,
              6784,  2752,  2624,  3008,  2880,  2240,  2112,  2496,  2368,  3776,  3648,
              4032,  3904,  3264,  3136,  3520,  3392, 22016, 20992, 24064, 23040, 17920,
             16896, 19968, 18944, 30208, 29184, 32256, 31232, 26112, 25088, 28160, 27136,
             11008, 10496, 12032, 11520,  8960,  8448,  9984,  9472, 15104, 14592, 16128,
             15616, 13056, 12544, 14080, 13568,   344,   328,   376,   360,   280,   264,
               312,   296,   472,   456,   504,   488,   408,   392,   440,   424,    88,
                72,   120,   104,    24,     8,    56,    40,   216,   200,   248,   232,
               152,   136,   184,   168,  1376,  1312,  1504,  1440,  1120,  1056,  1248,
              1184,  1888,  1824,  2016,  1952,  1632,  1568,  1760,  1696,   688,   656,
               752,   720,   560,   528,   624,   592,   944,   912,  1008,   976,   816,
               784,   880,   848 };

            /* bitreversed A-law */
            pcm_rA2lin = new Int16[] {
             -5504,  5504,  -344,   344,-22016, 22016, -1376,  1376, -2752,  2752,   -88,
                88,-11008, 11008,  -688,   688, -7552,  7552,  -472,   472,-30208, 30208,
             -1888,  1888, -3776,  3776,  -216,   216,-15104, 15104,  -944,   944, -4480,
              4480,  -280,   280,-17920, 17920, -1120,  1120, -2240,  2240,   -24,    24,
             -8960,  8960,  -560,   560, -6528,  6528,  -408,   408,-26112, 26112, -1632,
              1632, -3264,  3264,  -152,   152,-13056, 13056,  -816,   816, -6016,  6016,
              -376,   376,-24064, 24064, -1504,  1504, -3008,  3008,  -120,   120,-12032,
             12032,  -752,   752, -8064,  8064,  -504,   504,-32256, 32256, -2016,  2016,
             -4032,  4032,  -248,   248,-16128, 16128, -1008,  1008, -4992,  4992,  -312,
               312,-19968, 19968, -1248,  1248, -2496,  2496,   -56,    56, -9984,  9984,
              -624,   624, -7040,  7040,  -440,   440,-28160, 28160, -1760,  1760, -3520,
              3520,  -184,   184,-14080, 14080,  -880,   880, -5248,  5248,  -328,   328,
            -20992, 20992, -1312,  1312, -2624,  2624,   -72,    72,-10496, 10496,  -656,
               656, -7296,  7296,  -456,   456,-29184, 29184, -1824,  1824, -3648,  3648,
              -200,   200,-14592, 14592,  -912,   912, -4224,  4224,  -264,   264,-16896,
             16896, -1056,  1056, -2112,  2112,    -8,     8, -8448,  8448,  -528,   528,
             -6272,  6272,  -392,   392,-25088, 25088, -1568,  1568, -3136,  3136,  -136,
               136,-12544, 12544,  -784,   784, -5760,  5760,  -360,   360,-23040, 23040,
             -1440,  1440, -2880,  2880,  -104,   104,-11520, 11520,  -720,   720, -7808,
              7808,  -488,   488,-31232, 31232, -1952,  1952, -3904,  3904,  -232,   232,
            -15616, 15616,  -976,   976, -4736,  4736,  -296,   296,-18944, 18944, -1184,
              1184, -2368,  2368,   -40,    40, -9472,  9472,  -592,   592, -6784,  6784,
              -424,   424,-27136, 27136, -1696,  1696, -3392,  3392,  -168,   168,-13568,
             13568,  -848,   848
            };

            /* mu-law */
            pcm_u2lin = new Int16[] {
            -32124,-31100,-30076,-29052,-28028,-27004,-25980,-24956,-23932,-22908,-21884,
            -20860,-19836,-18812,-17788,-16764,-15996,-15484,-14972,-14460,-13948,-13436,
            -12924,-12412,-11900,-11388,-10876,-10364, -9852, -9340, -8828, -8316, -7932,
             -7676, -7420, -7164, -6908, -6652, -6396, -6140, -5884, -5628, -5372, -5116,
             -4860, -4604, -4348, -4092, -3900, -3772, -3644, -3516, -3388, -3260, -3132,
             -3004, -2876, -2748, -2620, -2492, -2364, -2236, -2108, -1980, -1884, -1820,
             -1756, -1692, -1628, -1564, -1500, -1436, -1372, -1308, -1244, -1180, -1116,
             -1052,  -988,  -924,  -876,  -844,  -812,  -780,  -748,  -716,  -684,  -652,
              -620,  -588,  -556,  -524,  -492,  -460,  -428,  -396,  -372,  -356,  -340,
              -324,  -308,  -292,  -276,  -260,  -244,  -228,  -212,  -196,  -180,  -164,
              -148,  -132,  -120,  -112,  -104,   -96,   -88,   -80,   -72,   -64,   -56,
               -48,   -40,   -32,   -24,   -16,    -8,     0, 32124, 31100, 30076, 29052,
             28028, 27004, 25980, 24956, 23932, 22908, 21884, 20860, 19836, 18812, 17788,
             16764, 15996, 15484, 14972, 14460, 13948, 13436, 12924, 12412, 11900, 11388,
             10876, 10364,  9852,  9340,  8828,  8316,  7932,  7676,  7420,  7164,  6908,
              6652,  6396,  6140,  5884,  5628,  5372,  5116,  4860,  4604,  4348,  4092,
              3900,  3772,  3644,  3516,  3388,  3260,  3132,  3004,  2876,  2748,  2620,
              2492,  2364,  2236,  2108,  1980,  1884,  1820,  1756,  1692,  1628,  1564,
              1500,  1436,  1372,  1308,  1244,  1180,  1116,  1052,   988,   924,   876,
               844,   812,   780,   748,   716,   684,   652,   620,   588,   556,   524,
               492,   460,   428,   396,   372,   356,   340,   324,   308,   292,   276,
               260,   244,   228,   212,   196,   180,   164,   148,   132,   120,   112,
               104,    96,    88,    80,    72,    64,    56,    48,    40,    32,    24,
                16,     8,     0
            };

            /* bitreversed mu-law */
            pcm_ru2lin = new Int16[] {
            -32124, 32124, -1884,  1884, -7932,  7932,  -372,   372,-15996, 15996,  -876,
               876, -3900,  3900,  -120,   120,-23932, 23932, -1372,  1372, -5884,  5884,
              -244,   244,-11900, 11900,  -620,   620, -2876,  2876,   -56,    56,-28028,
             28028, -1628,  1628, -6908,  6908,  -308,   308,-13948, 13948,  -748,   748,
             -3388,  3388,   -88,    88,-19836, 19836, -1116,  1116, -4860,  4860,  -180,
               180, -9852,  9852,  -492,   492, -2364,  2364,   -24,    24,-30076, 30076,
             -1756,  1756, -7420,  7420,  -340,   340,-14972, 14972,  -812,   812, -3644,
              3644,  -104,   104,-21884, 21884, -1244,  1244, -5372,  5372,  -212,   212,
            -10876, 10876,  -556,   556, -2620,  2620,   -40,    40,-25980, 25980, -1500,
              1500, -6396,  6396,  -276,   276,-12924, 12924,  -684,   684, -3132,  3132,
               -72,    72,-17788, 17788,  -988,   988, -4348,  4348,  -148,   148, -8828,
              8828,  -428,   428, -2108,  2108,    -8,     8,-31100, 31100, -1820,  1820,
             -7676,  7676,  -356,   356,-15484, 15484,  -844,   844, -3772,  3772,  -112,
               112,-22908, 22908, -1308,  1308, -5628,  5628,  -228,   228,-11388, 11388,
              -588,   588, -2748,  2748,   -48,    48,-27004, 27004, -1564,  1564, -6652,
              6652,  -292,   292,-13436, 13436,  -716,   716, -3260,  3260,   -80,    80,
            -18812, 18812, -1052,  1052, -4604,  4604,  -164,   164, -9340,  9340,  -460,
               460, -2236,  2236,   -16,    16,-29052, 29052, -1692,  1692, -7164,  7164,
              -324,   324,-14460, 14460,  -780,   780, -3516,  3516,   -96,    96,-20860,
             20860, -1180,  1180, -5116,  5116,  -196,   196,-10364, 10364,  -524,   524,
             -2492,  2492,   -32,    32,-24956, 24956, -1436,  1436, -6140,  6140,  -260,
               260,-12412, 12412,  -652,   652, -3004,  3004,   -64,    64,-16764, 16764,
              -924,   924, -4092,  4092,  -132,   132, -8316,  8316,  -396,   396, -1980,
              1980,     0,     0
            };

        }
    }
}
