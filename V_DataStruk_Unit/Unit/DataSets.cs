﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vulpine_Core_Data_Tests.Unit
{
    class DataSets
    {

        private static readonly double[] normal =
        {
            5.5924231748, 1.9471531576, 5.121950067,  6.2450254884, 6.7891170771,
            5.8361287154, 4.466910339,  7.2270010717, 5.7819554231, 4.4842880019,
            4.9712070265, 4.2078178717, 5.2182885599, 6.4745863176, 6.2448866075,
            4.2368130867, 4.7255601286, 4.8353027654, 4.5554503511, 8.3323329604,
            6.330112535,  6.9377684856, 5.6944463914, 5.3284683313, 5.6433240012,
            7.5222873413, 4.6209437459, 7.0472052348, 4.6403279876, 3.9731712951,
            5.6891540515, 5.1642534408, 4.9885844576, 5.8907941987, 5.7607506851,
            6.1131651223, 6.3042685974, 5.1931013466, 5.7185965649, 6.5703764734,
            6.7914222864, 5.2841522054, 5.5986689953, 6.6953022314, 6.2897407745,
            4.9363841217, 7.5943387817, 6.8189806694, 5.4657919303, 6.9450740346,
            6.0491399493, 4.9243475129, 6.6992545642, 7.1758586159, 6.6877191304,
            3.9640910372, 7.4695499761, 6.5768077632, 6.032812643,  5.1552840433,
            6.6038154826, 4.1405241412, 5.2374343215, 3.5641174277, 4.4768903553,
            6.0207354333, 5.8792140655, 4.4384832094, 5.1214997635, 4.6529082909,
            4.5490979814, 4.6713020161, 5.1401441322, 2.3895462865, 3.4017504194,
            3.6624519239, 6.0086501672, 4.1750202995, 5.5292865073, 6.2325919632,
            3.6583022026, 2.837163718,  3.4894756579, 6.27397775,   5.5912329452,
            4.5282864973, 6.8199778645, 4.3409354054, 2.7864482081, 5.7174356009,
            6.5076539254, 7.7958758557, 3.4148993103, 3.6529577811, 7.1916707215,
            6.6208990271, 4.1684406482, 2.6727527146, 5.0352591462, 5.3202919831,
            6.1933685196, 5.4374097727, 6.8799049232, 3.2111172499, 5.015635114,
            6.2858081588, 7.4332242309, 2.9067650612, 5.0567579581, 4.5612803075,
            5.9004014148, 2.3553476604, 6.7207737927, 4.2136423221, 5.5140111884,
            5.2607428936, 4.9697252338, 6.4195910357, 5.8769627405, 6.868270238,
            4.7459574498, 5.5566778145, 5.3821806517, 6.5897461951, 2.4558339129,
            5.2772099437, 3.6019882524, 6.7689561874, 7.1613320194, 5.9590315401,
            6.0072222032, 5.4861605719, 6.8843247158, 3.9299177516, 5.1945323091,
            6.5456719657, 6.666091464,  3.4177814968, 4.9259024299, 6.1980579896,
            5.7291950965, 5.9328482864, 5.9415819085, 5.75762391,   4.1734856491,
            5.2648806577, 6.6803696075, 7.3206452853, 5.7202053252, 2.2671039065,
            7.5349614014, 6.1388092891, 3.9194825779, 5.8034773533, 3.783159929,
            5.4279173357, 6.5228418742, 7.4724156687, 6.3511406938, 5.8362264363,
            5.7759864005, 5.8990114713, 6.5703900719, 4.3241696355, 5.5700645344,
            3.7320974304, 6.7800744324, 5.8740895089, 4.4661086204, 5.3337481245,
            5.8228932408, 4.4752409907, 4.9203870462, 6.5762105913, 5.7141311485,
            4.5056454178, 3.8516001514, 2.9504899432, 7.0119551562, 6.0701001773,
            5.2597732347, 4.8235183904, 4.7451661662, 7.0325217161, 6.0514950234,
            4.410835359,  5.337455284,  5.3786547435, 5.331288659,  4.1474151259,
            3.9606982806, 5.162856041,  4.8736233244, 5.2639571371, 6.6539938598,
            3.4592121762, 6.5440582015, 3.4118390619, 7.1771460508, 5.7201259494,
            7.3295695149, 5.9984761562, 4.909562639,  4.3815911049, 5.9398855099,
            5.3549266258, 5.3440581434, 4.1766405962, 6.0309899857, 6.1927856032,
            4.047590773,  5.3223984519, 4.5805516361, 7.4186047598, 6.327027882,
            4.5473025454, 6.2767350501, 5.9903221295, 5.3187186238, 6.4483918712,
            6.7928858412, 3.2094741345, 5.9365806456, 4.0572699822, 5.5490746121,
            4.4079576445, 4.8734926739, 5.745266077,  5.7287633209, 4.5914538949,
            5.4363606267, 7.0635374855, 4.565028386,  6.1236827449, 5.732562892,
            2.6090205335, 5.3957252221, 6.6182566637, 7.1726974872, 5.5780070597,
            4.046378259,  5.8957366855, 2.7956591028, 5.1417029062, 6.2057491342,
            4.3613048657, 7.6874193749, 5.4331378523, 4.7607402139, 5.6010207489,
            5.6343303903, 5.6343303903
        };

        /// <summary>
        /// Normaly Distributed Data
        /// 
        /// Q1:    4.5631543468
        /// Q2:    5.5292865073
        /// Q3:    6.2753564001
        /// </summary>
        public static IEnumerable<Double> Normal
        {
            get
            {
                foreach (double x in normal)
                    yield return x;
            }
        }

        private static readonly double[] uniform =
        {
            0.8276315135, 0.8012208439, 0.0540922539, 0.2116462283, 0.0747533816,
            0.9222986847, 0.3647232444, 0.6666057411, 0.8863215324, 0.357944714,
            0.7588656212, 0.2804714805, 0.3321236537, 0.1369996787, 0.145872624,
            0.6079509426, 0.3181539095, 0.8067697331, 0.3056639563, 0.5398121404,
            0.0119069125, 0.9463549461, 0.2326314359, 0.9263905242, 0.6001554483,
            0.8354765143, 0.7037293356, 0.0872199461, 0.7622437789, 0.7636374726,
            0.9155552699, 0.0355004964, 0.9825231493, 0.2589903791, 0.3127598812,
            0.3615185705, 0.2366765614, 0.9305703835, 0.1036690709, 0.6017292506,
            0.0602102703, 0.9682403165, 0.6818499364, 0.3216841794, 0.0272597211,
            0.4182671331, 0.7963715073, 0.6700399494, 0.4316282528, 0.4434116321,
            0.7820955782, 0.8807580073, 0.699258163,  0.3874183885, 0.1206233931,
            0.6490928518, 0.2312312275, 0.3873523972, 0.716061465,  0.2791599354,
            0.0767547902, 0.6880683957, 0.8905887196, 0.2637409088, 0.9528390091,
            0.4864174233, 0.5126585731, 0.750859099,  0.3904588966, 0.9245143344,
            0.6321242011, 0.2340622779, 0.9795906688, 0.2254153693, 0.9894884861,
            0.4052055784, 0.6064518941, 0.9755344194, 0.5451436942, 0.4220026939,
            0.9599222917, 0.556066185,  0.0667044082, 0.5675989483, 0.0887358238,
            0.5713591021, 0.7112379301, 0.7293513155, 0.7191423734, 0.8000127387,
            0.5619372547, 0.8207015848, 0.9747222229, 0.0571163629, 0.4787281233,
            0.7426564933, 0.6520468415, 0.3224444633, 0.9973090683, 0.1284630016,
            0.8313582337, 0.6605175857, 0.178617397,  0.3094816564, 0.3552296986,
            0.8126480689, 0.9943772405, 0.803012683,  0.2865418996, 0.4528598107,
            0.5268607033, 0.0394084019, 0.3728424571, 0.0597094031, 0.2878881433,
            0.4769909998, 0.2600976887, 0.0896183538, 0.6920787953, 0.7110410511,
            0.5551036403, 0.5598488418, 0.0474907683, 0.3245676427, 0.6273566933,
            0.9889143789, 0.1868881039, 0.0592133149, 0.0047635485, 0.3442747722,
            0.9502404854, 0.6215298061, 0.9903990328, 0.9494515026, 0.9946884794,
            0.0936277625, 0.0197042511, 0.0991332506, 0.8380199635, 0.7662059367,
            0.9381830173, 0.14722348,   0.7361002694, 0.5637529871, 0.2823808223,
            0.0138185989, 0.0565533176, 0.5682085325, 0.1950053167, 0.6221944315,
            0.3931369082, 0.4989516879, 0.7313562409, 0.5680553902, 0.6313539703,
            0.8371642564, 0.0170041359, 0.318777132,  0.3666981241, 0.9512252724,
            0.0075432825, 0.9872220969, 0.5180777932, 0.8270272859, 0.3227598879,
            0.8805511037, 0.5872090639, 0.4103806793, 0.9724469738, 0.3571629158,
            0.0878977721, 0.365040669,  0.5530218647, 0.3048204758, 0.7533129876,
            0.5555391864, 0.5565118119, 0.2327091704, 0.3407982592, 0.146041721,
            0.2911541718, 0.6614068968, 0.665032765,  0.2093258136, 0.2839385722,
            0.2383142992, 0.5238666957, 0.9291389674, 0.6199919356, 0.4796810283,
            0.8102971725, 0.3484595945, 0.6276851903, 0.8876235217, 0.4401536316,
            0.04057437,   0.6806149885, 0.600157983,  0.4145032179, 0.6343817122,
            0.6941699112, 0.9990476144, 0.4896233523, 0.3054786016, 0.4378489949,
            0.9857240049, 0.7525181189, 0.5032450883, 0.9323429836, 0.0703770769,
            0.8479447796, 0.3876014444, 0.6457091922, 0.6362025684, 0.6377943523,
            0.9391401111, 0.0041451644, 0.3345969892, 0.9686548094, 0.0442842448,
            0.3286730329, 0.9750205923, 0.4036361995, 0.6718008392, 0.7919196323,
            0.6908814653, 0.7197313652, 0.0742012855, 0.5843267418, 0.7553524135,
            0.2948318631, 0.3092848958, 0.0610596543, 0.6558447376, 0.7948398191,
            0.6573887019, 0.4827909614, 0.509100733,  0.2485297257, 0.0992079916,
            0.5983993733, 0.7563390634, 0.4227517976, 0.4892827088, 0.9836697884,
            0.7454244853, 0.6778915367, 0.7931969154, 0.5522509959, 0.6317321153,
            0.9412268776, 0.9412268776

        };

        /// <summary>
        /// Uniform Distriubtion
        /// 
        /// Q1:    0.2998261695
        /// Q2:    0.5555391864
        /// Q3:    0.7558457384
        /// </summary>
        public static IEnumerable<Double> Uniform
        {
            get
            {
                foreach (double x in uniform)
                    yield return x;
            }
        }

        private static readonly double[] log_normal =
        {
            0.9393472721, 1.2947054031, 1.7866013012, 0.9045287483, 0.7389711838,
            1.1659020322, 1.9673777789, 0.7436377951, 0.8061691241, 0.9670981428,
            0.7046126924, 0.9878390344, 2.4569570136, 0.9240840101, 0.6290002962,
            0.6979145146, 1.4595230007, 1.0170413957, 1.4137333987, 0.7053829725,
            1.1992297347, 0.9275999476, 0.5467706702, 0.6015865449, 0.9652734827,
            0.3257033841, 0.9354215828, 0.5267066314, 1.3743789584, 0.3286328055,
            0.7644655557, 0.5361370636, 1.6532774573, 1.3394925723, 0.5005069205,
            0.7133150164, 1.6032561235, 2.4463542248, 1.5760848465, 1.1905336149,
            0.9849531554, 2.7326350061, 0.9469228825, 0.5928208237, 0.6829439907,
            0.7460911659, 0.6598377466, 0.7780298636, 1.2486010644, 0.8484244614,
            2.9297663512, 1.0849831186, 0.76580737,   1.2582405874, 0.9421561599,
            1.7957583437, 1.238132664,  1.1544555315, 1.6684935064, 1.603135936,
            0.8528720741, 1.3488640177, 1.1128541477, 0.4119453801, 1.2052141012,
            1.1283437989, 1.4981992299, 1.1324192484, 0.7465908103, 0.8132425542,
            1.2970312528, 0.8250219423, 2.9245092089, 0.5250016901, 1.37585811,
            1.1712342451, 1.3589473546, 0.5496189368, 0.8418524633, 1.3263813414,
            0.8289398992, 0.9364356927, 0.8001005624, 3.4756441149, 0.7876053662,
            1.4984688386, 1.3960567214, 1.1062669584, 0.844225702,  0.7237661757,
            1.640117853,  1.074038016,  1.2155465391, 1.5501709998, 0.6968143227,
            0.4871747385, 0.742124936,  0.5561701449, 0.4622639847, 1.0510259038,
            1.0694959856, 1.0750478327, 1.2484014525, 0.9484175418, 1.6103222269,
            0.9024258592, 0.7867329497, 0.3313490036, 0.7782895469, 1.0697180434,
            1.0987986101, 1.4290296862, 0.5991170928, 1.2396656358, 1.0916327268,
            2.3777856536, 0.7567493834, 1.7457684316, 1.066719924,  1.3288726359,
            0.7259503327, 0.7398900945, 1.293459576,  1.1771355691, 1.0918941089,
            1.3709999793, 0.8527613844, 1.9958839806, 0.7537913885, 1.9363866915,
            1.9941746013, 1.2462067149, 0.9774618178, 0.8906110561, 0.6202122669,
            0.6645585799, 1.8907658971, 0.4868059063, 0.6337418376, 0.8949107385,
            0.9707515522, 0.6275161845, 1.2194882961, 0.5020506527, 0.7335983831,
            1.1664573472, 1.2037843691, 1.406024572,  1.0977919443, 0.4550918974,
            0.7506075483, 0.3967259038, 1.7422489964, 0.8376283547, 1.9976821275,
            1.2389074099, 0.8049821564, 0.4200820897, 0.8065415698, 1.4624185041,
            0.9128616495, 1.7419472357, 1.6412614909, 1.5846905034, 0.9834221175,
            1.0958052726, 1.8743516779, 0.71994883,   0.8069773959, 1.3509631785,
            0.7135643234, 1.2580563174, 1.51246781,   1.34138409,   1.535512099,
            1.2371201743, 1.9321459303, 0.809194197,  1.0614839659, 1.4770865643,
            0.9058598119, 1.2438876623, 0.9166274033, 0.9794826495, 2.931789772,
            1.1145502855, 0.8150926979, 1.118362082,  0.5396743565, 0.5547140796,
            1.1282424646, 2.1607012665, 0.6537330312, 3.5548327624, 0.3278031472,
            0.9775892891, 0.4453526152, 0.5048769364, 3.3141503307, 1.1935130746,
            0.5763659359, 0.6703031048, 0.7847274583, 1.4206957811, 2.3560990699,
            0.7414552496, 1.10310325,   0.8871697201, 0.9024931202, 0.8896696588,
            1.1471470718, 0.4735206964, 0.7549999422, 1.3203614519, 0.5663433849,
            0.3692463154, 0.7826182884, 2.6879973804, 2.4274205807, 0.9122581976,
            0.3993345558, 0.9221030779, 1.2982442275, 2.1924176851, 3.0312666449,
            1.1007380968, 1.769160665,  0.6750399106, 0.5023866488, 0.53006213,
            0.9766188197, 0.5199871167, 1.1857565797, 1.2541393387, 0.644107467,
            1.7049001934, 0.8223218603, 1.5116942175, 0.6855605422, 2.7658932347,
            0.8528173114, 1.6694310899, 0.9888358989, 0.8897280402, 1.1115173671,
            1.8027248354, 1.8940430399, 2.6621872648, 0.7890546164, 0.8960138277,
            0.7498765873, 0.7498765873

        };

        /// <summary>
        /// Log-Normal Distribution
        ///  
        /// Q1:    0.7482336988
        /// Q2:    0.9849531554
        /// Q3:    1.3549552666
        /// </summary>
        public static IEnumerable<Double> LogNorm
        {
            get
            {
                foreach (double x in log_normal)
                    yield return x;
            }
        }

        private static readonly double[] lapace =
        {
            3.2965951354, 2.5233173535, 3.4219706861, 2.4727344518, 3.3192676095,
            3.7540121779, 2.905481771,  4.0581039081, 1.9349020888, 3.2645866637,
            2.4246561077, 3.1005308637, 3.9068981497, 2.9288384601, 3.1328114294,
            2.769643178,  2.9714046599, 2.6942223069, 3.4508172699, 3.0184356449,
            3.3834485396, 2.8437566217, 2.9931706568, 2.5721706925, 2.4293137542,
            1.9586580934, 4.0327713382, 2.1448760609, 2.5895969886, 3.0159633109,
            2.9138748026, 3.0819337195, 2.5289725403, 0.8573332926, 2.9322625419,
            1.6329084669, 2.093674621,  2.1219420225, 1.7529177604, 3.0400861959,
            3.0331773179, 2.8165127604, 2.4655755617, 1.2017329144, 2.1649465022,
            3.6811537411, 2.9109894609, 2.2937961381, 3.0203884477, 3.3791925572,
            2.1350399589, 2.2781492659, 1.5483191341, 2.9431972355, 2.6836113395,
            3.4366461837, 2.9213310377, 3.0206794134, 3.0201758344, 3.11840807,
            3.2590855352, 2.4565392714, 2.971084392,  2.7108197944, 3.6371641251,
            2.5775554963, 3.8343530781, 2.8635346015, 2.5666953682, 2.5932315408,
            2.4852233546, 3.829127207,  3.2939534551, 3.2286568347, 2.6688238332,
            2.9348602933, 2.9641999457, 2.8499178967, 3.1300373572, 3.9955152023,
            2.8799854439, 3.568415577,  2.7127252355, 3.9672122791, 3.0649584746,
            2.8247289847, 2.1183873525, 2.967648486,  3.1689290511, 2.4459148347,
            3.4403778039, 3.1227837907, 3.008098491,  1.6106480921, 2.8110308568,
            2.4913067513, 3.7288850643, 3.2886259828, 2.8609187123, 2.9692683209,
            2.507562472,  1.1699827148, 2.2965393104, 2.9969790814, 2.0833086447,
            3.3354387265, 2.1693683678, 3.0330193096, 2.5614433785, 2.8808913078,
            3.3097646831, 3.1388659904, 2.7289016058, 2.896822382,  2.643084757,
            2.7892890677, 3.0711454432, 2.6128980785, 2.790916713,  2.9044246349,
            3.1663930638, 3.6316952117, 2.6457740696, 2.3435565549, 2.8435458444,
            2.7993598244, 2.8015216913, 3.222787992,  3.6387694866, 3.0256840625,
            3.2743770361, 4.7320618996, 3.2259965383, 2.8916627564, 4.0442831515,
            3.1842907709, 3.4498741582, 2.9656332498, 3.7263277971, 2.5164031315,
            2.8608887277, 2.9099183957, 2.7561181156, 2.4286266288, 3.3508647655, 
            3.002681962,  2.0277806283, 3.3562753126, 3.0770741209, 2.7071436856,
            3.3269870941, 3.4624336034, 2.7807128586, 3.1177851114, 3.419654454,
            2.2213021796, 3.0874344633, 2.6130277143, 3.1911887525, 3.920219449,
            2.9527812606, 2.8015478251, 2.8030464642, 3.1148099384, 3.4046013928,
            3.2627619403, 2.6026200341, 3.1016952849, 1.69796772, 3.5342953753,
            3.0016917747, 3.2119939562, 3.6263801369, 3.1886829234, 3.2278556496,
            2.4641378784, 2.6068253953, 2.9766968891, 2.8069453803, 2.5793696651,
            2.6716547987, 2.6501625989, 3.1013639923, 3.0183300596, 2.9526585082,
            3.036364315,  3.0220188103, 2.9761984138, 2.8526284123, 2.927772417,
            3.3961252545, 4.7966634307, 2.8278045651, 2.65131903,   2.0909369216,
            3.3090057177, 3.4737840001, 1.3939790725, 0.7547431959, 2.8468532368,
            3.1551574754, 2.6957957776, 2.9487359543, 3.6612349331, 3.5879092502,
            3.3735587057, 2.708436106,  3.1566768812, 3.2234707923, 3.0302184666,
            2.9390729736, 2.5410551645, 2.8119857333, 3.0913973422, 3.4045143819,
            5.2710437217, 2.8109531502, 2.7224959603, 2.6576422239, 3.4470674552,
            3.1785090623, 4.4037197668, 4.0941268965, 3.4452347063, 3.2181934225,
            3.3289503909, 3.2009503448, 3.1325912077, 3.6492608909, 2.8654168462,
            3.2558975056, 2.8014321447, 3.9939236438, 3.0432825637, 3.1186170294,
            3.1926478955, 3.084769692,  3.01958198,   3.099303786,  2.5691575555,
            2.6777618604, 1.8725310208, 3.1774040995, 4.3400715771, 2.8444135719,
            2.4377266547, 3.6552034893, 1.9281285232, 3.6321065494, 3.5196780389,
            4.1758174445, 4.1758174445


        };

        /// <summary>
        /// Laplace Distribution
        /// 
        /// Q1:    2.670239316
        /// Q2:    2.9714046599
        /// Q3:    3.2609237378
        /// </summary>
        public static IEnumerable<Double> Laplace
        {
            get
            {
                foreach (double x in lapace)
                    yield return x;
            }
        }

        private static readonly double[] exponential =
        {
            0.2538240292, 0.1641816126, 0.1385086921, 0.1346174128, 0.1848510664,
            0.0794164299, 0.2790488492, 0.2379578333, 0.1160153517, 0.4721511633,
            0.0483532442, 0.2602326841, 0.4815463375, 0.0437068654, 0.1181386041,
            0.1029370061, 0.2267885031, 0.0663219085, 0.5787085205, 0.039840717,
            0.0081076888, 0.1668344896, 0.0731363183, 0.400280577,  0.0033296302,
            0.1102630085, 0.4585680395, 0.1107012791, 0.0551745854, 0.3007147584,
            0.1114132494, 0.169936789,  0.2312964841, 0.8492227772, 0.2145595272,
            0.1331262358, 0.4844665014, 0.0109180788, 0.0303839167, 0.0519035785,
            0.0541420749, 0.051872087,  0.0609054042, 0.1593026559, 0.1924864808,
            0.0490003604, 0.1497004024, 0.0856323784, 0.2310940124, 0.7401750445,
            0.3770121959, 0.1108248083, 0.1477045972, 0.1370509793, 0.1739618672,
            0.1914599781, 0.4855300522, 0.1918711631, 0.0611771597, 0.5839769824,
            0.0640991928, 0.0248839291, 0.2859836071, 0.2925612818, 0.1658990885,
            0.0161261358, 0.350202941,  0.5180314517, 0.1766027479, 0.0329308957,
            0.0803952801, 0.0604052825, 0.0949054847, 0.242903603,  0.0336787615,
            0.2408257819, 0.3249541441, 0.0689907165, 0.2307478446, 0.0306498619,
            0.1394437581, 0.2093728532, 0.2011332097, 0.1942780156, 0.151405458,
            0.1972836213, 0.0596554235, 0.3559308763, 0.022804087,  0.1736357238,
            0.1984854787, 0.0881076373, 0.009426989,  0.0874790635, 0.3483591328,
            0.1274706293, 0.7889241564, 0.099108206,  0.0015594651, 0.2450483489,
            0.1454358206, 0.2538648497, 0.0342625562, 0.2288289936, 0.0133700153,
            0.1377076442, 0.3760923439, 0.0423624331, 0.154850553,  0.0398450805,
            0.142582291,  0.4252628788, 0.4520189856, 0.0205416455, 0.0615970403,
            0.1602894733, 0.0480293477, 0.4106049446, 0.3349438206, 0.0705824281,
            0.0010364558, 0.5306188413, 0.0744527563, 0.0429266531, 0.3264846012,
            0.0823373629, 0.4477914466, 0.208498023,  0.0584594435, 0.5669446597,
            0.0425889013, 0.0348887651, 0.1901393751, 0.0349598801, 0.4360744449,
            0.006019589,  0.0195062303, 0.1588250125, 0.4310839683, 0.0492345041,
            0.1932748597, 0.1127356307, 0.002387248,  0.2056303849, 0.2372450115,
            0.1452413255, 0.5653994513, 0.768273307,  0.0078395664, 0.0910648246,
            0.7589473827, 0.1097133065, 0.0574564948, 0.1131647664, 0.0138230308,
            0.1978828195, 0.1700115857, 0.1288542811, 0.096335849,  0.0102364387,
            0.081508951,  0.1173689547, 1.2888835818, 0.0736459006, 0.4462686236,
            0.0407198791, 0.1502391704, 0.4741372896, 0.2422597582, 0.7659559528,
            0.1447623383, 0.0469467104, 0.3220044007, 0.1026676891, 0.3145801623,
            0.0028914956, 0.081879366,  0.0770287821, 0.0188626998, 0.2800163345,
            0.1652159903, 0.2173179783, 0.2864634684, 0.3097018778, 0.194995483,
            0.2446336026, 0.5361098225, 0.203212149,  0.4582101613, 0.1701716599,
            0.0456085924, 0.2175473605, 0.1830188286, 0.3071736876, 0.2114838216,
            0.0008863178, 0.131553385,  0.035519251,  0.3365174787, 0.0910847259,
            0.1495608389, 0.0174401113, 0.158917233,  0.249692692,  0.0020225775,
            0.8060531091, 0.0697494789, 0.0149301047, 0.1152199789, 0.373106353,
            0.2177409208, 0.1694228658, 0.1132123272, 0.7555735465, 0.1572134473,
            0.3696280233, 0.5999040148, 0.0228030494, 0.330511946,  0.1832661744,
            0.0820940383, 0.2160971517, 0.2412156358, 0.423133591,  0.2229804602,
            0.1310468481, 0.0327024771, 0.0495778214, 0.2069183133, 0.3414443272,
            0.1386911626, 0.0016002915, 0.0488244866, 0.4008650034, 0.0763790088,
            0.1306464485, 0.0694425219, 0.0623933211, 0.131150626,  0.7815826829,
            0.2239466416, 0.106855512,  0.134072406,  0.4703848256, 0.32584563,
            0.1815496915, 0.0909654872, 0.2906561742, 0.31738321,   0.0173404871,
            0.1300233348, 0.1300233348

        };


        /// <summary>
        /// Exponential Distribution
        /// 
        /// Q1:    0.0652105507
        /// Q2:    0.1502391704
        /// Q3:    0.2570487669
        /// </summary>
        public static IEnumerable<Double> Exponential
        {
            get
            {
                foreach (double x in exponential)
                    yield return x;
            }
        }



        private static readonly int[] primes =
        {
              2,      3,      5,      7,     11,     13,     17,     19,     23,     29, 
             31,     37,     41,     43,     47,     53,     59,     61,     67,     71, 
             73,     79,     83,     89,     97,    101,    103,    107,    109,    113, 
            127,    131,    137,    139,    149,    151,    157,    163,    167,    173, 
            179,    181,    191,    193,    197,    199,    211,    223,    227,    229, 
            233,    239,    241,    251,    257,    263,    269,    271,    277,    281, 
            283,    293,    307,    311,    313,    317,    331,    337,    347,    349, 
            353,    359,    367,    373,    379,    383,    389,    397,    401,    409, 
            419,    421,    431,    433,    439,    443,    449,    457,    461,    463, 
            467,    479,    487,    491,    499,    503,    509,    521,    523,    541, 
            547,    557,    563,    569,    571,    577,    587,    593,    599,    601, 
            607,    613,    617,    619,    631,    641,    643,    647,    653,    659, 
            661,    673,    677,    683,    691,    701,    709,    719,    727,    733, 
            739,    743,    751,    757,    761,    769,    773,    787,    797,    809, 
            811,    821,    823,    827,    829,    839,    853,    857,    859,    863, 
            877,    881,    883,    887,    907,    911,    919,    929,    937,    941, 
            947,    953,    967,    971,    977,    983,    991,    997,   1009,   1013, 
           1019,   1021,   1031,   1033,   1039,   1049,   1051,   1061,   1063,   1069, 
           1087,   1091,   1093,   1097,   1103,   1109,   1117,   1123,   1129,   1151, 
           1153,   1163,   1171,   1181,   1187,   1193,   1201,   1213,   1217,   1223, 
           1229,   1231,   1237,   1249,   1259,   1277,   1279,   1283,   1289,   1291, 
           1297,   1301,   1303,   1307,   1319,   1321,   1327,   1361,   1367,   1373, 
           1381,   1399,   1409,   1423,   1427,   1429,   1433,   1439,   1447,   1451, 
           1453,   1459,   1471,   1481,   1483,   1487,   1489,   1493,   1499,   1511, 
           1523,   1531,   1543,   1549,   1553,   1559,   1567,   1571,   1579,   1583, 
           1597,   1601,   1607,   1609,   1613,   1619,   1621,   1627,   1637,   1657, 
           1663,   1667,   1669,   1693,   1697,   1699,   1709,   1721,   1723,   1733, 
           1741,   1747,   1753,   1759,   1777,   1783,   1787,   1789,   1801,   1811, 
           1823,   1831,   1847,   1861,   1867,   1871,   1873,   1877,   1879,   1889, 
           1901,   1907,   1913,   1931,   1933,   1949,   1951,   1973,   1979,   1987, 
           1993,   1997,   1999,   2003,   2011,   2017,   2027,   2029,   2039,   2053, 
           2063,   2069,   2081,   2083,   2087,   2089,   2099,   2111,   2113,   2129, 
           2131,   2137,   2141,   2143,   2153,   2161,   2179,   2203,   2207,   2213, 
           2221,   2237,   2239,   2243,   2251,   2267,   2269,   2273,   2281,   2287, 
           2293,   2297,   2309,   2311,   2333,   2339,   2341,   2347,   2351,   2357, 
           2371,   2377,   2381,   2383,   2389,   2393,   2399,   2411,   2417,   2423, 
           2437,   2441,   2447,   2459,   2467,   2473,   2477,   2503,   2521,   2531, 
           2539,   2543,   2549,   2551,   2557,   2579,   2591,   2593,   2609,   2617, 
           2621,   2633,   2647,   2657,   2659,   2663,   2671,   2677,   2683,   2687, 
           2689,   2693,   2699,   2707,   2711,   2713,   2719,   2729,   2731,   2741, 
           2749,   2753,   2767,   2777,   2789,   2791,   2797,   2801,   2803,   2819, 
           2833,   2837,   2843,   2851,   2857,   2861,   2879,   2887,   2897,   2903, 
           2909,   2917,   2927,   2939,   2953,   2957,   2963,   2969,   2971,   2999, 
           3001,   3011,   3019,   3023,   3037,   3041,   3049,   3061,   3067,   3079, 
           3083,   3089,   3109,   3119,   3121,   3137,   3163,   3167,   3169,   3181, 
           3187,   3191,   3203,   3209,   3217,   3221,   3229,   3251,   3253,   3257, 
           3259,   3271,   3299,   3301,   3307,   3313,   3319,   3323,   3329,   3331, 
           3343,   3347,   3359,   3361,   3371,   3373,   3389,   3391,   3407,   3413, 
           3433,   3449,   3457,   3461,   3463,   3467,   3469,   3491,   3499,   3511, 
           3517,   3527,   3529,   3533,   3539,   3541,   3547,   3557,   3559,   3571, 
           3581,   3583,   3593,   3607,   3613,   3617,   3623,   3631,   3637,   3643, 
           3659,   3671,   3673,   3677,   3691,   3697,   3701,   3709,   3719,   3727, 
           3733,   3739,   3761,   3767,   3769,   3779,   3793,   3797,   3803,   3821, 
           3823,   3833,   3847,   3851,   3853,   3863,   3877,   3881,   3889,   3907, 
           3911,   3917,   3919,   3923,   3929,   3931,   3943,   3947,   3967,   3989, 
           4001,   4003,   4007,   4013,   4019,   4021,   4027,   4049,   4051,   4057, 
           4073,   4079,   4091,   4093,   4099,   4111,   4127,   4129,   4133,   4139, 
           4153,   4157,   4159,   4177,   4201,   4211,   4217,   4219,   4229,   4231, 
           4241,   4243,   4253,   4259,   4261,   4271,   4273,   4283,   4289,   4297, 
           4327,   4337,   4339,   4349,   4357,   4363,   4373,   4391,   4397,   4409, 
           4421,   4423,   4441,   4447,   4451,   4457,   4463,   4481,   4483,   4493, 
           4507,   4513,   4517,   4519,   4523,   4547,   4549,   4561,   4567,   4583, 
           4591,   4597,   4603,   4621,   4637,   4639,   4643,   4649,   4651,   4657, 
           4663,   4673,   4679,   4691,   4703,   4721,   4723,   4729,   4733,   4751, 
           4759,   4783,   4787,   4789,   4793,   4799,   4801,   4813,   4817,   4831, 
           4861,   4871,   4877,   4889,   4903,   4909,   4919,   4931,   4933,   4937, 
           4943,   4951,   4957,   4967,   4969,   4973,   4987,   4993,   4999,   5003, 
           5009,   5011,   5021,   5023,   5039,   5051,   5059,   5077,   5081,   5087, 
           5099,   5101,   5107,   5113,   5119,   5147,   5153,   5167,   5171,   5179, 
           5189,   5197,   5209,   5227,   5231,   5233,   5237,   5261,   5273,   5279, 
           5281,   5297,   5303,   5309,   5323,   5333,   5347,   5351,   5381,   5387, 
           5393,   5399,   5407,   5413,   5417,   5419,   5431,   5437,   5441,   5443, 
           5449,   5471,   5477,   5479,   5483,   5501,   5503,   5507,   5519,   5521, 
           5527,   5531,   5557,   5563,   5569,   5573,   5581,   5591,   5623,   5639, 
           5641,   5647,   5651,   5653,   5657,   5659,   5669,   5683,   5689,   5693, 
           5701,   5711,   5717,   5737,   5741,   5743,   5749,   5779,   5783,   5791, 
           5801,   5807,   5813,   5821,   5827,   5839,   5843,   5849,   5851,   5857, 
           5861,   5867,   5869,   5879,   5881,   5897,   5903,   5923,   5927,   5939, 
           5953,   5981,   5987,   6007,   6011,   6029,   6037,   6043,   6047,   6053, 
           6067,   6073,   6079,   6089,   6091,   6101,   6113,   6121,   6131,   6133, 
           6143,   6151,   6163,   6173,   6197,   6199,   6203,   6211,   6217,   6221, 
           6229,   6247,   6257,   6263,   6269,   6271,   6277,   6287,   6299,   6301, 
           6311,   6317,   6323,   6329,   6337,   6343,   6353,   6359,   6361,   6367, 
           6373,   6379,   6389,   6397,   6421,   6427,   6449,   6451,   6469,   6473, 
           6481,   6491,   6521,   6529,   6547,   6551,   6553,   6563,   6569,   6571, 
           6577,   6581,   6599,   6607,   6619,   6637,   6653,   6659,   6661,   6673, 
           6679,   6689,   6691,   6701,   6703,   6709,   6719,   6733,   6737,   6761, 
           6763,   6779,   6781,   6791,   6793,   6803,   6823,   6827,   6829,   6833, 
           6841,   6857,   6863,   6869,   6871,   6883,   6899,   6907,   6911,   6917, 
           6947,   6949,   6959,   6961,   6967,   6971,   6977,   6983,   6991,   6997, 
           7001,   7013,   7019,   7027,   7039,   7043,   7057,   7069,   7079,   7103, 
           7109,   7121,   7127,   7129,   7151,   7159,   7177,   7187,   7193,   7207,
           7211,   7213,   7219,   7229,   7237,   7243,   7247,   7253,   7283,   7297, 
           7307,   7309,   7321,   7331,   7333,   7349,   7351,   7369,   7393,   7411, 
           7417,   7433,   7451,   7457,   7459,   7477,   7481,   7487,   7489,   7499, 
           7507,   7517,   7523,   7529,   7537,   7541,   7547,   7549,   7559,   7561, 
           7573,   7577,   7583,   7589,   7591,   7603,   7607,   7621,   7639,   7643, 
           7649,   7669,   7673,   7681,   7687,   7691,   7699,   7703,   7717,   7723, 
           7727,   7741,   7753,   7757,   7759,   7789,   7793,   7817,   7823,   7829, 
           7841,   7853,   7867,   7873,   7877,   7879,   7883,   7901,   7907,   7919, 
        };


        public static IEnumerable<Int32> Primes
        {
            get
            {
                foreach (int p in primes)
                    yield return p;
            }
        }


    }
}
