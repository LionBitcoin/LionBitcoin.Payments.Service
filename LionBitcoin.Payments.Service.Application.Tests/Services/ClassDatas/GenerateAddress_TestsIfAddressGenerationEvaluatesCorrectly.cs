using System.Collections;
using LionBitcoin.Payments.Service.Application.Services.Enums;
using LionBitcoin.Payments.Service.Application.Shared;

namespace LionBitcoin.Payments.Service.Application.Tests.Services.ClassDatas;

public class GenerateAddress_TestsIfAddressGenerationEvaluatesCorrectly : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return
        [
            Network.Main, 
            0, 
            AddressType.Receiving, 
            "bc1qx84c6md658u8seq7gc595t37exh56mxtthu4ju",
            "xpub6CYBRo48EXzvTSzrztRHVMRH7i1ujfcRmqe5BEyBq31nW8T1DH34tnrfZsw8eXzosCuq1xnfUE8zgM9pishDnCHb2CgfT1sLwQQyyiBKU8e"
        ];
        yield return
        [
            Network.Main, 
            0, 
            AddressType.Change, 
            "bc1qqnzqd86r9rd6afrfyyyzht4t0gedcqtk3z0mtw", 
            "xpub6CYBRo48EXzvTSzrztRHVMRH7i1ujfcRmqe5BEyBq31nW8T1DH34tnrfZsw8eXzosCuq1xnfUE8zgM9pishDnCHb2CgfT1sLwQQyyiBKU8e"
        ];
        yield return
        [
            Network.Main, 
            8, 
            AddressType.Receiving, 
            "bc1qnmxeevtndk0w808yrhah9hhafqquj0j6yzq4kp", 
            "xpub6CYBRo48EXzvTSzrztRHVMRH7i1ujfcRmqe5BEyBq31nW8T1DH34tnrfZsw8eXzosCuq1xnfUE8zgM9pishDnCHb2CgfT1sLwQQyyiBKU8e"
        ];
        yield return
        [
            Network.Main, 
            9, 
            AddressType.Change, 
            "bc1qvcwzfa826cre8zn8zvwtc4x4yuctda0n0l40mq", 
            "xpub6CYBRo48EXzvYKuSJd5ecRvf6LM92KhCurzew94yhNtNTq4bDyDvaFc6UdtJUrWQ3THJWrnbTpc9Fxigr2ojHE8FNuaMUnuG7QpHT6Z91HX"
        ];
        yield return
        [
            Network.Test, 
            9, 
            AddressType.Change, 
            "tb1qljztxl29kp5a465sfwgdm2c8ca8mfu5u2zaspk", 
            "tpubDCMhyiTBF5afY68yPVyeM993n43fxedemQJgPuJWBjs7b5ASonDjQkUgkEwXNrifjUndjaJByWpygjoNKy1ZsUUgvyKMYBjnTya7MFba5rk"
        ];
        yield return
        [
            Network.Test, 
            1567, 
            AddressType.Receiving, 
            "tb1q6nhq0kqfqu0e9zzyphsv2686ncng2hlwz7agrg", 
            "tpubDCMhyiTBF5afY68yPVyeM993n43fxedemQJgPuJWBjs7b5ASonDjQkUgkEwXNrifjUndjaJByWpygjoNKy1ZsUUgvyKMYBjnTya7MFba5rk"
        ];
        
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}