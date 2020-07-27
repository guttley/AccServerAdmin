using System.ComponentModel;

namespace AccServerAdmin.Domain.AccConfig
{
    public enum CarModel
    {
        [Description("Not Forced")]
        NotForced = -1,

        [Description("Porsche 991 GT3")]
        Porsche991Gt3 = 0,

        [Description("Mercedes AMG GT3")]
        MercedesAmgGt3 = 1,

        [Description("Ferrari 488 GT3")]
        Ferrari488Gt3 = 2,

        [Description("Audi R8 LMS")]
        AudiR8Lms = 3,

        [Description("Lamborghini Huracan GT3")]
        LamborghiniHuracanGt3 = 4,

        [Description("Mclaren 650s GT3")]
        Mclaren650SGt3 = 5,

        [Description("Nissan GT R Nismo GT3 2018")]
        NissanGtRNismoGt32018 = 6,

        [Description("BMW M6 GT3")]
        BmwM6Gt3 = 7,

        [Description("Bentley Continental GT3 2018")]
        BentleyContinentalGt32018 = 8,

        [Description("Porsche 991.2 GT3 Cup")]
        Porsche9912Gt3Cup = 9,

        [Description("Nissan GT-R Nismo GT3 2017")]
        NissanGtRNismoGt32017 = 10,

        [Description("Bentley Continental GT3 2016")]
        BentleyContinentalGt32016 = 11,

        [Description("Aston Martin Vantage V12 GT3")]
        AstonMartinVantageV12Gt3 = 12,

        [Description("Lamborghini Gallardo R-EX")]
        LamborghiniGallardoREx = 13,

        [Description("Jaguar G3")]
        JaguarG3 = 14,

        [Description("Lexus RC F GT3")]
        LexusRcFGt3 = 15,

        [Description("Lamborghini Huracan Evo 2019")]
        LamborghiniHuracanEvo2019 = 16,

        [Description("Honda NSX GT3")]
        HondaNsxGt3 = 17,

        [Description(" Lamborghini Huracan SuperTrofeo")]
        LamborghiniHuracanSuperTrofeo = 18,

        [Description(" Audi R8 LMS Evo 2019")]
        AudiR8LmsEvo2019 = 19,

        [Description(" AMR V8 Vantage 2019")]
        AmrV8Vantage2019 = 20,

        [Description(" Honda NSX Evo 2019")]
        HondaNsxEvo2019 = 21,

        [Description("McLaren 720S GT3 (Special)")]
        McLaren720SGt3Special = 22,

        [Description("Porsche 911 II GT3 R 2019")]
        Porsche911IiGt3R2019 = 23,
        
            
        [Description("Alpine A1110 GT4")]
        AlpineA1110Gt4 = 50,

        [Description("Aston Martin Vantage GT4")]
        AstonMartinVantageGt4 = 51,

        [Description("Audi R8 LMS GT4")]
        AudiR8LmsGt4 = 52,

        [Description("BMW M4 GT4")]
        BmwM4Gt4 = 53,

        [Description("Chevrolet Camaro GT4")]
        ChevroletCamaroGt4 = 55,

        [Description("Ginetta G55 GT4")]
        GinettaG55Gt4 = 56,

        [Description("KTM X-Bow GT4")]
        KtmXBowGt4 = 57,

        [Description("Maserati MC GT4")]
        MaseratiMcGt4 = 58,

        [Description("McLaren 570S GT4")]
        McLaren570SGt4 = 59,

        [Description("Mercedes AMG GT4")]
        MercedesAmgGt4 = 60,

        [Description("Porsche 718 Cayman GT4")]
        Porsche718CaymanGt4 = 61,
    }
}
