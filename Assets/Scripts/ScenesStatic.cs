using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public static class ScenesStatic
{
    public static string[] AllScenes { get; set; } = { "Level 1", "Level 2", "Level 3", "Level 4"};
    public static string CurrentLevel { get; set; } = "Default Name";
    private static string[] publicLeaderboardKeys { get; set; } = {
        "d9d5fd747b78cb916bef0936ed4b114eaa0a2531dafd4bd5b65961e92560266a",
        "f3bf6d41effa40b952ecbb352b569d667df048e97a6defe8d08313bbd5a1f297",
        "e0c7a430ad8d0681e25ee42d050d33a5eb2d17231432da1493ff8fb71125fadc",
        "057cbe085219b869238a4fef43663fa1c73181f20ce199cc016703cc974aac6f" };

    private static string[] privateLeaderboardKeys { get; set; } = {
        "7a26154e2b72cbd050de9d7b930ebecc39ea4575c065d777eaea524679f413a3960fc884e8a26ae3c383894e66f9dbf38b949ea9a1b463af40d24d639a18cf1379593d3dad0ba6a6eb64f67231b13b4299918d517f3a0874cbeefafe734b81ec07af133547f2adc2ff146d579e2bd4adc5801640346a58deab0d5de6085c5580",
        "49d7110d4550ec0d906e2f6b58a42271cf8ebe869436a2d2afe4241e1eef1fdba2026e2444e366209874fcc1c7d767f7096376edd2524231802cf92c0ca73d53214407c30ddf836398bfb9361e4e993943f49cda97f0eaad6594895be41eaa8177b48781d9d4f9cddf99612c760d6c6ff88c59adc703e6bb37bfd4627b9cb37a",
        "76a3d2148fc7f1d137a1e7eebb261f9021492560f417dae8f50fa2a1d7a67bab1a42d652f37c567239cac11136bbf9b726153cfadeea9ae5e9b164a6bbadcb8558e40cd0420562a35f46f00ce317a8afd55844437c7dcc694e372dae1330cdcb2219c1542346385bce39aeb3339d3809d303119acc8c27299bc492691a52adcd",
        "25bde5482344f51cb2f5f014c1ddf83d95d01e38a123ff944acf9a4b0dd271b687635a2f961e6f2b86b57a1494c27d29d92edf4a89cfda5d26e5495a4f2b7aba9516818fbda9d46ba312b7fbb517acdf93f2de7f4919f5a314a808cdd311d1342289fd53cd84721cbe33f68d5f16baea867647c3dbcc775e9138e8af1164453a"
    };
    public static string GetNextLevel(string currentLevel)
    {
        int index = ( Array.IndexOf(AllScenes, currentLevel) +1 ) % AllScenes.Length;
        if (index != -1)
        {
            return AllScenes[index];
        }

        return AllScenes[0];
    }
    public static string GetPublicKey(string currentLevel)
    {
        int index = (Array.IndexOf(AllScenes, currentLevel));
        if (index != -1)
        {
            return publicLeaderboardKeys[index];
        }

        return publicLeaderboardKeys[0];
    }
}
